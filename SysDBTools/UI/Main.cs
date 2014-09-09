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

namespace SysDBTools.UI
{
    public partial class Main : Form
    {
        public Thread oThread;
        public Main()
        {
            InitializeComponent();
            tfLog.Text = null;
            try
            {
                oThread = new Thread(new ThreadStart(this.DoWork));
                oThread.Start();
            }
            catch (Exception ex)
            {               
                MessageBox.Show("Occoreu um erro:\n" + ex.Message + "\n" + ex.InnerException);
                this.Close();
            }
        }

        public void DoWork()
        {
            bool IsWorked = true;
            while (true)
            {
                if (IsWorked)
                {
                    DBTools db = new DBTools(this);
                    db.directory_pg_dump = @"C:\Program Files\PostgreSQL\9.2\bin";
                    db.dataBase = "sysgrupodb";
                    db.host = "localhost";
                    db.port = 5432;
                    db.username = "postgres";
                    db.fileSqlBackup = Path.GetTempPath() + @"\temp.sql";
                    db.fileCryptBackup = @"C:\Users\William\Desktop\SysCryptBackup.syscrypt";
                    db.pgPassword = "p@ssw0rd";
                    db.startBackup();
                    IsWorked = false;
                    oThread.Abort();                    
                }                
            }
        }

        public void log(string _log)
        {
            if (tfLog.InvokeRequired)
            {
                tfLog.BeginInvoke((MethodInvoker)delegate
                {
                    if (!String.IsNullOrEmpty(tfLog.Text))
                    {
                        tfLog.Text += "\n";
                    }
                    tfLog.Text += _log;
                });
            }
            else
            {
                if (!String.IsNullOrEmpty(tfLog.Text))
                {
                    tfLog.Text += "\n";
                }
                tfLog.Text += _log;
            }
                
        }

        public void OnOK()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                });
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        public void OnError()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                    log("");
                    oThread.Abort();
                });
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                log("");
                oThread.Abort();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnError();
        }
    }
}
