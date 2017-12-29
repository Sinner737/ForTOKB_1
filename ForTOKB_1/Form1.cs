using ForTOKB_1_1;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ForTOKB_3;

namespace ForTOKB_1
{   
    public partial class Form1 : Form
    {
        public int i1 = 0;  // для цикла из 3 вводов пароля
        bool visibility;
        int nForRepeatEncryption = 0; // проверка для повторного шифрования ключа при повторном входе
        string sForNewPassEncrypted;
        string KeyWord;
        int startSeed;
        const int LengthForKey = 3;
        public Form1()
        {
            
            InitializeComponent();
            FileStream file1 = new FileStream("password.txt", FileMode.OpenOrCreate);
            
            if (file1.Length != 0)                    // если файл не пустой, появляется поле для ввода ключа
            {
                nForRepeatEncryption = 1;    // значит повторное шифрование при входе повторном
                visibility = true;
                label2.Visible = true;
                label2.Enabled = true;
                textBox2.Visible = true;
                textBox2.Enabled = true;
                //используется в методе EnryptionNotBlank.., если файл не пустой (введеный ключ)
            }
            else
            {
                nForRepeatEncryption = 0;
                visibility = false;
                label2.Visible = false;
                label2.Enabled = false;
                textBox2.Visible = false;
                textBox2.Enabled = false;
            }
                file1.Close();
        
        }
        public string key1;
        
        public void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите пароль");
            }
            else
            {  
            if (visibility == true)
                {
                    if (textBox2.Text.Length == 0)
                    {
                        MessageBox.Show("Введите ключ");
                    }
                }
            CaesarCipher AB = new CaesarCipher();
            string pass, str, strDecrypt;
            FileStream file1 = new FileStream("password.txt", FileMode.Open);
            pass = textBox1.Text;
            
            if (AB.CheckingForRus(pass) == 1)
            {
                            //зашифрованный пароль
                if (i1 == 2)
                {
                    MessageBox.Show("Отказано в доступе");
                    file1.Close();
                    Application.Exit();
                }
                if (file1.Length == 0)            // если файл пустой, то пароль в файл
                {
                    startSeed = AB.For_Shift();
                    KeyWord = AB.RandomKeyFor(LengthForKey, startSeed);
                    string EncrPass1 = AB.Encryption(pass, KeyWord );
                    using (StreamWriter sw = new StreamWriter(file1))          //создание потока для записи
                    {
                        sw.WriteLine(EncrPass1);
                    }
                    strDecrypt = pass;
                    key1 = KeyWord;
                }
                else                           // если файл не пустой (там есть зашифрованный пароль)
                {
                    key1 = Convert.ToString(textBox2.Text);
                    string EncrPass1 = AB.Encryption(pass, key1);
                    using (StreamReader fr = new StreamReader(file1))           //считывание пароля из файла
                    {

                        //   fr.Position = 0;
                        str = fr.ReadLine().ToString();
                        // fr.Position = 1;
                        strDecrypt = AB.Decryption(str, key1);     // расшифрованный пароль из файла
                    }
                }              
                file1.Close();
                if (pass.ToUpper() == strDecrypt.ToUpper())
                {   
                    if (nForRepeatEncryption == 1) //если пароль уже был в файле, он зашифровывается по новому ключу и записывается в файл
                        {
                         startSeed = AB.For_Shift();
                         KeyWord = AB.RandomKeyFor(LengthForKey, startSeed);
                         sForNewPassEncrypted = AB.Encryption(pass, KeyWord);
                         File.WriteAllText("password.txt", sForNewPassEncrypted);
                         key1 = KeyWord;
                        }
                    Form3 newForm3 = new Form3();
                    newForm3.Show();
                    newForm3.label3.Text = key1;
                }
                else
                {
                    i1++;
                    MessageBox.Show("Неверный пароль");
                }

            }
            else
            {
                MessageBox.Show("Нельзя вводить русские буквы!");
                    file1.Close();
            }
                    }
             
        }

       
    /*    private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57)  (e.KeyChar ))
            {
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("Вводите только латинские символы");
            }
            
        }*/
        private void button2_Click (object sender, EventArgs e)
        {
            this.Close();

        }

    }
}
