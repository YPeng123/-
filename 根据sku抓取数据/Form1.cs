using Excel;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 根据sku抓取数据
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        HashSet<string> hashsetkeyword = new HashSet<string>();
        int totalcount = 0;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            idcolindex = int.Parse(textBox1.Text);
            skuscolindex = int.Parse(textBox2.Text);
            imagescolindex = int.Parse(textBox3.Text);
            detailscolindex = int.Parse(textBox4.Text);
            keywordcolindex = int.Parse(textBox5.Text);

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
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();
                totalcount = 0;
                int count = result.Tables[0].Rows.Count;
                Thread th = new Thread(() =>
                  {
                      Parallel.ForEach<DataRow>(result.Tables[0].AsEnumerable(), row =>
                      {
                          Parse(row);
                          lock (this)
                          {
                              totalcount++;
                              this.BeginInvoke(new MethodInvoker(() =>
                              {
                                  labelcount.Text = string.Format("{0}/{1}", totalcount, count);
                              }));
                          }
                      });
                        
                  });
                th.IsBackground = true;
                th.Start();
                
            }
            form.Dispose();
        }
        int idcolindex = 0;
        int skuscolindex = 11;
        int imagescolindex = 16;
        int detailscolindex = 17;
        int keywordcolindex = 18;
        private void Parse(DataRow row)
        {
            string id = row[idcolindex].ToString();
            string skus = row[skuscolindex].ToString();
            string images = row[imagescolindex].ToString();
            string details = row[detailscolindex].ToString();
            string keyword = row[keywordcolindex].ToString();

            string strroot = Path.Combine(Application.StartupPath, "数据");
            string strpath = Path.Combine(strroot, keyword);
            string stridpath = Path.Combine(strpath, id);
            string strskupath = Path.Combine(stridpath, "sku");
            string strimagespath = Path.Combine(stridpath, "标题图片");
            string strdetailspath = Path.Combine(stridpath, "详情图片");
            var directory = Directory.CreateDirectory(strpath);
            Directory.CreateDirectory(stridpath);
            Directory.CreateDirectory(strskupath);
            Directory.CreateDirectory(strimagespath);
            Directory.CreateDirectory(strdetailspath);

            try
            {
                JArray jo = (JArray)JsonConvert.DeserializeObject(skus);
                //string str = jo["image"].ToString();
                foreach (var item in jo)
                {
                    foreach (var value in item["values"])
                    {
                        string strdesc = value["desc"].ToString();
                        string imageuri = value["image"]?.ToString();
                        if (!string.IsNullOrEmpty(imageuri))
                        {
                            try
                            {
                                //下载图片
                                //DownloadOneFileByURL(strdesc, imageuri, strskupath, 8000000);
                                DownloadOneFileByURLWithWebClient(strdesc, imageuri, strskupath);
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            } //下载sku图片

            try
            {
                JArray ja = (JArray)JsonConvert.DeserializeObject(images);
                int count = 1;
                foreach (var item in ja)
                {
                    string struri = item["image_url"].ToString();
                    if (!string.IsNullOrEmpty(struri))
                    {
                        //下载图片
                        DownloadOneFileByURLWithWebClient(count.ToString(), struri, strimagespath);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {

            }//下载商品图片
            try
            {
                string[] strarr = details.Split(new char[] { ' ', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
                int count = 1;
                foreach (var item in strarr)
                {
                    if (item.Contains("src=\""))
                    {
                        string strimage = item.Substring(5, item.Length - 6);
                        if (!string.IsNullOrEmpty(strimage))
                        {
                            //下载图片
                            DownloadOneFileByURLWithWebClient(count.ToString(), strimage, strdetailspath);
                            count++;

                        }
                    }
                }
            }
            catch (Exception)
            {

            }//下载详情
        }
        public static void DownloadOneFileByURL(string fileName, string url, string localPath, int timeout)
        {
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;
            request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            request.Timeout = timeout;//8000 Not work ?
            response = (System.Net.HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            BinaryReader br = new BinaryReader(s);
            //int length2 = Int32.TryParse(response.ContentLength.ToString(), out 0);
            int length2 = Int32.Parse(response.ContentLength.ToString());
            byte[] byteArr = new byte[length2];
            s.Read(byteArr, 0, length2);
            fileName = fileName.Replace("*", "乘");
            string filepath = Path.Combine(localPath, fileName + ".jpg");
            if (File.Exists(filepath)) { File.Delete(filepath); }
            if (Directory.Exists(localPath) == false) { Directory.CreateDirectory(localPath); }
            FileStream fs = File.Create(filepath);
            fs.Write(byteArr, 0, length2);
            fs.Close();
            br.Close();
        }

        public static void DownloadOneFileByURLWithWebClient(string fileName, string url, string localPath)
        {
            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    fileName = fileName.Replace("*", "乘");
                    string filepath = Path.Combine(localPath, fileName + ".jpg");
                    if (File.Exists(filepath)) { File.Delete(filepath); }
                    if (Directory.Exists(localPath) == false) { Directory.CreateDirectory(localPath); }
                    wc.DownloadFile(url, filepath);
                }
            }
            catch (Exception ex)
            {

            }
        }


    }
}
