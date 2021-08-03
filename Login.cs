// Decompiled with JetBrains decompiler
// Type: Z_Bank_Final.Login
// Assembly: Z Bank Final, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7535C92C-6010-4363-AB0C-1391C8D89DC6
// Assembly location: C:\Users\Cristi\Documents\zbank\Z Bank\Z Bank\Z Bank Final.exe

using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using Z_Bank_Final.Properties;

namespace Z_Bank_Final
{
  public class Login : Form
  {
    private MySqlConnection connection = new MySqlConnection("datasource=remotemysql.com;port=3306;username=R8WwnfQ8yx;password=LVkCqsWJC5");
    private MySqlCommand cmd;
    private MySqlDataReader mdr;
    private string externalAddress;
    private int code;
    private Random random = new Random();
    private IContainer components = (IContainer) null;
    private PictureBox pictureBox1;
    private TextBox textBox1;
    private Button button1;
    private TextBox textBox2;
    private Button button2;
    private Label label1;
    private LinkLabel linkLabel1;
    private Label label2;
    private Label label3;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fereastraToolStripMenuItem;
    private ToolStripMenuItem minimizeazaToolStripMenuItem;
    private ToolStripMenuItem inchideToolStripMenuItem;
    private ToolStripMenuItem fAQToolStripMenuItem;
    private ToolStripMenuItem nuAiPrimitMailToolStripMenuItem;
    private ToolStripMenuItem verificaSectiuneaSPAMToolStripMenuItem;

    public Login() => this.InitializeComponent();

    public MySqlConnection returnConnection { get; set; }

    public int returnState { get; set; }

    public string returnAccount { get; set; }

    private void Login_Load(object sender, EventArgs e)
    {
      this.returnState = 0;
      bool flag = false;
      try
      {
        using (WebClient webClient = new WebClient())
          this.externalAddress = webClient.DownloadString("https://ipinfo.io/ip");
      }
      catch (Exception ex)
      {
        int num1 = (int) MessageBox.Show("Verifica conexiunea la internet inainte de a utiliza Z Bank - Internet Banking", "Eroare");
        int num2 = (int) MessageBox.Show(ex.ToString());
        flag = true;
      }
      if (flag)
      {
        this.Close();
      }
      else
      {
        try
        {
          this.connection.Open();
          this.label1.Text = "Adresa ta IP : " + this.externalAddress;
          this.returnConnection = this.connection;
        }
        catch (Exception ex)
        {
          int num1 = (int) MessageBox.Show("A aparut o eroare in comunicarea cu baza de date!", "Eroare");
          int num2 = (int) MessageBox.Show(ex.ToString());
          this.Close();
        }
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      try
      {
        this.cmd = new MySqlCommand(string.Format("select email from R8WwnfQ8yx.conturi where cont='{0}';", (object) this.textBox1.Text), this.connection);
        this.mdr = this.cmd.ExecuteReader();
        if (!this.mdr.Read())
        {
          int num = (int) MessageBox.Show("Contul specificat nu exista!", "Eroare");
          this.mdr.Close();
        }
        else
        {
          string addresses = this.mdr.GetString("email");
          MailMessage message = new MailMessage();
          SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
          message.From = new MailAddress("zibank2019@gmail.com");
          message.To.Add(addresses);
          message.Subject = "Z Bank PIN code";
          this.code = this.random.Next(100000, 999999);
          message.Body = "Codul Z Bank pentru logarea in cont : " + this.code.ToString();
          smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential("zibank2019", "zbankbusu");
          smtpClient.EnableSsl = true;
          smtpClient.Port = 587;
          smtpClient.Send(message);
          int num = (int) MessageBox.Show("Un mail cu PIN-ul de logare a fost trimis la adresa asociata contului bancar!", "Info");
          this.textBox1.Enabled = false;
          this.mdr.Close();
        }
      }
      catch (Exception ex)
      {
        int num1 = (int) MessageBox.Show("A aparut o eroare in comunicarea cu baza de date!", "Eroare");
        int num2 = (int) MessageBox.Show(ex.ToString());
        this.mdr.Close();
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (this.textBox2.Text == this.code.ToString())
      {
        this.returnState = 1;
        this.returnAccount = this.textBox1.Text;
        this.Close();
      }
      else
      {
        int num = (int) MessageBox.Show("Codul PIN introdus este gresit!", "Eroare");
        this.textBox2.Text = "";
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => new Register(this.connection, this.externalAddress).Show();

    private void minimizeazaToolStripMenuItem_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

    private void inchideToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Login));
      this.textBox1 = new TextBox();
      this.button1 = new Button();
      this.textBox2 = new TextBox();
      this.button2 = new Button();
      this.label1 = new Label();
      this.linkLabel1 = new LinkLabel();
      this.label2 = new Label();
      this.label3 = new Label();
      this.pictureBox1 = new PictureBox();
      this.menuStrip1 = new MenuStrip();
      this.fereastraToolStripMenuItem = new ToolStripMenuItem();
      this.minimizeazaToolStripMenuItem = new ToolStripMenuItem();
      this.inchideToolStripMenuItem = new ToolStripMenuItem();
      this.fAQToolStripMenuItem = new ToolStripMenuItem();
      this.nuAiPrimitMailToolStripMenuItem = new ToolStripMenuItem();
      this.verificaSectiuneaSPAMToolStripMenuItem = new ToolStripMenuItem();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.textBox1.BackColor = SystemColors.Control;
      this.textBox1.Font = new Font("Microsoft Sans Serif", 14f);
      this.textBox1.Location = new Point(12, 211);
      this.textBox1.MaxLength = 90;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(293, 34);
      this.textBox1.TabIndex = 1;
      this.button1.Location = new Point(311, 211);
      this.button1.Name = "button1";
      this.button1.Size = new Size(114, 34);
      this.button1.TabIndex = 2;
      this.button1.Text = "Trimite PIN";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.textBox2.BackColor = SystemColors.Control;
      this.textBox2.Font = new Font("Microsoft Sans Serif", 14f);
      this.textBox2.Location = new Point(12, 271);
      this.textBox2.MaxLength = 90;
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new Size(413, 34);
      this.textBox2.TabIndex = 3;
      this.button2.Font = new Font("Microsoft Sans Serif", 12f);
      this.button2.Location = new Point(12, 311);
      this.button2.Name = "button2";
      this.button2.Size = new Size(413, 52);
      this.button2.TabIndex = 4;
      this.button2.Text = "Intra in cont";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 9f);
      this.label1.Location = new Point(107, 370);
      this.label1.Name = "label1";
      this.label1.Size = new Size(46, 18);
      this.label1.TabIndex = 5;
      this.label1.Text = "label1";
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Font = new Font("Microsoft Sans Serif", 13f);
      this.linkLabel1.Location = new Point(85, 407);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(266, 26);
      this.linkLabel1.TabIndex = 6;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Deschide-ti cont la ZBank!";
      this.linkLabel1.VisitedLinkColor = Color.Blue;
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(9, 251);
      this.label2.Name = "label2";
      this.label2.Size = new Size(63, 17);
      this.label2.TabIndex = 7;
      this.label2.Text = "Cod PIN:";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(9, 191);
      this.label3.Name = "label3";
      this.label3.Size = new Size(85, 17);
      this.label3.TabIndex = 8;
      this.label3.Text = "Cod bancar:";
      this.pictureBox1.Image = (Image) Resources._77088851_2462990970416275_3782010491321188352_n1;
      this.pictureBox1.Location = new Point(12, 31);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(413, 153);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.menuStrip1.ImageScalingSize = new Size(20, 20);
      this.menuStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.fereastraToolStripMenuItem,
        (ToolStripItem) this.fAQToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(437, 28);
      this.menuStrip1.TabIndex = 9;
      this.menuStrip1.Text = "menuStrip1";
      this.fereastraToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.minimizeazaToolStripMenuItem,
        (ToolStripItem) this.inchideToolStripMenuItem
      });
      this.fereastraToolStripMenuItem.Name = "fereastraToolStripMenuItem";
      this.fereastraToolStripMenuItem.Size = new Size(83, 24);
      this.fereastraToolStripMenuItem.Text = "Fereastra";
      this.minimizeazaToolStripMenuItem.Name = "minimizeazaToolStripMenuItem";
      this.minimizeazaToolStripMenuItem.Size = new Size(176, 26);
      this.minimizeazaToolStripMenuItem.Text = "Minimizeaza";
      this.minimizeazaToolStripMenuItem.Click += new EventHandler(this.minimizeazaToolStripMenuItem_Click);
      this.inchideToolStripMenuItem.Name = "inchideToolStripMenuItem";
      this.inchideToolStripMenuItem.Size = new Size(176, 26);
      this.inchideToolStripMenuItem.Text = "Inchide";
      this.inchideToolStripMenuItem.Click += new EventHandler(this.inchideToolStripMenuItem_Click);
      this.fAQToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.nuAiPrimitMailToolStripMenuItem
      });
      this.fAQToolStripMenuItem.Name = "fAQToolStripMenuItem";
      this.fAQToolStripMenuItem.Size = new Size(50, 24);
      this.fAQToolStripMenuItem.Text = "FAQ";
      this.nuAiPrimitMailToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.verificaSectiuneaSPAMToolStripMenuItem
      });
      this.nuAiPrimitMailToolStripMenuItem.Name = "nuAiPrimitMailToolStripMenuItem";
      this.nuAiPrimitMailToolStripMenuItem.Size = new Size(211, 26);
      this.nuAiPrimitMailToolStripMenuItem.Text = "Nu ai primit mail?";
      this.verificaSectiuneaSPAMToolStripMenuItem.Name = "verificaSectiuneaSPAMToolStripMenuItem";
      this.verificaSectiuneaSPAMToolStripMenuItem.Size = new Size(252, 26);
      this.verificaSectiuneaSPAMToolStripMenuItem.Text = "Verifica sectiunea SPAM.";
      this.AutoScaleDimensions = new SizeF(8f, 16f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(437, 445);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.linkLabel1);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.menuStrip1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.Name = nameof (Login);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "ZBank | Login";
      this.Load += new EventHandler(this.Login_Load);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
