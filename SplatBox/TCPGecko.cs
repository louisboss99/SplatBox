using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace SplatBox
{
  public class TCPGecko
  {
    private static readonly byte[] GCAllowedVersions = new byte[1]{ (byte) 130 };
    private tcpconn PTCP;
    private const uint packetsize = 1024;
    private const uint uplpacketsize = 1024;
    private const byte cmd_poke08 = 1;
    private const byte cmd_poke16 = 2;
    private const byte cmd_pokemem = 3;
    private const byte cmd_readmem = 4;
    private const byte cmd_pause = 6;
    private const byte cmd_unfreeze = 7;
    private const byte cmd_breakpoint = 9;
    private const byte cmd_writekern = 11;
    private const byte cmd_readkern = 12;
    private const byte cmd_breakpointx = 16;
    private const byte cmd_sendregs = 47;
    private const byte cmd_getregs = 48;
    private const byte cmd_cancelbp = 56;
    private const byte cmd_sendcheats = 64;
    private const byte cmd_upload = 65;
    private const byte cmd_hook = 66;
    private const byte cmd_hookpause = 67;
    private const byte cmd_step = 68;
    private const byte cmd_status = 80;
    private const byte cmd_cheatexec = 96;
    private const byte cmd_rpc = 112;
    private const byte cmd_nbreakpoint = 137;
    private const byte cmd_version = 153;
    private const byte cmd_os_version = 154;
    private const byte GCBPHit = 17;
    private const byte GCACK = 170;
    private const byte GCRETRY = 187;
    private const byte GCFAIL = 204;
    private const byte GCDONE = 255;
    private const byte BlockZero = 176;
    private const byte BlockNonZero = 189;
    private const byte GCWiiVer = 128;
    private const byte GCNgcVer = 129;
    private const byte GCWiiUVer = 130;
    private const byte BPExecute = 3;
    private const byte BPRead = 5;
    private const byte BPWrite = 6;
    private const byte BPReadWrite = 7;
    private bool PConnected;
    private bool PCancelDump;

    public bool connected
    {
      get
      {
        return this.PConnected;
      }
    }

    public bool CancelDump
    {
      get
      {
        return this.PCancelDump;
      }
      set
      {
        this.PCancelDump = value;
      }
    }

    public string Host
    {
      get
      {
        return this.PTCP.Host;
      }
      set
      {
        if (this.PConnected)
          return;
        this.PTCP = new tcpconn(value, this.PTCP.Port);
      }
    }

    private event GeckoProgress PChunkUpdate;

    public event GeckoProgress chunkUpdate
    {
      add
      {
        this.PChunkUpdate += value;
      }
      remove
      {
        this.PChunkUpdate -= value;
      }
    }

    public TCPGecko(string host, int port)
    {
      this.PTCP = new tcpconn(host, port);
      this.PConnected = false;
      // ISSUE: reference to a compiler-generated field
      this.PChunkUpdate = (GeckoProgress) null;
    }

    ~TCPGecko()
    {
      if (!this.PConnected)
        return;
      this.Disconnect();
    }

    protected bool InitGecko()
    {
      return true;
    }

    public bool Connect()
    {
      if (this.PConnected)
        this.Disconnect();
      this.PConnected = false;
      try
      {
        this.PTCP.Connect();
      }
      catch (IOException ex)
      {
        this.Disconnect();
        throw new ETCPGeckoException(ETCPErrorCode.noTCPGeckoFound);
      }
      if (!this.InitGecko())
        return false;
      Thread.Sleep(150);
      this.PConnected = true;
      return true;
    }

    public void Disconnect()
    {
      this.PConnected = false;
      this.PTCP.Close();
    }

    protected FTDICommand GeckoRead(byte[] recbyte, uint nobytes)
    {
      uint bytes_read = 0;
      try
      {
        this.PTCP.Read(recbyte, nobytes, ref bytes_read);
      }
      catch (IOException ex)
      {
        this.Disconnect();
        return FTDICommand.CMD_FatalError;
      }
      return (int) bytes_read != (int) nobytes ? FTDICommand.CMD_ResultError : FTDICommand.CMD_OK;
    }

    protected FTDICommand GeckoWrite(byte[] sendbyte, int nobytes)
    {
      uint bytes_written = 0;
      try
      {
        this.PTCP.Write(sendbyte, nobytes, ref bytes_written);
      }
      catch (IOException ex)
      {
        this.Disconnect();
        return FTDICommand.CMD_FatalError;
      }
      return (long) bytes_written != (long) nobytes ? FTDICommand.CMD_ResultError : FTDICommand.CMD_OK;
    }

    protected void SendUpdate(uint address, uint currentchunk, uint allchunks, uint transferred, uint length, bool okay, bool dump)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.PChunkUpdate == null)
        return;
      // ISSUE: reference to a compiler-generated field
      this.PChunkUpdate(address, currentchunk, allchunks, transferred, length, okay, dump);
    }

    public void Dump(Dump dump)
    {
      this.Dump(dump.StartAddress, dump.EndAddress, dump);
    }

    public void Dump(uint startdump, uint enddump, Stream saveStream)
    {
      Stream[] saveStream1 = new Stream[1]{ saveStream };
      this.Dump(startdump, enddump, saveStream1);
    }

    public void Dump(uint startdump, uint enddump, Stream[] saveStream)
    {
      this.InitGecko();
      if (ValidMemory.rangeCheckId(startdump) != ValidMemory.rangeCheckId(enddump))
        enddump = ValidMemory.ValidAreas[ValidMemory.rangeCheckId(startdump)].high;
      if (!ValidMemory.validAddress(startdump))
        return;
      uint num1 = enddump - startdump;
      uint num2 = num1 / 1024U;
      uint nobytes = num1 % 1024U;
      uint num3 = num2;
      if (nobytes > 0U)
        ++num3;
      ulong num4 = ByteSwap.Swap(((ulong) startdump << 32) + (ulong) enddump);
      if (this.GeckoWrite(BitConverter.GetBytes((short) 4), 1) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoWrite(BitConverter.GetBytes(num4), 8) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      uint currentchunk = 0;
      byte num5 = 0;
      bool flag = false;
      this.CancelDump = false;
      byte[] numArray = new byte[1024];
      while (currentchunk < num2 && !flag)
      {
        this.SendUpdate(startdump + currentchunk * 1024U, currentchunk, num3, currentchunk * 1024U, num1, (int) num5 == 0, true);
        byte[] recbyte = new byte[1];
        if (this.GeckoRead(recbyte, 1U) != FTDICommand.CMD_OK)
        {
          int num6 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
          throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
        }
        if ((int) recbyte[0] == 176)
        {
          for (int index = 0; (long) index < 1024L; ++index)
            numArray[index] = (byte) 0;
        }
        else
        {
          switch (this.GeckoRead(numArray, 1024U))
          {
            case FTDICommand.CMD_ResultError:
              ++num5;
              if ((int) num5 >= 3)
              {
                int num6 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
                throw new ETCPGeckoException(ETCPErrorCode.TooManyRetries);
              }
              continue;
            case FTDICommand.CMD_FatalError:
              int num7 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
              throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
          }
        }
        foreach (Stream stream in saveStream)
          stream.Write(numArray, 0, 1024);
        num5 = (byte) 0;
        ++currentchunk;
        if (this.CancelDump)
        {
          int num6 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
          flag = true;
        }
      }
      while (!flag && nobytes > 0U)
      {
        this.SendUpdate(startdump + currentchunk * 1024U, currentchunk, num3, currentchunk * 1024U, num1, (int) num5 == 0, true);
        byte[] recbyte = new byte[1];
        if (this.GeckoRead(recbyte, 1U) != FTDICommand.CMD_OK)
        {
          int num6 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
          throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
        }
        if ((int) recbyte[0] == 176)
        {
          for (int index = 0; (long) index < (long) nobytes; ++index)
            numArray[index] = (byte) 0;
        }
        else
        {
          switch (this.GeckoRead(numArray, nobytes))
          {
            case FTDICommand.CMD_ResultError:
              ++num5;
              if ((int) num5 >= 3)
              {
                int num6 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
                throw new ETCPGeckoException(ETCPErrorCode.TooManyRetries);
              }
              continue;
            case FTDICommand.CMD_FatalError:
              int num7 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
              throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
          }
        }
        foreach (Stream stream in saveStream)
          stream.Write(numArray, 0, (int) nobytes);
        num5 = (byte) 0;
        flag = true;
      }
      this.SendUpdate(enddump, num3, num3, num1, num1, true, true);
    }

    public void Dump(uint startdump, uint enddump, Dump memdump)
    {
      this.InitGecko();
      uint num1 = enddump - startdump;
      uint num2 = num1 / 1024U;
      uint nobytes = num1 % 1024U;
      uint num3 = num2;
      if (nobytes > 0U)
        ++num3;
      ulong num4 = ByteSwap.Swap(((ulong) startdump << 32) + (ulong) enddump);
      if (this.GeckoWrite(BitConverter.GetBytes((short) 4), 1) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoWrite(BitConverter.GetBytes(num4), 8) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      uint currentchunk = 0;
      byte num5 = 0;
      bool flag = false;
      this.CancelDump = false;
      byte[] recbyte1 = new byte[1024];
      while (currentchunk < num2 && !flag)
      {
        this.SendUpdate(startdump + currentchunk * 1024U, currentchunk, num3, currentchunk * 1024U, num1, (int) num5 == 0, true);
        byte[] recbyte2 = new byte[1];
        if (this.GeckoRead(recbyte2, 1U) != FTDICommand.CMD_OK)
        {
          int num6 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
          throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
        }
        if ((int) recbyte2[0] == 176)
        {
          for (int index = 0; (long) index < 1024L; ++index)
            recbyte1[index] = (byte) 0;
        }
        else
        {
          switch (this.GeckoRead(recbyte1, 1024U))
          {
            case FTDICommand.CMD_ResultError:
              ++num5;
              if ((int) num5 >= 3)
              {
                int num6 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
                throw new ETCPGeckoException(ETCPErrorCode.TooManyRetries);
              }
              continue;
            case FTDICommand.CMD_FatalError:
              int num7 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
              throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
          }
        }
        Buffer.BlockCopy((Array) recbyte1, 0, (Array) memdump.mem, (int) currentchunk * 1024 + ((int) startdump - (int) memdump.StartAddress), 1024);
        memdump.ReadCompletedAddress = (uint) (((int) currentchunk + 1) * 1024) + startdump;
        num5 = (byte) 0;
        ++currentchunk;
        if (this.CancelDump)
        {
          int num6 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
          flag = true;
        }
      }
      while (!flag && nobytes > 0U)
      {
        this.SendUpdate(startdump + currentchunk * 1024U, currentchunk, num3, currentchunk * 1024U, num1, (int) num5 == 0, true);
        byte[] recbyte2 = new byte[1];
        if (this.GeckoRead(recbyte2, 1U) != FTDICommand.CMD_OK)
        {
          int num6 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
          throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
        }
        if ((int) recbyte2[0] == 176)
        {
          for (int index = 0; (long) index < (long) nobytes; ++index)
            recbyte1[index] = (byte) 0;
        }
        else
        {
          switch (this.GeckoRead(recbyte1, nobytes))
          {
            case FTDICommand.CMD_ResultError:
              ++num5;
              if ((int) num5 >= 3)
              {
                int num6 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
                throw new ETCPGeckoException(ETCPErrorCode.TooManyRetries);
              }
              continue;
            case FTDICommand.CMD_FatalError:
              int num7 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
              throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
          }
        }
        Buffer.BlockCopy((Array) recbyte1, 0, (Array) memdump.mem, (int) currentchunk * 1024 + ((int) startdump - (int) memdump.StartAddress), (int) nobytes);
        num5 = (byte) 0;
        flag = true;
      }
      this.SendUpdate(enddump, num3, num3, num1, num1, true, true);
    }

    public void Upload(uint startupload, uint endupload, Stream sendStream)
    {
      this.InitGecko();
      uint num1 = endupload - startupload;
      uint num2 = num1 / 1024U;
      uint num3 = num1 % 1024U;
      uint num4 = num2;
      if (num3 > 0U)
        ++num4;
      ulong num5 = ByteSwap.Swap(((ulong) startupload << 32) + (ulong) endupload);
      if (this.GeckoWrite(BitConverter.GetBytes((short) 65), 1) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoWrite(BitConverter.GetBytes(num5), 8) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      uint currentchunk = 0;
      byte num6 = 0;
      while (currentchunk < num2)
      {
        this.SendUpdate(startupload + currentchunk * 1024U, currentchunk, num4, currentchunk * 1024U, num1, (int) num6 == 0, false);
        byte[] numArray = new byte[1024];
        sendStream.Read(numArray, 0, 1024);
        switch (this.GeckoWrite(numArray, 1024))
        {
          case FTDICommand.CMD_ResultError:
            ++num6;
            if ((int) num6 >= 3)
            {
              this.Disconnect();
              throw new ETCPGeckoException(ETCPErrorCode.TooManyRetries);
            }
            sendStream.Seek(-1024L, SeekOrigin.Current);
            continue;
          case FTDICommand.CMD_FatalError:
            this.Disconnect();
            throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
          default:
            num6 = (byte) 0;
            ++currentchunk;
            continue;
        }
      }
      while (num3 > 0U)
      {
        this.SendUpdate(startupload + currentchunk * 1024U, currentchunk, num4, currentchunk * 1024U, num1, (int) num6 == 0, false);
        byte[] numArray = new byte[(int) num3];
        sendStream.Read(numArray, 0, (int) num3);
        switch (this.GeckoWrite(numArray, (int) num3))
        {
          case FTDICommand.CMD_ResultError:
            ++num6;
            if ((int) num6 >= 3)
            {
              this.Disconnect();
              throw new ETCPGeckoException(ETCPErrorCode.TooManyRetries);
            }
            sendStream.Seek((long) (-1 * (int) num3), SeekOrigin.Current);
            continue;
          case FTDICommand.CMD_FatalError:
            this.Disconnect();
            throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
          default:
            num6 = (byte) 0;
            num3 = 0U;
            continue;
        }
      }
      byte[] recbyte = new byte[1];
      if (this.GeckoRead(recbyte, 1U) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
      if ((int) recbyte[0] != 170)
        throw new ETCPGeckoException(ETCPErrorCode.FTDIInvalidReply);
      this.SendUpdate(endupload, num4, num4, num1, num1, true, false);
    }

    public bool Reconnect()
    {
      this.Disconnect();
      try
      {
        return this.Connect();
      }
      catch
      {
        return false;
      }
    }

    public FTDICommand RawCommand(byte id)
    {
      return this.GeckoWrite(BitConverter.GetBytes((short) id), 1);
    }

    public void Pause()
    {
      if (this.RawCommand((byte) 6) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    public void SafePause()
    {
      for (bool flag = this.status() == WiiStatus.Running; flag; flag = this.status() == WiiStatus.Running)
      {
        this.Pause();
        Thread.Sleep(100);
      }
    }

    public void Resume()
    {
      if (this.RawCommand((byte) 7) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    public void sendfail()
    {
      int num = (int) this.RawCommand((byte) 204);
    }

    public void poke(uint address, uint value)
    {
      address &= 4294967292U;
      ulong num = ByteSwap.Swap((ulong) address << 32 | (ulong) value);
      if (this.RawCommand((byte) 3) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoWrite(BitConverter.GetBytes(num), 8) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    public void poke32(uint address, uint value)
    {
      this.poke(address, value);
    }

    public void poke16(uint address, ushort value)
    {
      address &= 4294967294U;
      ulong num = ByteSwap.Swap((ulong) address << 32 | (ulong) value);
      if (this.RawCommand((byte) 2) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoWrite(BitConverter.GetBytes(num), 8) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    public void poke08(uint address, byte value)
    {
      ulong num = ByteSwap.Swap((ulong) address << 32 | (ulong) value);
      if (this.RawCommand((byte) 1) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoWrite(BitConverter.GetBytes(num), 8) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    public void poke_kern(uint address, uint value)
    {
      ulong num = ByteSwap.Swap((ulong) address << 32 | (ulong) value);
      if (this.RawCommand((byte) 11) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoWrite(BitConverter.GetBytes(num), 8) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    public uint peek_kern(uint address)
    {
      address = ByteSwap.Swap(address);
      if (this.RawCommand((byte) 12) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoWrite(BitConverter.GetBytes(address), 4) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      byte[] recbyte = new byte[4];
      if (this.GeckoRead(recbyte, 4U) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      return ByteSwap.Swap(BitConverter.ToUInt32(recbyte, 0));
    }

    public WiiStatus status()
    {
      Thread.Sleep(100);
      if (!this.InitGecko())
        throw new ETCPGeckoException(ETCPErrorCode.FTDIResetError);
      if (this.RawCommand((byte) 80) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      byte[] recbyte = new byte[1];
      if (this.GeckoRead(recbyte, 1U) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
      switch (recbyte[0])
      {
        case 0:
          return WiiStatus.Running;
        case 1:
          return WiiStatus.Paused;
        case 2:
          return WiiStatus.Breakpoint;
        case 3:
          return WiiStatus.Loader;
        default:
          return WiiStatus.Unknown;
      }
    }

    public void Step()
    {
      if (!this.InitGecko())
        throw new ETCPGeckoException(ETCPErrorCode.FTDIResetError);
      if (this.RawCommand((byte) 68) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    protected void Breakpoint(uint address, byte bptype, bool exact)
    {
      this.InitGecko();
      uint input = address & 4294967288U | (uint) bptype;
      bool flag = false;
      if (exact)
        flag = (int) this.VersionRequest() != 129;
      if (!flag)
      {
        if (this.RawCommand((byte) 9) != FTDICommand.CMD_OK)
          throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
        if (this.GeckoWrite(BitConverter.GetBytes(ByteSwap.Swap(input)), 4) != FTDICommand.CMD_OK)
          throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      }
      else
      {
        if (this.RawCommand((byte) 137) != FTDICommand.CMD_OK)
          throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
        if (this.GeckoWrite(BitConverter.GetBytes(ByteSwap.Swap((ulong) input << 32 | (ulong) address)), 8) != FTDICommand.CMD_OK)
          throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      }
    }

    public void BreakpointR(uint address, bool exact)
    {
      this.Breakpoint(address, (byte) 5, exact);
    }

    public void BreakpointR(uint address)
    {
      this.Breakpoint(address, (byte) 5, true);
    }

    public void BreakpointW(uint address, bool exact)
    {
      this.Breakpoint(address, (byte) 6, exact);
    }

    public void BreakpointW(uint address)
    {
      this.Breakpoint(address, (byte) 6, true);
    }

    public void BreakpointRW(uint address, bool exact)
    {
      this.Breakpoint(address, (byte) 7, exact);
    }

    public void BreakpointRW(uint address)
    {
      this.Breakpoint(address, (byte) 7, true);
    }

    public void BreakpointX(uint address)
    {
      this.InitGecko();
      uint num = ByteSwap.Swap((uint) ((int) address & -4 | 3));
      if (this.RawCommand((byte) 16) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoWrite(BitConverter.GetBytes(num), 4) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    public bool BreakpointHit()
    {
      byte[] recbyte = new byte[1];
      if (this.GeckoRead(recbyte, 1U) != FTDICommand.CMD_OK)
        return false;
      return (int) recbyte[0] == 17;
    }

    public void CancelBreakpoint()
    {
      if (this.RawCommand((byte) 56) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    protected bool AllowedVersion(byte version)
    {
      for (int index = 0; index < TCPGecko.GCAllowedVersions.Length; ++index)
      {
        if ((int) TCPGecko.GCAllowedVersions[index] == (int) version)
          return true;
      }
      return false;
    }

    public byte VersionRequest()
    {
      this.InitGecko();
      if (this.RawCommand((byte) 153) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      byte num1 = 0;
      byte num2 = 0;
      byte[] recbyte = new byte[1];
      while (this.GeckoRead(recbyte, 1U) != FTDICommand.CMD_OK || !this.AllowedVersion(recbyte[0]))
      {
        ++num1;
        if ((int) num1 >= 3)
          goto label_6;
      }
      num2 = recbyte[0];
label_6:
      return num2;
    }

    public uint OsVersionRequest()
    {
      if (this.RawCommand((byte) 154) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      byte[] recbyte = new byte[4];
      if (this.GeckoRead(recbyte, 4U) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      return ByteSwap.Swap(BitConverter.ToUInt32(recbyte, 0));
    }

    public uint peek(uint address)
    {
      if (!ValidMemory.validAddress(address))
        return 0;
      uint startdump = address & 4294967292U;
      MemoryStream memoryStream = new MemoryStream();
      // ISSUE: reference to a compiler-generated field
      GeckoProgress pchunkUpdate = this.PChunkUpdate;
      // ISSUE: reference to a compiler-generated field
      this.PChunkUpdate = (GeckoProgress) null;
      try
      {
        this.Dump(startdump, startdump + 4U, (Stream) memoryStream);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        byte[] buffer = new byte[4];
        memoryStream.Read(buffer, 0, 4);
        return ByteSwap.Swap(BitConverter.ToUInt32(buffer, 0));
      }
      finally
      {
        // ISSUE: reference to a compiler-generated field
        this.PChunkUpdate = pchunkUpdate;
        memoryStream.Close();
      }
    }

    public void GetRegisters(Stream stream, uint contextAddress)
    {
      uint num = 432;
      MemoryStream memoryStream = new MemoryStream();
      this.Dump(contextAddress + 8U, contextAddress + 8U + num, (Stream) memoryStream);
      byte[] array = memoryStream.ToArray();
      stream.Write(array, 128, 4);
      stream.Write(array, 140, 4);
      stream.Write(array, 136, 4);
      stream.Write(new byte[8], 0, 8);
      stream.Write(array, 144, 8);
      stream.Write(array, 0, 128);
      stream.Write(array, 132, 4);
      stream.Write(array, 176, 256);
    }

    public void SendRegisters(Stream sendStream, uint contextAddress)
    {
      MemoryStream memoryStream = new MemoryStream();
      byte[] buffer = new byte[160];
      sendStream.Seek(0L, SeekOrigin.Begin);
      sendStream.Read(buffer, 0, buffer.Length);
      memoryStream.Write(buffer, 28, 128);
      memoryStream.Write(buffer, 0, 4);
      memoryStream.Write(buffer, 156, 4);
      memoryStream.Write(buffer, 8, 4);
      memoryStream.Write(buffer, 4, 4);
      memoryStream.Write(buffer, 20, 8);
      memoryStream.Seek(0L, SeekOrigin.Begin);
      this.Upload(contextAddress + 8U, (uint) ((int) contextAddress + 8 + 152), (Stream) memoryStream);
    }

    private ulong readInt64(Stream inputstream)
    {
      byte[] buffer = new byte[8];
      inputstream.Read(buffer, 0, 8);
      return ByteSwap.Swap(BitConverter.ToUInt64(buffer, 0));
    }

    private void writeInt64(Stream outputstream, ulong value)
    {
      byte[] bytes = BitConverter.GetBytes(ByteSwap.Swap(value));
      outputstream.Write(bytes, 0, 8);
    }

    private void insertInto(Stream insertStream, ulong value)
    {
      MemoryStream memoryStream = new MemoryStream();
      this.writeInt64((Stream) memoryStream, value);
      insertStream.Seek(0L, SeekOrigin.Begin);
      byte[] buffer1 = new byte[insertStream.Length];
      insertStream.Read(buffer1, 0, (int) insertStream.Length);
      memoryStream.Write(buffer1, 0, (int) insertStream.Length);
      insertStream.Seek(0L, SeekOrigin.Begin);
      memoryStream.Seek(0L, SeekOrigin.Begin);
      byte[] buffer2 = new byte[memoryStream.Length];
      memoryStream.Read(buffer2, 0, (int) memoryStream.Length);
      insertStream.Write(buffer2, 0, (int) memoryStream.Length);
      memoryStream.Close();
    }

    public void sendCheats(Stream inputStream)
    {
      MemoryStream memoryStream = new MemoryStream();
      byte[] buffer = new byte[inputStream.Length];
      inputStream.Seek(0L, SeekOrigin.Begin);
      inputStream.Read(buffer, 0, (int) inputStream.Length);
      memoryStream.Write(buffer, 0, (int) inputStream.Length);
      if ((uint) memoryStream.Length % 8U > 0U)
      {
        memoryStream.Close();
        throw new ETCPGeckoException(ETCPErrorCode.CheatStreamSizeInvalid);
      }
      this.InitGecko();
      memoryStream.Seek(-8L, SeekOrigin.End);
      ulong num1 = this.readInt64((Stream) memoryStream) & 18302628885633695744UL;
      if ((long) num1 != -1152921504606846976L && (long) num1 != -144115188075855872L)
      {
        memoryStream.Seek(0L, SeekOrigin.End);
        this.writeInt64((Stream) memoryStream, 17293822569102704640UL);
      }
      memoryStream.Seek(0L, SeekOrigin.Begin);
      if ((long) this.readInt64((Stream) memoryStream) != 58758854884770014L)
        this.insertInto((Stream) memoryStream, 58758854884770014UL);
      memoryStream.Seek(0L, SeekOrigin.Begin);
      uint length = (uint) memoryStream.Length;
      if (this.GeckoWrite(BitConverter.GetBytes((short) 64), 1) != FTDICommand.CMD_OK)
      {
        memoryStream.Close();
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      }
      uint num2 = length / 1024U;
      uint num3 = length % 1024U;
      uint num4 = num2;
      if (num3 > 0U)
        ++num4;
      byte num5 = 0;
      while ((int) num5 < 10)
      {
        byte[] recbyte = new byte[1];
        if (this.GeckoRead(recbyte, 1U) != FTDICommand.CMD_OK)
        {
          memoryStream.Close();
          throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
        }
        if ((int) recbyte[0] != 170)
        {
          if ((int) num5 == 9)
          {
            memoryStream.Close();
            throw new ETCPGeckoException(ETCPErrorCode.FTDIInvalidReply);
          }
        }
        else
          break;
      }
      if (this.GeckoWrite(BitConverter.GetBytes(ByteSwap.Swap(length)), 4) != FTDICommand.CMD_OK)
      {
        memoryStream.Close();
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      }
      uint currentchunk = 0;
      byte num6 = 0;
      while (currentchunk < num2)
      {
        this.SendUpdate(13680862U, currentchunk, num4, currentchunk * 1024U, length, (int) num6 == 0, false);
        byte[] numArray = new byte[1024];
        memoryStream.Read(numArray, 0, 1024);
        switch (this.GeckoWrite(numArray, 1024))
        {
          case FTDICommand.CMD_ResultError:
            ++num6;
            if ((int) num6 >= 3)
            {
              int num7 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
              memoryStream.Close();
              throw new ETCPGeckoException(ETCPErrorCode.TooManyRetries);
            }
            memoryStream.Seek(-1024L, SeekOrigin.Current);
            int num8 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 187), 1);
            continue;
          case FTDICommand.CMD_FatalError:
            int num9 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
            memoryStream.Close();
            throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
          default:
            byte[] recbyte = new byte[1];
            FTDICommand ftdiCommand = this.GeckoRead(recbyte, 1U);
            if (ftdiCommand == FTDICommand.CMD_ResultError || (int) recbyte[0] != 170)
            {
              ++num6;
              if ((int) num6 >= 3)
              {
                int num7 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
                memoryStream.Close();
                throw new ETCPGeckoException(ETCPErrorCode.TooManyRetries);
              }
              memoryStream.Seek(-1024L, SeekOrigin.Current);
              int num10 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 187), 1);
              continue;
            }
            if (ftdiCommand == FTDICommand.CMD_FatalError)
            {
              int num7 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
              memoryStream.Close();
              throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
            }
            num6 = (byte) 0;
            ++currentchunk;
            continue;
        }
      }
      while (num3 > 0U)
      {
        this.SendUpdate(13680862U, currentchunk, num4, currentchunk * 1024U, length, (int) num6 == 0, false);
        byte[] numArray = new byte[(int) num3];
        memoryStream.Read(numArray, 0, (int) num3);
        switch (this.GeckoWrite(numArray, (int) num3))
        {
          case FTDICommand.CMD_ResultError:
            ++num6;
            if ((int) num6 >= 3)
            {
              int num7 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
              memoryStream.Close();
              throw new ETCPGeckoException(ETCPErrorCode.TooManyRetries);
            }
            memoryStream.Seek((long) (-1 * (int) num3), SeekOrigin.Current);
            int num8 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 187), 1);
            continue;
          case FTDICommand.CMD_FatalError:
            int num9 = (int) this.GeckoWrite(BitConverter.GetBytes((short) 204), 1);
            memoryStream.Close();
            throw new ETCPGeckoException(ETCPErrorCode.FTDIReadDataError);
          default:
            num6 = (byte) 0;
            num3 = 0U;
            continue;
        }
      }
      this.SendUpdate(13680862U, num4, num4, length, length, true, false);
      memoryStream.Close();
    }

    public void ExecuteCheats()
    {
      if (this.RawCommand((byte) 96) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    public void Hook(bool pause, WiiLanguage language, WiiPatches patches, WiiHookType hookType)
    {
      this.InitGecko();
      if (this.RawCommand((byte) ((!pause ? 66U : 67U) + (uint) (byte) hookType)) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.RawCommand((uint) language <= 0U ? (byte) 205 : (byte) (language - 1)) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.RawCommand((byte) patches) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
    }

    public void Hook()
    {
      this.Hook(false, WiiLanguage.NoOverride, WiiPatches.NoPatches, WiiHookType.VI);
    }

    private static byte ConvertSafely(double floatValue)
    {
      return (byte) Math.Round(Math.Max(0.0, Math.Min(floatValue, (double) byte.MaxValue)));
    }

    private static Bitmap ProcessImage(uint width, uint height, Stream analyze)
    {
      Bitmap bitmap = new Bitmap((int) width, (int) height, PixelFormat.Format24bppRgb);
      BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, (int) width, (int) height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
      int length = bitmapdata.Stride * bitmapdata.Height;
      byte[] numArray = new byte[length];
      Marshal.Copy(bitmapdata.Scan0, numArray, 0, length);
      byte[] buffer = new byte[(int) width * (int) height * 2];
      int num1 = 0;
      int num2 = 0;
      analyze.Read(buffer, 0, (int) width * (int) height * 2);
      for (int index1 = 0; (long) index1 < (long) (width * height); ++index1)
      {
        int index2 = index1 * 2;
        int num3;
        if (index1 % 2 == 0)
        {
          num3 = (int) buffer[index2];
          num1 = (int) buffer[index2 + 1];
          num2 = (int) buffer[index2 + 3];
        }
        else
          num3 = (int) buffer[index2];
        int index3 = index1 * 3;
        numArray[index3] = TCPGecko.ConvertSafely(1.164 * (double) (num3 - 16) + 2.017 * (double) (num1 - 128));
        numArray[index3 + 1] = TCPGecko.ConvertSafely(1.164 * (double) (num3 - 16) - 0.392 * (double) (num1 - 128) - 0.813 * (double) (num2 - 128));
        numArray[index3 + 2] = TCPGecko.ConvertSafely(1.164 * (double) (num3 - 16) + 1.596 * (double) (num2 - 128));
      }
      Marshal.Copy(numArray, 0, bitmapdata.Scan0, numArray.Length);
      bitmap.UnlockBits(bitmapdata);
      return bitmap;
    }

    public Image Screenshot()
    {
      MemoryStream memoryStream1 = new MemoryStream();
      this.Dump(3422560256U, 3422560384U, (Stream) memoryStream1);
      memoryStream1.Seek(0L, SeekOrigin.Begin);
      byte[] buffer = new byte[128];
      memoryStream1.Read(buffer, 0, 128);
      memoryStream1.Close();
      uint width = (uint) buffer[73] << 3;
      uint height = (uint) (((int) buffer[0] << 5 | (int) buffer[1] >> 3) & 2046);
      uint num = (uint) ((int) buffer[29] << 16 | (int) buffer[30] << 8) | (uint) buffer[31];
      if (((int) buffer[28] & 16) == 16)
        num <<= 5;
      uint startdump = num + 2147483648U - (uint) (((int) buffer[28] & 15) << 3);
      MemoryStream memoryStream2 = new MemoryStream();
      this.Dump(startdump, startdump + (uint) ((int) height * (int) width * 2), (Stream) memoryStream2);
      memoryStream2.Seek(0L, SeekOrigin.Begin);
      if (height > 600U)
      {
        height /= 2U;
        width *= 2U;
      }
      Bitmap bitmap = TCPGecko.ProcessImage(width, height, (Stream) memoryStream2);
      memoryStream2.Close();
      return (Image) bitmap;
    }

    public uint rpc(uint address, params uint[] args)
    {
      return (uint) (this.rpc64(address, args) >> 32);
    }

    public ulong rpc64(uint address, params uint[] args)
    {
      byte[] numArray = new byte[36];
      address = ByteSwap.Swap(address);
      BitConverter.GetBytes(address).CopyTo((Array) numArray, 0);
      for (int index = 0; index < 8; ++index)
      {
        if (index < args.Length)
          BitConverter.GetBytes(ByteSwap.Swap(args[index])).CopyTo((Array) numArray, 4 + index * 4);
        else
          BitConverter.GetBytes(4274704570U).CopyTo((Array) numArray, 4 + index * 4);
      }
      if (this.RawCommand((byte) 112) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoWrite(numArray, numArray.Length) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      if (this.GeckoRead(numArray, 8U) != FTDICommand.CMD_OK)
        throw new ETCPGeckoException(ETCPErrorCode.FTDICommandSendError);
      return ByteSwap.Swap(BitConverter.ToUInt64(numArray, 0));
    }
  }
}
