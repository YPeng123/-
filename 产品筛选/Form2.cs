using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 产品筛选
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            textBox1.Text = idcolindex.ToString();
            textBox2.Text = titlecolindex.ToString();
            textBox3.Text = skuscolindex.ToString();
            textBox4.Text = pricecolindex.ToString();
            textBox5.Text = chengjiaocolindex.ToString();
            textBox6.Text = yishoucolindex.ToString();
            textBox7.Text = pinluncolindex.ToString();
            textBox8.Text = keywordcolindex.ToString();
        }

        public static int idcolindex = 0;
        public static int titlecolindex = 1;
        public static int skuscolindex = 7;
        public static int pricecolindex = 3;
        public static int chengjiaocolindex = 5;
        public static int yishoucolindex = 6;
        public static int pinluncolindex = 8;
        public static int keywordcolindex = 18;


        private void button1_Click(object sender, EventArgs e)
        {
            idcolindex = int.Parse(textBox1.Text);
            titlecolindex = int.Parse(textBox2.Text);
            skuscolindex = int.Parse(textBox3.Text);
            pricecolindex = int.Parse(textBox4.Text);
            chengjiaocolindex = int.Parse(textBox5.Text);
            yishoucolindex = int.Parse(textBox6.Text);
            pinluncolindex = int.Parse(textBox7.Text);
            keywordcolindex = int.Parse(textBox8.Text);
            DialogResult = DialogResult.OK;
        }
    }
}
