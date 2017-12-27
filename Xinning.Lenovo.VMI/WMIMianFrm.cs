using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xinning.Lenovo.Library.VMIService;
using System.Collections;
using Xinning.Lenovo.Library.DAL;
using XNG.VMI.Logic;
using XNG.VMI.Logic.Schema.DAL;
using System.Threading;
using XNG.Common.Utils;

namespace Xinning.Lenovo.VMI
{
    public partial class WMIMianFrm : Form
    {
        public WMIMianFrm()
        {
            InitializeComponent();
        }

        private void btnKeySet_Click(object sender, EventArgs e)
        {
           
        }

        private void btnValueSet_Click(object sender, EventArgs e)
        {
           
        }

        private void WMIMianFrm_Load(object sender, EventArgs e)
        {
            //this.Text += XNG.VMI.Logic.VMIGlobal.SUBINV_CODE +"-"+ XNG.VMI.Logic.VMIGlobal.ENV+"ver.20131105";
            //this.Text += XNG.VMI.Logic.VMIGlobal.SUBINV_CODE + "-" + XNG.VMI.Logic.VMIGlobal.ENV + "ver.2016050501";
            this.Text += XNG.VMI.Logic.VMIGlobal.SUBINV_CODE + "-" + XNG.VMI.Logic.VMIGlobal.ENV + "ver.2016051101";
            this.work.MySftpConnection.Open();
            this.CommomTimer.Enabled = true;
            this.CommomTimer.Start();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                this.work.MySftpConnection.Open();
                ArrayList filelist = work.MySftpConnection.GetFileList(VMIGlobal.strVMIDownloadPath);
                ReceiveFileTree fileTree = new ReceiveFileTree(filelist);
                work.ReceiveVmiSentFile(fileTree.FileList, VMIGlobal.SUBINV_CODE);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            #region Process Received File
            work.processReceivedVMIFile(VMIGlobal.SUBINV_CODE, VMIGlobal.APP_NAME);

            work.doDistribution();

            work.doSychronization();

            work.doAcknowledgement();
            #endregion

        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                work.SendReceiveFileByResponse();
            }
            catch
            { }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //VMIStrategy.RefeshSendLOI_GR();
            work.doGenerateLOIGR();
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //VMIStrategy.RefeshSendSOI_GR();
            work.doGenerateSOIGR();
        }

        private void button14_Click(object sender, EventArgs e)
        {

            
            //VMIStrategy.RefeshSendINVENTORY_SNAPSHOT();
            work.doGenerateStock();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //VMIStrategy.RefeshSendPO_SNAPSHOT();
            work.doGeneratePO_Snapshot();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                work.SendGRFile();
            }
            catch
            { }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                //VMIStrategy.RefeshSendDispatchList();
                //work.SendDispatchList("INHOUSE");
                work.doGenerateSTOGI();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                work.SendPO_SNAPSHOT();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                work.doGenerateStock();
                work.doGeneratePO_Snapshot();
               // work.SendInventory();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void button28_Click(object sender, EventArgs e)
        {
            
            AutoRUNSFTP();
        }

        public void AutoRUNSFTP()
        {
      
            #region old code
            //try
            //{
            //    //VMIWorkFlow.Running.Add("12313123");
            //    this.work.MySftpConnection.Open();
            //    ArrayList filelist = work.MySftpConnection.GetFileList(VMIGlobal.strVMIDownloadPath);
            //    try
            //    {
            //         ReceiveFileTree fileTree = new ReceiveFileTree(filelist);
            //         work.ReceiveVmiSentFile(fileTree.FileList, VMIGlobal.SUBINV_CODE);
            //    }
            //    catch(Exception ex)
            //    {
            //        LenovoCommonDal.SendMail("联想成品执行异常", "yuan.min@xinning.com.cn", "xnmis.list@xinning.com.cn", "", ex.ToString(), "", "main_system");
            //        //Application.Exit();
            //    }
            //    finally
            //    {
            //        //work.DoReceiveVmiSentFile("INHOUSE");
            //        work.SendReceiveFileByResponse();
            //        work.AutoSendReportFile("INHOUSE");
            //        if (DateTime.Now.Hour != Xinning.Lenovo.Library.VMIService.VMIServiceGlobal.GenTime.Hour) isSend = true;
            //        if (DateTime.Now.Hour == Xinning.Lenovo.Library.VMIService.VMIServiceGlobal.GenTime.Hour && 
            //            DateTime.Now.Minute >=Xinning.Lenovo.Library.VMIService.VMIServiceGlobal.GenTime.Minute && 
            //            isSend == true)
            //        {
            //            VMIWorkFlow.Running.Clear();
            //            VMIStrategy.RefeshSendPO_SNAPSHOT();
            //            work.SendPO_SNAPSHOT();
            //            VMIStrategy.RefeshSendINVENTORY_SNAPSHOT();
            //            work.SendInventory();
            //            isSend = false;
            //        }
            //    }

            #endregion

            /*
             * 1. 下载文件
             * 2. 接收文件
             * 3. 处理各种文件: 叫料 等
             * 4. 生成各种文件
             * 5. 上传文件
             */
            this.CommomTimer.Enabled = false;
            try
            {
                //loginfo.AppendText("[" + DateTime.Now + "] 开始下载文件...\n");
                this.work.MySftpConnection.Open();
                ArrayList filelist = work.MySftpConnection.GetFileList(VMIGlobal.strVMIDownloadPath);
                Thread tDownLoadFile = new Thread(() =>
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        loginfo.AppendText("[" + DateTime.Now + "]下载文件\r\n");
                        LogHelper.Info(this, "下载文件\r\n");
                    }));
                    try
                    {
                        ReceiveFileTree fileTree = new ReceiveFileTree(filelist);
                        work.ReceiveVmiSentFile(fileTree.FileList, VMIGlobal.SUBINV_CODE);
                        #region Process Received File
                        work.processReceivedVMIFile(VMIGlobal.SUBINV_CODE, VMIGlobal.APP_NAME);

                        work.doDistribution();

                        work.doSychronization();

                        work.doAcknowledgement();
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        LenovoCommonDal.SendMail("联想成品执行异常", "yuan.min@xinning.com.cn", "xnmis.list@xinning.com.cn", "", ex.ToString(), "", "main_system");
                    }
                    finally
                    {
                    }
                });

                Thread tSTO = new Thread(() =>
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        loginfo.AppendText(DateTime.Now + "处理工单" + "\n");
                        LogHelper.Info(this, "处理工单\r\n");
                    }));
                    try
                    {
                        //处理工单
                        //loginfo.AppendText("[" + DateTime.Now + "] 处理工单...\n");
                        work.doProcessSTO();
                    }
                    catch (Exception ex)
                    {
                        loginfo.AppendText("[" + DateTime.Now + "] " + ex .ToString()+ "");
                        LenovoCommonDal.SendMail("EDI执行异常:" + VMIGlobal.SUBINV_CODE, "george.gao@xinning.com.cn;yuan.min@xinning.com.cn; herbert.lee@xinning.com.cn", "", "", ex.ToString(), "", "main_system");
                    }
                });

                Thread tGenFile = new Thread(() =>
                {
                    try
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            //刷新Confirm GI
                            loginfo.AppendText("[" + DateTime.Now + "] 处理Confirm...\n");
                            LogHelper.Info(this, "处理Confirm\r\n");
                        }));
                        work.doGenerateConfirmGI();
                        
                    }
                    catch (Exception ex)
                    {
                        LenovoCommonDal.SendMail("EDI执行异常:" + VMIGlobal.SUBINV_CODE, "george.gao@xinning.com.cn;yuan.min@xinning.com.cn; herbert.lee@xinning.com.cn", "", "", ex.ToString(), "", "main_system");
                    }

                    try
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            //刷新STO GI
                            loginfo.AppendText("[" + DateTime.Now + "] 处理STO...\n");
                            LogHelper.Info(this, "处理STO\r\n");
                        }));
                        work.doGenerateSTOGI();
                    }
                    catch (Exception ex)
                    {
                        LenovoCommonDal.SendMail("EDI执行异常:" + VMIGlobal.SUBINV_CODE, "george.gao@xinning.com.cn;yuan.min@xinning.com.cn; herbert.lee@xinning.com.cn", "", "", ex.ToString(), "", "main_system");
                    }
                    try
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            //刷新SOIGR
                            loginfo.AppendText("[" + DateTime.Now + "] 处理SOIGR...\n");
                            LogHelper.Info(this, "处理SOIGR\r\n");
                        }));
                        work.doGenerateSOIGR();
                    }
                    catch (Exception ex)
                    {
                        LenovoCommonDal.SendMail("EDI执行异常:" + VMIGlobal.SUBINV_CODE, "george.gao@xinning.com.cn;yuan.min@xinning.com.cn; herbert.lee@xinning.com.cn", "", "", ex.ToString(), "", "main_system");
                    }

                    try
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            //刷新LOIGR
                            loginfo.AppendText("[" + DateTime.Now + "] 处理LOIGR...\n");
                            LogHelper.Info(this, "处理LOIGR\r\n");
                        }));
                        work.doGenerateLOIGR();
                    }
                    catch (Exception ex)
                    {
                        LenovoCommonDal.SendMail("EDI执行异常:" + VMIGlobal.SUBINV_CODE, "george.gao@xinning.com.cn;yuan.min@xinning.com.cn; herbert.lee@xinning.com.cn", "", "", ex.ToString(), "", "main_system");
                    }


                    try
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            //刷新SOTLOI
                            loginfo.AppendText("[" + DateTime.Now + "] 处理SOTLOI...\n");
                            LogHelper.Info(this, "处理SOTLOI\r\n");
                        }));
                        work.doGenerateSOILOI();
                    }
                    catch (Exception ex)
                    {
                        LenovoCommonDal.SendMail("EDI执行异常:" + VMIGlobal.SUBINV_CODE, "george.gao@xinning.com.cn;yuan.min@xinning.com.cn; herbert.lee@xinning.com.cn", "", "", ex.ToString(), "", "main_system");
                    }
                });
                tDownLoadFile.Start();
                tSTO.Start();
                tGenFile.Start();
                tDownLoadFile.Join(new TimeSpan(0, 25, 0));
                tSTO.Join(new TimeSpan(0, 25, 0));
                tGenFile.Join(new TimeSpan(0, 25, 0));

                try
                {
                    tDownLoadFile.Abort();
                    tSTO.Abort();
                    tGenFile.Abort();
                }
                catch (Exception extimeout)
                {
                    loginfo.AppendText(DateTime.Now + extimeout.ToString() + "\n");
                }


                Thread tCheckFile = new Thread(() =>
                {
                    try
                    {
                        if (DateTime.Now.Hour != XNG.VMI.Logic.VMIGlobal.GEN_TIME.Hour) isSend = true;
                        if (DateTime.Now.Hour == XNG.VMI.Logic.VMIGlobal.GEN_TIME.Hour &&
                            DateTime.Now.Minute >= XNG.VMI.Logic.VMIGlobal.GEN_TIME.Minute &&
                            isSend == true)
                        {
                            LenovoCommonDal.SendMail("EDI执行异常:" + VMIGlobal.SUBINV_CODE, "george.gao@xinning.com.cn;yuan.min@xinning.com.cn; herbert.lee@xinning.com.cn", "", "", "对账文件上传开始于：--〉"+DateTime.Now.ToString(), "", "main_system");
                            work.doGenerateStock();
                            work.doGeneratePO_Snapshot();
                            isSend = false;
                            LenovoCommonDal.SendMail("EDI执行异常:" + VMIGlobal.SUBINV_CODE, "george.gao@xinning.com.cn;yuan.min@xinning.com.cn; herbert.lee@xinning.com.cn", "", "", "对账文件上传结束于：--〉" + DateTime.Now.ToString(), "", "main_system");
                        }
                    }
                    catch (Exception ex)
                    {
                        LenovoCommonDal.SendMail("EDI执行异常:" + VMIGlobal.SUBINV_CODE, "george.gao@xinning.com.cn;yuan.min@xinning.com.cn; herbert.lee@xinning.com.cn", "", "", ex.ToString(), "", "main_system");
                    }


                });
                try
                {
                    tCheckFile.Start();
                    var b_tCheckFile = tCheckFile.Join(new TimeSpan(0, 10, 0));
                    if (!b_tCheckFile)
                    {
                        LogHelper.Info(this, "进程生成对账文本超时，未能正常退出\r\n");
                        tCheckFile.Abort();
                    }
                }
                catch (Exception ex)
                {
                    loginfo.AppendText(DateTime.Now + ex.ToString() + "\n");
                }

                Thread tUploadFile = new Thread(() =>
                {
                    try
                    {
                        loginfo.AppendText("[" + DateTime.Now + "] 处理上传文本...\n");
                        ArrayList alFileList = work.GetUploadFileList();
                        XNG.VMI.Logic.Schema.Objects.SendFileInfo sendFiles = new XNG.VMI.Logic.Schema.Objects.SendFileInfo(alFileList);
                        if (alFileList.Count > 0)
                            Xinning.Lenovo.Library.VMIService.VMIWorkFlow.Running.Add("[" + System.DateTime.Now.ToShortTimeString() + "]: Uploading file for Lenovo...");

                        foreach (string s in alFileList)
                        {
                            Xinning.Lenovo.Library.VMIService.VMIWorkFlow.Running.Add(s);
                        }


                        work.uploadFileToVMI(sendFiles.FileList, XNG.VMI.Logic.VMIGlobal.SUBINV_CODE);
                    }
                    catch (Exception ex)
                    {
                        loginfo.AppendText("[" + DateTime.Now + "] " + ex.ToString() + "");
                        LenovoCommonDal.SendMail("EDI执行异常:" + VMIGlobal.SUBINV_CODE, "yuan.min@xinning.com.cn; herbert.lee@xinning.com.cn", "xnmis.list@xinning.com.cn", "", ex.ToString(), "", "main_system");
                    }
                });

                try
                {
                    tUploadFile.Start();
                    var b_tUploadFile = tUploadFile.Join(new TimeSpan(0, 10, 0));
                    if (!b_tUploadFile)
                    {

                        LogHelper.Info(this, "进程b_tUploadFile超时，未能正常退出\r\n");
                        tUploadFile.Abort();
                    }
                }
                catch (Exception ex)
                {
                    loginfo.AppendText(DateTime.Now + ex.ToString() + "\n");
                }

            }
            catch (Exception ex)
            {
                LenovoCommonDal.SendMail("联想成品执行异常", "yuan.min@xinning.com.cn", "xnmis.list@xinning.com.cn", "", ex.ToString(), "", "main_system");
            }

            
            this.CommomTimer.Enabled = true;

        }

        private bool isSend = false;

        private void CommomTimer_Tick(object sender, EventArgs e)
        {
            AutoRUNSFTP();
        }

        private void WMIMianFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            work.MySftpConnection.Close();
        }

        private Xinning.Lenovo.Library.VMIService.VMIWorkFlow work = new Xinning.Lenovo.Library.VMIService.VMIWorkFlow();

        private void butsendinlot_Click(object sender, EventArgs e)
        {
            string strSql;
            strSql = " SELECT [auto_id] ,[in_date] ,[car_no] ,[machine_seq],[lot_no] ,[wc_code] "
                 + " ,[item_code],[item_name] ,[scan_time] ,[username] ,[plan_qty],[qa_status] ,[pallet_ID] "
                 + " FROM [tblenovo_lotno]  where issend=0";
            DataTable dt_lotno = VMIStrategy.myDataAccess.ExecuteDataTable(strSql);

            if (dt_lotno.Rows.Count > 0)
            {
                AutoSendMail.sendtxt(VMIServiceGlobal.Mailto, "peter.lu@xinning.biz", "LenovoInboundScanSHInfo", dt_lotno, VMIServiceGlobal.MailPath);
                foreach (DataRow row in dt_lotno.Rows)
                {
                    strSql = " update tblenovo_lotno set issend =1 where id =" + row["auto_id"].ToString();
                    VMIStrategy.myDataAccess.ExecuteNoneQuery(strSql);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //work.SendPull_Confirm("INHOUSE");
            work.doGenerateConfirmGI();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList alFileList = work.GetUploadFileList();
                XNG.VMI.Logic.Schema.Objects.SendFileInfo sendFiles = new XNG.VMI.Logic.Schema.Objects.SendFileInfo(alFileList);

                if (alFileList.Count > 0)
                    Xinning.Lenovo.Library.VMIService.VMIWorkFlow.Running.Add ("[" + System.DateTime.Now.ToShortTimeString() + "]: Uploading file for Lenovo...");

                foreach (string s in alFileList)
                {
                    Xinning.Lenovo.Library.VMIService.VMIWorkFlow.Running.Add(s);
                }

                work.uploadFileToVMI(sendFiles.FileList, XNG.VMI.Logic.VMIGlobal.SUBINV_CODE);

            }
            catch (Exception ex)
            {
                Xinning.Lenovo.Library.VMIService.VMIWorkFlow.Running.Add ( "Error when Uploading: " + ex.StackTrace );
            }

            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            work.doProcessSTO();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            work.doGenerateSOILOI();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            work.SendGR2LCFC();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            work.SendDispatch2LCFC();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            work.SendStock2LCFC();
        }


    }
}