using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace 产品筛选
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //m_datatable.Columns.Add("ID", typeof(string));
            //m_datatable.Columns.Add("标题", typeof(string));
            //m_datatable.Columns.Add("最小价格", typeof(double));
            //m_datatable.Columns.Add("最大价格", typeof(double));
            //m_datatable.Columns.Add("30天内交易成功数", typeof(int));
            //m_datatable.Columns.Add("30天内已出售", typeof(int));
            //m_datatable.Columns.Add("sku数", typeof(int));
            //m_datatable.Columns.Add("累计评论数", typeof(int));
            //m_datatable.Columns.Add("关键字", typeof(string));


        }
        private ProductDataset.ProductDataTable m_datatable = new ProductDataset.ProductDataTable();
        private HashSet<string> lstkeywords = new HashSet<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (form2.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            //导入
            OpenFileDialog form = new OpenFileDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var stream = form.OpenFile();
                string extension = Path.GetExtension(form.FileName);
                IExcelDataReader excelReader = null;
                if (extension == ".xls")
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (extension == ".xlsx")
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                //将数据转为datatable
                m_datatable.Clear();
                lstkeywords.Clear();
                foreach (DataRow row in excelReader.AsDataSet().Tables[0].Rows)
                {
                    string id = row[Form2.idcolindex]?.ToString();
                    string title = row[Form2.titlecolindex]?.ToString();
                    double minprice;
                    double maxprice;
                    ParsePrice(row[Form2.pricecolindex]?.ToString(), out minprice, out maxprice);
                    int chengjiaonum;
                    if (!int.TryParse(row[Form2.chengjiaocolindex]?.ToString(), out chengjiaonum))
                    {
                        chengjiaonum = 0;
                    }
                    int yishounum;
                    if (!int.TryParse(row[Form2.yishoucolindex]?.ToString(), out yishounum))
                    {
                        yishounum = 0;
                    }
                    int skunum = 0;
                    try
                    {
                        JArray ja = (JArray)JsonConvert.DeserializeObject(row[Form2.skuscolindex]?.ToString());
                        //string str = jo["image"].ToString();
                        skunum = ja.Count;
                    }
                    catch (Exception ex)
                    {

                    }
                    int pinlunshu;
                    if (!int.TryParse(row[Form2.pinluncolindex]?.ToString(), out pinlunshu))
                    {
                        pinlunshu = 0;
                    }
                    string keyword = row[Form2.keywordcolindex]?.ToString();

                    lstkeywords.Add(keyword);
                    m_datatable.Rows.Add(id, title, minprice, maxprice, chengjiaonum, yishounum, skunum, pinlunshu, keyword);
                }
            }
            dataGridView1.DataSource = m_datatable;
            comboBox1.DataSource = lstkeywords.ToArray();
        }

        bool ParsePrice(string strprice, out double minprice, out double maxprice)
        {
            minprice = 0;
            maxprice = 0;
            try
            {
                var arr = strprice.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    minprice = maxprice = double.Parse(arr[0]);
                }
                if (arr.Length > 1)
                {
                    maxprice = double.Parse(arr[1]);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private ProductDataset.ProductDataTable curtalbe = null;
        private void button2_Click(object sender, EventArgs e)
        {
            string keyword = comboBox1.SelectedItem?.ToString();
            if(string.IsNullOrEmpty( keyword ))
            {
                keyword = "";
            }
            double minprice = Convert.ToDouble(numericUpDown4.Value);
            var tmp = from row in m_datatable
                      where row.关键字 == keyword && row._30天内交易成功数 >= numericUpDown1.Value
                      && row._30天内已出售 >= numericUpDown2.Value && row.SKU数 <= numericUpDown3.Value
                      && row.最小价格 >= minprice && row.累计评论数 >= numericUpDown5.Value
                      select row;
            ProductDataset.ProductDataTable table = new ProductDataset.ProductDataTable();
            foreach (var item in tmp)
            {
                table.ImportRow(item);
            }
            curtalbe = table;
            dataGridView1.DataSource = table;
        }
    }
}
