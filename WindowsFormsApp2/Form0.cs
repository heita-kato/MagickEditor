using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form0 : Form
    {
        public Form0()
        {
            InitializeComponent();
            // フォームのロード時に遷移を開始
            //this.Load += Form0_Load;
        }

        private async void Form0_Load(object sender, EventArgs e)
        {
            // 2秒待機
            await Task.Delay(2000);
            // 次のフォームに遷移
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide(); // 現在のフォームを非表示にする
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
      
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
