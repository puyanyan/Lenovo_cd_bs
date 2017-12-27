using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Xinning.Lenovo.VMI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            System.Diagnostics.Process[] pros = System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            if (pros.Length > 1)
            {
                //MessageBox.Show("已经启动了一个程序，请先退出！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
                return;
            }
            else
            {
                Application.Run(new WMIMianFrm()); //这句是系统自动写的 
            }
            //bool flag = false;
            ////System.Threading.Mutex mutex = new System.Threading.Mutex(true, "MutexExample", out flag);
            //System.Threading.Mutex mutex = new System.Threading.Mutex(true, "Xinning.Lenovo.VMI", out flag);
            //if (flag)
            //{
            //    Console.Write("Running");
            //    Console.ReadLine();
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new WMIMianFrm());
            //}
            //else
            //{
            //    Console.Write("Another is Running");
            //    //Application.Exit();
            //}
          //  Application.Run(new WMIMianFrm());
        }
    }
}
