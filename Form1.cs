using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace security_project
{
    public partial class Form1 : Form
    {

        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog sfd = new SaveFileDialog();  
        string path = "";
        FileStream sr;
        byte[] arrayByte;
        byte[] arrayEnc;
        int flag = -1;
        int counter = 0;


        public Form1()
        {
            InitializeComponent();
        }

        public int cipher(int number, byte[] Key1)  //Caesar cipher Encryption
        {
            counter = counter % Key1.Length;
            return ((number + Key1[counter]) % 256);
            counter++;
        }//Caesar cipher


        public int decipher(int number, byte[] Key1) //Caesar cipher Decryptin
        {
            counter = counter % Key1.Length;
            return (((number - Key1[counter]) + 256) % 256);
            counter++;
        }// De Caesear cipher

        public int Vernam(int number, byte[] Key1) //Vernam cipher Encryptin
        {
            counter = counter % Key1.Length;
            return (number ^ Key1[counter]) ;
            counter++;
        }// Vernam cipher

        public byte[] ColumnarEnc(int key, byte[] data) //Columner cipher Encryptin
        {
            byte[] res =new byte[data.Length];
            int k = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = i; j < data.Length; j += key)
                {
                    if (j < data.Length)
                    {
                        res[k] = data[j];
                        // MessageBox.Show(data.Length+" " +data[i].ToString());
                        k++;
                    }
                }//end loop j
            }//end loop i
            return res;
        }// Columner cipher Encryptin

        public byte[] ColumnarDec(int key, byte[] data) //Columner cipher Decryptin
        {
            byte[] res = new byte[data.Length];
            int k = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = i; j < data.Length; j += key)
                {
                    if (j < data.Length)
                    {
                        res[j] = data[k];
                        // MessageBox.Show(data.Length+" " +data[i].ToString());
                        k++;
                    }
                }//end loop j
            }//end loop i
            return res;
        }// Columner cipher Decryptin




        public void VernamCode()//vernam code
        {
            if (flag == 1)
            {

                string key;
                key = Interaction.InputBox("Enter the Key ", "key Box", "", -1, -1);
                byte[] KeyByte = Encoding.ASCII.GetBytes(key);
                arrayEnc = new byte[arrayByte.Length];

                for (int i = 0; i < arrayByte.Length; i++)
                {
                    int t = Convert.ToInt32(arrayByte[i]);
                    arrayEnc[i] = Convert.ToByte(Vernam(t, KeyByte));
                }//end for loop

                string str = Encoding.UTF8.GetString(arrayEnc);
                richTextBox2.Text = str;
                MessageBox.Show("Encyption is done select folder to save your encrypted file");
              if(  sfd.ShowDialog()==DialogResult.OK)
                File.WriteAllBytes(sfd.FileName, arrayEnc);

                sr.Close();
                Ref();

            }//end flag
            else
            {
                MessageBox.Show("Open the file you want to encrypt first");
            }
        }//end Vernam code


        private void button3_Click(object sender, EventArgs e)  //open file button
        {
            if (ofd.ShowDialog() == DialogResult.OK)              //اختيار الملف
            {
                textBox1.Text = ofd.SafeFileName;                 // اسم الملف
                textBox2.Text = ofd.FileName;                     // الباث
                path = ofd.FileName;                              //حفظ الباث في path

                sr = File.OpenRead(path);
                arrayByte = new byte[sr.Length];
                sr.Read(arrayByte, 0, arrayByte.Length);

                string str = Encoding.UTF8.GetString(arrayByte);
                richTextBox1.Text = str;
                flag = 1;
            }
        } // نهاية كبسة ال open file 


        private void button1_Click(object sender, EventArgs e)//Caesar Cipher
        {
            if (flag == 1)
            { 
            string key;
            key = Interaction.InputBox("Enter the Key ", "key Box", "", -1, -1);
            byte[] KeyByte = Encoding.ASCII.GetBytes(key);
            arrayEnc = new byte[arrayByte.Length];
            
                    for (int i = 0; i < arrayByte.Length; i++)
                    {
                        
                        int t=Convert.ToInt32(arrayByte[i]);
                        arrayEnc[i] = Convert.ToByte(cipher(t, KeyByte));

                    }//end for loop

                    string str = Encoding.UTF8.GetString(arrayEnc);
                    richTextBox2.Text = str;
                    MessageBox.Show("Encyption is done select folder to save your encrypted file");
                    if (sfd.ShowDialog()==DialogResult.OK)
                    {
                        File.WriteAllBytes(sfd.FileName, arrayEnc);
                    }
                    sr.Close();
                    Ref();

                }//end flag
            else
            {
                MessageBox.Show("Open the file you want to encrypt first");
            }
        }// end ceaser cipher 

        
        

         private void button2_Click(object sender, EventArgs e) //Caesar Cipher
         {
             if (flag == 1) {
             string key;
             key = Interaction.InputBox("Enter the Key ", "key Box", "", -1, -1);
             byte[] KeyByte = Encoding.ASCII.GetBytes(key);
             arrayEnc = new byte[arrayByte.Length];
             
                     for (int i = 0; i < arrayByte.Length; i++)
                     {

                         int t = Convert.ToInt32(arrayByte[i]);
                         arrayEnc[i] = Convert.ToByte(decipher(t, KeyByte));

                     }//end for loop
                     string str = Encoding.UTF8.GetString(arrayEnc);
                     richTextBox2.Text = str;
                     MessageBox.Show("Decyption is done select folder to save your encrypted file");
                     if(sfd.ShowDialog()==DialogResult.OK);
                     File.WriteAllBytes(sfd.FileName, arrayEnc);

                    sr.Close();
                    Ref();
                 }//end flag
             else
             {
                 MessageBox.Show("Open the file you want to decrypt first");
             }
         } //end of decryption Caesar


         private void button5_Click(object sender, EventArgs e) //Vernam Encryptin
         {
             VernamCode();

         }//end vernam Cipher Decryption

         private void button4_Click(object sender, EventArgs e)//Vernam Decryptin
         {
             VernamCode();
         }//end vernam cipher decryption

        

        public void Ref()
         {
             flag = -1;
             textBox1.Text = "";
             textBox2.Text = "";
             richTextBox1.Text = "";
             richTextBox2.Text = "";
             counter = 0;
         }

       

       

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)//Columnar Button
        {
            if (flag == 1)
            {
                string keys;
                keys = Interaction.InputBox("Enter the Key ", "key Box", "", -1, -1);
                byte[] KeyByte = Encoding.ASCII.GetBytes(key);
                if (KeyByte.Length>2)
                    keys =(( KeyByte[(KeyByte.Length) - 2] + KeyByte[(KeyByte.Length) - 1])%2).ToString();
                MessageBox.Show(keys);

                arrayEnc  = ColumnarEnc(int.Parse(key), arrayByte);
                            
                string str = Encoding.UTF8.GetString(arrayEnc);
                richTextBox2.Text = str;
                MessageBox.Show("Encyption is done select folder to save your encrypted file");
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    File.WriteAllBytes(sfd.FileName, arrayEnc);
                }
                sr.Close();
                Ref();

            }//end flag
            else
            {
                MessageBox.Show("Open the file you want to encrypt first");
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                string key;
                key = Interaction.InputBox("Enter the Key number ", "key Box", "", -1, -1);
                // arrayEnc = new byte[arrayByte.Length]; 

                arrayEnc = ColumnarDec(int.Parse(key), arrayByte);

                string str = Encoding.UTF8.GetString(arrayEnc);
                richTextBox2.Text = str;
                MessageBox.Show("Decryptin is done select folder to save your encrypted file");
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    File.WriteAllBytes(sfd.FileName, arrayEnc);
                }
                sr.Close();
                Ref();

            }//end flag
            else
            {
                MessageBox.Show("Open the file you want to encrypt first");
            }
        }//end columnar Button

       
         }//end of class form 1
        
    }

