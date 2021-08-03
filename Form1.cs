// Decompiled with JetBrains decompiler
// Type: Z_Bank_Final.Form1
// Assembly: Z Bank Final, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7535C92C-6010-4363-AB0C-1391C8D89DC6
// Assembly location: C:\Users\Cristi\Documents\zbank\Z Bank\Z Bank\Z Bank Final.exe

using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Z_Bank_Final.Properties;

namespace Z_Bank_Final
{
  public class Form1 : Form
  {
    private MySqlConnection connection;
    private MySqlCommand cmd;
    private MySqlDataReader mdr;
    private string account;
    private string moneda;
    private bool okMoney;
    private bool okCont;
    private double eurUSD = 1.1;
    private double eurRON = 4.77;
    private double usdEUR = 0.91;
    private double usdRON = 4.34;
    private double ronEUR = 0.21;
    private double ronUSD = 0.23;
    private IContainer components = (IContainer) null;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fereastraToolStripMenuItem;
    private ToolStripMenuItem minimizeazaToolStripMenuItem;
    private ToolStripMenuItem inchideToolStripMenuItem;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private Label label1;
    private Label label2;
    private Label label3;
    private TextBox textBox1;
    private Label label4;
    private TextBox textBox2;
    private PictureBox pictureBox1;
    private Label label5;
    private TextBox textBox3;
    private Button button1;
    private Label label6;
    private TextBox textBox4;
    private Button button2;
    private Label label7;
    private ToolStripMenuItem despreToolStripMenuItem;
    private ToolStripMenuItem despreToolStripMenuItem1;
    private Label label8;
    private Label label9;
    private Label label10;
    private Label label11;
    private Label label12;
    private Label label13;
    private Label label14;
    private Label label15;
    private Label label16;
    private PictureBox pictureBox2;
    private PictureBox pictureBox3;

    public Form1() => this.InitializeComponent();

    private void Form1_Load(object sender, EventArgs e)
    {
      Login login = new Login();
      int num1 = (int) login.ShowDialog();
      if (login.returnState == 0)
      {
        this.Close();
      }
      else
      {
        this.connection = login.returnConnection;
        this.account = login.returnAccount;
        this.Text = "ZBank | " + this.account;
        this.pictureBox1.Image = (Image) null;
        try
        {
          this.cmd = new MySqlCommand(string.Format("select * from R8WwnfQ8yx.conturi where cont='{0}';", (object) this.account), this.connection);
          this.mdr = this.cmd.ExecuteReader();
          this.mdr.Read();
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date!", "Eroare");
          int num3 = (int) MessageBox.Show(ex.ToString());
          this.Close();
        }
        string[] strArray = this.mdr.GetString("titular").Split('^');
        Convert.ToInt32(this.mdr.GetString("nr_tranzactii"));
        this.label1.Text = "Nume : " + strArray[0];
        this.label2.Text = "Prenume : " + strArray[1];
        this.moneda = this.mdr.GetString("moneda");
        this.label3.Text = "Sold : " + this.mdr.GetString("sold") + " " + this.moneda;
        this.label5.Text = this.moneda + " =>";
        this.label6.Text = "";
        this.mdr.Close();
        try
        {
          this.cmd = new MySqlCommand(string.Format("select * from R8WwnfQ8yx.{0};", (object) this.account), this.connection);
          this.mdr = this.cmd.ExecuteReader();
          int num2 = 0;
          while (this.mdr.Read())
          {
            ++num2;
            string str1 = "[" + this.mdr[3].ToString() + "]";
            string str2 = this.mdr[2].ToString();
            string str3;
            if (str2.Contains("-"))
            {
              str3 = "catre";
              str2 = str2.Remove(0, 1);
            }
            else
              str3 = "de la";
            this.textBox1.AppendText(str1 + " " + str2 + " " + this.moneda + " " + str3 + " " + this.mdr[1]?.ToString());
            this.textBox1.AppendText(Environment.NewLine);
          }
          if (num2 == 0)
          {
            this.textBox1.AppendText("Nu exista nicio tranzactie");
            this.textBox1.AppendText(Environment.NewLine);
          }
          this.mdr.Close();
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date!", "Eroare");
          int num3 = (int) MessageBox.Show(ex.ToString());
          this.mdr.Close();
          this.Close();
        }
      }
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {
      if (this.textBox2.Text == this.account)
      {
        this.okCont = false;
        this.pictureBox1.Image = (Image) Resources.error;
      }
      else
      {
        try
        {
          this.cmd = new MySqlCommand(string.Format("select moneda from R8WwnfQ8yx.conturi where cont='{0}';", (object) this.textBox2.Text), this.connection);
          this.mdr = this.cmd.ExecuteReader();
          if (!this.mdr.Read())
          {
            this.okCont = false;
            this.textBox3.Enabled = false;
            this.button1.Enabled = false;
            this.pictureBox1.Image = (Image) Resources.error;
            this.mdr.Close();
          }
          else
          {
            this.pictureBox1.Image = (Image) Resources.success;
            this.label6.Text = this.mdr.GetString("moneda");
            this.okCont = true;
            this.textBox3.Enabled = true;
            if (this.okMoney)
              this.button1.Enabled = true;
            else
              this.button1.Enabled = false;
            this.mdr.Close();
          }
        }
        catch (Exception ex)
        {
          int num1 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date", "Eroare");
          int num2 = (int) MessageBox.Show(ex.ToString());
          this.mdr.Close();
        }
      }
    }

    private void textBox3_TextChanged(object sender, EventArgs e)
    {
      do
      {
        if (!(this.moneda == this.label6.Text))
        {
          if (!(this.moneda == "EUR") || !(this.label6.Text == "USD") || !(this.label6.Text != ""))
          {
            if (!(this.moneda == "EUR") || !(this.label6.Text == "RON") || !(this.label6.Text != ""))
            {
              if (!(this.moneda == "USD") || !(this.label6.Text == "EUR") || !(this.label6.Text != ""))
              {
                if (!(this.moneda == "USD") || !(this.label6.Text == "RON") || !(this.label6.Text != ""))
                {
                  if (this.moneda == "RON" && this.label6.Text == "EUR" && this.label6.Text != "")
                    goto label_11;
                }
                else
                  goto label_9;
              }
              else
                goto label_7;
            }
            else
              goto label_5;
          }
          else
            goto label_3;
        }
        else
          goto label_1;
      }
      while (!(this.moneda == "RON") || !(this.label6.Text == "USD") || !(this.label6.Text != ""));
      goto label_13;
label_1:
      this.textBox4.Text = this.textBox3.Text;
      goto label_15;
label_3:
      double result1;
      double.TryParse(this.textBox3.Text, out result1);
      this.textBox4.Text = (result1 * this.eurUSD).ToString();
      goto label_15;
label_5:
      double result2;
      double.TryParse(this.textBox3.Text, out result2);
      this.textBox4.Text = (result2 * this.eurRON).ToString();
      goto label_15;
label_7:
      double result3;
      double.TryParse(this.textBox3.Text, out result3);
      this.textBox4.Text = (result3 * this.usdEUR).ToString();
      goto label_15;
label_9:
      double result4;
      double.TryParse(this.textBox3.Text, out result4);
      this.textBox4.Text = (result4 * this.usdRON).ToString();
      goto label_15;
label_11:
      double result5;
      double.TryParse(this.textBox3.Text, out result5);
      this.textBox4.Text = (result5 * this.ronEUR).ToString();
      goto label_15;
label_13:
      double result6;
      double.TryParse(this.textBox3.Text, out result6);
      this.textBox4.Text = (result6 * this.ronUSD).ToString();
label_15:
      try
      {
        this.cmd = new MySqlCommand(string.Format("select sold from R8WwnfQ8yx.conturi where cont='{0}';", (object) this.account), this.connection);
        this.mdr = this.cmd.ExecuteReader();
        this.mdr.Read();
        if (Convert.ToDouble(this.textBox3.Text) > Convert.ToDouble(this.mdr.GetString("sold")))
        {
          this.okMoney = false;
          this.button1.Enabled = false;
          this.mdr.Close();
        }
        else
        {
          this.okMoney = true;
          if (this.okCont)
            this.button1.Enabled = true;
          else
            this.button1.Enabled = false;
          this.mdr.Close();
        }
      }
      catch (FormatException ex)
      {
        this.okMoney = false;
        this.button1.Enabled = false;
        this.textBox4.Text = '0'.ToString();
        this.mdr.Close();
      }
      catch (Exception ex)
      {
        int num1 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date", "Eroare");
        int num2 = (int) MessageBox.Show(ex.ToString());
        this.mdr.Close();
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.textBox1.Text = "";
      try
      {
        this.cmd = new MySqlCommand(string.Format("select * from R8WwnfQ8yx.conturi where cont='{0}';", (object) this.account), this.connection);
        this.mdr = this.cmd.ExecuteReader();
        this.mdr.Read();
      }
      catch (Exception ex)
      {
        int num1 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date!", "Eroare");
        int num2 = (int) MessageBox.Show(ex.ToString());
        this.Close();
      }
      string[] strArray = this.mdr.GetString("titular").Split('^');
      Convert.ToInt32(this.mdr.GetString("nr_tranzactii"));
      this.label1.Text = "Nume : " + strArray[0];
      this.label2.Text = "Prenume : " + strArray[1];
      this.moneda = this.mdr.GetString("moneda");
      this.label3.Text = "Sold : " + this.mdr.GetString("sold") + " " + this.moneda;
      this.label5.Text = this.moneda + " =>";
      this.label6.Text = "";
      this.mdr.Close();
      try
      {
        this.cmd = new MySqlCommand(string.Format("select * from R8WwnfQ8yx.{0};", (object) this.account), this.connection);
        this.mdr = this.cmd.ExecuteReader();
        int num = 0;
        while (this.mdr.Read())
        {
          ++num;
          string str1 = "[" + this.mdr[3].ToString() + "]";
          string str2 = this.mdr[2].ToString();
          string str3;
          if (str2.Contains("-"))
          {
            str3 = "catre";
            str2 = str2.Remove(0, 1);
          }
          else
            str3 = "de la";
          this.textBox1.AppendText(str1 + " " + str2 + " " + this.moneda + " " + str3 + " " + this.mdr[1]?.ToString());
          this.textBox1.AppendText(Environment.NewLine);
        }
        if (num == 0)
        {
          this.textBox1.AppendText("Nu exista nicio tranzactie");
          this.textBox1.AppendText(Environment.NewLine);
        }
        this.mdr.Close();
      }
      catch (Exception ex)
      {
        int num1 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date!", "Eroare");
        int num2 = (int) MessageBox.Show(ex.ToString());
        this.mdr.Close();
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      string str1;
      try
      {
        using (WebClient webClient = new WebClient())
          str1 = webClient.DownloadString("https://ipinfo.io/ip");
      }
      catch (Exception ex)
      {
        int num1 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date!", "Eroare");
        int num2 = (int) MessageBox.Show(ex.ToString());
        return;
      }
      try
      {
        this.cmd = new MySqlCommand(string.Format("select sold from R8WwnfQ8yx.conturi where cont='{0}';", (object) this.account), this.connection);
        this.mdr = this.cmd.ExecuteReader();
        this.mdr.Read();
        double num1 = Convert.ToDouble(this.mdr.GetString("sold")) - Convert.ToDouble(this.textBox3.Text);
        this.mdr.Close();
        this.cmd = new MySqlCommand(string.Format("select sold from R8WwnfQ8yx.conturi where cont='{0}';", (object) this.textBox2.Text), this.connection);
        this.mdr = this.cmd.ExecuteReader();
        this.mdr.Read();
        double num2 = Convert.ToDouble(this.mdr.GetString("sold")) + Convert.ToDouble(this.textBox4.Text);
        this.mdr.Close();
        this.cmd = new MySqlCommand(string.Format("update R8WwnfQ8yx.conturi set sold='{0}' where cont='{1}';", (object) num1, (object) this.account), this.connection);
        this.cmd.ExecuteNonQuery();
        this.cmd = new MySqlCommand(string.Format("update R8WwnfQ8yx.conturi set sold='{0}' where cont='{1}';", (object) num2, (object) this.textBox2.Text), this.connection);
        this.cmd.ExecuteNonQuery();
        this.cmd = new MySqlCommand(string.Format("insert into R8WwnfQ8yx.{0}(sender,suma,data,ip) values('{1}','{2}','{3}','{4}');", (object) this.account, (object) this.textBox2.Text, (object) ("-" + this.textBox3.Text), (object) DateTime.Now, (object) str1), this.connection);
        this.cmd.ExecuteNonQuery();
        this.cmd = new MySqlCommand(string.Format("insert into R8WwnfQ8yx.{0}(sender,suma,data,ip) values('{1}','{2}','{3}','{4}');", (object) this.textBox2.Text, (object) this.account, (object) this.textBox4.Text, (object) DateTime.Now, (object) str1), this.connection);
        this.cmd.ExecuteNonQuery();
        int num3 = (int) MessageBox.Show("Tranzactia s-a efectuat cu succes!");
        this.textBox1.Text = "";
        try
        {
          this.cmd = new MySqlCommand(string.Format("select * from R8WwnfQ8yx.conturi where cont='{0}';", (object) this.account), this.connection);
          this.mdr = this.cmd.ExecuteReader();
          this.mdr.Read();
        }
        catch (Exception ex)
        {
          int num4 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date!", "Eroare");
          int num5 = (int) MessageBox.Show(ex.ToString());
          this.Close();
        }
        string[] strArray = this.mdr.GetString("titular").Split('^');
        Convert.ToInt32(this.mdr.GetString("nr_tranzactii"));
        this.label1.Text = "Nume : " + strArray[0];
        this.label2.Text = "Prenume : " + strArray[1];
        this.moneda = this.mdr.GetString("moneda");
        this.label3.Text = "Sold : " + this.mdr.GetString("sold") + " " + this.moneda;
        this.label5.Text = this.moneda + " =>";
        this.label6.Text = "";
        this.mdr.Close();
        try
        {
          this.cmd = new MySqlCommand(string.Format("select * from R8WwnfQ8yx.{0};", (object) this.account), this.connection);
          this.mdr = this.cmd.ExecuteReader();
          int num4 = 0;
          int[] numArray = new int[1000];
          while (this.mdr.Read())
          {
            ++num4;
            string str2 = "[" + this.mdr[3].ToString() + "]";
            string str3 = this.mdr[2].ToString();
            string str4;
            if (str3.Contains("-"))
            {
              str4 = "catre";
              str3 = str3.Remove(0, 1);
            }
            else
              str4 = "de la";
            this.textBox1.AppendText(str2 + " " + str3 + " " + this.moneda + " " + str4 + " " + this.mdr[1]?.ToString());
            this.textBox1.AppendText(Environment.NewLine);
          }
          if (num4 == 0)
          {
            this.textBox1.AppendText("Nu exista nicio tranzactie");
            this.textBox1.AppendText(Environment.NewLine);
          }
          this.mdr.Close();
        }
        catch (Exception ex)
        {
          int num4 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date!", "Eroare");
          int num5 = (int) MessageBox.Show(ex.ToString());
          this.mdr.Close();
        }
      }
      catch (Exception ex)
      {
        int num1 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date!", "Eroare");
        int num2 = (int) MessageBox.Show(ex.ToString());
        this.mdr.Close();
      }
    }

    private void minimizeazaToolStripMenuItem_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

    private void inchideToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

    private void despreToolStripMenuItem1_Click(object sender, EventArgs e) => new askCNP().Show();

    private void pictureBox2_Click(object sender, EventArgs e) => new Process()
    {
      StartInfo = new ProcessStartInfo()
      {
        FileName = "cmd.exe",
        WindowStyle = ProcessWindowStyle.Hidden,
        Arguments = "/c start www.instagram.com/zbank2019"
      }
    }.Start();

    private void pictureBox3_Click(object sender, EventArgs e) => new Process()
    {
      StartInfo = new ProcessStartInfo()
      {
        FileName = "cmd.exe",
        WindowStyle = ProcessWindowStyle.Hidden,
        Arguments = "/c start www.facebook.com/ZBank-112163690254566/"
      }
    }.Start();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.menuStrip1 = new MenuStrip();
      this.fereastraToolStripMenuItem = new ToolStripMenuItem();
      this.minimizeazaToolStripMenuItem = new ToolStripMenuItem();
      this.inchideToolStripMenuItem = new ToolStripMenuItem();
      this.despreToolStripMenuItem = new ToolStripMenuItem();
      this.despreToolStripMenuItem1 = new ToolStripMenuItem();
      this.tabControl1 = new TabControl();
      this.tabPage1 = new TabPage();
      this.textBox1 = new TextBox();
      this.tabPage2 = new TabPage();
      this.label6 = new Label();
      this.textBox4 = new TextBox();
      this.label5 = new Label();
      this.textBox3 = new TextBox();
      this.button1 = new Button();
      this.label4 = new Label();
      this.textBox2 = new TextBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label7 = new Label();
      this.label8 = new Label();
      this.label9 = new Label();
      this.label10 = new Label();
      this.label11 = new Label();
      this.label12 = new Label();
      this.label13 = new Label();
      this.label14 = new Label();
      this.label15 = new Label();
      this.label16 = new Label();
      this.pictureBox3 = new PictureBox();
      this.pictureBox2 = new PictureBox();
      this.button2 = new Button();
      this.pictureBox1 = new PictureBox();
      this.menuStrip1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((ISupportInitialize) this.pictureBox3).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.menuStrip1.ImageScalingSize = new Size(20, 20);
      this.menuStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.fereastraToolStripMenuItem,
        (ToolStripItem) this.despreToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(800, 30);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      this.fereastraToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.minimizeazaToolStripMenuItem,
        (ToolStripItem) this.inchideToolStripMenuItem
      });
      this.fereastraToolStripMenuItem.Name = "fereastraToolStripMenuItem";
      this.fereastraToolStripMenuItem.Size = new Size(83, 26);
      this.fereastraToolStripMenuItem.Text = "Fereastra";
      this.minimizeazaToolStripMenuItem.Name = "minimizeazaToolStripMenuItem";
      this.minimizeazaToolStripMenuItem.Size = new Size(176, 26);
      this.minimizeazaToolStripMenuItem.Text = "Minimizeaza";
      this.minimizeazaToolStripMenuItem.Click += new EventHandler(this.minimizeazaToolStripMenuItem_Click);
      this.inchideToolStripMenuItem.Name = "inchideToolStripMenuItem";
      this.inchideToolStripMenuItem.Size = new Size(176, 26);
      this.inchideToolStripMenuItem.Text = "Inchide";
      this.inchideToolStripMenuItem.Click += new EventHandler(this.inchideToolStripMenuItem_Click);
      this.despreToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.despreToolStripMenuItem1
      });
      this.despreToolStripMenuItem.Name = "despreToolStripMenuItem";
      this.despreToolStripMenuItem.Size = new Size(64, 26);
      this.despreToolStripMenuItem.Text = "ZBank";
      this.despreToolStripMenuItem1.Name = "despreToolStripMenuItem1";
      this.despreToolStripMenuItem1.Size = new Size(139, 26);
      this.despreToolStripMenuItem1.Text = "Despre";
      this.despreToolStripMenuItem1.Click += new EventHandler(this.despreToolStripMenuItem1_Click);
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.Location = new Point(12, 89);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(435, 349);
      this.tabControl1.TabIndex = 1;
      this.tabPage1.Controls.Add((Control) this.textBox1);
      this.tabPage1.Location = new Point(4, 25);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(427, 320);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Tranzactii";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.textBox1.Font = new Font("Microsoft Sans Serif", 10f);
      this.textBox1.Location = new Point(0, 0);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.ScrollBars = ScrollBars.Both;
      this.textBox1.Size = new Size(427, 320);
      this.textBox1.TabIndex = 0;
      this.tabPage2.Controls.Add((Control) this.label6);
      this.tabPage2.Controls.Add((Control) this.textBox4);
      this.tabPage2.Controls.Add((Control) this.label5);
      this.tabPage2.Controls.Add((Control) this.textBox3);
      this.tabPage2.Controls.Add((Control) this.button1);
      this.tabPage2.Controls.Add((Control) this.label4);
      this.tabPage2.Controls.Add((Control) this.textBox2);
      this.tabPage2.Controls.Add((Control) this.pictureBox1);
      this.tabPage2.Location = new Point(4, 25);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(427, 320);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Trimite bani";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.label6.AutoSize = true;
      this.label6.Font = new Font("Microsoft Sans Serif", 10f);
      this.label6.Location = new Point(305, 68);
      this.label6.Name = "label6";
      this.label6.Size = new Size(45, 20);
      this.label6.TabIndex = 23;
      this.label6.Text = "USD";
      this.textBox4.Enabled = false;
      this.textBox4.Font = new Font("Microsoft Sans Serif", 10f);
      this.textBox4.Location = new Point(212, 64);
      this.textBox4.MaxLength = 90;
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new Size(93, 26);
      this.textBox4.TabIndex = 22;
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 10f);
      this.label5.Location = new Point(148, 68);
      this.label5.Name = "label5";
      this.label5.Size = new Size(65, 20);
      this.label5.TabIndex = 21;
      this.label5.Text = "USD=>";
      this.textBox3.Enabled = false;
      this.textBox3.Font = new Font("Microsoft Sans Serif", 10f);
      this.textBox3.Location = new Point(55, 64);
      this.textBox3.MaxLength = 90;
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new Size(93, 26);
      this.textBox3.TabIndex = 20;
      this.textBox3.TextChanged += new EventHandler(this.textBox3_TextChanged);
      this.button1.Enabled = false;
      this.button1.Location = new Point(6, 271);
      this.button1.Name = "button1";
      this.button1.Size = new Size(413, 43);
      this.button1.TabIndex = 19;
      this.button1.Text = "Trimite suma";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 10f);
      this.label4.Location = new Point(51, 9);
      this.label4.Name = "label4";
      this.label4.Size = new Size(97, 20);
      this.label4.TabIndex = 1;
      this.label4.Text = "Destinatar :";
      this.textBox2.Font = new Font("Microsoft Sans Serif", 10f);
      this.textBox2.Location = new Point(55, 32);
      this.textBox2.MaxLength = 90;
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new Size(267, 26);
      this.textBox2.TabIndex = 0;
      this.textBox2.TextChanged += new EventHandler(this.textBox2_TextChanged);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 10f);
      this.label1.Location = new Point(12, 27);
      this.label1.Name = "label1";
      this.label1.Size = new Size(53, 20);
      this.label1.TabIndex = 2;
      this.label1.Text = "label1";
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 10f);
      this.label2.Location = new Point(12, 48);
      this.label2.Name = "label2";
      this.label2.Size = new Size(53, 20);
      this.label2.TabIndex = 3;
      this.label2.Text = "label2";
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 10f);
      this.label3.Location = new Point(12, 66);
      this.label3.Name = "label3";
      this.label3.Size = new Size(53, 20);
      this.label3.TabIndex = 4;
      this.label3.Text = "label3";
      this.label7.AutoSize = true;
      this.label7.Font = new Font("Microsoft Sans Serif", 14f);
      this.label7.Location = new Point(465, 110);
      this.label7.Name = "label7";
      this.label7.Size = new Size(312, 29);
      this.label7.TabIndex = 6;
      this.label7.Text = "ZBank | Banca generatiei Z |";
      this.label8.AutoSize = true;
      this.label8.Font = new Font("Microsoft Sans Serif", 10f);
      this.label8.Location = new Point(475, 148);
      this.label8.Name = "label8";
      this.label8.Size = new Size(298, 20);
      this.label8.TabIndex = 7;
      this.label8.Text = "Durlanescu Stefan - General manager ";
      this.label9.AutoSize = true;
      this.label9.Font = new Font("Microsoft Sans Serif", 10f);
      this.label9.Location = new Point(552, 168);
      this.label9.Name = "label9";
      this.label9.Size = new Size(133, 20);
      this.label9.TabIndex = 8;
      this.label9.Text = "+40 772 154 735";
      this.label10.AutoSize = true;
      this.label10.Font = new Font("Microsoft Sans Serif", 10f);
      this.label10.Location = new Point(476, 188);
      this.label10.Name = "label10";
      this.label10.Size = new Size(227, 20);
      this.label10.TabIndex = 9;
      this.label10.Text = "Gheorghe Cristi - Manager IT";
      this.label11.AutoSize = true;
      this.label11.Font = new Font("Microsoft Sans Serif", 10f);
      this.label11.Location = new Point(552, 208);
      this.label11.Name = "label11";
      this.label11.Size = new Size(133, 20);
      this.label11.TabIndex = 10;
      this.label11.Text = "+40 784 095 411";
      this.label12.AutoSize = true;
      this.label12.Font = new Font("Microsoft Sans Serif", 10f);
      this.label12.Location = new Point(476, 228);
      this.label12.Name = "label12";
      this.label12.Size = new Size(290, 20);
      this.label12.TabIndex = 11;
      this.label12.Text = "Enescu Maria - Relationship manager";
      this.label13.AutoSize = true;
      this.label13.Font = new Font("Microsoft Sans Serif", 10f);
      this.label13.Location = new Point(552, 248);
      this.label13.Name = "label13";
      this.label13.Size = new Size(133, 20);
      this.label13.TabIndex = 12;
      this.label13.Text = "+40 773 332 352";
      this.label14.AutoSize = true;
      this.label14.Font = new Font("Microsoft Sans Serif", 10f);
      this.label14.Location = new Point(476, 268);
      this.label14.Name = "label14";
      this.label14.Size = new Size(274, 20);
      this.label14.TabIndex = 13;
      this.label14.Text = "Enache Alexia - Marketing manager";
      this.label15.AutoSize = true;
      this.label15.Font = new Font("Microsoft Sans Serif", 10f);
      this.label15.Location = new Point(552, 288);
      this.label15.Name = "label15";
      this.label15.Size = new Size(133, 20);
      this.label15.TabIndex = 14;
      this.label15.Text = "+40 758 066 093";
      this.label16.AutoSize = true;
      this.label16.Font = new Font("Microsoft Sans Serif", 10f);
      this.label16.Location = new Point(544, 325);
      this.label16.Name = "label16";
      this.label16.Size = new Size(149, 20);
      this.label16.TabIndex = 15;
      this.label16.Text = "Ne poti gasi si pe :";
      this.pictureBox3.Cursor = Cursors.Hand;
      this.pictureBox3.Image = (Image) Resources.facebook;
      this.pictureBox3.Location = new Point(627, 348);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new Size(72, 72);
      this.pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox3.TabIndex = 17;
      this.pictureBox3.TabStop = false;
      this.pictureBox3.Click += new EventHandler(this.pictureBox3_Click);
      this.pictureBox2.Cursor = Cursors.Hand;
      this.pictureBox2.Image = (Image) Resources.instagram;
      this.pictureBox2.Location = new Point(534, 348);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(72, 72);
      this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox2.TabIndex = 16;
      this.pictureBox2.TabStop = false;
      this.pictureBox2.Click += new EventHandler(this.pictureBox2_Click);
      this.button2.BackgroundImage = (Image) Resources.iconfinder_refresh_326679;
      this.button2.BackgroundImageLayout = ImageLayout.Stretch;
      this.button2.Location = new Point(718, 31);
      this.button2.Name = "button2";
      this.button2.Size = new Size(70, 67);
      this.button2.TabIndex = 5;
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.pictureBox1.Image = (Image) Resources.error;
      this.pictureBox1.Location = new Point(328, 26);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(42, 39);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 18;
      this.pictureBox1.TabStop = false;
      this.AutoScaleDimensions = new SizeF(8f, 16f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(800, 450);
      this.Controls.Add((Control) this.pictureBox3);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.label16);
      this.Controls.Add((Control) this.label15);
      this.Controls.Add((Control) this.label14);
      this.Controls.Add((Control) this.label13);
      this.Controls.Add((Control) this.label12);
      this.Controls.Add((Control) this.label11);
      this.Controls.Add((Control) this.label10);
      this.Controls.Add((Control) this.label9);
      this.Controls.Add((Control) this.label8);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.tabControl1);
      this.Controls.Add((Control) this.menuStrip1);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MainMenuStrip = this.menuStrip1;
      this.MaximizeBox = false;
      this.Name = nameof (Form1);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (Form1);
      this.Load += new EventHandler(this.Form1_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      ((ISupportInitialize) this.pictureBox3).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
