// Decompiled with JetBrains decompiler
// Type: Z_Bank_Final.Register
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
  public class Register : Form
  {
    private MySqlConnection connection;
    private MySqlCommand cmd;
    private MySqlDataReader mdr;
    private string account;
    private int code;
    private string externalAddress;
    private bool okCNP;
    private bool okMail;
    private int count;
    private Random random = new Random();
    private IContainer components = (IContainer) null;
    private TextBox textBox1;
    private Button button1;
    private Label label1;
    private TextBox textBox2;
    private Label label2;
    private TextBox textBox3;
    private Label label3;
    private ComboBox comboBox1;
    private Label label4;
    private TextBox textBox4;
    private Label label5;
    private TextBox textBox5;
    private Button button2;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private PictureBox pictureBox3;
    private TextBox textBox6;
    private Label label6;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fereastraToolStripMenuItem;
    private ToolStripMenuItem inchideToolStripMenuItem;
    private ToolStripMenuItem inchideToolStripMenuItem1;
    private ToolStripMenuItem fAQToolStripMenuItem;
    private ToolStripMenuItem cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem;
    private ToolStripMenuItem emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem;
    private ToolStripMenuItem emailulIntrodusNuTrebuieSaFieAsociatAltuiContToolStripMenuItem;
    private ToolStripMenuItem cNPulTrebuieSaAiba13CifreToolStripMenuItem;
    private ToolStripMenuItem cNPulIntrodusTrebuieSaFieUnicneasociatAltuiContBancarToolStripMenuItem;
    private ToolStripMenuItem nuAmPrimitNiciunEmailToolStripMenuItem;
    private ToolStripMenuItem verificaSectiuneaSPAMToolStripMenuItem;

    private string generateAccount() => "RO40IBAN" + new Random().Next(10000000, 99999999).ToString();

    public Register(MySqlConnection conn, string ip)
    {
      this.connection = conn;
      this.externalAddress = ip;
      this.InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      try
      {
        while (true)
        {
          this.account = this.generateAccount();
          this.cmd = new MySqlCommand(string.Format("select * from R8WwnfQ8yx.conturi where cont='{0}';", (object) this.account), this.connection);
          this.mdr = this.cmd.ExecuteReader();
          if (this.mdr.Read())
            this.mdr.Close();
          else
            break;
        }
        this.mdr.Close();
        this.textBox1.Text = this.account;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("A intervenit o eroare in timpul comunicarii cu baza de date", "Eroare");
        this.mdr.Close();
      }
    }

    private void Register_Load(object sender, EventArgs e)
    {
      this.comboBox1.SelectedIndex = 0;
      this.okCNP = false;
      this.okMail = false;
      this.pictureBox3.Image = (Image) null;
      try
      {
        while (true)
        {
          this.account = this.generateAccount();
          this.cmd = new MySqlCommand(string.Format("select * from R8WwnfQ8yx.conturi where cont='{0}';", (object) this.account), this.connection);
          this.mdr = this.cmd.ExecuteReader();
          if (this.mdr.Read())
            this.mdr.Close();
          else
            break;
        }
        this.mdr.Close();
        this.textBox1.Text = this.account;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("A intervenit o eroare in timpul comunicarii cu baza de date", "Eroare");
        this.mdr.Close();
        this.Close();
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      try
      {
        this.textBox2.Enabled = this.textBox3.Enabled = this.textBox4.Enabled = this.textBox5.Enabled = this.button2.Enabled = this.button1.Enabled = this.comboBox1.Enabled = false;
        this.code = this.random.Next(100000, 999999);
        MailMessage message = new MailMessage();
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        message.From = new MailAddress("zibank2019@gmail.com");
        message.To.Add(this.textBox5.Text);
        message.Subject = "Deschidere cont bancar Z Bank | Cod de verificare";
        message.Body = "S-a trimis o cerere pentru deschiderea unui cont la Z Bank :\nIP : " + this.externalAddress + "\nCont : " + this.textBox1.Text + "\nTitular : " + this.textBox2.Text + " " + this.textBox3.Text + "\nCNP : " + this.textBox4.Text + "\nEmail : " + this.textBox5.Text + "\nPentru deschiderea contului, va rugam sa introduceti urmatorul cod in formularul afisat : " + this.code.ToString();
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential("zibank2019", "zbankbusu");
        smtpClient.Send(message);
        int num = (int) MessageBox.Show("Un mail cu mesajul de confirmare a fost trimis la adresa de mail precizata!", "Info");
        this.textBox6.Enabled = true;
        this.pictureBox3.Image = (Image) Resources.error;
      }
      catch (Exception ex)
      {
        int num1 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu serverul!", "Eroare");
        int num2 = (int) MessageBox.Show(ex.ToString());
        this.textBox2.Enabled = this.textBox3.Enabled = this.textBox4.Enabled = this.textBox5.Enabled = this.button2.Enabled = this.button1.Enabled = this.comboBox1.Enabled = true;
        this.textBox6.Enabled = false;
        this.pictureBox3.Image = (Image) null;
      }
    }

    private void textBox4_TextChanged(object sender, EventArgs e)
    {
      if (this.textBox4.Text.Length < 13)
      {
        this.okCNP = false;
        this.pictureBox1.Image = (Image) Resources.error;
        this.button2.Enabled = false;
      }
      else if (this.textBox4.Text.Length > 13)
      {
        this.okCNP = false;
        this.pictureBox1.Image = (Image) Resources.error;
        this.button2.Enabled = false;
      }
      else
      {
        try
        {
          this.cmd = new MySqlCommand(string.Format("select cnp from R8WwnfQ8yx.conturi where cnp='{0}';", (object) this.textBox4.Text), this.connection);
          this.mdr = this.cmd.ExecuteReader();
          if (this.mdr.Read())
          {
            this.okCNP = false;
            this.pictureBox1.Image = (Image) Resources.error;
            this.mdr.Close();
          }
          else
          {
            this.okCNP = true;
            this.pictureBox1.Image = (Image) Resources.success;
            if (this.okMail)
              this.button2.Enabled = true;
            else
              this.button2.Enabled = false;
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
    }

    private void textBox5_TextChanged(object sender, EventArgs e)
    {
      bool flag = false;
      if (this.textBox5.Text.Contains("@gmail.com"))
      {
        this.okMail = false;
        this.pictureBox2.Image = (Image) Resources.error;
        flag = true;
      }
      else if (this.textBox5.Text.Contains("@yahoo.com"))
      {
        this.okMail = false;
        this.pictureBox2.Image = (Image) Resources.error;
        flag = true;
      }
      else if (this.textBox5.Text.Contains("@icloud.com"))
      {
        this.okMail = false;
        this.pictureBox2.Image = (Image) Resources.error;
        flag = true;
      }
      if (!flag)
      {
        this.pictureBox2.Image = (Image) Resources.error;
        this.button2.Enabled = false;
      }
      else
      {
        try
        {
          this.cmd = new MySqlCommand(string.Format("select * from R8WwnfQ8yx.conturi where email='{0}';", (object) this.textBox5.Text), this.connection);
          this.mdr = this.cmd.ExecuteReader();
          if (this.mdr.Read())
          {
            this.okMail = false;
            this.pictureBox2.Image = (Image) Resources.error;
            this.mdr.Close();
          }
          else
          {
            this.okMail = true;
            this.pictureBox2.Image = (Image) Resources.success;
            if (this.okCNP)
              this.button2.Enabled = true;
            else
              this.button2.Enabled = false;
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
    }

    private void textBox6_TextChanged(object sender, EventArgs e)
    {
      if (this.textBox6.Text == this.code.ToString())
      {
        this.pictureBox3.Image = (Image) Resources.success;
        try
        {
          this.cmd = new MySqlCommand(string.Format("insert into R8WwnfQ8yx.conturi(cont,titular,email,nr_tranzactii,cnp,sold,moneda,ip) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');", (object) this.textBox1.Text, (object) (this.textBox2.Text + "^" + this.textBox3.Text), (object) this.textBox5.Text, (object) 0, (object) this.textBox4.Text, (object) 0, (object) this.comboBox1.SelectedItem.ToString(), (object) this.externalAddress), this.connection);
          this.cmd.ExecuteNonQuery();
          this.cmd = new MySqlCommand(string.Format("create table R8WwnfQ8yx.{0}(id_tranzactie int not null auto_increment, sender varchar(100), suma varchar(100), data varchar(100), ip varchar(100), primary key (id_tranzactie));", (object) this.textBox1.Text), this.connection);
          this.cmd.ExecuteNonQuery();
          int num = (int) MessageBox.Show("Contul tau a fost deschis cu succes!\nBun venit la Z Bank!", "Felicitari");
          this.Close();
        }
        catch (Exception ex)
        {
          int num1 = (int) MessageBox.Show("A aparut o eroare in timpul comunicarii cu baza de date!", "Eroare");
          int num2 = (int) MessageBox.Show(ex.ToString());
        }
      }
      else
        this.pictureBox3.Image = (Image) Resources.error;
    }

    private void inchideToolStripMenuItem_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

    private void inchideToolStripMenuItem1_Click(object sender, EventArgs e) => this.Close();

    private void cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem_Click(
      object sender,
      EventArgs e)
    {
    }

    private void emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem_Click(
      object sender,
      EventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Register));
      this.textBox1 = new TextBox();
      this.button1 = new Button();
      this.label1 = new Label();
      this.textBox2 = new TextBox();
      this.label2 = new Label();
      this.textBox3 = new TextBox();
      this.label3 = new Label();
      this.comboBox1 = new ComboBox();
      this.label4 = new Label();
      this.textBox4 = new TextBox();
      this.label5 = new Label();
      this.textBox5 = new TextBox();
      this.button2 = new Button();
      this.textBox6 = new TextBox();
      this.label6 = new Label();
      this.menuStrip1 = new MenuStrip();
      this.fereastraToolStripMenuItem = new ToolStripMenuItem();
      this.inchideToolStripMenuItem = new ToolStripMenuItem();
      this.inchideToolStripMenuItem1 = new ToolStripMenuItem();
      this.fAQToolStripMenuItem = new ToolStripMenuItem();
      this.cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem = new ToolStripMenuItem();
      this.cNPulTrebuieSaAiba13CifreToolStripMenuItem = new ToolStripMenuItem();
      this.cNPulIntrodusTrebuieSaFieUnicneasociatAltuiContBancarToolStripMenuItem = new ToolStripMenuItem();
      this.emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem = new ToolStripMenuItem();
      this.emailulIntrodusNuTrebuieSaFieAsociatAltuiContToolStripMenuItem = new ToolStripMenuItem();
      this.pictureBox3 = new PictureBox();
      this.pictureBox2 = new PictureBox();
      this.pictureBox1 = new PictureBox();
      this.nuAmPrimitNiciunEmailToolStripMenuItem = new ToolStripMenuItem();
      this.verificaSectiuneaSPAMToolStripMenuItem = new ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox3).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.textBox1.BackColor = SystemColors.Control;
      this.textBox1.Enabled = false;
      this.textBox1.Font = new Font("Microsoft Sans Serif", 14f);
      this.textBox1.Location = new Point(12, 31);
      this.textBox1.MaxLength = 90;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(413, 34);
      this.textBox1.TabIndex = 4;
      this.button1.Location = new Point(431, 31);
      this.button1.Name = "button1";
      this.button1.Size = new Size(206, 34);
      this.button1.TabIndex = 5;
      this.button1.Text = "Genereaza cont disponibil";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 10f);
      this.label1.Location = new Point(9, 68);
      this.label1.Name = "label1";
      this.label1.Size = new Size(63, 20);
      this.label1.TabIndex = 6;
      this.label1.Text = "Nume :";
      this.textBox2.BackColor = SystemColors.Control;
      this.textBox2.Font = new Font("Microsoft Sans Serif", 14f);
      this.textBox2.Location = new Point(12, 91);
      this.textBox2.MaxLength = 90;
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new Size(413, 34);
      this.textBox2.TabIndex = 7;
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 10f);
      this.label2.Location = new Point(8, 128);
      this.label2.Name = "label2";
      this.label2.Size = new Size(86, 20);
      this.label2.TabIndex = 8;
      this.label2.Text = "Prenume :";
      this.textBox3.BackColor = SystemColors.Control;
      this.textBox3.Font = new Font("Microsoft Sans Serif", 14f);
      this.textBox3.Location = new Point(12, 151);
      this.textBox3.MaxLength = 90;
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new Size(413, 34);
      this.textBox3.TabIndex = 9;
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 10f);
      this.label3.Location = new Point(475, 108);
      this.label3.Name = "label3";
      this.label3.Size = new Size(78, 20);
      this.label3.TabIndex = 10;
      this.label3.Text = "Moneda :";
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[3]
      {
        (object) "EUR",
        (object) "RON",
        (object) "USD"
      });
      this.comboBox1.Location = new Point(479, 131);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(121, 24);
      this.comboBox1.TabIndex = 11;
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 10f);
      this.label4.Location = new Point(8, 188);
      this.label4.Name = "label4";
      this.label4.Size = new Size(54, 20);
      this.label4.TabIndex = 12;
      this.label4.Text = "CNP :";
      this.textBox4.BackColor = SystemColors.Control;
      this.textBox4.Font = new Font("Microsoft Sans Serif", 14f);
      this.textBox4.Location = new Point(12, 211);
      this.textBox4.MaxLength = 90;
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new Size(413, 34);
      this.textBox4.TabIndex = 13;
      this.textBox4.TextChanged += new EventHandler(this.textBox4_TextChanged);
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 10f);
      this.label5.Location = new Point(8, 252);
      this.label5.Name = "label5";
      this.label5.Size = new Size(66, 20);
      this.label5.TabIndex = 14;
      this.label5.Text = "Email : ";
      this.textBox5.BackColor = SystemColors.Control;
      this.textBox5.Font = new Font("Microsoft Sans Serif", 14f);
      this.textBox5.Location = new Point(12, 275);
      this.textBox5.MaxLength = 90;
      this.textBox5.Name = "textBox5";
      this.textBox5.Size = new Size(413, 34);
      this.textBox5.TabIndex = 15;
      this.textBox5.TextChanged += new EventHandler(this.textBox5_TextChanged);
      this.button2.Enabled = false;
      this.button2.Font = new Font("Microsoft Sans Serif", 12f);
      this.button2.Location = new Point(12, 382);
      this.button2.Name = "button2";
      this.button2.Size = new Size(625, 81);
      this.button2.TabIndex = 16;
      this.button2.Text = "Deschide cont";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.textBox6.BackColor = SystemColors.Control;
      this.textBox6.Enabled = false;
      this.textBox6.Font = new Font("Microsoft Sans Serif", 14f);
      this.textBox6.Location = new Point(13, 335);
      this.textBox6.MaxLength = 90;
      this.textBox6.Name = "textBox6";
      this.textBox6.Size = new Size(413, 34);
      this.textBox6.TabIndex = 19;
      this.textBox6.TextChanged += new EventHandler(this.textBox6_TextChanged);
      this.label6.AutoSize = true;
      this.label6.Font = new Font("Microsoft Sans Serif", 10f);
      this.label6.Location = new Point(9, 312);
      this.label6.Name = "label6";
      this.label6.Size = new Size(146, 20);
      this.label6.TabIndex = 21;
      this.label6.Text = "Cod de verificare :";
      this.menuStrip1.ImageScalingSize = new Size(20, 20);
      this.menuStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.fereastraToolStripMenuItem,
        (ToolStripItem) this.fAQToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(649, 28);
      this.menuStrip1.TabIndex = 22;
      this.menuStrip1.Text = "menuStrip1";
      this.fereastraToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.inchideToolStripMenuItem,
        (ToolStripItem) this.inchideToolStripMenuItem1
      });
      this.fereastraToolStripMenuItem.Name = "fereastraToolStripMenuItem";
      this.fereastraToolStripMenuItem.Size = new Size(83, 24);
      this.fereastraToolStripMenuItem.Text = "Fereastra";
      this.inchideToolStripMenuItem.Name = "inchideToolStripMenuItem";
      this.inchideToolStripMenuItem.Size = new Size(176, 26);
      this.inchideToolStripMenuItem.Text = "Minimizeaza";
      this.inchideToolStripMenuItem.Click += new EventHandler(this.inchideToolStripMenuItem_Click);
      this.inchideToolStripMenuItem1.Name = "inchideToolStripMenuItem1";
      this.inchideToolStripMenuItem1.Size = new Size(176, 26);
      this.inchideToolStripMenuItem1.Text = "Inchide";
      this.inchideToolStripMenuItem1.Click += new EventHandler(this.inchideToolStripMenuItem1_Click);
      this.fAQToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem,
        (ToolStripItem) this.emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem,
        (ToolStripItem) this.nuAmPrimitNiciunEmailToolStripMenuItem
      });
      this.fAQToolStripMenuItem.Name = "fAQToolStripMenuItem";
      this.fAQToolStripMenuItem.Size = new Size(50, 24);
      this.fAQToolStripMenuItem.Text = "FAQ";
      this.cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.cNPulTrebuieSaAiba13CifreToolStripMenuItem,
        (ToolStripItem) this.cNPulIntrodusTrebuieSaFieUnicneasociatAltuiContBancarToolStripMenuItem
      });
      this.cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem.Name = "cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem";
      this.cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem.Size = new Size(386, 26);
      this.cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem.Text = "CNP-ul pare corect.De ce nu este acceptat?";
      this.cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem.Click += new EventHandler(this.cNPulPareCorectDeCeNuEsteDisponibiToolStripMenuItem_Click);
      this.cNPulTrebuieSaAiba13CifreToolStripMenuItem.Name = "cNPulTrebuieSaAiba13CifreToolStripMenuItem";
      this.cNPulTrebuieSaAiba13CifreToolStripMenuItem.Size = new Size(508, 26);
      this.cNPulTrebuieSaAiba13CifreToolStripMenuItem.Text = "CNP-ul introdus trebuie sa aiba 13 cifre.";
      this.cNPulIntrodusTrebuieSaFieUnicneasociatAltuiContBancarToolStripMenuItem.Name = "cNPulIntrodusTrebuieSaFieUnicneasociatAltuiContBancarToolStripMenuItem";
      this.cNPulIntrodusTrebuieSaFieUnicneasociatAltuiContBancarToolStripMenuItem.Size = new Size(508, 26);
      this.cNPulIntrodusTrebuieSaFieUnicneasociatAltuiContBancarToolStripMenuItem.Text = "CNP-ul introdus trebuie sa fie unic(neasociat altui cont bancar).";
      this.emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.emailulIntrodusNuTrebuieSaFieAsociatAltuiContToolStripMenuItem
      });
      this.emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem.Name = "emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem";
      this.emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem.Size = new Size(386, 26);
      this.emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem.Text = "Email-ul pare corect.De ce nu este acceptat?";
      this.emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem.Click += new EventHandler(this.emailulPareCorectDeCeNuEsteDisponibilToolStripMenuItem_Click);
      this.emailulIntrodusNuTrebuieSaFieAsociatAltuiContToolStripMenuItem.Name = "emailulIntrodusNuTrebuieSaFieAsociatAltuiContToolStripMenuItem";
      this.emailulIntrodusNuTrebuieSaFieAsociatAltuiContToolStripMenuItem.Size = new Size(432, 26);
      this.emailulIntrodusNuTrebuieSaFieAsociatAltuiContToolStripMenuItem.Text = "Email-ul introdus nu trebuie sa fie asociat altui cont";
      this.pictureBox3.Image = (Image) Resources.error;
      this.pictureBox3.Location = new Point(441, 332);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new Size(42, 39);
      this.pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox3.TabIndex = 20;
      this.pictureBox3.TabStop = false;
      this.pictureBox2.Image = (Image) Resources.error;
      this.pictureBox2.Location = new Point(440, 272);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(42, 39);
      this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox2.TabIndex = 18;
      this.pictureBox2.TabStop = false;
      this.pictureBox1.Image = (Image) Resources.error;
      this.pictureBox1.Location = new Point(440, 210);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(42, 39);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 17;
      this.pictureBox1.TabStop = false;
      this.nuAmPrimitNiciunEmailToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.verificaSectiuneaSPAMToolStripMenuItem
      });
      this.nuAmPrimitNiciunEmailToolStripMenuItem.Name = "nuAmPrimitNiciunEmailToolStripMenuItem";
      this.nuAmPrimitNiciunEmailToolStripMenuItem.Size = new Size(386, 26);
      this.nuAmPrimitNiciunEmailToolStripMenuItem.Text = "Nu ai primit mail?";
      this.verificaSectiuneaSPAMToolStripMenuItem.Name = "verificaSectiuneaSPAMToolStripMenuItem";
      this.verificaSectiuneaSPAMToolStripMenuItem.Size = new Size(252, 26);
      this.verificaSectiuneaSPAMToolStripMenuItem.Text = "Verifica sectiunea SPAM.";
      this.AutoScaleDimensions = new SizeF(8f, 16f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(649, 473);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.pictureBox3);
      this.Controls.Add((Control) this.textBox6);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.textBox5);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.textBox4);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.textBox3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.menuStrip1);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MainMenuStrip = this.menuStrip1;
      this.MaximizeBox = false;
      this.Name = nameof (Register);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "ZBank | Register";
      this.Load += new EventHandler(this.Register_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((ISupportInitialize) this.pictureBox3).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
