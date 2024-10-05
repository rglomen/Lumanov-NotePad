using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Resources;
using System.Reflection;
using System.Threading;
/*
 * Powerd By Resul Lömen
 * 
 */
namespace HN03
{
    public partial class HN03 : Form
    {
        public HN03()
        {
            InitializeComponent();
        }
        public static string gonderilenbilgi;
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        public void klasoracsil()
        {
            Directory.CreateDirectory(formlar.yedekklasorunyolu);//yedeks isminde klasör açıcak klasörün var olma ihtimali olmucak.
            System.IO.Directory.Delete(formlar.yedekklasorunyolu, true);// yedeks içindekilerle beraber silecek.
            Directory.CreateDirectory(formlar.yedekklasorunyolu);//yedeks isminde klasör açıcak klasörün var olma ihtimali olmucak.
            Directory.CreateDirectory(formlar.klasorunyolu);
        }
        public void farklikaydet()
        {
            if (calismaalani.Text == "" || calismaalani.Text == " ") // Eğer başlık ve içerik boş bırakılırsa Boş belge kaydetemezsiniz diye hata mesaj vericek.
            {
                MessageBox.Show("Boş belge kaydetemezsiniz...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);//messagebox ta gösterilecek hata mesajı.
            }
            else // Eğer başlık ve içerik boş değilse buradaki kod satırı yapıacak
            {
                try //Dosya adı geçersiz karakterler girilirse veya farklı bir hata olursa catch işlemi yapıcak.
                {
                    saveFileDialog1.Title = "Lütfen kayıt yerini seçiniz.";
                    //Pencere Açıldığında txt ve uzantılı ve diğer tüm dosyaların gözükmesini sağladık...
                    saveFileDialog1.Filter = "(*.txt)|*.txt|Tüm dosyalar(*.*)|*.*";
                    saveFileDialog1.FileName = "";
                    DialogResult drSecim = saveFileDialog1.ShowDialog();
                    if (drSecim == DialogResult.OK)
                    {
                        using (StreamWriter yaz = new StreamWriter(saveFileDialog1.FileName))
                        {
                            yaz.WriteLine(calismaalani.Text);//oluşturduğumuz bu dosyanın içeriğine richtextboxxtaki yazıları atadık
                            yaz.Close(); // ve yazma işlemini bitirdik.
                            yaz.Dispose();//Ramden yer kazanmak için.
                            calismaalani.Clear();
                            MessageBox.Show("Başarıyla kayıt edildi.", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        // Dosyanın yolunu,başlığını ve sonunda ise uzantısını belirttik.
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
                catch //Dosya adı geçersiz karakterler girilirse veya farklı bir hata olursa burada ki işlemler yapıcak.
                {
                    MessageBox.Show("Bir dosya adı başlıktaki açıklamada belirtilen karakterleri içeremez...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); //Hata mesajı
                }
                // Kayıt ekle sekmesi Kayıt Ekle butonu kod satırı bitti. 
            }
        }
        public void yeni()
        {
            if (calismaalani.Text == "" || calismaalani.Text == " ") { }
            else {
                     DialogResult dr = MessageBox.Show("Dosya kaydedilmemiş. Kaydetmek ister misiniz?", "Pardon", MessageBoxButtons.YesNo);
                     if (dr == DialogResult.Yes)
                     {
                         farklikaydet();
                     }else
                     {
                         calismaalani.Clear();
                     }
                 }
        }
        public void ac()
        {
            openFileDialog1.Title = "Lütfen dosyayı seçiniz.";
            //Pencere Açıldığında txt ve uzantılı ve diğer tüm dosyaların gözükmesini sağladık...
            openFileDialog1.Filter = "(*.txt)|*.txt|Tüm dosyalar(*.*)|*.*";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo dosyabilgisi = new FileInfo(openFileDialog1.FileName);//yedek dosyanın bilgileri çağırılacak.
                using (TextReader oku = dosyabilgisi.OpenText())//başlığı ve içeriği okunacak.
                {
                    calismaalani.Text = oku.ReadToEnd();
                    oku.Close();
                    oku.Dispose();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        #region Dosya Menüsü
        private void DosyaYeni_Click(object sender, EventArgs e)
        {
            yeni();
        }

        private void DosyaAc_Click(object sender, EventArgs e)
        {
            ac();
        }

        private void DosyaHizliKaydet_Click(object sender, EventArgs e)
        {
            if (calismaalani.Text == "" || calismaalani.Text == " ")
            {
                MessageBox.Show("Boş belge kaydetemezsiniz...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); //Hata mesajı
            }
            else
            {
                farklikaydet();
            } 
        }

        private void DosyaFarkliKaydet_Click(object sender, EventArgs e)
        {
            farklikaydet();
        }

        private void yazdırToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DosyaCikis_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        #endregion 

        #region Düzenle Menüsü
        private void DuzenleGeriAl_Click(object sender, EventArgs e)
        {
            calismaalani.Undo();
        }

        private void DuzenleYinele_Click(object sender, EventArgs e)
        {
            calismaalani.Redo();
        }

        private void DuzenleKes_Click(object sender, EventArgs e)
        {
            calismaalani.Cut();
        }

        private void DuzenleKopyala_Click(object sender, EventArgs e)
        {
            calismaalani.Copy();
        }

        private void DuzenleYapistir_Click(object sender, EventArgs e)
        {
            calismaalani.Paste();
        }

        private void DuzenleTemizle_Click(object sender, EventArgs e)
        {
            calismaalani.Clear();
        }

        private void DuzenleTumunuSec_Click(object sender, EventArgs e)
        {
            calismaalani.SelectAll();
        }
        #endregion

        #region Soru işareti Menüsü

        private void YardimHakkinda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu program C# Ders Örnekleri Blog Sayfası için basit örnek olsun diye hazırlanmıştır.\nhttps://csharpdersornekleri.blogspot.com.tr/");
        }
        #endregion

        #region Hızlı Menü
        private void hizlimenuyeni_Click(object sender, EventArgs e)
        {
            yeni();
        }
        private void hizlimenuac_Click(object sender, EventArgs e)
        {
            ac();
        }
        private void hizlimenuhizlikaydet_Click(object sender, EventArgs e)
        {
            if (calismaalani.Text == "" || calismaalani.Text == " ")
            {
                MessageBox.Show("Boş belge kaydetemezsiniz...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); //Hata mesajı
            }
            else 
            {
                farklikaydet();
            } 
        }

        private void hizlimenuyazdir_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void hizlimenukes_Click(object sender, EventArgs e)
        {
            calismaalani.Cut();
        }

        private void hizlimenukopyala_Click(object sender, EventArgs e)
        {
            calismaalani.Copy();
        }

        private void hizlimenuyapistir_Click(object sender, EventArgs e)
        {
            calismaalani.Paste();
        }

        private void hizlimenubold_Click(object sender, EventArgs e)
        {
            if (calismaalani.SelectionFont != null)
            {
                System.Drawing.Font currentFont = calismaalani.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (calismaalani.SelectionFont.Bold == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Bold;
                }

                calismaalani.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void hizlimenuitalic_Click(object sender, EventArgs e)
        {
            if (calismaalani.SelectionFont != null)
            {
                System.Drawing.Font currentFont = calismaalani.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (calismaalani.SelectionFont.Italic == true)
                {
                    newFontStyle = FontStyle.Italic;
                }
                else
                {
                    newFontStyle = FontStyle.Italic;
                }

                calismaalani.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void hizlimenuustunuciz_Click(object sender, EventArgs e)
        {
            if (calismaalani.SelectionFont != null)
            {
                System.Drawing.Font currentFont = calismaalani.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (calismaalani.SelectionFont.Strikeout == true)
                {
                    newFontStyle = FontStyle.Strikeout;
                }
                else
                {
                    newFontStyle = FontStyle.Strikeout;
                }

                calismaalani.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void hizlimenualtiniciz_Click(object sender, EventArgs e)
        {
            if (calismaalani.SelectionFont != null)
            {
                System.Drawing.Font currentFont = calismaalani.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (calismaalani.SelectionFont.Underline == true)
                {
                    newFontStyle = FontStyle.Underline;
                }
                else
                {
                    newFontStyle = FontStyle.Underline;
                }

                calismaalani.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void hizlimenusolayasla_Click(object sender, EventArgs e)
        {
            calismaalani.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void hizlimenuortala_Click(object sender, EventArgs e)
        {
            calismaalani.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void hizlimenusagayasla_Click(object sender, EventArgs e)
        {
            calismaalani.SelectionAlignment = HorizontalAlignment.Right;
        }
        private void hizlimenuyardim_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu program C# Ders Örnekleri Blog Sayfası için basit örnek olsun diye hazırlanmıştır.\nhttps://csharpdersornekleri.blogspot.com.tr/");
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            calismaalani.Font = fontDialog1.Font;
            calismaalani.ForeColor = fontDialog1.Color;
        }

        #endregion


    }
}
