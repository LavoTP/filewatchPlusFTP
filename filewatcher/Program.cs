using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Console;
using static System.ConsoleColor;
using watchLibrary;

namespace filewatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            // init
            var fileWatch = new FileSystemWatcher();
            
            // listeners
            fileWatch.Created += file_created;
            fileWatch.Changed += file_changed;
            fileWatch.Deleted += file_deleted;
            fileWatch.Renamed += file_renamed;

            // path
            fileWatch.Path = ConfigurationManager.AppSettings["watchDir"];

            // start 
            fileWatch.EnableRaisingEvents = true;

            ftp_file_service("/My Documents");

            
            WriteLine("Listening...");
            WriteLine("(Press any key to exit.)");
            
            ReadLine();
        }

        private static void file_renamed(object sender, RenamedEventArgs e){
            ForegroundColor = Yellow;
            WriteLine($"A new file has been renamed from {e.OldName} to {e.Name}");
        }
        private static void file_deleted(object sender, FileSystemEventArgs e){
            ForegroundColor = Red;
            WriteLine($"A new file has been deleted - {e.Name}");
        }
        private static void file_changed(object sender, FileSystemEventArgs e){
            ForegroundColor = Green;
            WriteLine($"A new file has been changed - {e.Name}");
        }
        private static void file_created(object sender, FileSystemEventArgs e){
            ForegroundColor = Blue;
            WriteLine($"A new file has been created - {e.Name}");
        }

        public static void ftp_file_service(string path, string file_name = ""){

             /* Create Object Instance */
            ftp ftpClient = new ftp(@"ftp.drivehq.com", "lucyfa45@gmail.com", "asDF1@#ASdf");

            // /* Upload a File */
            // ftpClient.upload("etc/test.txt", @"C:\Users\metastruct\Desktop\test.txt");

            // /* Download a File */
            // ftpClient.download("etc/test.txt", @"C:\Users\metastruct\Desktop\test.txt");

            // /* Delete a File */
            // ftpClient.delete("etc/test.txt");

            // /* Rename a File */
            // ftpClient.rename("etc/test.txt", "test2.txt");

            // /* Create a New Directory */
            // ftpClient.createDirectory("etc/test");

            // /* Get the Date/Time a File was Created */
            // string fileDateTime = ftpClient.getFileCreatedDateTime("etc/test.txt");
            // Console.WriteLine(fileDateTime);

            // /* Get the Size of a File */
            // string fileSize = ftpClient.getFileSize("etc/test.txt");
            // Console.WriteLine(fileSize);

            /* Get Contents of a Directory (Names Only) */
            string[] simpleDirectoryListing = ftpClient.directoryListDetailed(path);
            for (int i = 0; i < simpleDirectoryListing.Count(); i++) { Console.WriteLine(simpleDirectoryListing[i]); }

            WriteLine(simpleDirectoryListing);

            /* Get Contents of a Directory with Detailed File/Directory Info */
            // string[] detailDirectoryListing = ftpClient.directoryListDetailed("/etc");
            // for (int i = 0; i < detailDirectoryListing.Count(); i++) { Console.WriteLine(detailDirectoryListing[i]); }
            /* Release Resources */
            ftpClient = null;
        }
    }
}
