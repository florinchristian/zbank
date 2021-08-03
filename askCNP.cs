// Decompiled with JetBrains decompiler
// Type: Z_Bank_Final.askCNP
// Assembly: Z Bank Final, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7535C92C-6010-4363-AB0C-1391C8D89DC6
// Assembly location: C:\Users\Cristi\Documents\zbank\Z Bank\Z Bank\Z Bank Final.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Z_Bank_Final.Properties;

namespace Z_Bank_Final
{
  public class askCNP : Form
  {
    private IContainer components = (IContainer) null;
    private Label label1;
    private Label label2;
    private Label label3;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;

    public askCNP() => this.InitializeComponent();

    private void askCNP_Load(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (askCNP));
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.pictureBox2 = new PictureBox();
      this.pictureBox1 = new PictureBox();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 14f);
      this.label1.Location = new Point(231, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(312, 29);
      this.label1.TabIndex = 0;
      this.label1.Text = "ZBank | Banca generatiei Z |";
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 11f);
      this.label2.Location = new Point(31, 50);
      this.label2.Name = "label2";
      this.label2.Size = new Size(709, 24);
      this.label2.TabIndex = 1;
      this.label2.Text = "Platforma creata si dezvoltata de Gheorghe Cristi, manager IT in cadrul firmei ZBank.";
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 10f);
      this.label3.Location = new Point(304, 88);
      this.label3.Name = "label3";
      this.label3.Size = new Size(140, 20);
      this.label3.TabIndex = 2;
      this.label3.Text = "Brought to you by";
      this.pictureBox2.Image = (Image) Resources.logo_mysql_170x115;
      this.pictureBox2.Location = new Point(343, 111);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(196, 120);
      this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox2.TabIndex = 4;
      this.pictureBox2.TabStop = false;
      this.pictureBox1.Image = (Image) Resources.C_Sharp_logo;
      this.pictureBox1.Location = new Point(196, 111);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(141, 120);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 3;
      this.pictureBox1.TabStop = false;
      this.AutoScaleDimensions = new SizeF(8f, 16f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(781, 258);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.Name = nameof (askCNP);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "ZBank | Despre";
      this.Load += new EventHandler(this.askCNP_Load);
      ((ISupportInitialize) this.pictureBox2).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
