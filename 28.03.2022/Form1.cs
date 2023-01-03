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
namespace _28._03._2022
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Saved URLs", 20000);
            if (File.Exists(dosyayolu))
            {
                getData();
            }
        }

        string dosyayolu = @"C:\Users\"+Environment.UserName+@"\Documents\SavedURLs.txt";
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtURL.Text.Trim() == "")
                MessageBox.Show("There is null input. You can't save null data.");
            else
            {
                String[] row = { txtURL.Text };
                var roww = new ListViewItem(row);
                listView1.Items.Add(roww);
                //--------------------------------------------
                string veri = txtURL.Text;
                StreamWriter sw = File.AppendText(dosyayolu);
                sw.WriteLine(veri);
                sw.Close();
                txtURL.Text = "";
                txtURL.Focus();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                int selected = listView1.SelectedItems[0].Index;
                listView1.Items.RemoveAt(selected);
                FileStream fs = new FileStream(dosyayolu, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    sw.WriteLine(listView1.Items[i].Text);
                }
                sw.Close();
                fs.Close();
            }
            getData();

        }


        private void getData()
        {
            listView1.Items.Clear();
            string row1;
            StreamReader sr = new StreamReader(dosyayolu);
            while (true)
            {

                row1 = sr.ReadLine();
                if (row1 == null) { break; }
                string[] row = { row1 };
                var roww = new ListViewItem(row);
                listView1.Items.Add(roww);

            }
            sr.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtURL.Text = "";
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string selected = listView1.SelectedItems[0].Text;
            System.Diagnostics.Process.Start(selected);
        }


        private void txtURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave_Click(this, new EventArgs());
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(this, new EventArgs());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
