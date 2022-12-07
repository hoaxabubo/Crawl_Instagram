using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceClose
    {

        public static void ByPort(int port)
        {
            try
            {
                var prc = new ProcManager();
                prc.KillByPort(port);
            }
            catch 
            {

                
            }
        }



       

       

        public class PRC
        {
            public int PID { get; set; }
            public int Port { get; set; }
            public string Protocol { get; set; }
        }
        public class ProcManager
        {
            public void KillByPort(int port)
            {
                var processes = GetAllProcesses();
                if (processes.Any(p => p.Port == port))
                    try
                    {
                        var process = Process.GetProcessById(processes.First(p => p.Port == port).PID);
                        process.Kill();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                else
                {
                    Console.WriteLine("No process to kill!");
                }
            }

            public List<PRC> GetAllProcesses()
            {
                var pStartInfo = new ProcessStartInfo();
                pStartInfo.FileName = "netstat.exe";
                pStartInfo.Arguments = "-a -n -o";
                pStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pStartInfo.UseShellExecute = false;
                pStartInfo.RedirectStandardInput = true;
                pStartInfo.RedirectStandardOutput = true;
                pStartInfo.RedirectStandardError = true;
                pStartInfo.CreateNoWindow = true;

                var process = new Process()
                {
                    StartInfo = pStartInfo
                };
                process.Start();

                var soStream = process.StandardOutput;

                var output = soStream.ReadToEnd();
                if (process.ExitCode != 0)
                    throw new Exception("somethign broke");

                var result = new List<PRC>();

                var lines = Regex.Split(output, "\r\n");
                foreach (var line in lines)
                {
                    if (line.Trim().StartsWith("Proto"))
                        continue;

                    var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    var len = parts.Length;
                    if (len > 2)
                        result.Add(new PRC
                        {
                            Protocol = parts[0],
                            Port = int.Parse(parts[1].Split(':').Last()),
                            PID = int.Parse(parts[len - 1])
                        });


                }
                return result;
            }
        }
    }
}
