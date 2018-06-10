﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 根据sku抓取数据
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args==null||args.Length==0)
            {
                Application.Run(new Form1());
            }
            else
            {
                //MessageBox.Show("测试");
                Application.Run(new Form1(args[0]));
            }
        }
    }
}
