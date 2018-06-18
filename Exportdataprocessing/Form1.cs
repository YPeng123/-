using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exportdataprocessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“yiyilandbDataSet.products”中。您可以根据需要移动或删除它。
            this.productsTableAdapter.Fill(this.yiyilandbDataSet.products);
        }

        private DataTable maintable = null;

        private string 哪个市场(string url)
        {
            if (string.IsNullOrEmpty(url)) return "未知";
            if (url.Contains("detail")) return "天猫";
            else if (url.Contains("taobao")) return "淘宝";
            return "未知";
        }

        private void DaoRu()
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
                maintable = excelReader.AsDataSet().Tables[0];
                foreach (DataRow row in maintable.Rows)
                {
                    string id = row[Form2.idcolindex]?.ToString();
                    string url = row[Form2.urlcolindex]?.ToString();
                    string title = row[Form2.titlecolindex]?.ToString();
                    string market = 哪个市场(url);
                    decimal minprice;
                    decimal maxprice;
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
                        if(ja.Count>0)
                        {
                            skunum = ja[0]["values"].Count();
                        }
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

                    //this.yiyilandbDataSet.products.AddproductsRow(id, url, title, skunum, minprice, maxprice, chengjiaonum, yishounum, pinlunshu, keyword, market);
                    using (yiyilandbEntities dbContext = new yiyilandbEntities())
                    {
                        bool isnew = false;
                        products pro = dbContext.products.Find(id);
                        if (pro == null)
                        {
                            pro = new products();
                            pro.id = id;
                            isnew = true;
                        }
                        pro.url = url;
                        pro.title = title;
                        pro.skunum = skunum;
                        pro.minprice = minprice;
                        pro.maxprice = maxprice;
                        pro.minchengjiao = chengjiaonum;
                        pro.minyishou = yishounum;
                        pro.review_quantity = pinlunshu;
                        pro.keyword = keyword;
                        pro.market = market;
                        if(isnew)
                        {
                            dbContext.Set<products>().Add(pro);
                        }
                        dbContext.SaveChanges();
                    }


                    //lstkeywords.Add(keyword);
                    //m_datatable.Rows.Add(id, title, minprice, maxprice, chengjiaonum, yishounum, skunum, pinlunshu, keyword);

                }
            }
            //dataGridView1.DataSource = m_datatable;
            //comboBox1.DataSource = lstkeywords.ToArray();
        }


        bool ParsePrice(string strprice, out decimal minprice, out decimal maxprice)
        {
            minprice = 0;
            maxprice = 0;
            try
            {
                var arr = strprice.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    minprice = maxprice = decimal.Parse(arr[0]);
                }
                if (arr.Length > 1)
                {
                    maxprice = decimal.Parse(arr[1]);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void btndaoru_Click(object sender, EventArgs e)
        {
            DaoRu();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            this.yiyilandbDataSet.products.Clear();
            this.productsTableAdapter.Fill(this.yiyilandbDataSet.products);
        }
    }
}
