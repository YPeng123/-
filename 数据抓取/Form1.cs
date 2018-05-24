using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using CsvHelper;
using System.Threading;
using SufeiUtil;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ScrapySharp.Extensions;

namespace 数据抓取
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            panel2.Controls.Add(webBrowser1);
            webBrowser1.Dock = DockStyle.Fill;
            panel3.Controls.Add(webBrowser2);
            webBrowser2.Dock = DockStyle.Fill;

            InitBrowser();

        }
        ChromiumWebBrowser webBrowser1 = new ChromiumWebBrowser("");
        ChromiumWebBrowser webBrowser2 = new ChromiumWebBrowser("");

        public void InitBrowser()
        {
            try
            {
                BrowserSettings bset = new BrowserSettings();
                bset.Plugins = CefState.Enabled;
                //关于跨域限制
                bset.WebSecurity = CefState.Disabled;
                webBrowser1.LifeSpanHandler = new OpenPageSelf(this);
                webBrowser1.BrowserSettings = bset;

                BrowserSettings bset2 = new BrowserSettings();
                bset.Plugins = CefState.Enabled;

                //关于跨域限制
                bset2.WebSecurity = CefState.Disabled;
                webBrowser2.BrowserSettings = bset2;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                string url_tmp = this.textBox1.Text;
                if (url_tmp != "")
                {
                    //this.Controls.Add(webBrowser1);
                    //webBrowser1.Navigate(url_tmp);
                    webBrowser1.Load(url_tmp);
                    //this.Controls.Add(webBrowser1);
                    //webBrowser1.Dock = DockStyle.Fill;
                    webBrowser1.Update();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public void SetURL(string url)
        {
            textBox1.Text = url;
        }
        public static string GetWebClient(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, Encoding.Default);//注意编码
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }
        private async void BtnTest_Click(object sender, EventArgs e)
        {
            //var browser = new ScrapingBrowser();
            //var html = browser.DownloadString(new Uri("http://www.cnblogs.com/"));

            string strsource = await webBrowser1.GetSourceAsync();

            string newfilepath = Path.Combine(Application.StartupPath, "产品数据" + ".csv");

            using (FileStream fs = new FileStream(newfilepath, FileMode.Create))
            using (TextWriter writer = new StreamWriter(fs, Encoding.GetEncoding("GB2312")))
            {
                var csv = new CsvWriter(writer);
                获取连接(strsource, csv);
            }

        }

        private void 获取连接(string strsource, CsvWriter csv)
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(strsource);


            var html = htmlDocument.DocumentNode;
            //标题
            var title = html.SelectSingleNode("//*[@id='J_Title']/h3");
            csv.WriteField(title);
            string strtitle = title.GetAttributeValue("data-title","");
            csv.WriteField(strtitle);

            //MessageBox.Show(strtitle);
            //累计评论
            var comment = html.SelectSingleNode("//*[@id='J_RateCounter']").InnerText;
            Console.WriteLine("累计评论 {0}", comment);
            csv.WriteField(comment);

            var 标题图片 = html.SelectNodes("//*[@id='J_UlThumb']/li/div/a/img");
            StringBuilder sb = new StringBuilder();
            foreach (var item in 标题图片)
            {
                Console.WriteLine(item.InnerText);
                sb.AppendLine(item.GetAttributeValue("src", ""));
            }
            csv.WriteField(sb.ToString().TrimEnd());
            var 快递 = html.SelectSingleNode("//*[@id='J_WlServiceTitle']").InnerText;
            Console.WriteLine(快递);
            csv.WriteField(快递);

            var 分类 = html.SelectNodes("//*[@id='J_isku']/div/dl[1]/dd/ul/li");
            sb.Clear();
            foreach (var item in 分类)
            {
                var sku = item.SelectSingleNode("./a/span").InnerText;
                Console.WriteLine(sku);
                sb.AppendLine(sku);
            }
            csv.WriteField(sb.ToString().TrimEnd());
            
            var 详情 = html.SelectNodes("//*[@id='J_DivItemDesc']/p/img");
            if(详情==null)
            {
                详情 = html.SelectNodes("//*[@id='J_DivItemDesc']/div/div/img");
            }
            if(详情!=null)
            {
                //var 图片 = html.SelectNodes("//*[@id='J_DivItemDesc']/p/strong/img");
                sb.Clear();
                //*[@id="J_DivItemDesc"]/p[2]/img[1]
                //*[@id="J_DivItemDesc"]/p[5]/strong/img[1]
                foreach (var item in 详情)
                {
                    var pics = item.CssSelect("img");
                    foreach (var pic in pics)
                    {
                        var tp = pic.GetAttributeValue("src");
                        Console.WriteLine(tp);
                        sb.AppendLine(tp);
                    }
                }
            }
            csv.WriteField(sb.ToString().TrimEnd());
            csv.NextRecord();
        }

        private void 获取天猫连接(string strsource, CsvWriter csv)
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(strsource);


            var html = htmlDocument.DocumentNode;
            
            //标题
            var title = html.SelectSingleNode("//*[@id='J_DetailMeta']/div[1]/div[1]/div/div[1]/h1");
            csv.WriteField(title.InnerText.Trim());
            //string strtitle = title.GetAttributeValue("data-title");
            //csv.WriteField(strtitle);

            //MessageBox.Show(strtitle);
            //累计评论
            var comment = html.SelectSingleNode("//*[@id='J_ItemRates']/div/span[2]").InnerText;
            Console.WriteLine("累计评论 {0}", comment);
            csv.WriteField(comment);
            //*[@id="J_UlThumb"]/li[2]/a/img
            var 标题图片 = html.SelectNodes("//*[@id='J_UlThumb']/li/a/img");
            StringBuilder sb = new StringBuilder();
            foreach (var item in 标题图片)
            {
                Console.WriteLine(item.InnerText);
                sb.AppendLine(item.GetAttributeValue("src"));
            }
            csv.WriteField(sb.ToString().TrimEnd());
            var 快递 = html.SelectSingleNode("//*[@id='J_PostageToggleCont']/p").InnerText;
            Console.WriteLine(快递);
            csv.WriteField(快递);

            var 分类 = html.SelectNodes("//*[@id='J_DetailMeta']/div[1]/div[1]/div/div[4]/div/div/dl[1]/dd/ul/li");
            sb.Clear();
            foreach (var item in 分类)
            {
                var sku = item.GetAttributeValue("title");
                var tupian = item.SelectSingleNode("./a").GetAttributeValue("style");
                if (!string.IsNullOrEmpty(tupian))
                {
                    tupian = tupian.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)?[0];
                }
                sku = string.Format("{0} | {1}", sku, tupian);

                Console.WriteLine(sku);
                sb.AppendLine(sku);
            }
            csv.WriteField(sb.ToString().TrimEnd());

            //*[@id="description"]/div/p/img[1]
            var 详情 = html.SelectNodes("//*[@id='description']/div/p/img");
            //var 图片 = html.SelectNodes("//*[@id='J_DivItemDesc']/p/strong/img");
            sb.Clear();
            //*[@id="J_DivItemDesc"]/p[2]/img[1]
            //*[@id="J_DivItemDesc"]/p[5]/strong/img[1]
            foreach (var item in 详情)
            {
                var tp = item.GetAttributeValue("src");
                Console.WriteLine(tp);
                sb.AppendLine(tp);
            }
            csv.WriteField(sb.ToString().TrimEnd());
            csv.NextRecord();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            webBrowser2.Load("www.baidu.com");
        }

        bool first = true;
        private void LoadUri2(string url)
        {
            webBrowser2.Load(url);
            webBrowser2.Update();
        }
        private async void GetSource2()
        {
            string source = await webBrowser2.GetSourceAsync();

        }
        private void Btngetall_Click(object sender, EventArgs e)
        {
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true; //隐藏 命令窗口  
            var option = new ChromeOptions();
            //option.Proxy = proxy;

            //option.AddArgument("disable-infobars"); //隐藏 自动化标题  
            //option.AddArgument("headless"); //隐藏 chorme浏览器  
            option.AddArgument("--incognito");//隐身模式  

            IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(service, option, TimeSpan.FromSeconds(40));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(10));
            driver.Url = "https://www.taobao.com";
            var input = wait.Until<IWebElement>((d) => { return d.FindElement(By.CssSelector("#q")); });
            var submit = wait.Until<IWebElement>((d) => { return d.FindElement(By.CssSelector("#J_TSearchForm > div.search-button > button")); });
            input.SendKeys("厨具");
            submit.Click();
            var total = wait.Until<IWebElement>((d) => { return d.FindElement(By.CssSelector("#mainsrp-pager > div > div > div > div.total")); });
            //MessageBox.Show(total.Text);
            //webBrowser2.Stop();
            //var browser = new ScrapingBrowser();
            //browser.Encoding = Encoding.Default;
            //var html = browser.DownloadString(new Uri("https://s.taobao.com/search?q=%E4%BF%9D%E6%B8%A9%E7%93%B6&imgfile=&js=1&stats_click=search_radio_all%3A1&initiative_id=staobaoz_20180522&ie=utf8&sort=sale-desc"));
            //var html = browser.DownloadString(new Uri("https://s.taobao.com/search?q=%E4%BF%9D%E6%B8%A9%E7%93%B6&imgfile=&js=1&stats_click=search_radio_all%3A1&initiative_id=staobaoz_20180522&ie=utf8&sort=sale-desc"));

            HttpHelper httphelper = new HttpHelper();
            HttpItem httpitem = new HttpItem()
            {
                Host = "item.taobao.com",
                URL = "https://s.taobao.com/search?q=%E4%BF%9D%E6%B8%A9%E7%93%B6&imgfile=&js=1&stats_click=search_radio_all%3A1&initiative_id=staobaoz_20180522&ie=utf8&sort=sale-desc",//URL     必需项
                Encoding = null,//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                //Encoding = Encoding.Default,
                Method = "get",//URL     可选项 默认为Get
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = "VIQQXDCajBh0Z%2FDhlFb3RBQz3qm5oFiPql%2Ffnwm4uWKWXPfmvve3nxoWiE0%2B2Nfvalh5rKNnZbMR9PmVkSL41w%3D%3D; t=329be4969020dec9f70977c8ee3b76e0; isg=BJ6eI77NScyjBpx_d6ngPp2z7DQg92HAitly3kgjFuHcazxFse3n6NjCZ_fnyFrx; cna=QP1YE1DovScCAbdfMCb7mQwi; hng=CN%7Czh-CN%7CCNY%7C156; thw=cn; mt=ci%3D-1_1; cookie2=41cc4340e189344c4cb14e53374ca3eb; v=0; _tb_token_=e63ff33d77de0",//字符串Cookie     可选项
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:60.0) Gecko/20100101 Firefox/60.0)",//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",//    可选项有默认值
                //ContentType = "text/html",//返回类型    可选项有默认值
                //Referer = "http://www.sufeinet.com",//来源URL     可选项
                //Allowautoredirect = true,//是否根据３０１跳转     可选项
                //CerPath = "d:\\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                Postdata = "C:\\PERKYSU_20121129150608_ScrubLog.txt",//Post数据     可选项GET时不需要写
                PostDataType = PostDataType.FilePath,//默认为传入String类型，也可以设置PostDataType.Byte传入Byte类型数据
                //ProxyIp = "192.168.1.105：8015",//代理服务器ID 端口可以直接加到后面以：分开就行了    可选项 不需要代理 时可以不设置这三个参数
                //ProxyPwd = "123456",//代理服务器密码     可选项
                //ProxyUserName = "administrator",//代理服务器账户名     可选项
                //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                //PostdataByte = System.Text.Encoding.Default.GetBytes("测试一下"),//如果PostDataType为Byte时要设置本属性的值
                //CookieCollection = new System.Net.CookieCollection(),//可以直接传一个Cookie集合进来
            };
            string strsource = driver.PageSource;

            //webBrowser2.Load("https://s.taobao.com/search?q=%E4%BF%9D%E6%B8%A9%E7%93%B6&imgfile=&js=1&stats_click=search_radio_all%3A1&initiative_id=staobaoz_20180522&ie=utf8&sort=sale-desc");
            //webBrowser2.Update();

            SaveFileDialog form = new SaveFileDialog();
            form.Filter = "csv(*.csv)|*.csv";
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            //string strsource = GetSource2();
            List<string> lst = 从淘宝搜索页获取商品链接(strsource);

            using (FileStream fs = new FileStream(form.FileName, FileMode.Create))
            using (TextWriter writer = new StreamWriter(fs, Encoding.GetEncoding("GB2312")))
            using (var csv = new CsvWriter(writer))
            {
                foreach (var item in lst)
                {
                    //解析淘宝商品搜索页
                    //webBrowser2.Load(item);
                    //webBrowser2.Update();
                    //httpitem.URL = item;
                    //var html = httphelper.GetHtml(httpitem);
                    //driver.Url = item;
                    driver.Navigate().GoToUrl(string.Format("https://{0}",item));
                    //将页面滚动条拖到底部
                    for (int i = 0; i < 10; i++)
                    {
                        int row = (i + 1) * 1000;
                        ((IJavaScriptExecutor)driver).ExecuteScript(string.Format("window.scrollTo(500,{0});", row));
                        Thread.Sleep(100);
                    }
                    //strsource = browser.DownloadString(new Uri(item));
                    if (item.Contains("taobao"))
                    {
                        获取连接(driver.PageSource, csv);
                    }
                    else if (item.Contains("tmall"))
                    {
                        获取天猫连接(driver.PageSource, csv);
                    }
                }

            }

            //Console.WriteLine(strsource);
        }
        private List<string> 从淘宝搜索页获取商品链接(string strsource)
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(strsource);
            var html = htmlDocument.DocumentNode;
            //*[@id="mainsrp-itemlist"]/div/div/div[1]
            //*[@id="mainsrp-itemlist"]/div/div/div[1]
            var nodes = html.SelectNodes("//*[@id='mainsrp-itemlist']/div/div/div/div");
            List<string> lst = new List<string>();
            foreach (var item in nodes)
            {
                var a = item.SelectSingleNode("./div/div/div/a");
                if (a != null)
                {
                    string uri = a.GetAttributeValue("href");
                    if(!uri.Contains("click.simba"))
                    {
                        lst.Add(a.GetAttributeValue("href"));
                    }
                }
            }
            return lst;
        }

    }







    /// <summary>
    /// 在自己窗口打开链接
    /// </summary>
    internal class OpenPageSelf : ILifeSpanHandler
    {
        Form1 form = null;
        public OpenPageSelf(Form1 mainform)
        {
            form = mainform;
        }
        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return false;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl,
string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures,
IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            newBrowser = null;
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
            chromiumWebBrowser.Load(targetUrl);
            form.SetURL(targetUrl);
            return true; //Return true to cancel the popup creation copyright by codebye.com.
        }
    }
}
