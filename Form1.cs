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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string fileContent , filePath;
        private IEnumerable<string> inp;
        private List<string> outp = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            bool go = false;
                foreach (string a in inp)
                {
                    if (a.Contains("G0"))
                    {
                        if (go)
                        {
                            outp.Add(a);
                        }
                        else
                        {
                            go = true;
                            outp.Add("M5");
                            outp.Add(a);
                        }
                    }
                    else
                    {
                        if (go)
                        {
                            go = false;
                            outp.Add("M3");
                            outp.Add(a);
                        }
                        else
                        {
                            outp.Add(a);
                        }
                    }
                }


            textBox2.Text = String.Join(Environment.NewLine, outp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox4.Text != "")
            {
                string path = textBox4.Text;
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (string c in outp)
                    {
                        sw.WriteLine(c);
                    }
                }

                MessageBox.Show("The file is Generated Successfully");
            }
            else
            {
                MessageBox.Show("Please Enter the file Name !");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Savefilename;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Savefilename = saveFileDialog1.FileName;
                textBox4.Text = Savefilename;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog1.FileName;
                textBox3.Text = filePath;

                //Read the contents of the file into a stream
                var fileStream = openFileDialog1.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }

                inp = File.ReadLines(filePath);
                textBox1.Text = fileContent;
            }
        }
    }
}
