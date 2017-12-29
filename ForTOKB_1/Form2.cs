
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ForTOKB_1_1;
using ForTOKB_3;
using ForTOKB_1;

namespace ForTOKB_2
{
   
    public partial class Form2 : Form
    {   
        public Form2()
        {    
            InitializeComponent();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            if ((String.IsNullOrWhiteSpace(textBox1.Text)) || (String.IsNullOrWhiteSpace(textBox2.Text)) || (String.IsNullOrWhiteSpace(textBox3.Text)))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                string str, strOldDecrypt;
                string oldKey;
                string passOld, passNew, EncrPass2, EncrPassNew;
                passOld = textBox1.Text;
                oldKey = Convert.ToString(textBox3.Text);
                string commonKey;
                string KeyWord;
                int startSeed;
                const int LengthForKey = 3;
                passNew = textBox2.Text;
                CaesarCipher NewAB = new CaesarCipher();
                if (NewAB.CheckingForRus(passOld) == 1 && NewAB.CheckingForRus(passNew) == 1)
                {
                    startSeed = NewAB.For_Shift();
                    KeyWord = NewAB.RandomKeyFor(LengthForKey, startSeed);
                    EncrPass2 = NewAB.Encryption(passOld, KeyWord);
                    FileStream file1 = new FileStream("password.txt", FileMode.Open);
                    using (StreamReader fr = new StreamReader(file1))
                    {           //считывание пароля из файла

                        str = fr.ReadLine().ToString();
                    }
                    strOldDecrypt = NewAB.Decryption(str, oldKey);
                    commonKey = oldKey;
                    //ЗАПИСЬ НОВОГО ПАРОЛЯ, ЕСЛИ СТАРЫЙ ПАРОЛЬ СОВПАДАЕТ
                    if (strOldDecrypt.ToUpper() == passOld.ToUpper())
                    {
                        file1.Close();
                        startSeed = NewAB.For_Shift();
                        KeyWord = NewAB.RandomKeyFor(LengthForKey, startSeed);
                        EncrPassNew = NewAB.Encryption(passNew, KeyWord);
                        FileStream file2 = new FileStream("password.txt", FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(file2))
                        {

                            // sw.Position = 0;
                            sw.WriteLine(EncrPassNew);
                            //   sw.Position = 1;
                            label5.Text = "Ваш ключ " + KeyWord;
                            commonKey = KeyWord;
                        }
                        file2.Close();

                        // MessageBox.Show("Пароль успешно изменен, Ваш ключ " + commonKey);

                        Form3 newForm3_3 = new Form3();
                        newForm3_3.Show();
                        newForm3_3.label1.Text = "Вы успешно изменили пароль";
                        newForm3_3.label3.Text = Convert.ToString(commonKey);

                        this.Close();

                    }

                    else
                    {
                        MessageBox.Show("Старый пароль не совпадает");
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя вводить русские буквы!");
                }
            }
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if (!(l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("Вводите только латинские символы");
            }
        }

        public void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
