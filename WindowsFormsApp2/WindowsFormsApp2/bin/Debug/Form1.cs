
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ListBox aynaliste;
        ListBox dondurliste;
        ListBox hst;
        Bitmap arka_resim = null;
        Bitmap arka_geri = null;
        Bitmap geri = null;



        TextBox genislik_txt = new TextBox();
        TextBox yukseklik_txt = new TextBox();
        Button resize = new Button();
        //int yukseklik, genislik;

        int yukseklik_ekran, genislik_ekran;

        int renk_k = 0;
        int renk_y = 0;
        int renk_m = 0;

        int degisken = 0;
        int gri = 0;


        int eski_genislik;
        int eski_yukseklik;

        ListBox renk_kanali;

        public Form1()
        {
            InitializeComponent();

            Bitmap image = Bitmap.FromFile("C:\\Users\\Pc\\Desktop\\editor\\dosyaac.png") as Bitmap;
            Bitmap boyutlanmis = new Bitmap(image, new Size(30, 30));
            button1.Image = boyutlanmis;
            button1.ImageAlign = ContentAlignment.MiddleLeft;

            image = Bitmap.FromFile("C:\\Users\\Pc\\Desktop\\editor\\histogram.jpg") as Bitmap;
            boyutlanmis = new Bitmap(image, new Size(30, 30));
            button2.Image = boyutlanmis;
            button2.ImageAlign = ContentAlignment.MiddleLeft;

            image = Bitmap.FromFile("C:\\Users\\Pc\\Desktop\\editor\\tersleme.jpg") as Bitmap;
            boyutlanmis = new Bitmap(image, new Size(30, 30));
            button3.Image = boyutlanmis;
            button3.ImageAlign = ContentAlignment.MiddleLeft;

            image = Bitmap.FromFile("C:\\Users\\Pc\\Desktop\\editor\\aynalama.jpg") as Bitmap;
            boyutlanmis = new Bitmap(image, new Size(30, 30));
            button4.Image = boyutlanmis;
            button4.ImageAlign = ContentAlignment.MiddleLeft;

            image = Bitmap.FromFile("C:\\Users\\Pc\\Desktop\\editor\\döndür.png") as Bitmap;
            boyutlanmis = new Bitmap(image, new Size(30, 30));
            button5.Image = boyutlanmis;
            button5.ImageAlign = ContentAlignment.MiddleLeft;

            image = Bitmap.FromFile("C:\\Users\\Pc\\Desktop\\editor\\boyutlandır.png") as Bitmap;
            boyutlanmis = new Bitmap(image, new Size(30, 30));
            button6.Image = boyutlanmis;
            button6.ImageAlign = ContentAlignment.MiddleLeft;

            image = Bitmap.FromFile("C:\\Users\\Pc\\Desktop\\editor\\tekrarac.png") as Bitmap;
            boyutlanmis = new Bitmap(image, new Size(30, 30));
            button7.Image = boyutlanmis;
            button7.ImageAlign = ContentAlignment.MiddleLeft;

            image = Bitmap.FromFile("C:\\Users\\Pc\\Desktop\\editor\\grayscale.jpg") as Bitmap;
            boyutlanmis = new Bitmap(image, new Size(47, 30));
            button8.Image = boyutlanmis;
            button8.ImageAlign = ContentAlignment.MiddleLeft;

            image = Bitmap.FromFile("C:\\Users\\Pc\\Desktop\\editor\\renkkanalları.png") as Bitmap;
            boyutlanmis = new Bitmap(image, new Size(30, 30));
            button9.Image = boyutlanmis;
            button9.ImageAlign = ContentAlignment.MiddleLeft;


            Bitmap islemler = (Bitmap)Image.FromFile(@"C:\\Users\\Pc\\Desktop\\editor\\islem.jpg", true);
            Bitmap islem2 = (Bitmap)Image.FromFile(@"C:\\Users\\Pc\\Desktop\\editor\\ekran.png", true);


            ekran.Size = new Size(1100, 620);
            ekran.BackColor = Color.FromArgb(255, 255, 255);
            ekran.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(ekran);

            pictureBox2.Image = islemler;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Image = islem2;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;


            aynaliste = new ListBox();
            aynaliste.Items.Add("Sağa Aynala");
            aynaliste.Items.Add("Sola Aynala");
            aynaliste.BackColor = Color.Honeydew;
            aynaliste.ForeColor = Color.DarkBlue;
            aynaliste.Font = new Font("Arial", 8, FontStyle.Bold);
            aynaliste.Height = 35;
            aynaliste.Width = 75;
            aynaliste.Location = new Point(160, 318);
            aynaliste.SelectedValueChanged += new EventHandler(aynaliste_SelectedValueChanged);

            dondurliste = new ListBox();
            dondurliste.Items.Add("Sağa 90 Derece Döndür");
            dondurliste.Items.Add("Sola 90 Derece Döndür");
            dondurliste.BackColor = Color.Honeydew;
            dondurliste.ForeColor = Color.DarkBlue;
            dondurliste.Font = new Font("Arial", 8, FontStyle.Bold);
            dondurliste.Height = 35;
            dondurliste.Width = 140;
            dondurliste.Location = new Point(160, 378);
            dondurliste.SelectedValueChanged += new EventHandler(dondurliste_SelectedValueChanged);

            hst = new ListBox();
            hst.Items.Add("Gri Seviye Görüntü Histogramı");
            hst.Items.Add("Renkli Görüntü Histogramı");
            hst.BackColor = Color.Honeydew;
            hst.ForeColor = Color.DarkBlue;
            hst.Font = new Font("Arial", 8, FontStyle.Bold);
            hst.Height = 35;
            hst.Width = 180;
            hst.Location = new Point(160, 198);
            hst.SelectedValueChanged += new EventHandler(histogram_SelectedValueChanged);

            hst.Visible = false;

            genislik_txt.Name = "textbox1";
            genislik_txt.Location = new Point(215, 17);
            genislik_txt.Size = new Size(50, 50);
            genislik_txt.BackColor = Color.FromArgb(255, 255, 255);
            genislik_txt.BorderStyle = BorderStyle.FixedSingle;
            genislik_txt.Text = "Genişlik";
            genislik_txt.Enter += new EventHandler(genislik_txt_Enter);

            yukseklik_txt.Name = "textbox2";
            yukseklik_txt.Location = new Point(270, 17);
            yukseklik_txt.Size = new Size(50, 50);
            yukseklik_txt.BackColor = Color.FromArgb(255, 255, 255);
            yukseklik_txt.BorderStyle = BorderStyle.FixedSingle;

            yukseklik_txt.Text = "Yükseklik:";
            yukseklik_txt.Enter += new EventHandler(yukseklik_txt_Enter);

            genislik_txt.Visible = false;
            yukseklik_txt.Visible = false;

            resize.Location = new Point(330, 15);
            resize.Click += new EventHandler(resizeClickOneEvent);
            //button.Tag = i;
            resize.BackColor = Color.Turquoise;
            resize.ForeColor = Color.Black;
            resize.Size = new Size(80, 25);
            resize.Name = "Değiştir";
            resize.Text = "Değiştir";
            resize.Font = new Font("Arial", 8, FontStyle.Bold);
            this.Controls.Add(resize);

            resize.Visible = false;

            renk_kanali = new ListBox();

            renk_kanali.Items.Add("Kırmızı(R)");
            renk_kanali.Items.Add("Yeşil(G)");
            renk_kanali.Items.Add("Mavi(B)");
            renk_kanali.Items.Add("Orijinal");
            renk_kanali.BackColor = Color.Honeydew;
            renk_kanali.ForeColor = Color.DarkBlue;
            renk_kanali.Font = new Font("Arial", 8, FontStyle.Bold);
            renk_kanali.Height = 50;
            renk_kanali.Width = 65;
            renk_kanali.Location = new Point(160, 618);
            renk_kanali.SelectedValueChanged += new EventHandler(renk_kanali_SelectedValueChanged);
            renk_kanali.Visible = false;


            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.button10, "Geri");


            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.button12, "Kaydet");


            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(HMI_FormClosing);

        }


        int hangi_buton = -1;
        int basildi = 0;
        private void button10_Click(object sender, EventArgs e)
        {

            if (basildi == 0)
            {

                hst.Visible = false;
                aynaliste.Visible = false;
                dondurliste.Visible = false;

                genislik_txt.Visible = false;
                yukseklik_txt.Visible = false;

                resize.Visible = false;

                ekran.Image = geri;
                arka_resim = arka_geri;

                if (ekran.Image == null)
                {
                    MessageBox.Show("Önce Bir Resim Seçin");

                }
                else
                {



                    int a = ekran.Width - ekran.Image.Width;
                    int b = ekran.Height - ekran.Image.Height;
                    Padding p = new System.Windows.Forms.Padding();
                    p.Left = a / 2;
                    p.Top = b / 2;
                    ekran.Padding = p;

                    if (hangi_buton == 5)
                    {
                        int temp;

                        temp = genislik_ekran;
                        genislik_ekran = yukseklik_ekran;
                        yukseklik_ekran = temp;
                    }
                    else if (hangi_buton == 6)
                    {

                        genislik_ekran = genislik_onceki;
                        yukseklik_ekran = yukseklik_onceki;
                    }

                    else if (hangi_buton == 8)
                    {
                        gri = 0;

                    }

                }
                basildi = 1;
            }

        }

        int kapatma_kontrol = 0;

        private void HMI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (arka_resim != null)
            {

                kapatma_formu();
                if (kapatma_kontrol == 0)
                {
                    e.Cancel = true;
                }

            }
            else
            {
                e.Cancel = false;
            }


        }
        Form form2;
        private void kapatma_formu()
        {
            form2 = new Form();
            form2.Size = new Size(380, 150);
            form2.StartPosition = FormStartPosition.CenterScreen;
            form2.Text = "GÖRÜNTÜ İŞLEME EDİTÖRÜ";
            form2.Icon = new Icon("C:\\Users\\Pc\\Desktop\\editor\\System Registry.ico");
            form2.BackColor = Color.FromArgb(255, 255, 255);

            Button kaydet = new Button();
            kaydet.Font = new Font("Arial", 8, FontStyle.Bold);
            Button farkli_kaydet = new Button();
            farkli_kaydet.Font = new Font("Arial", 8, FontStyle.Bold);
            Button kaydetme = new Button();
            kaydetme.Font = new Font("Arial", 8, FontStyle.Bold);

            kaydet.Click += new EventHandler(kaydet_ClickOneEvent);
            farkli_kaydet.Click += new EventHandler(farkli_kaydet_ClickOneEvent);
            kaydetme.Click += new EventHandler(kaydetme_ClickOneEvent);


            kaydet.Size = new Size(100, 35);
            farkli_kaydet.Size = new Size(100, 35);
            kaydetme.Size = new Size(100, 35);

            kaydet.Text = "Kaydet";
            kaydetme.Text = "Kaydetme";
            farkli_kaydet.Text = "Farklı Kaydet";

            TextBox soru = new TextBox();

            soru.Text = "Değişiklikleri kaydetmek istiyor musunuz?";
            soru.Location = new Point(20, 20);
            soru.Size = new Size(330, 30);
            soru.BackColor = Color.FromArgb(255, 255, 255);
            soru.Font = new Font("Arial", 10, FontStyle.Bold);
            soru.ReadOnly = true;
            soru.SelectionStart = 0;
            soru.SelectionLength = 0;

            kaydet.Location = new Point(25, 60);
            farkli_kaydet.Location = new Point(135, 60);
            kaydetme.Location = new Point(245, 60);

            form2.Controls.Add(soru);
            form2.Controls.Add(kaydet);
            form2.Controls.Add(farkli_kaydet);
            form2.Controls.Add(kaydetme);

            form2.ShowDialog();

        }

        void kaydet_ClickOneEvent(object sender, EventArgs e)
        {
            kapatma_kontrol = 1;

            if (arka_resim != null)
            {
                Bitmap eski_ekran = new Bitmap(ekran.Image);
                boyutlandir();
                Image<Bgr, byte> imgInput = new Image<Bgr, byte>(new Bitmap(ekran.Image));
                ekran.Image = eski_ekran;

                imgInput.Save(ofd.FileName);
            }

            form2.Close();

        }


        void kaydetme_ClickOneEvent(object sender, EventArgs e)
        {
            kapatma_kontrol = 1;
            form2.Close();
        }


        void farkli_kaydet_ClickOneEvent(object sender, EventArgs e)
        {
            kapatma_kontrol = 1;
            if (arka_resim != null)
            {
                Bitmap eski_ekran = new Bitmap(ekran.Image);
                boyutlandir();
                Image<Bgr, byte> imgInput = new Image<Bgr, byte>(new Bitmap(ekran.Image));
                ekran.Image = eski_ekran;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Files | *.jpg; *.jpeg; *.png;*.bmp;*.tiff;*.gif;*.ico";
                saveFileDialog.DefaultExt = "jpg";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imgInput.Save(saveFileDialog.FileName);
                }


            }

            form2.Close();

        }



        private void boyutlandir()
        {
            Bitmap boyut = new Bitmap(genislik_ekran, yukseklik_ekran);

            double genislik_olcek = (double)boyut.Width / (double)arka_resim.Width;
            double yukseklik_olcek = (double)boyut.Height / (double)arka_resim.Height;

            for (int cy = 0; cy < boyut.Height; cy++)
            {
                for (int cx = 0; cx < boyut.Width; cx++)
                {
                    int piksel = (cy * (boyut.Width)) + (cx);
                    int en_yakin_konum = (((int)(cy / yukseklik_olcek) * (arka_resim.Width)) + ((int)(cx / genislik_olcek)));

                    boyut.SetPixel(piksel % boyut.Width, piksel / boyut.Width, arka_resim.GetPixel(en_yakin_konum % arka_resim.Width, en_yakin_konum / arka_resim.Width));

                }
            }


            ekran.Image = boyut;

            int a = ekran.Width - ekran.Image.Width;
            int b = ekran.Height - ekran.Image.Height;
            Padding p = new System.Windows.Forms.Padding();
            p.Left = a / 2;
            p.Top = b / 2;
            ekran.Padding = p;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            hst.Visible = false;
            renk_kanali.Visible = false;
            aynaliste.Visible = false;
            dondurliste.Visible = false;


            genislik_txt.Visible = false;
            yukseklik_txt.Visible = false;


            resize.Visible = false;

            Bitmap rsm = arka_resim;

            if (rsm != null)
            {
                kapatma_formu();

                MessageBox.Show("Yeni Dosyayı Ekleyiniz.");

            }

            ofd.Filter = "Files | *.jpg; *.jpeg; *.png;*.bmp;*.tiff;*.gif;*.ico";




            if (ofd.ShowDialog() == DialogResult.OK)
            {
                arka_resim = null;
                arka_geri = null;
                geri = null;
                basildi = 0;

                Image<Bgr, byte> imgInput = new Image<Bgr, byte>(ofd.FileName);


                degisken = 0;
                ekran.Image = imgInput.ToBitmap();


                resimekle(imgInput.ToBitmap());


            }
        }


        private void resimekle(Bitmap imgInput)
        {
            Bitmap resim = new Bitmap(imgInput);

            if (imgInput.Width <= ekran.Width && imgInput.Height <= ekran.Height)
            {


            }
            else
            {
                PictureBox pictureboxtemp = new PictureBox();
                pictureboxtemp.Image = ekran.Image;
                ekran.Image = null;


                if (MessageBox.Show("Resim Yeniden Boyutlandırılsın Mı?", "Uyarı!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // gri = 0;

                    if (imgInput.Width > ekran.Width && imgInput.Height > ekran.Height)
                    {

                        resim = new Bitmap(imgInput, new Size(ekran.Width, ekran.Height));


                    }
                    else if (imgInput.Width > ekran.Width)
                    {
                        resim = new Bitmap(imgInput, new Size(ekran.Width, imgInput.Height));

                    }
                    else
                    {
                        resim = new Bitmap(imgInput, new Size(imgInput.Width, ekran.Height));
                    }
                    ekran.Image = pictureboxtemp.Image;
                }

            }

            if (resim != null)
            {
                ekran.Image = resim;
                int a = ekran.Width - ekran.Image.Width;
                int b = ekran.Height - ekran.Image.Height;
                Padding p = new System.Windows.Forms.Padding();
                p.Left = a / 2;
                p.Top = b / 2;
                ekran.Padding = p;

                if (degisken == 0)
                {
                    //kapatma_formu();
                    renk_k = 0;
                    renk_m = 0;
                    renk_y = 0;
                    basildi = 0;
                    gri = 0;

                    yukseklik_ekran = resim.Height;
                    genislik_ekran = resim.Width;
                    arka_resim = resim;
                    degisken = 1;
                }
            }
        }

        Form2 frm;

        PictureBox ekran2;

        PictureBox grafik;


        public class Form2 : Form
        {
            public Form2()
            {
                Text = "GÖRÜNTÜ İŞLEME EDİTÖRÜ ";

                Size = new Size(800, 600);

                StartPosition = FormStartPosition.CenterScreen;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            frm = new Form2();
            ekran2 = new PictureBox();
            grafik = new PictureBox();


            ekran2.Size = new Size(700, 500);
            ekran2.BackColor = Color.FromArgb(255, 255, 255);
            ekran2.BorderStyle = BorderStyle.FixedSingle;
            ekran2.Location = new Point(30, 30);
            frm.Controls.Add(ekran2);

            grafik.Size = new Size(200, 200);
            grafik.BackColor = Color.FromArgb(255, 255, 255);
            grafik.BorderStyle = BorderStyle.FixedSingle;
            grafik.Location = new Point(530, 30);
            frm.Controls.Add(grafik);


            dondurliste.Visible = false;
            aynaliste.Visible = false;
            yukseklik_txt.Visible = false;
            genislik_txt.Visible = false;
            resize.Visible = false;
            renk_kanali.Visible = false;

            //pictureBox1.Visible = false;

            if (ekran.Image == null)
            {
                MessageBox.Show("Önce Bir Resim Seçin");
            }
            else
            {

                this.Controls.Add(hst);
                hst.BringToFront();
                hst.Visible = true;

            }
        }

        int temp1, temp2;

        private void kontrol()
        {
            temp1 = genislik_ekran;
            temp2 = yukseklik_ekran;
            genislik_ekran = ekran.Image.Width;
            yukseklik_ekran = ekran.Image.Height;

        }
        private void kontrolters()
        {

            genislik_ekran = temp1;
            yukseklik_ekran = temp2;

        }



        private void resimekle2(Bitmap imgInput, PictureBox temp)//Image<Bgr, byte> imgInput)
        {
            Bitmap resim = imgInput;



            if (imgInput.Width <= temp.Width && imgInput.Height <= temp.Height)
            {
                //gri = 0;

            }
            else
            {


                // gri = 0;

                if (imgInput.Width > temp.Width && imgInput.Height > temp.Height)
                {

                    resim = new Bitmap(imgInput, new Size(temp.Width, temp.Height));


                }
                else if (imgInput.Width > temp.Width)
                {
                    resim = new Bitmap(imgInput, new Size(temp.Width, imgInput.Height));

                }
                else
                {

                    resim = new Bitmap(imgInput, new Size(imgInput.Width, temp.Height));
                }


            }



            if (resim != null)
            {
                temp.Image = resim;
                int a = temp.Width - temp.Image.Width;
                int b = temp.Height - temp.Image.Height;
                Padding p = new System.Windows.Forms.Padding();
                p.Left = a / 2;
                p.Top = b / 2;
                temp.Padding = p;

            }
        }




        private void histogram_SelectedValueChanged(object sender, EventArgs e)
        {
            Bitmap bt = new Bitmap(arka_resim);


            TextBox txt = new TextBox();
            //txt.ID = "textBox1";
            txt.BackColor = Color.FromArgb(255, 255, 255);
            txt.Font = new Font("Arial", 9, FontStyle.Bold);
            txt.ReadOnly = true;
            txt.SelectionStart = 0;
            txt.SelectionLength = 0;
            txt.Location = new Point(733, 30);
            txt.Size = new Size(50, 50);
            txt.BringToFront();
            frm.Controls.Add(txt);
            txt.Visible = false;


            TextBox txt2 = new TextBox();
            //txt.ID = "textBox1";
            txt2.BackColor = Color.FromArgb(255, 255, 255);
            txt2.Font = new Font("Arial", 9, FontStyle.Bold);
            txt2.ReadOnly = true;
            txt2.SelectionStart = 0;
            txt2.SelectionLength = 0;
            txt2.Location = new Point(733, 80);
            txt2.Size = new Size(50, 50);
            txt2.BringToFront();
            frm.Controls.Add(txt2);
            txt2.Visible = false;

            TextBox txt3 = new TextBox();
            //txt.ID = "textBox1";
            txt3.BackColor = Color.FromArgb(255, 255, 255);
            txt3.Font = new Font("Arial", 9, FontStyle.Bold);
            txt3.ReadOnly = true;
            txt3.SelectionStart = 0;
            txt3.SelectionLength = 0;
            txt3.Location = new Point(733, 130);
            txt3.Size = new Size(50, 50);
            txt3.BringToFront();
            frm.Controls.Add(txt3);
            txt3.Visible = false;

            basildi = 0;
            hangi_buton = -1;
            if (hst.GetSelected(0))
            {

                int[] histogram = new int[256];

                for (int i = 0; i < 256; i++)
                    histogram[i] = 0;

                for (int y = 0; y < bt.Height; y++)
                {
                    for (int x = 0; x < bt.Width; x++)
                    {
                        Color c = bt.GetPixel(x, y);

                        int r = c.R;
                        int g = c.G;
                        int b = c.B;
                        byte gri = (byte)(.3 * c.R + .59 * c.G + .11 * c.B);

                        histogram[gri]++;

                        bt.SetPixel(x, y, Color.FromArgb(c.A, gri, gri, gri));
                    }
                }
                int max = 0;

                for (int i = 0; i < 256; i++)
                {
                    if (max < histogram[i])
                        max = histogram[i];
                }

                resimekle2(bt, ekran2);

                frm.Show();




                Bitmap bm = new Bitmap(300, max + 1);

                for (int i = 0; i < histogram.Length; i++)
                {
                    for (int j = max; j > max - histogram[i]; j--)
                    {
                        bm.SetPixel(i + 10, j, Color.Black);
                    }
                }


                txt.Visible = true;
                txt.Text = max.ToString();

                /// grafik.Image = bm;
                grafik.SizeMode = PictureBoxSizeMode.StretchImage;
                grafik.Image = bm;
                // resimekle2(bm,grafik);
                grafik.BringToFront();


            }

            else
            {

                txt.Visible = true;
                txt2.Visible = true;
                txt3.Visible = true;

                txt.ForeColor = Color.Red;
                txt2.ForeColor = Color.Green;
                txt3.ForeColor = Color.Blue;



                int[] kirmizi = new int[256];
                int[] yesil = new int[256];
                int[] mavi = new int[256];

                for (int i = 0; i < 256; i++)
                {
                    kirmizi[i] = 0;
                    yesil[i] = 0;
                    mavi[i] = 0;

                }

                for (int y = 0; y < bt.Height; y++)
                {
                    for (int x = 0; x < bt.Width; x++)
                    {
                        Color c = bt.GetPixel(x, y);

                        int r = c.R;
                        int g = c.G;
                        int b = c.B;

                        kirmizi[r]++;
                        yesil[g]++;
                        mavi[b]++;

                    }
                }

                int max = 0;

                int k_max = 0, y_max = 0, b_max = 0;

                for (int i = 0; i < 256; i++)
                {
                    if (max < kirmizi[i])
                        max = kirmizi[i];
                    if (max < mavi[i])
                        max = mavi[i];
                    if (max < yesil[i])
                        max = yesil[i];

                    if (kirmizi[i] > k_max)
                        k_max = kirmizi[i];

                    if (yesil[i] > y_max)
                        y_max = yesil[i];

                    if (mavi[i] > b_max)
                        b_max = mavi[i];
                }


                resimekle2(bt, ekran2);

                frm.Show();


                Bitmap bm = new Bitmap(300, max + 1);

                grafik.Image = bm;
                Pen redPen = new Pen(Color.Red, 1);
                Pen bluePen = new Pen(Color.Blue, 1);
                Pen greenPen = new Pen(Color.Green, 1);


                greenPen.Alignment = PenAlignment.Center;
                grafik.SizeMode = PictureBoxSizeMode.StretchImage;
                Image img = grafik.Image;

                Graphics gc = Graphics.FromImage(img);

                for (int i = 0; i < 256; i++)
                {



                    gc.DrawLine(greenPen, i + 10, max - yesil[i] + 15, i + 10, max - yesil[i] - 15);

                    gc.DrawLine(redPen, i + 10, max - kirmizi[i] + 15, i + 10, max - kirmizi[i] - 15);

                    gc.DrawLine(bluePen, i + 10, max - mavi[i] + 15, i + 10, max - mavi[i] - 15);


                    if (i != 0)
                    {

                        gc.DrawLine(greenPen, i + 9, max - yesil[i - 1], i + 10, max - yesil[i]);

                        gc.DrawLine(redPen, i + 9, max - kirmizi[i - 1], i + 10, max - kirmizi[i]);

                        gc.DrawLine(bluePen, i + 9, max - mavi[i - 1], i + 10, max - mavi[i]);

                    }


                }

                txt.Text = k_max.ToString();
                txt2.Text = y_max.ToString();
                txt3.Text = b_max.ToString();




                gc.DrawImage(img, new Point(0, 0));


                grafik.BringToFront();
            }

            hst.Visible = false;


        }



        private void button3_Click(object sender, EventArgs e)
        {

            hst.Visible = false;
            renk_kanali.Visible = false;
            aynaliste.Visible = false;
            dondurliste.Visible = false;

            genislik_txt.Visible = false;
            yukseklik_txt.Visible = false;

            resize.Visible = false;


            if (ekran.Image == null)
            {
                MessageBox.Show("Önce Bir Resim Seçin");
            }
            else
            {
                geri = new Bitmap(ekran.Image);
                arka_geri = new Bitmap(arka_resim);


                gri = 0;
                int i, j;
                Color r;
                //Bitmap bt =arka_resim;
                for (i = 0; i <= arka_resim.Width - 1; i++)
                {
                    for (j = 0; j <= arka_resim.Height - 1; j++)
                    {
                        r = arka_resim.GetPixel(i, j);
                        r = Color.FromArgb(r.A, (byte)~r.R, (byte)~r.G, (byte)~r.B);
                        arka_resim.SetPixel(i, j, r);
                        if ((i % 10) == 0)
                        {
                            Application.DoEvents();
                        }
                    }
                }
                kontrol();
                boyutlandir();
                kontrolters();
                basildi = 0;
                hangi_buton = 3;

            }

        }



        private void button4_Click(object sender, EventArgs e)
        {

            hst.Visible = false;
            renk_kanali.Visible = false;
            dondurliste.Visible = false;

            genislik_txt.Visible = false;
            yukseklik_txt.Visible = false;


            if (ekran.Image == null)
            {
                MessageBox.Show("Önce Bir Resim Seçin");

            }
            else
            {

                geri = new Bitmap(ekran.Image);
                arka_geri = new Bitmap(arka_resim);

                this.Controls.Add(aynaliste);
                aynaliste.BringToFront();
                aynaliste.Visible = true;


                resize.Visible = false;

            }
        }
        private void aynaliste_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox listbox = (ListBox)sender;

            basildi = 0;
            //Bitmap bt =arka_resim;

            Bitmap btAyna = new Bitmap(arka_resim.Width, arka_resim.Height);

            for (int i = 0; i < arka_resim.Height; i++)
            {
                for (int j = 0; j < arka_resim.Width; j++)
                {
                    Color r = arka_resim.GetPixel(j, i);
                    btAyna.SetPixel(arka_resim.Width - 1 - j, i, r);
                }
            }

            arka_resim = btAyna;

            kontrol();
            boyutlandir();
            kontrolters();

            hangi_buton = 4;

            aynaliste.Visible = false;
        }



        private void button5_Click(object sender, EventArgs e)
        {
            //pictureBox1.Visible = false;
            hst.Visible = false;
            renk_kanali.Visible = false;
            genislik_txt.Visible = false;
            yukseklik_txt.Visible = false;

            aynaliste.Visible = false;

            resize.Visible = false;

            if (ekran.Image == null)
            {
                MessageBox.Show("Önce Bir Resim Seçin");

            }
            else
            {

                this.Controls.Add(dondurliste);
                dondurliste.BringToFront();
                dondurliste.Visible = true;
            }

        }


        private void dondurliste_SelectedValueChanged(object sender, EventArgs e)
        {

            basildi = 0;
            //Bitmap bt = new Bitmap(arka_resim)
            Bitmap btDondur = new Bitmap(arka_resim.Height, arka_resim.Width);

            if (dondurliste.GetSelected(0))
            {


                for (int i = 0; i < arka_resim.Height; i++)
                {
                    for (int j = 0; j < arka_resim.Width; j++)
                    {
                        Color r = arka_resim.GetPixel(j, i);


                        btDondur.SetPixel(arka_resim.Height - i - 1, j, r);


                    }
                }
            }

            else
            {
                for (int y = 0; y < arka_resim.Height; y++)
                {
                    for (int x = 0; x < arka_resim.Width; x++)
                    {
                        Color r = arka_resim.GetPixel(x, y);

                        btDondur.SetPixel(y, arka_resim.Width - 1 - x, r);


                    }
                }
            }
            arka_geri = new Bitmap(arka_resim);
            arka_resim = btDondur;

            int temp_ara = yukseklik_ekran;
            yukseklik_ekran = genislik_ekran;
            genislik_ekran = temp_ara;

            geri = new Bitmap(ekran.Image);
            boyutlandir();


            hangi_buton = 5;



            ekran.Visible = false;

            Bitmap rsm = new Bitmap(ekran.Image);
            resimekle(rsm);
            ekran.Visible = true;


            dondurliste.Visible = false;

        }








        private void button6_Click(object sender, EventArgs e)
        {
            // pictureBox1.Visible = false;
            hst.Visible = false;
            renk_kanali.Visible = false;

            aynaliste.Visible = false;
            dondurliste.Visible = false;

            genislik_txt.Visible = true;
            yukseklik_txt.Visible = true;

            resize.Visible = true;


            if (ekran.Image == null)
            {
                MessageBox.Show("Önce Bir Resim Seçin");
            }
            else
            {
                this.Controls.Add(genislik_txt);
                this.Controls.Add(yukseklik_txt);


            }


        }


        private void genislik_txt_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";

        }


        private void yukseklik_txt_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
        }

        int yukseklik_onceki, genislik_onceki;

        void resizeClickOneEvent(object sender, EventArgs e)
        {
            basildi = 0;
            Button button = sender as Button;

            int sayac = 0;

            string tString = genislik_txt.Text;

            for (int i = 0; i < tString.Length; i++)
            {
                if (!char.IsNumber(tString[i]))
                {
                    sayac = 1;
                    genislik_txt.Text = "";

                }
            }

            string tString2 = yukseklik_txt.Text;
            for (int i = 0; i < tString2.Length; i++)
            {
                if (!char.IsNumber(tString2[i]))
                {
                    sayac = 1;
                    yukseklik_txt.Text = "";

                }
            }


            if (sayac == 0 && genislik_txt.Text.Length != 0 && yukseklik_txt.Text.Length != 0)
            {

                int genislik_gecici = Int32.Parse(genislik_txt.Text);
                int yukseklik_gecici = Int32.Parse(yukseklik_txt.Text);



                if (yukseklik_gecici <= ekran.Height && genislik_gecici <= ekran.Width)
                {
                    geri = new Bitmap(ekran.Image);
                    arka_geri = new Bitmap(arka_resim);


                    yukseklik_onceki = yukseklik_ekran;
                    genislik_onceki = genislik_ekran;


                    genislik_ekran = genislik_gecici;
                    yukseklik_ekran = yukseklik_gecici;

                    boyutlandir();



                    hangi_buton = 6;


                }
                else
                {
                    MessageBox.Show("Yükseklik veya Genişlik değeri ekran boyutundan fazladır.");
                }



            }
            else
                MessageBox.Show("Geçersiz Değer Girdiniz!");
        }


        private void button7_Click(object sender, EventArgs e)
        {
            hst.Visible = false;
            renk_kanali.Visible = false;

            aynaliste.Visible = false;
            dondurliste.Visible = false;

            genislik_txt.Visible = false;
            yukseklik_txt.Visible = false;

            resize.Visible = false;

            if (ekran.Image == null)
            {
                MessageBox.Show("Önce Bir Resim Seçin");

            }

            else
            {
                Image<Bgr, byte> imgInput = new Image<Bgr, byte>(ofd.FileName);
                arka_geri = null;
                arka_resim = null;
                geri = null;
                degisken = 0;
                resimekle(imgInput.ToBitmap());
            }



        }

        private void button8_Click(object sender, EventArgs e)
        {
            //pictureBox1.Visible = false;
            hst.Visible = false;
            renk_kanali.Visible = false;
            aynaliste.Visible = false;
            dondurliste.Visible = false;

            genislik_txt.Visible = false;
            yukseklik_txt.Visible = false;

            resize.Visible = false;

            if (ekran.Image == null)
            {
                MessageBox.Show("Önce Bir Resim Seçin");

            }
            else
            {


                geri = new Bitmap(ekran.Image);
                arka_geri = new Bitmap(arka_resim);

                if (gri != 1)
                {
                    Bitmap bt = arka_resim;
                    for (int y = 0; y < bt.Height; y++)
                    {
                        for (int x = 0; x < bt.Width; x++)
                        {
                            Color c = bt.GetPixel(x, y);

                            int r = c.R;
                            int g = c.G;
                            int b = c.B;
                            byte gray = (byte)(.21 * c.R + .71 * c.G + .071 * c.B);
                            //textBox1.Text = gray.ToString();

                            bt.SetPixel(x, y, Color.FromArgb(c.A, gray, gray, gray));
                        }
                    }


                }
                basildi = 0;
                kontrol();
                boyutlandir();
                kontrolters();

                hangi_buton = 8;

                gri = 1;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

            if (arka_resim != null)
            {
                Bitmap eski_ekran = new Bitmap(ekran.Image);
                boyutlandir();
                Image<Bgr, byte> imgInput = new Image<Bgr, byte>(new Bitmap(ekran.Image));
                ekran.Image = eski_ekran;

                imgInput.Save(ofd.FileName);
            }
            else
            {
                MessageBox.Show("Önce Bir Resim Seçin");
            }





        }

        private void button9_Click(object sender, EventArgs e)
        {
            //pictureBox1.Visible = false;
            hst.Visible = false;
            aynaliste.Visible = false;
            dondurliste.Visible = false;

            genislik_txt.Visible = false;
            yukseklik_txt.Visible = false;

            resize.Visible = false;

            if (ekran.Image == null)
            {
                MessageBox.Show("Önce Bir Resim Seçin");

            }
            else
            {

                this.Controls.Add(renk_kanali);
                renk_kanali.BringToFront();
                renk_kanali.Visible = true;


            }


        }



        private void renk_kanali_SelectedValueChanged(object sender, EventArgs e)
        {
            Bitmap bt = new Bitmap(arka_resim);
            basildi = 0;
            hangi_buton = -1;
            if (renk_kanali.GetSelected(0) && renk_k == 0)
            {
                for (int y = 0; y < bt.Height; y++)
                {
                    for (int x = 0; x < bt.Width; x++)
                    {
                        Color c = bt.GetPixel(x, y);

                        int a = c.A;
                        int r = c.R;
                        int g = c.G;
                        int b = c.B;
                        bt.SetPixel(x, y, Color.FromArgb(a, r, 0, 0));


                    }

                }
                //ekran.Image = bt;
                renk_k = 1;

            }
            else if (renk_kanali.GetSelected(1) && renk_y == 0)
            {
                for (int y = 0; y < bt.Height; y++)
                {
                    for (int x = 0; x < bt.Width; x++)
                    {
                        Color c = bt.GetPixel(x, y);

                        int a = c.A;
                        int r = c.R;
                        int g = c.G;
                        int b = c.B;
                        //byte gray = (byte)(.21 * c.R + .71 * c.G + .071 * c.B);
                        bt.SetPixel(x, y, Color.FromArgb(a, 0, g, 0));

                    }

                }
                //ekran.Image = bt;
                renk_y = 1;


            }
            else if (renk_kanali.GetSelected(2) && renk_m == 0)
            {


                for (int y = 0; y < bt.Height; y++)
                {
                    for (int x = 0; x < bt.Width; x++)
                    {
                        Color c = bt.GetPixel(x, y);

                        int a = c.A;

                        int r = c.R;
                        int g = c.G;
                        int b = c.B;
                        //byte gray = (byte)(.21 * c.R + .71 * c.G + .071 * c.B);
                        bt.SetPixel(x, y, Color.FromArgb(a, 0, 0, b));

                    }


                }
                //ekran.Image = bt;
                renk_m = 1;


            }
            else if (renk_kanali.GetSelected(3))
            {
                bt = new Bitmap(arka_resim);
                //ekran.Image = bt;
            }

            //ekran.Image = bt;

            kontrol();
            ekran.Image = bt;
            Bitmap deneme = new Bitmap(arka_resim);
            arka_resim = bt;

            boyutlandir();
            arka_resim = deneme;
            kontrolters();
            renk_kanali.Visible = false;
        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
