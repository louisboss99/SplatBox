// Decompiled with JetBrains decompiler
// Type: SplatTrack.TrackerForm
// Assembly: SplatBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70B17917-7F0A-4EA6-9502-2FC50A359611
// Assembly location: C:\Users\Grant\Desktop\heh\Splatobox.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace SplatTrack
{
  public class TrackerForm : Form
  {
    private IContainer components = (IContainer) null;

    public TrackerForm()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Text = "TrackerForm";
    }
  }
}
