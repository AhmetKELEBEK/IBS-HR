using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IBS_HR
{
    public partial class Form1 : Form
    {
        public object SplashScreenManager { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            button2.Visible = false;
                try
            {
            label1.Text = "Veri Tabanı Araçları Kontrol Ediliyor.";
            string VeritataniAdi = "ibs";
            SqlConnection baglanti = new SqlConnection("server=.\\SQLEXPRESS;database=master; Integrated Security=SSPI");
            
            SqlCommand komut = new SqlCommand("SELECT Count(name) FROM master.dbo.sysdatabases WHERE name=@prmVeritabani", baglanti);
            komut.Parameters.AddWithValue("@prmVeriTabani", VeritataniAdi);
            
            baglanti.Open();
            
            int sonuc = (int)komut.ExecuteScalar();
                if (sonuc != 0)
                {
                    label1.Text = "";
                    label1.Text = "Kontrol Başarılı, Giriş Yapılabilir";
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Programı İlk Defa Kullanıdığınız Tespit Edildi.\n Veri Tabanı Kurulumu Yapılsın mu ?", "İlk Açılış", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        komut.CommandText = "Create Database " + VeritataniAdi;
                        komut.ExecuteNonQuery();
                        using (SqlCommand command = new SqlCommand("CREATE TABLE Kisiler(Ad char(50),Soyad char(50),Adres char(50));", baglanti))
                            baglanti.Close();
                        
                        string baglantiCumlesi = "server=.\\SQLEXPRESS; database=ibs; integrated security=SSPI";
                        using (SqlConnection tolustur = new SqlConnection(baglantiCumlesi))
                            try
                            {

                                tolustur.Open();
                                using (SqlCommand command = new SqlCommand("CREATE TABLE Kisiler(Ad char(50),Soyad char(50),Adres char(50));", tolustur))

                                   command.ExecuteNonQuery();

                                
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Kurgu Hazırlanırken Hata." + ex.Message);
                            }

                        label1.Text = "";
                        label1.Text = "İlk Kurulum Başarılı Oldu.";
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        Application.Exit();
                    }
                    

                }
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yerel Sunucu İle Bağlantı Başarısız. \n" + ex.Message);
                label1.Text = "Yerel Sunucu Bağlantısı Başarısız. Giriş Yapılamaz.";
                button2.Visible = true;
                button1.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            yardım Form1 = new yardım();
            this.Hide();
            Form1.Show();
        }
    }
    }

