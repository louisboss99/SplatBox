using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace SplatBox
{
  internal class tcpconn
  {
    private TcpClient client;
    private NetworkStream stream;

    public string Host { get; private set; }

    public int Port { get; private set; }

    public tcpconn(string host, int port)
    {
      this.Host = host;
      this.Port = port;
      this.client = (TcpClient) null;
      this.stream = (NetworkStream) null;
    }

    public void Connect()
    {
      try
      {
        this.Close();
      }
      catch (Exception ex)
      {
      }
      this.client = new TcpClient();
      this.client.NoDelay = true;
      IAsyncResult asyncResult = this.client.BeginConnect(this.Host, this.Port, (AsyncCallback) null, (object) null);
      WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
      try
      {
        if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5.0), false))
        {
          this.client.Close();
          throw new IOException("Connection timoeut.", (Exception) new TimeoutException());
        }
        this.client.EndConnect(asyncResult);
      }
      finally
      {
        asyncWaitHandle.Close();
      }
      this.stream = this.client.GetStream();
      this.stream.ReadTimeout = 10000;
      this.stream.WriteTimeout = 10000;
    }

    public void Close()
    {
      try
      {
        if (this.client == null)
          throw new IOException("Not connected.", (Exception) new NullReferenceException());
        this.client.Close();
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.client = (TcpClient) null;
      }
    }

    public void Purge()
    {
      if (this.stream == null)
        throw new IOException("Not connected.", (Exception) new NullReferenceException());
      this.stream.Flush();
    }

    public void Read(byte[] buffer, uint nobytes, ref uint bytes_read)
    {
      try
      {
        int offset = 0;
        if (this.stream == null)
          throw new IOException("Not connected.", (Exception) new NullReferenceException());
        bytes_read = 0U;
        while (nobytes > 0U)
        {
          int num = this.stream.Read(buffer, offset, (int) nobytes);
          if (num < 0)
            break;
          bytes_read = bytes_read + (uint) num;
          offset += num;
          nobytes -= (uint) num;
        }
      }
      catch (ObjectDisposedException ex)
      {
        throw new IOException("Connection closed.", (Exception) ex);
      }
    }

    public void Write(byte[] buffer, int nobytes, ref uint bytes_written)
    {
      try
      {
        if (this.stream == null)
          throw new IOException("Not connected.", (Exception) new NullReferenceException());
        this.stream.Write(buffer, 0, nobytes);
        bytes_written = nobytes < 0 ? 0U : (uint) nobytes;
        this.stream.Flush();
      }
      catch (ObjectDisposedException ex)
      {
        throw new IOException("Connection closed.", (Exception) ex);
      }
    }
  }
}
