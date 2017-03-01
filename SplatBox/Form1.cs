using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SplatBox
{
  public class Form1 : Form
  {
    private bool GeckoConnected = false;
    private bool hasExtendedHandlerInstalled = false;
    private string reply = "false";
    private bool quitting = false;
    private uint ppadd = 588;
    private uint ppbase = 275662568;
    private uint pp2add = 6480;
    private uint pp2base = 275664296;
    private uint val1 = 16;
    private uint val2 = 32;
    private uint val3 = 8;
    private uint val4 = 40;
    private uint val5 = 4;
    private uint val6 = 4;
    private uint[] val1states = new uint[3];
    private uint[] val2states = new uint[3];
    private uint[] val3states = new uint[3];
    private uint[] val4states = new uint[3];
    private uint[] val5states = new uint[3];
    private uint[] val6states = new uint[3];
    private uint[] val7states = new uint[3];
    private IContainer components = (IContainer) null;
    public TCPGecko Gecko;
    public uint ZAddress;
    public uint P2ZAddress;
    private TextBox IPBox;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private Button DisconnButton;
    private Label label2;
    private Button State1SaveButton;
    private Button State3SaveButton;
    private Button State2SaveButton;
    private Button State3LoadButton;
    private Button State2LoadButton;
    private Button State1LoadButton;
    private Button RecalcPointerButton;
    private Button ConnectButton;
    private ListBox EventLogBox;
    private GroupBox ManControlBox;
    private RadioButton radioButton3;
    private RadioButton radioButton2;
    private RadioButton radioButton1;
    private Button ForwardManButton;
    private Button ZUpManButton;
    private Button UpManButton;
    private RadioButton radioButton4;
    private TextBox textBox1;
    private CheckBox checkBox1;
    private GroupBox MapLoaderBox;
    private Button MapPokeButton;
    private ComboBox seCBox;
    private ComboBox NameCBox;
    private CheckBox OnlineCheckBox;
    private CheckBox DojoCheckBox;
    private Label label3;
    private Label label1;
    private Button button1;
    private Button BackwardsManButton;
    private Button FreezeZButton;
    private CheckBox checkBox2;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private TabPage tabPage3;
    private Label label4;
    private TabPage tabPage4;
    private Button button9;
    private Button button8;
    private Button button7;
    private Button button6;
    private Button button5;
    private Button button4;
    private Button button3;
    private Button button2;
    private TabPage tabPage5;
    private Button TpToP2Button;

    public Form1()
    {
      this.InitializeComponent();
     // this.IntegrityCheck("2120");
      this.NameCBox.DataSource = (object) new NameWrapper[58]
      {
        new NameWrapper("<no change>", "<no change>"),
        new NameWrapper("Urchin Underpass", "Fld_Crank00_Vss"),
        new NameWrapper("Walleye Warehouse", "Fld_Warehouse00_Vss"),
        new NameWrapper("Saltspray Rig", "Fld_SeaPlant00_Vss"),
        new NameWrapper("Arowana Mall", "Fld_UpDown00_Vss"),
        new NameWrapper("Blackbelly Skatepark", "Fld_SkatePark00_Vss"),
        new NameWrapper("Camp Triggerfish", "Fld_Athletic00_Vss"),
        new NameWrapper("Port Mackerel", "Fld_Amida00_Vss"),
        new NameWrapper("Kelp Dome", "Fld_Maze00_Vss"),
        new NameWrapper("Moray Towers", "Fld_Tuzura00_Vss"),
        new NameWrapper("Bluefin Depot", "Fld_Ruins00_Vss"),
        new NameWrapper("Shooting Range", "Fld_ShootingRange_Shr"),
        new NameWrapper("Ancho-V Games", "Fld_Office00_Vss"),
        new NameWrapper("Piranha Pit", "Fld_Quarry00_Vss"),
        new NameWrapper("Flounder Heights", "Fld_Jyoheki00_Vss"),
        new NameWrapper("Museum d'Alfonsino", "Fld_Pivot00_Vss"),
        new NameWrapper("Mahi-Mahi Resort", "Fld_Hiagari00_Vss"),
        new NameWrapper("Hammerhead Bridge", "Fld_Kaisou00_Vss"),
        new NameWrapper("Urchin Underpass (Dojo)", "Fld_Crank00_Dul"),
        new NameWrapper("Walleye Warehouse (Dojo)", "Fld_Warehouse00_Dul"),
        new NameWrapper("Saltspray Rig (Dojo)", "Fld_SeaPlant00_Dul"),
        new NameWrapper("Arowana Mall (Dojo)", "Fld_UpDown00_Dul"),
        new NameWrapper("Blackbelly Skatepark (Dojo)", "Fld_SkatePark00_Dul"),
        new NameWrapper("Tutorial 1", "Fld_Tutorial00_Ttr"),
        new NameWrapper("Tutorial 2", "Fld_TutorialShow00_Ttr"),
        new NameWrapper("Octotrooper Hideout", "Fld_EasyHide00_Msn"),
        new NameWrapper("Lair of the Octoballs", "Fld_EasyClimb00_Msn"),
        new NameWrapper("Rise of the Octocopters", "Fld_EasyJump00_Msn"),
        new NameWrapper("Gusher Gauntlet", "Fld_Geyser00_Msn"),
        new NameWrapper("Floating Sponge Garden", "Fld_Sponge00_Msn"),
        new NameWrapper("Propeller Lift Fortress", "Fld_Propeller00_Msn"),
        new NameWrapper("Spreader Splatfest", "Fld_PaintingLift00_Msn"),
        new NameWrapper("Octoling Invasion", "Fld_RvlMaze00_Msn"),
        new NameWrapper("Unidentified Flying Object", "Fld_OctZero00_Msn"),
        new NameWrapper("Inkrail Skyscape", "Fld_InkRail00_Msn"),
        new NameWrapper("Inkvisible Avenues", "Fld_Invisible00_Msn"),
        new NameWrapper("Flooder Junkyard", "Fld_Dozer00_Msn"),
        new NameWrapper("Shifting Splatforms", "Fld_SlideLift00_Msn"),
        new NameWrapper("Octoling Assault", "Fld_RvlSkatePark00_Msn"),
        new NameWrapper("Undeniable Flying Object", "Fld_OctRuins00_Msn"),
        new NameWrapper("Propeller Lift Playground", "Fld_Propeller01_Msn"),
        new NameWrapper("Octosniper Ramparts", "Fld_Charge00_Msn"),
        new NameWrapper("Spinning Spreaders", "Fld_PaintingLift01_Msn"),
        new NameWrapper("Tumbling Splatforms", "Fld_TurnLift00_Msn"),
        new NameWrapper("Octoling Uprising", "Fld_RvlRuins00_Msn"),
        new NameWrapper("Unwelcome Flying Object", "Fld_OctCrank00_Msn"),
        new NameWrapper("Switch Box Shake-Up", "Fld_Trance00_Msn"),
        new NameWrapper("Spongy Observatory", "Fld_Sponge01_Msn"),
        new NameWrapper("Pinwheel Power Plant", "Fld_Fusya00_Msn"),
        new NameWrapper("Far-Flung Flooders", "Fld_Dozer01_Msn"),
        new NameWrapper("Octoling Onslaught", "Fld_RvlSeaPlant00_Msn"),
        new NameWrapper("Unavoidable Flying Object", "Fld_OctSkatePark00_Msn"),
        new NameWrapper("Staff Roll", "Fld_StaffRoll00_Stf"),
        new NameWrapper("Boss 1", "Fld_BossStampKing_Bos_Msn"),
        new NameWrapper("Boss 2", "Fld_BossCylinderKing_Bos_Msn"),
        new NameWrapper("Boss 3", "Fld_BossBallKing_Bos_Msn"),
        new NameWrapper("Boss 4", "Fld_BossMouthKing_Bos_Msn"),
        new NameWrapper("Boss 5", "Fld_BossRailKing_Bos_Msn")
      };
      this.NameCBox.SelectedIndex = 0;
      this.seCBox.DataSource = (object) new NameWrapper[18]
      {
        new NameWrapper("<no change>", "<no change>"),
        new NameWrapper("Day 1", "MisSkyDay01,Common"),
        new NameWrapper("Twilight 1", "MisSkyTwilight,Common"),
        new NameWrapper("Day 2", "MisSkyDay,Common"),
        new NameWrapper("Green", "MisSkyGreen,Common"),
        new NameWrapper("Sunset", "MisSkySunset,Common"),
        new NameWrapper("Night", "MisSkyNight,Common"),
        new NameWrapper("Galaxy Monitors", "MisSkyGalaxy,Common"),
        new NameWrapper("Gray", "MisSkyGray,Common"),
        new NameWrapper("Twilight 2", "MisTwilight,Common"),
        new NameWrapper("Dozer", "MisDozer,Common"),
        new NameWrapper("Battle", "MisBattle"),
        new NameWrapper("Broken Monitors", "MisMonitorBroken,Common"),
        new NameWrapper("Boss 1", "Stampking,Common"),
        new NameWrapper("Boss 2", "CylinderKing,Common"),
        new NameWrapper("Boss 3", "BallKing,Common"),
        new NameWrapper("Boss 4", "Mouthking,Common"),
        new NameWrapper("Boss 5", "RailKing,Common")
      };
      this.seCBox.SelectedIndex = 0;
    }
    /*
    [DllImport("ntdll.dll", SetLastError = true)]
    private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

    public void IntegrityCheck(string appid)
    {
      WebClient webClient = new WebClient();
      string passPhrase = "qw414plfds+äsda";
      string cipherText = "qEW3ipho3BBowQBrSVjn2RO2ICX0uOT06n1knRFIzvckPfX02fwGscZfl3KnyxC0rXfrVqTso3Qmp5AiOp/stl/cCqhnaYLuXFS02Q94yL3wG8tO9B77gT1NkHSAZ8+B";
      try
      {
        Form1.NoSSLTrust();
        this.reply = webClient.DownloadString(StringCipher.Decrypt(cipherText, passPhrase) + "?ver=" + appid + "&usr=" + Environment.UserName);
        using (MD5 md5 = MD5.Create())
        {
          using (FileStream fileStream = System.IO.File.OpenRead(Assembly.GetEntryAssembly().Location))
          {
            if (BitConverter.ToString(md5.ComputeHash((Stream) fileStream)).Replace("-", "") != this.reply)
            {
              int num = (int) MessageBox.Show("¯\\_(ツ)_/¯", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              int processInformation = 1;
              int processInformationClass = 29;
              Process.EnterDebugMode();
              Form1.NtSetInformationProcess(Process.GetCurrentProcess().Handle, processInformationClass, ref processInformation, 4);
              this.Close();
              Application.Exit();
            }
          }
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Failure: \n\n" + (object) ex + "\n\nQuitting.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.Close();
        this.quitting = true;
        Application.Exit();
      }
      if (!(this.reply == "invalid") || this.quitting)
        return;
      int num1 = (int) MessageBox.Show("¯\\_(ツ)_/¯", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      int processInformation1 = 1;
      int processInformationClass1 = 29;
      Process.EnterDebugMode();
      Form1.NtSetInformationProcess(Process.GetCurrentProcess().Handle, processInformationClass1, ref processInformation1, 4);
      this.Close();
      Application.Exit();
    } */

    public string ByteArrayToString(byte[] input)
    {
      return new UTF8Encoding().GetString(input);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Gecko = new TCPGecko(this.IPBox.Text, 7331);
      try
      {
        this.Gecko.Connect();
      }
      catch (ETCPGeckoException ex)
      {
        int num = (int) MessageBox.Show("Connection to the TCPGecko failed: \n\n" + (object) ex + "\n\nCheck your connection/firewall.", "Connection failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      this.groupBox2.Enabled = true;
      this.ManControlBox.Enabled = true;
      this.DisconnButton.Enabled = true;
      this.ConnectButton.Enabled = false;
      this.MapLoaderBox.Enabled = true;
      this.IPBox.Enabled = false;
      this.GeckoConnected = true;
      this.tabControl1.Enabled = true;
      this.Gecko.poke(268520700U, 1U);
      if ((int) this.Gecko.peek(268520704U) == 0)
      {
        int num = (int) MessageBox.Show("Cafe Code Type handler not found! Some codes are not usable!", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.hasExtendedHandlerInstalled = false;
      }
      else
      {
        this.hasExtendedHandlerInstalled = true;
        this.checkBox2.Enabled = true;
      }
      this.Gecko.poke(268520700U, 0U);
      this.EventLogBox.Items.Add((object) ("Connected to TCPGecko at " + this.IPBox.Text));
      this.ZAddress = this.getZAddressForMap();
      int num1 = (int) MessageBox.Show(string.Format("{0:x2}", (object) this.ZAddress), "info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    private void DisconnectGecko(object sender, EventArgs e)
    {
      this.Gecko.Disconnect();
      this.groupBox2.Enabled = false;
      this.DisconnButton.Enabled = false;
      this.ConnectButton.Enabled = true;
      this.ManControlBox.Enabled = false;
      this.MapLoaderBox.Enabled = false;
      this.IPBox.Enabled = true;
      this.GeckoConnected = false;
      this.checkBox2.Enabled = false;
      this.tabControl1.Enabled = false;
    }

    private uint getZAddressForMap()
    {
      try
      {
        return this.Gecko.peek(this.ppbase) + this.ppadd;
      }
      catch (Exception ex)
      {
        return uint.MaxValue;
      }
    }

    private uint getP2ZAddressForMap()
    {
      try
      {
        uint num1 = this.Gecko.peek(this.pp2base);
        int num2 = (int) MessageBox.Show("Operation failed: \n\n" + string.Format("{0:x2}", (object) this.Gecko.peek(this.pp2base)) + "\n\nError reading memory.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return num1 + this.pp2add;
      }
      catch (Exception ex)
      {
        return uint.MaxValue;
      }
    }

    private void SaveState(int state)
    {
      if ((int) this.Gecko.peek(this.ZAddress) == 0)
      {
        int num1 = (int) MessageBox.Show("Gecko.peek(ZAddress) == 0x00000000!\n\nYou may need to recalculate pointers!", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        try
        {
          this.val1states[state] = this.Gecko.peek(this.ZAddress - this.val1);
          this.val2states[state] = this.Gecko.peek(this.ZAddress - this.val2);
          this.val3states[state] = this.Gecko.peek(this.ZAddress - this.val3);
          this.val4states[state] = this.Gecko.peek(this.ZAddress - this.val4);
          this.val5states[state] = this.Gecko.peek(this.ZAddress - this.val5);
          this.val6states[state] = this.Gecko.peek(this.ZAddress + this.val6);
          this.val7states[state] = this.Gecko.peek(this.ZAddress);
          this.EventLogBox.Items.Add((object) ("Saved state " + (object) state + ", peeked vals:" + string.Format("{0:x2}", (object) this.val1states[state]) + "," + string.Format("{0:x2}", (object) this.val2states[state]) + "," + string.Format("{0:x2}", (object) this.val3states[state]) + "," + string.Format("{0:x2}", (object) this.val4states[state]) + "," + string.Format("{0:x2}", (object) this.val5states[state]) + "," + string.Format("{0:x2}", (object) this.val6states[state]) + ","));
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("Operation failed: \n\n" + (object) ex + "\n\nError reading memory.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void PokeState(int state)
    {
      if ((int) this.Gecko.peek(this.ZAddress) == 0)
      {
        int num1 = (int) MessageBox.Show("Gecko.peek(ZAddress) == 0x00000000!\n\nYou may need to recalculate pointers!", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        try
        {
          this.Gecko.poke(this.ZAddress - this.val1, this.val1states[state]);
          this.Gecko.poke(this.ZAddress - this.val2, this.val2states[state]);
          this.Gecko.poke(this.ZAddress - this.val3, this.val3states[state]);
          this.Gecko.poke(this.ZAddress - this.val4, this.val4states[state]);
          this.Gecko.poke(this.ZAddress - this.val5, this.val5states[state]);
          this.Gecko.poke(this.ZAddress + this.val6, this.val6states[state]);
          this.Gecko.poke(this.ZAddress, this.val7states[state]);
          this.EventLogBox.Items.Add((object) ("Poked state " + (object) state + ", poked vals:" + string.Format("{0:x2}", (object) this.val1states[state]) + "," + string.Format("{0:x2}", (object) this.val2states[state]) + "," + string.Format("{0:x2}", (object) this.val3states[state]) + "," + string.Format("{0:x2}", (object) this.val4states[state]) + "," + string.Format("{0:x2}", (object) this.val5states[state]) + "," + string.Format("{0:x2}", (object) this.val6states[state]) + ","));
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("Operation failed: \n\n" + (object) ex + "\n\nError writing to memory.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void State1SaveButton_Click(object sender, EventArgs e)
    {
      this.SaveState(0);
    }

    private void RecalcPointerButton_Click(object sender, EventArgs e)
    {
      this.ZAddress = this.getZAddressForMap();
      this.P2ZAddress = this.getP2ZAddressForMap();
    }

    private void State1LoadButton_Click(object sender, EventArgs e)
    {
      this.PokeState(0);
    }

    private void State2SaveButton_Click(object sender, EventArgs e)
    {
      this.SaveState(1);
    }

    private void State2LoadButton_Click(object sender, EventArgs e)
    {
      this.PokeState(1);
    }

    private void State3SaveButton_Click(object sender, EventArgs e)
    {
      this.SaveState(2);
    }

    private void State3LoadButton_Click(object sender, EventArgs e)
    {
      this.PokeState(2);
    }

    private void PokeSpecialFeature()
    {
      this.writeStringSimple(274808508U, "Join_Fest", "Join_Regular".Length);
      this.writeStringSimple(274808524U, "Join_Fest", "Join_Gachi".Length);
    }

    private void ZUpManButtonClick(object sender, EventArgs e)
    {
      if (this.checkBox1.Checked)
      {
        this.EventLogBox.Items.Add((object) ("Getting val for Z: " + string.Format("{0:x2}", (object) this.Gecko.peek(this.ZAddress))));
        this.textBox1.Text = string.Format("{0:x2}", (object) this.Gecko.peek(this.ZAddress));
      }
      else
      {
        uint input = this.Gecko.peek(this.ZAddress);
        uint num1 = this.Gecko.peek(this.ZAddress);
        uint num2 = !this.radioButton1.Checked ? (!this.radioButton2.Checked ? (!this.radioButton3.Checked ? (!this.radioButton4.Checked ? this.ChangeUintPos(input, 0, '4') : Convert.ToUInt32(this.textBox1.Text, 16)) : this.ChangeUintPos(this.ChangeUintPos(input, 0, '4'), 1, '4')) : this.ChangeUintPos(this.ChangeUintPos(input, 0, '4'), 1, '3')) : this.ChangeUintPos(this.ChangeUintPos(input, 0, '4'), 1, '1');
        try
        {
          this.Gecko.poke(this.ZAddress, num2);
          this.EventLogBox.Items.Add((object) ("Poked! VAL: " + string.Format("{0:x2}", (object) num2) + ", PRE: " + string.Format("{0:x2}", (object) num1)));
        }
        catch (Exception ex)
        {
          int num3 = (int) MessageBox.Show("Operation failed: \n\n" + (object) ex + "\n\nError reading/writing to memory.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    public uint ChangeUintPos(uint input, int pos, char tochangeto)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder(string.Format("{0:x2}", (object) input));
        stringBuilder[pos] = tochangeto;
        return Convert.ToUInt32(stringBuilder.ToString(), 16);
      }
      catch (Exception ex)
      {
        return uint.MaxValue;
      }
    }

    private void FetchBoxChange(object sender, EventArgs e)
    {
      if (this.checkBox1.Checked)
      {
        this.radioButton1.Enabled = false;
        this.radioButton2.Enabled = false;
        this.radioButton3.Enabled = false;
        this.radioButton4.Enabled = false;
        this.textBox1.ReadOnly = true;
      }
      else
      {
        this.radioButton1.Enabled = true;
        this.radioButton2.Enabled = true;
        this.radioButton3.Enabled = true;
        this.radioButton4.Enabled = true;
        this.textBox1.ReadOnly = false;
      }
    }

    private void RightManButton_Click(object sender, EventArgs e)
    {
      if (this.checkBox1.Checked)
      {
        this.EventLogBox.Items.Add((object) ("Getting val for Z: " + string.Format("{0:x2}", (object) this.Gecko.peek(this.ZAddress - 4U))));
        this.textBox1.Text = string.Format("{0:x2}", (object) this.Gecko.peek(this.ZAddress - 4U));
      }
      else
      {
        uint input = this.Gecko.peek(this.ZAddress - 4U);
        uint num1 = this.Gecko.peek(this.ZAddress - 4U);
        uint num2 = !this.radioButton1.Checked ? (!this.radioButton2.Checked ? (!this.radioButton3.Checked ? (!this.radioButton4.Checked ? this.ChangeUintPos(input, 0, '4') : Convert.ToUInt32(this.textBox1.Text, 16)) : input + 6291456U) : input + 4194304U) : input + 262144U;
        try
        {
          this.Gecko.poke(this.ZAddress - this.val5, num2);
          this.EventLogBox.Items.Add((object) ("Poked! VAL: " + string.Format("{0:x2}", (object) num2) + ", PRE: " + string.Format("{0:x2}", (object) num1)));
        }
        catch (Exception ex)
        {
          int num3 = (int) MessageBox.Show("Operation failed: \n\n" + (object) ex + "\n\nError reading/writing to memory.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void LeftManButton_Click(object sender, EventArgs e)
    {
      if (this.checkBox1.Checked)
      {
        this.EventLogBox.Items.Add((object) ("Getting val: " + string.Format("{0:x2}", (object) this.Gecko.peek(this.ZAddress - 4U))));
        this.textBox1.Text = string.Format("{0:x2}", (object) this.Gecko.peek(this.ZAddress - 4U));
      }
      else
      {
        uint input = this.Gecko.peek(this.ZAddress - 4U);
        uint num1 = this.Gecko.peek(this.ZAddress - 4U);
        uint num2 = !this.radioButton1.Checked ? (!this.radioButton2.Checked ? (!this.radioButton3.Checked ? (!this.radioButton4.Checked ? this.ChangeUintPos(input, 0, '4') : Convert.ToUInt32(this.textBox1.Text, 16)) : input - 6291456U) : input - 4194304U) : input - 262144U;
        try
        {
          this.Gecko.poke(this.ZAddress - this.val5, num2);
          this.EventLogBox.Items.Add((object) ("Poked! VAL: " + string.Format("{0:x2}", (object) num2) + ", PRE: " + string.Format("{0:x2}", (object) num1)));
        }
        catch (Exception ex)
        {
          int num3 = (int) MessageBox.Show("Operation failed: \n\n" + (object) ex + "\n\nError reading/writing to memory.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void PokeAllMaps(string NewMapName)
    {
      if (this.OnlineCheckBox.Checked && NewMapName != "<no change>")
      {
        this.writeStringSimple(313451924U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313451804U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313451684U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313451564U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313451444U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313451324U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313451204U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313451084U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313450964U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313450844U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313450724U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313450604U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313450484U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313450364U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313450244U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313450124U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313874148U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313834044U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313834692U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313835416U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313835988U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313836636U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313837284U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313837932U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313838580U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313839228U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313839876U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313840524U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
        this.writeStringSimple(313841172U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
      }
      if (!this.DojoCheckBox.Checked || !(NewMapName != "<no change>"))
        return;
      this.writeStringSimple(313865148U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
      this.writeStringSimple(313865796U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
      this.writeStringSimple(313866444U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
      this.writeStringSimple(313867092U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
      this.writeStringSimple(313867740U, NewMapName, "Fld_BossCylinderKing_Bos_Msn".Length);
    }

    public void PokeAllSceneEnvSetNames(string SetName)
    {
      if (!(SetName != "<no change>"))
        return;
      this.writeStringSimple(313841400U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313834272U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313840752U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313840104U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313839456U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313838808U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313838160U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313837512U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313836864U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313836216U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313835568U, SetName, "MisMonitorBroken,Common".Length);
      this.writeStringSimple(313834920U, SetName, "MisMonitorBroken,Common".Length);
    }

    private void PokeButton_Click(object sender, EventArgs e)
    {
      try
      {
        this.PokeAllMaps(((NameWrapper) this.NameCBox.SelectedItem).dataName);
      }
      catch (NullReferenceException ex)
      {
        this.PokeAllMaps(this.NameCBox.Text);
      }
      try
      {
        this.PokeAllSceneEnvSetNames(((NameWrapper) this.seCBox.SelectedItem).dataName);
      }
      catch (NullReferenceException ex)
      {
        this.PokeAllSceneEnvSetNames(this.seCBox.Text);
      }
      int num = (int) MessageBox.Show("Success!", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void HandleFormKeyPress(object sender, KeyPressEventArgs e)
    {
      if (!this.GeckoConnected)
        ;
    }

    public void FreezeZ(object sender, EventArgs e)
    {
      if (this.hasExtendedHandlerInstalled)
      {
        this.Gecko.poke(268521472U, 131072U);
        this.Gecko.poke(268521476U, this.ZAddress);
        this.Gecko.poke(268521480U, this.Gecko.peek(this.ZAddress));
        this.Gecko.poke(268521484U, 0U);
      }
      else
      {
        int num = (int) MessageBox.Show("Extended handler not installed.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void ForwardManButton_Click(object sender, EventArgs e)
    {
      if (this.checkBox1.Checked)
      {
        this.EventLogBox.Items.Add((object) ("Getting val: " + string.Format("{0:x2}", (object) this.Gecko.peek(this.ZAddress + 4U))));
        this.textBox1.Text = string.Format("{0:x2}", (object) this.Gecko.peek(this.ZAddress + 4U));
      }
      else
      {
        uint input = this.Gecko.peek(this.ZAddress + 4U);
        uint num1 = this.Gecko.peek(this.ZAddress + 4U);
        uint num2 = !this.radioButton1.Checked ? (!this.radioButton2.Checked ? (!this.radioButton3.Checked ? (!this.radioButton4.Checked ? this.ChangeUintPos(input, 0, '4') : Convert.ToUInt32(this.textBox1.Text, 16)) : input + 6291456U) : input + 4194304U) : input + 262144U;
        try
        {
          this.Gecko.poke(this.ZAddress + this.val5, num2);
          this.EventLogBox.Items.Add((object) ("Poked! VAL: " + string.Format("{0:x2}", (object) num2) + ", PRE: " + string.Format("{0:x2}", (object) num1)));
        }
        catch (Exception ex)
        {
          int num3 = (int) MessageBox.Show("Operation failed: \n\n" + (object) ex + "\n\nError reading/writing to memory.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void BackwardsManButton_Click(object sender, EventArgs e)
    {
      if (this.checkBox1.Checked)
      {
        this.EventLogBox.Items.Add((object) ("Getting val: " + string.Format("{0:x2}", (object) this.Gecko.peek(this.ZAddress + 4U))));
        this.textBox1.Text = string.Format("{0:x2}", (object) this.Gecko.peek(this.ZAddress + 4U));
      }
      else
      {
        uint input = this.Gecko.peek(this.ZAddress + 4U);
        uint num1 = this.Gecko.peek(this.ZAddress + 4U);
        uint num2 = !this.radioButton1.Checked ? (!this.radioButton2.Checked ? (!this.radioButton3.Checked ? (!this.radioButton4.Checked ? this.ChangeUintPos(input, 0, '4') : Convert.ToUInt32(this.textBox1.Text, 16)) : input - 6291456U) : input - 4194304U) : input - 262144U;
        try
        {
          this.Gecko.poke(this.ZAddress + this.val5, num2);
          this.EventLogBox.Items.Add((object) ("Poked! VAL: " + string.Format("{0:x2}", (object) num2) + ", PRE: " + string.Format("{0:x2}", (object) num1)));
        }
        catch (Exception ex)
        {
          int num3 = (int) MessageBox.Show("Operation failed: \n\n" + (object) ex + "\n\nError reading/writing to memory.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {
      if (this.checkBox2.Checked)
      {
        this.Gecko.poke(268520700U, 1U);
        this.FreezeZButton.Enabled = false;
      }
      else
      {
        this.Gecko.poke(268520700U, 0U);
        this.FreezeZButton.Enabled = true;
      }
    }

    public static void NoSSLTrust()
    {
      try
      {
        ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) delegate
        {
          return true;
        };
      }
      catch (Exception ex)
      {
      }
    }

    private void writeString(string s, uint offset)
    {
      uint num1 = 0;
      int index1 = 0;
      if (offset % 4U > 0U)
      {
        string str = s;
        int num2 = index1;
        index1 = num2 + 1;
        int index2 = num2;
        uint num3 = (uint) str[index2];
        offset -= offset % 4U;
        this.Gecko.poke(offset, num3);
        offset += 4U;
      }
      while (index1 < s.Length)
      {
        if (index1 + 1 == s.Length)
          num1 = (uint) (((int) s[index1] << 16) + ((int) this.Gecko.peek(offset) & (int) byte.MaxValue));
        uint num2 = ((uint) s[index1] << 16) + (uint) s[index1 + 1];
        this.Gecko.poke(offset, num2);
        index1 += 2;
        offset += 4U;
      }
    }

    private void writeStringSimple(uint offset, string s)
    {
      this.writeStringSimple(offset, s, s.Length);
    }

    private void writeStringSimple(uint offset, string s, int length)
    {
      uint num1 = 0;
      int num2 = 0;
      if (offset % 4U > 0U)
      {
        for (int index1 = 0; (long) index1 < (long) (offset % 4U); ++index1)
        {
          int num3 = (int) num1 << 8;
          string str = s;
          int num4 = num2;
          num2 = num4 + 1;
          int index2 = num4;
          int num5 = (int) str[index2];
          num1 = (uint) (num3 | num5);
        }
        if ((int) (offset % 4U) == 1)
          num1 = this.Gecko.peek(offset - offset % 4U) & 4278190080U | num1;
        if ((int) (offset % 4U) == 2)
          num1 = this.Gecko.peek(offset - offset % 4U) & 4294901760U | num1;
        if ((int) (offset % 4U) == 3)
          num1 = this.Gecko.peek(offset - offset % 4U) & 4294967040U | num1;
        this.Gecko.poke(offset, num1);
        offset += offset % 4U;
      }
      while (num2 < s.Length)
      {
        uint num3 = 0;
        if (num2 + 1 == s.Length)
        {
          string str = s;
          int num4 = num2;
          num2 = num4 + 1;
          int index = num4;
          uint num5 = (uint) ((int) str[index] << 24 | (int) this.Gecko.peek(offset) & 16777215);
          this.Gecko.poke(offset, num5);
          ++offset;
          break;
        }
        if (num2 + 2 == s.Length)
        {
          string str1 = s;
          int num4 = num2;
          int num5 = num4 + 1;
          int index1 = num4;
          int num6 = (int) str1[index1] << 8;
          string str2 = s;
          int num7 = num5;
          num2 = num7 + 1;
          int index2 = num7;
          int num8 = (int) str2[index2];
          uint num9 = (uint) ((num6 | num8) << 16 | (int) this.Gecko.peek(offset) & (int) ushort.MaxValue);
          this.Gecko.poke(offset, num9);
          offset += 2U;
          break;
        }
        if (num2 + 3 == s.Length)
        {
          string str1 = s;
          int num4 = num2;
          int num5 = num4 + 1;
          int index1 = num4;
          int num6 = (int) str1[index1] << 8;
          string str2 = s;
          int num7 = num5;
          int num8 = num7 + 1;
          int index2 = num7;
          int num9 = (int) str2[index2];
          int num10 = (num6 | num9) << 8;
          string str3 = s;
          int num11 = num8;
          num2 = num11 + 1;
          int index3 = num11;
          int num12 = (int) str3[index3];
          uint num13 = (uint) ((num10 | num12) << 8 | (int) this.Gecko.peek(offset) & (int) byte.MaxValue);
          this.Gecko.poke(offset, num13);
          break;
        }
        for (int index1 = 0; index1 < 4; ++index1)
        {
          int num4 = (int) num3 << 8;
          string str = s;
          int num5 = num2;
          num2 = num5 + 1;
          int index2 = num5;
          int num6 = (int) str[index2];
          num3 = (uint) (num4 | num6);
        }
        this.Gecko.poke(offset, num3);
        offset += 4U;
      }
      while (num2 < length)
      {
        if (num2 % 4 == 1)
        {
          this.Gecko.poke(offset, this.Gecko.peek(offset) & 4278190080U);
          --num2;
        }
        else if (num2 % 4 == 2)
        {
          this.Gecko.poke(offset, this.Gecko.peek(offset) & 4294901760U);
          num2 = num2 - 1 - 1;
        }
        else if (num2 % 4 == 3)
        {
          this.Gecko.poke(offset, this.Gecko.peek(offset) & 4294967040U);
          num2 = num2 - 1 - 1 - 1;
        }
        else
        {
          int num3;
          if (num2 + 1 == length)
          {
            uint num4 = this.Gecko.peek(offset) & 16777215U;
            this.Gecko.poke(offset, num4);
            ++offset;
            num3 = num2 + 1;
            break;
          }
          if (num2 + 2 == length)
          {
            uint num4 = this.Gecko.peek(offset) & (uint) ushort.MaxValue;
            this.Gecko.poke(offset, num4);
            offset += 2U;
            num3 = num2 + 2;
            break;
          }
          if (num2 + 3 == length)
          {
            uint num4 = this.Gecko.peek(offset) & (uint) byte.MaxValue;
            this.Gecko.poke(offset, num4);
            offset += 3U;
            num3 = num2 + 3;
            break;
          }
          this.Gecko.poke(offset, 0U);
        }
        offset += 4U;
        num2 += 4;
      }
    }

    private void tabPage3_Click(object sender, EventArgs e)
    {
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Gecko.poke(315452832U, 0U);
      this.Gecko.poke(315440544U, 0U);
      this.Gecko.poke(315428256U, 0U);
      this.Gecko.poke(315428232U, 0U);
      this.Gecko.poke(315428224U, 0U);
      this.Gecko.poke(315428228U, 0U);
    }

    private void button3_Click(object sender, EventArgs e)
    {
      this.Gecko.poke(315452832U, 27001U);
      this.Gecko.poke(315440544U, 27001U);
      this.Gecko.poke(315428256U, 27001U);
      this.Gecko.poke(315428232U, 27001U);
      this.Gecko.poke(315428224U, 27001U);
      this.Gecko.poke(315428228U, 27001U);
    }

    private void button4_Click(object sender, EventArgs e)
    {
      this.Gecko.poke(315452832U, 27002U);
      this.Gecko.poke(315440544U, 27002U);
      this.Gecko.poke(315428256U, 27002U);
      this.Gecko.poke(315428232U, 27002U);
      this.Gecko.poke(315428224U, 27002U);
      this.Gecko.poke(315428228U, 27002U);
    }

    private void button5_Click(object sender, EventArgs e)
    {
      this.Gecko.poke(315452832U, 27003U);
      this.Gecko.poke(315440544U, 27003U);
      this.Gecko.poke(315428256U, 27003U);
      this.Gecko.poke(315428232U, 27003U);
      this.Gecko.poke(315428224U, 27003U);
      this.Gecko.poke(315428228U, 27003U);
    }

    private void button6_Click(object sender, EventArgs e)
    {
      this.Gecko.poke(315452832U, 28001U);
      this.Gecko.poke(315428232U, 28001U);
    }

    private void button7_Click(object sender, EventArgs e)
    {
      this.Gecko.poke(315452832U, 29500U);
      this.Gecko.poke(315440544U, 29500U);
      this.Gecko.poke(315428256U, 29500U);
      this.Gecko.poke(315428232U, 29500U);
      this.Gecko.poke(315428224U, 29500U);
      this.Gecko.poke(315428228U, 29500U);
    }

    private void button8_Click(object sender, EventArgs e)
    {
      this.Gecko.poke(315440544U, 29501U);
      this.Gecko.poke(315428224U, 29501U);
    }

    private void button9_Click(object sender, EventArgs e)
    {
      this.Gecko.poke(315452832U, 1U);
      this.Gecko.poke(315440544U, 1U);
      this.Gecko.poke(315428256U, 1U);
      this.Gecko.poke(315428232U, 1U);
      this.Gecko.poke(315428224U, 1U);
      this.Gecko.poke(315428228U, 1U);
    }

    private static byte[] GetBytes(string str)
    {
      byte[] numArray = new byte[str.Length * 2];
      Buffer.BlockCopy((Array) str.ToCharArray(), 0, (Array) numArray, 0, numArray.Length);
      return numArray;
    }

    private void TpToP2Button_Click(object sender, EventArgs e)
    {
      if ((int) this.Gecko.peek(this.ZAddress) == 0)
      {
        int num1 = (int) MessageBox.Show("Gecko.peek(ZAddress) == 0x00000000!\n\nYou may need to recalculate pointers!", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        try
        {
          this.Gecko.poke(this.ZAddress - this.val1, this.Gecko.peek(this.P2ZAddress - this.val1));
          this.Gecko.poke(this.ZAddress - this.val2, this.Gecko.peek(this.P2ZAddress - this.val2));
          this.Gecko.poke(this.ZAddress - this.val3, this.Gecko.peek(this.P2ZAddress - this.val3));
          this.Gecko.poke(this.ZAddress - this.val4, this.Gecko.peek(this.P2ZAddress - this.val4));
          this.Gecko.poke(this.ZAddress - this.val5, this.Gecko.peek(this.P2ZAddress - this.val5));
          this.Gecko.poke(this.ZAddress + this.val6, this.Gecko.peek(this.P2ZAddress + this.val6));
          this.Gecko.poke(this.ZAddress, this.P2ZAddress);
          this.EventLogBox.Items.Add((object) "Poked coords to p2.");
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("Operation failed: \n\n" + (object) ex + "\n\nError writing to memory.", "GeckoTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.IPBox = new TextBox();
      this.groupBox1 = new GroupBox();
      this.checkBox2 = new CheckBox();
      this.ConnectButton = new Button();
      this.DisconnButton = new Button();
      this.groupBox2 = new GroupBox();
      this.EventLogBox = new ListBox();
      this.RecalcPointerButton = new Button();
      this.State3LoadButton = new Button();
      this.State2LoadButton = new Button();
      this.State1LoadButton = new Button();
      this.State3SaveButton = new Button();
      this.State2SaveButton = new Button();
      this.State1SaveButton = new Button();
      this.label2 = new Label();
      this.ManControlBox = new GroupBox();
      this.label4 = new Label();
      this.FreezeZButton = new Button();
      this.BackwardsManButton = new Button();
      this.button1 = new Button();
      this.checkBox1 = new CheckBox();
      this.textBox1 = new TextBox();
      this.radioButton4 = new RadioButton();
      this.radioButton3 = new RadioButton();
      this.radioButton2 = new RadioButton();
      this.radioButton1 = new RadioButton();
      this.ForwardManButton = new Button();
      this.ZUpManButton = new Button();
      this.UpManButton = new Button();
      this.MapLoaderBox = new GroupBox();
      this.label3 = new Label();
      this.label1 = new Label();
      this.OnlineCheckBox = new CheckBox();
      this.DojoCheckBox = new CheckBox();
      this.MapPokeButton = new Button();
      this.seCBox = new ComboBox();
      this.NameCBox = new ComboBox();
      this.tabControl1 = new TabControl();
      this.tabPage1 = new TabPage();
      this.tabPage2 = new TabPage();
      this.tabPage3 = new TabPage();
      this.tabPage4 = new TabPage();
      this.button9 = new Button();
      this.button8 = new Button();
      this.button7 = new Button();
      this.button6 = new Button();
      this.button5 = new Button();
      this.button4 = new Button();
      this.button3 = new Button();
      this.button2 = new Button();
      this.tabPage5 = new TabPage();
      this.TpToP2Button = new Button();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.ManControlBox.SuspendLayout();
      this.MapLoaderBox.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.tabPage5.SuspendLayout();
      this.SuspendLayout();
      this.IPBox.Location = new Point(6, 19);
      this.IPBox.Name = "IPBox";
      this.IPBox.Size = new Size(100, 20);
      this.IPBox.TabIndex = 0;
      this.IPBox.Text = "192.168.178.22";
      this.groupBox1.Controls.Add((Control) this.checkBox2);
      this.groupBox1.Controls.Add((Control) this.ConnectButton);
      this.groupBox1.Controls.Add((Control) this.DisconnButton);
      this.groupBox1.Controls.Add((Control) this.IPBox);
      this.groupBox1.Location = new Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(361, 70);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "TCPGecko Connection";
      this.checkBox2.AutoSize = true;
      this.checkBox2.Enabled = false;
      this.checkBox2.Location = new Point(6, 45);
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.Size = new Size(175, 17);
      this.checkBox2.TabIndex = 13;
      this.checkBox2.Text = "Activate extended Codehandler";
      this.checkBox2.UseVisualStyleBackColor = true;
      this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
      this.ConnectButton.Location = new Point(112, 17);
      this.ConnectButton.Name = "ConnectButton";
      this.ConnectButton.Size = new Size(125, 23);
      this.ConnectButton.TabIndex = 3;
      this.ConnectButton.Text = "Connect";
      this.ConnectButton.UseVisualStyleBackColor = true;
      this.ConnectButton.Click += new EventHandler(this.button1_Click);
      this.DisconnButton.Enabled = false;
      this.DisconnButton.Location = new Point(243, 17);
      this.DisconnButton.Name = "DisconnButton";
      this.DisconnButton.Size = new Size(112, 23);
      this.DisconnButton.TabIndex = 2;
      this.DisconnButton.Text = "Disconnect";
      this.DisconnButton.UseVisualStyleBackColor = true;
      this.DisconnButton.Click += new EventHandler(this.DisconnectGecko);
      this.groupBox2.Controls.Add((Control) this.EventLogBox);
      this.groupBox2.Controls.Add((Control) this.RecalcPointerButton);
      this.groupBox2.Controls.Add((Control) this.State3LoadButton);
      this.groupBox2.Controls.Add((Control) this.State2LoadButton);
      this.groupBox2.Controls.Add((Control) this.State1LoadButton);
      this.groupBox2.Controls.Add((Control) this.State3SaveButton);
      this.groupBox2.Controls.Add((Control) this.State2SaveButton);
      this.groupBox2.Controls.Add((Control) this.State1SaveButton);
      this.groupBox2.Enabled = false;
      this.groupBox2.Location = new Point(6, 6);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(361, 226);
      this.groupBox2.TabIndex = 5;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "GeckoTool";
      this.EventLogBox.FormattingEnabled = true;
      this.EventLogBox.HorizontalExtent = 15;
      this.EventLogBox.HorizontalScrollbar = true;
      this.EventLogBox.Location = new Point(6, 161);
      this.EventLogBox.Name = "EventLogBox";
      this.EventLogBox.Size = new Size(349, 56);
      this.EventLogBox.TabIndex = 10;
      this.RecalcPointerButton.Location = new Point(10, 19);
      this.RecalcPointerButton.Name = "RecalcPointerButton";
      this.RecalcPointerButton.Size = new Size(111, 21);
      this.RecalcPointerButton.TabIndex = 9;
      this.RecalcPointerButton.Text = "Recalc pointer";
      this.RecalcPointerButton.UseVisualStyleBackColor = true;
      this.RecalcPointerButton.Click += new EventHandler(this.RecalcPointerButton_Click);
      this.State3LoadButton.Location = new Point(243, 112);
      this.State3LoadButton.Name = "State3LoadButton";
      this.State3LoadButton.Size = new Size(111, 43);
      this.State3LoadButton.TabIndex = 8;
      this.State3LoadButton.Text = "State3Load(6)";
      this.State3LoadButton.UseVisualStyleBackColor = true;
      this.State3LoadButton.Click += new EventHandler(this.State3LoadButton_Click);
      this.State2LoadButton.Location = new Point(126, 112);
      this.State2LoadButton.Name = "State2LoadButton";
      this.State2LoadButton.Size = new Size(111, 43);
      this.State2LoadButton.TabIndex = 7;
      this.State2LoadButton.Text = "State2Load(4)";
      this.State2LoadButton.UseVisualStyleBackColor = true;
      this.State2LoadButton.Click += new EventHandler(this.State2LoadButton_Click);
      this.State1LoadButton.Location = new Point(10, 112);
      this.State1LoadButton.Name = "State1LoadButton";
      this.State1LoadButton.Size = new Size(111, 43);
      this.State1LoadButton.TabIndex = 6;
      this.State1LoadButton.Text = "State1Load(2)";
      this.State1LoadButton.UseVisualStyleBackColor = true;
      this.State1LoadButton.Click += new EventHandler(this.State1LoadButton_Click);
      this.State3SaveButton.Location = new Point(243, 46);
      this.State3SaveButton.Name = "State3SaveButton";
      this.State3SaveButton.Size = new Size(111, 60);
      this.State3SaveButton.TabIndex = 5;
      this.State3SaveButton.Text = "State3Save(5)";
      this.State3SaveButton.UseVisualStyleBackColor = true;
      this.State3SaveButton.Click += new EventHandler(this.State3SaveButton_Click);
      this.State2SaveButton.Location = new Point(126, 46);
      this.State2SaveButton.Name = "State2SaveButton";
      this.State2SaveButton.Size = new Size(111, 60);
      this.State2SaveButton.TabIndex = 4;
      this.State2SaveButton.Text = "State2Save(3)";
      this.State2SaveButton.UseVisualStyleBackColor = true;
      this.State2SaveButton.Click += new EventHandler(this.State2SaveButton_Click);
      this.State1SaveButton.Location = new Point(9, 46);
      this.State1SaveButton.Name = "State1SaveButton";
      this.State1SaveButton.Size = new Size(111, 60);
      this.State1SaveButton.TabIndex = 3;
      this.State1SaveButton.Text = "State1Save(1)";
      this.State1SaveButton.UseVisualStyleBackColor = true;
      this.State1SaveButton.Click += new EventHandler(this.State1SaveButton_Click);
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.ForeColor = SystemColors.ControlDarkDark;
      this.label2.Location = new Point(-2, 514);
      this.label2.Name = "label2";
      this.label2.Size = new Size(387, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "by MegaMew and amibu";
      this.ManControlBox.Controls.Add((Control) this.label4);
      this.ManControlBox.Controls.Add((Control) this.FreezeZButton);
      this.ManControlBox.Controls.Add((Control) this.BackwardsManButton);
      this.ManControlBox.Controls.Add((Control) this.button1);
      this.ManControlBox.Controls.Add((Control) this.checkBox1);
      this.ManControlBox.Controls.Add((Control) this.textBox1);
      this.ManControlBox.Controls.Add((Control) this.radioButton4);
      this.ManControlBox.Controls.Add((Control) this.radioButton3);
      this.ManControlBox.Controls.Add((Control) this.radioButton2);
      this.ManControlBox.Controls.Add((Control) this.radioButton1);
      this.ManControlBox.Controls.Add((Control) this.ForwardManButton);
      this.ManControlBox.Controls.Add((Control) this.ZUpManButton);
      this.ManControlBox.Controls.Add((Control) this.UpManButton);
      this.ManControlBox.Enabled = false;
      this.ManControlBox.Location = new Point(6, 238);
      this.ManControlBox.Name = "ManControlBox";
      this.ManControlBox.Size = new Size(361, 154);
      this.ManControlBox.TabIndex = 7;
      this.ManControlBox.TabStop = false;
      this.ManControlBox.Text = "ManControls";
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4.ForeColor = SystemColors.ControlDarkDark;
      this.label4.Location = new Point(87, 131);
      this.label4.Name = "label4";
      this.label4.Size = new Size(252, 13);
      this.label4.TabIndex = 13;
      this.label4.Text = "Check \"Activate extended Codehandler\" to activate.";
      this.FreezeZButton.Location = new Point(6, 125);
      this.FreezeZButton.Name = "FreezeZButton";
      this.FreezeZButton.Size = new Size(75, 23);
      this.FreezeZButton.TabIndex = 11;
      this.FreezeZButton.Text = "Freeze Z";
      this.FreezeZButton.UseVisualStyleBackColor = true;
      this.FreezeZButton.Click += new EventHandler(this.FreezeZ);
      this.BackwardsManButton.Location = new Point(39, 73);
      this.BackwardsManButton.Name = "BackwardsManButton";
      this.BackwardsManButton.Size = new Size(30, 31);
      this.BackwardsManButton.TabIndex = 13;
      this.BackwardsManButton.Text = "~";
      this.BackwardsManButton.UseVisualStyleBackColor = true;
      this.BackwardsManButton.Click += new EventHandler(this.BackwardsManButton_Click);
      this.button1.Location = new Point(3, 50);
      this.button1.Name = "button1";
      this.button1.Size = new Size(30, 31);
      this.button1.TabIndex = 12;
      this.button1.Text = "<";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.LeftManButton_Click);
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new Point(7, 108);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(151, 17);
      this.checkBox1.TabIndex = 11;
      this.checkBox1.Text = "fetch value to box on click";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new EventHandler(this.FetchBoxChange);
      this.textBox1.Location = new Point(243, 96);
      this.textBox1.MaxLength = 8;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(88, 20);
      this.textBox1.TabIndex = 9;
      this.radioButton4.AutoSize = true;
      this.radioButton4.Location = new Point(225, 97);
      this.radioButton4.Name = "radioButton4";
      this.radioButton4.Size = new Size(85, 17);
      this.radioButton4.TabIndex = 10;
      this.radioButton4.TabStop = true;
      this.radioButton4.Text = "radioButton4";
      this.radioButton4.UseVisualStyleBackColor = true;
      this.radioButton3.AutoSize = true;
      this.radioButton3.Location = new Point(225, 74);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new Size(58, 17);
      this.radioButton3.TabIndex = 8;
      this.radioButton3.Text = "Scale3";
      this.radioButton3.UseVisualStyleBackColor = true;
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new Point(225, 50);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new Size(58, 17);
      this.radioButton2.TabIndex = 7;
      this.radioButton2.Text = "Scale2";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.Location = new Point(225, 26);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new Size(58, 17);
      this.radioButton1.TabIndex = 6;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "Scale1";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.ForwardManButton.Location = new Point(39, 36);
      this.ForwardManButton.Name = "ForwardManButton";
      this.ForwardManButton.Size = new Size(30, 31);
      this.ForwardManButton.TabIndex = 3;
      this.ForwardManButton.Text = "^";
      this.ForwardManButton.UseVisualStyleBackColor = true;
      this.ForwardManButton.Click += new EventHandler(this.ForwardManButton_Click);
      this.ZUpManButton.Location = new Point(128, 50);
      this.ZUpManButton.Name = "ZUpManButton";
      this.ZUpManButton.Size = new Size(30, 31);
      this.ZUpManButton.TabIndex = 1;
      this.ZUpManButton.Text = "^";
      this.ZUpManButton.UseVisualStyleBackColor = true;
      this.ZUpManButton.Click += new EventHandler(this.ZUpManButtonClick);
      this.UpManButton.Location = new Point(75, 50);
      this.UpManButton.Name = "UpManButton";
      this.UpManButton.Size = new Size(30, 31);
      this.UpManButton.TabIndex = 0;
      this.UpManButton.Text = ">";
      this.UpManButton.UseVisualStyleBackColor = true;
      this.UpManButton.Click += new EventHandler(this.RightManButton_Click);
      this.MapLoaderBox.Controls.Add((Control) this.label3);
      this.MapLoaderBox.Controls.Add((Control) this.label1);
      this.MapLoaderBox.Controls.Add((Control) this.OnlineCheckBox);
      this.MapLoaderBox.Controls.Add((Control) this.DojoCheckBox);
      this.MapLoaderBox.Controls.Add((Control) this.MapPokeButton);
      this.MapLoaderBox.Controls.Add((Control) this.seCBox);
      this.MapLoaderBox.Controls.Add((Control) this.NameCBox);
      this.MapLoaderBox.Enabled = false;
      this.MapLoaderBox.Location = new Point(6, 6);
      this.MapLoaderBox.Name = "MapLoaderBox";
      this.MapLoaderBox.Size = new Size(361, 121);
      this.MapLoaderBox.TabIndex = 8;
      this.MapLoaderBox.TabStop = false;
      this.MapLoaderBox.Text = "MapLoader";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(10, 63);
      this.label3.Name = "label3";
      this.label3.Size = new Size(73, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "SceneEnvSet";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(10, 19);
      this.label1.Name = "label1";
      this.label1.Size = new Size(28, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Map";
      this.OnlineCheckBox.AutoSize = true;
      this.OnlineCheckBox.Checked = true;
      this.OnlineCheckBox.CheckState = CheckState.Checked;
      this.OnlineCheckBox.Location = new Point(280, 46);
      this.OnlineCheckBox.Name = "OnlineCheckBox";
      this.OnlineCheckBox.Size = new Size(56, 17);
      this.OnlineCheckBox.TabIndex = 4;
      this.OnlineCheckBox.Text = "Online";
      this.OnlineCheckBox.UseVisualStyleBackColor = true;
      this.DojoCheckBox.AutoSize = true;
      this.DojoCheckBox.Checked = true;
      this.DojoCheckBox.CheckState = CheckState.Checked;
      this.DojoCheckBox.Location = new Point(280, 69);
      this.DojoCheckBox.Name = "DojoCheckBox";
      this.DojoCheckBox.Size = new Size(48, 17);
      this.DojoCheckBox.TabIndex = 3;
      this.DojoCheckBox.Text = "Dojo";
      this.DojoCheckBox.UseVisualStyleBackColor = true;
      this.MapPokeButton.Location = new Point(280, 92);
      this.MapPokeButton.Name = "MapPokeButton";
      this.MapPokeButton.Size = new Size(75, 23);
      this.MapPokeButton.TabIndex = 2;
      this.MapPokeButton.Text = "Poke";
      this.MapPokeButton.UseVisualStyleBackColor = true;
      this.MapPokeButton.Click += new EventHandler(this.PokeButton_Click);
      this.seCBox.FormattingEnabled = true;
      this.seCBox.Location = new Point(10, 79);
      this.seCBox.Name = "seCBox";
      this.seCBox.Size = new Size(210, 21);
      this.seCBox.TabIndex = 1;
      this.NameCBox.FormattingEnabled = true;
      this.NameCBox.Location = new Point(10, 35);
      this.NameCBox.Name = "NameCBox";
      this.NameCBox.Size = new Size(210, 21);
      this.NameCBox.TabIndex = 0;
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.Controls.Add((Control) this.tabPage3);
      this.tabControl1.Controls.Add((Control) this.tabPage4);
      this.tabControl1.Controls.Add((Control) this.tabPage5);
      this.tabControl1.Enabled = false;
      this.tabControl1.Location = new Point(2, 88);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(383, 423);
      this.tabControl1.TabIndex = 12;
      this.tabPage1.Controls.Add((Control) this.groupBox2);
      this.tabPage1.Controls.Add((Control) this.ManControlBox);
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(375, 397);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Movement";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.tabPage2.Controls.Add((Control) this.MapLoaderBox);
      this.tabPage2.Location = new Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(375, 397);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "MapTester";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.tabPage3.Location = new Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new Size(375, 397);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "MegaMew";
      this.tabPage3.UseVisualStyleBackColor = true;
      this.tabPage3.Click += new EventHandler(this.tabPage3_Click);
      this.tabPage4.Controls.Add((Control) this.button9);
      this.tabPage4.Controls.Add((Control) this.button8);
      this.tabPage4.Controls.Add((Control) this.button7);
      this.tabPage4.Controls.Add((Control) this.button6);
      this.tabPage4.Controls.Add((Control) this.button5);
      this.tabPage4.Controls.Add((Control) this.button4);
      this.tabPage4.Controls.Add((Control) this.button3);
      this.tabPage4.Controls.Add((Control) this.button2);
      this.tabPage4.Location = new Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new Padding(3);
      this.tabPage4.Size = new Size(375, 397);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Gear";
      this.tabPage4.UseVisualStyleBackColor = true;
      this.button9.Location = new Point(249, 190);
      this.button9.Name = "button9";
      this.button9.Size = new Size(75, 23);
      this.button9.TabIndex = 7;
      this.button9.Text = "Reset";
      this.button9.UseVisualStyleBackColor = true;
      this.button9.Click += new EventHandler(this.button9_Click);
      this.button8.Location = new Point(37, 350);
      this.button8.Name = "button8";
      this.button8.Size = new Size(75, 23);
      this.button8.TabIndex = 6;
      this.button8.Text = "SUP001";
      this.button8.UseVisualStyleBackColor = true;
      this.button8.Click += new EventHandler(this.button8_Click);
      this.button7.Location = new Point(37, 296);
      this.button7.Name = "button7";
      this.button7.Size = new Size(75, 23);
      this.button7.TabIndex = 5;
      this.button7.Text = "SUP000";
      this.button7.UseVisualStyleBackColor = true;
      this.button7.Click += new EventHandler(this.button7_Click);
      this.button6.Location = new Point(37, 244);
      this.button6.Name = "button6";
      this.button6.Size = new Size(75, 23);
      this.button6.TabIndex = 4;
      this.button6.Text = "RVL001";
      this.button6.UseVisualStyleBackColor = true;
      this.button6.Click += new EventHandler(this.button6_Click);
      this.button5.Location = new Point(37, 190);
      this.button5.Name = "button5";
      this.button5.Size = new Size(75, 23);
      this.button5.TabIndex = 3;
      this.button5.Text = "MSN003";
      this.button5.UseVisualStyleBackColor = true;
      this.button5.Click += new EventHandler(this.button5_Click);
      this.button4.Location = new Point(37, 137);
      this.button4.Name = "button4";
      this.button4.Size = new Size(75, 23);
      this.button4.TabIndex = 2;
      this.button4.Text = "MSN002";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.button3.Location = new Point(37, 86);
      this.button3.Name = "button3";
      this.button3.Size = new Size(75, 23);
      this.button3.TabIndex = 1;
      this.button3.Text = "MSN001";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button2.Location = new Point(37, 38);
      this.button2.Name = "button2";
      this.button2.Size = new Size(75, 23);
      this.button2.TabIndex = 0;
      this.button2.Text = "NoClothes";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.tabPage5.Controls.Add((Control) this.TpToP2Button);
      this.tabPage5.Location = new Point(4, 22);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new Padding(3);
      this.tabPage5.Size = new Size(375, 397);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "Player Teleportation";
      this.tabPage5.UseVisualStyleBackColor = true;
      this.TpToP2Button.Location = new Point(12, 17);
      this.TpToP2Button.Name = "TpToP2Button";
      this.TpToP2Button.Size = new Size(75, 23);
      this.TpToP2Button.TabIndex = 0;
      this.TpToP2Button.Text = "Player 2";
      this.TpToP2Button.UseVisualStyleBackColor = true;
      this.TpToP2Button.Click += new EventHandler(this.TpToP2Button_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(386, 532);
      this.Controls.Add((Control) this.tabControl1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.groupBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.ShowIcon = false;
      this.Text = "GeckoTool";
      this.KeyPress += new KeyPressEventHandler(this.HandleFormKeyPress);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.ManControlBox.ResumeLayout(false);
      this.ManControlBox.PerformLayout();
      this.MapLoaderBox.ResumeLayout(false);
      this.MapLoaderBox.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.tabPage4.ResumeLayout(false);
      this.tabPage5.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
