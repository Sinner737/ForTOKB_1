using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// меняется ключ при повторном входе и зашифровывается по новому ключу

namespace ForTOKB_1_1
{
    public class CaesarCipher
    {
        public int key;
       // public static string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // длина 62 символа (61)
        public int Shift;
        public int For_Shift()               //генерация ключа
        {
            Random rand = new Random();
            key = rand.Next(1000);
            return key;
        }

        static char[] characters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                                                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                                'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0' };
        public int N = characters.Length;

        public string RandomKeyFor(int length, int startSeed)
        {
            Random rand = new Random(startSeed);

            string result = "";

            for (int i = 0; i < length; i++)
                result += characters[rand.Next(0, characters.Length)];

            return result;
        }
        public string Encryption(string input, string keyword)
        {
            input = input.ToUpper();
            keyword = keyword.ToUpper();

            string result = "";

            int keyword_index = 0;

            foreach (char symbol in input)
            {
                int c = (Array.IndexOf(characters, symbol) +
                    Array.IndexOf(characters, keyword[keyword_index])) % N;

                result += characters[c];

                keyword_index++;

                if ((keyword_index + 1) == keyword.Length)
                    keyword_index = 0;
            }

            return result;
        }
        public string Decryption(string input, string keyword)
        {
            input = input.ToUpper();
            keyword = keyword.ToUpper();

            string result = "";

            int keyword_index = 0;

            foreach (char symbol in input)
            {
                int p = (Array.IndexOf(characters, symbol) + N -
                    Array.IndexOf(characters, keyword[keyword_index])) % N;

                result += characters[p];

                keyword_index++;

                if ((keyword_index + 1) == keyword.Length)
                    keyword_index = 0;
            }

            return result;
        }
        
        public int CheckingForRus(string text1) {                                  //проврека на русские символы

            int Booll = 1; 
            string RusAlphabet = "абвгдеёжзийклмнопрстуфхцчщшьыъэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЬЫЪЭЮЯ";
            for (int i = 0; i < text1.Length; i++)
            {
                for (int j = 0; j < RusAlphabet.Length; j++)
                {
                    if (text1[i] == RusAlphabet[j])
                    {
                        Booll = 0;
                        return Booll;
                    }
                 }
              }          
            return Booll;
            }
        public string ForLabelOfForm3 ()
        {
            return Convert.ToString(Shift);
        }
        }

}




/*      public string EncryptionNotBlank(string text, int keyInput)            // если файл не пустой, то этот метод шифрует
       {
           Shift = keyInput;
           var res = new StringBuilder();
           for (int i = 0; i < text.Length; i++)
           {
               for (int j = 0; j < Alphabet.Length; j++)
               {
                   if (text[i] == Alphabet[j])
                       res.Append(Alphabet[(j + Shift) % Alphabet.Length]);
               }
           }
           return res.ToString();*
       }*/
/*  public string Encryption(string text)
  {
      Shift = For_Shift();
      var res = new StringBuilder();
      for (int i = 0; i < text.Length; i++)
      {
          for (int j = 0; j < Alphabet.Length; j++)
          {
              if (text[i] == Alphabet[j])
                  res.Append(Alphabet[(j + Shift) % Alphabet.Length]);
          }
      }
      return res.ToString();
  }*/
/*    public string Decryption(string crypt, int key2)
    {
        var res = new StringBuilder();
        for (int i = 0; i < crypt.Length; i++)
        {
            for (int j = 0; j < Alphabet.Length; j++)
            {
                if (crypt[i] == Alphabet[j])
                    res.Append(Alphabet[(j - key2 + Alphabet.Length) % Alphabet.Length]);
            }
        }
        return res.ToString();
    }*/
