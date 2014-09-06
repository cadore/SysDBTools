using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SysDBTools
{
    public static class DBTools
    {
        public static string directory_pg_dump { get; set; }
        public static string pgPassword { get; set; }

        public static string host { get; set; }
        public static string port { get; set; }
        public static string username { get; set; }
        public static string fileTempBackup { get; set; }
        public static string fileBackupCrypt { get; set; }
        public static string dataBase { get; set; }

        static string args = "";
        
        public static void startBackup()
        {
            directory_pg_dump = @"C:\Program Files\PostgreSQL\9.2\bin";
            pgPassword = "p@ssw0rd";
            args += ("--host localhost --port 5432 --username \"postgres\"");
            args += (" --format plain --data-only --encoding UTF8 --inserts --column-inserts --verbose");
            args += (" --file \"C:\\Users\\William\\Desktop\\teste.sql\" \"sysgrupodb\"");
            startProcess();

        }

        static void startProcess()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe");
            processStartInfo.RedirectStandardInput = true;
            //processStartInfo.RedirectStandardOutput = true;
            //processStartInfo.RedirectStandardError = true;
            processStartInfo.UseShellExecute = false;

            processStartInfo.WorkingDirectory = directory_pg_dump;
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();

            if (process != null)
            {
                process.StandardInput.WriteLine(String.Format("SET PGPASSWORD={0}", pgPassword));
                process.StandardInput.WriteLine(String.Format(@".\pg_dump.exe {0}", args.ToString()));
                process.StandardInput.WriteLine(String.Format("SET PGPASSWORD=null"));
                process.StandardInput.WriteLine(String.Format("pause"));

                process.StandardInput.Close();
            }
            process.WaitForExit();
        }
    }
}