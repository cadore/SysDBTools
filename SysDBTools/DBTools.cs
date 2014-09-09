using SysDBTools.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace SysDBTools
{
    public class DBTools
    {
        public DBTools(Main _mainForm)
        {
            this.mainForm = _mainForm;
        }
        public Main mainForm = null;

        public string directory_pg_dump { get; set; }
        public string pgPassword { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public string username { get; set; }
        public string fileSqlBackup { get; set; }
        public string fileCryptBackup { get; set; }
        public string dataBase { get; set; }

        string args = "";
        
        public void startBackup()
        {
            mainForm.log("Verificando parametros...");
            if (String.IsNullOrEmpty(directory_pg_dump)
                || String.IsNullOrEmpty(pgPassword)
                || String.IsNullOrEmpty(host)
                || port == 0
                || String.IsNullOrEmpty(username)
                || String.IsNullOrEmpty(fileSqlBackup)
                || String.IsNullOrEmpty(fileCryptBackup)
                || String.IsNullOrEmpty(dataBase))
            {
                mainForm.OnError();
                throw new Exception("Não foi informado todos o parametros necessários para executar o backup.");
            }

            try
            {
                args += String.Format("--host {0} --port {1} --username \"{2}\"", host, port, username);
                args += String.Format(" --format plain --data-only --encoding UTF8 --inserts --column-inserts --verbose");
                args += String.Format(" --file \"{0}\" \"{1}\"", fileSqlBackup, dataBase);
                startProcess();
                cryptoFileBackup();
                mainForm.OnOK();
            }
            catch (Exception ex)
            {
                mainForm.OnError();
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        void startProcess()
        {
            try
            {
                mainForm.log("Iniciando Backup...");
                ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe");
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.UseShellExecute = false;

                processStartInfo.WorkingDirectory = directory_pg_dump;
                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();
                mainForm.log("Backup iniciado.");
                if (process != null)
                {
                    process.StandardInput.WriteLine(String.Format("SET PGPASSWORD={0}", pgPassword));
                    process.StandardInput.WriteLine(String.Format(@".\pg_dump.exe {0}", args.ToString()));
                    process.StandardInput.WriteLine(String.Format("SET PGPASSWORD=null"));
                    process.StandardInput.Close();
                    process.WaitForExit();
                }
                mainForm.log("Backup Concluído.");
            }
            catch (Exception ex)
            {
                mainForm.OnError();
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        void cryptoFileBackup()
        {
            try
            {
                mainForm.log("Criptografando arquivo...");
                Cryptology.EncryptFile(fileSqlBackup, fileCryptBackup, "a1s2 d3f4&beguta");
                mainForm.log("Criptografia do arquivo concluída.");
                mainForm.log("Excluindo arquivos temporarios...");
                File.Delete(fileSqlBackup);
                mainForm.log("Backup Concluído com sucesso.");
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                mainForm.OnError();
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}