using System;
using System.IO;
using System.Text;

namespace Assignment1
{
    public class DirWalker
    {

        public void Walk(String path, string destinationPath, ref int validRows, ref int skippedRows)
        {
            Exceptions ex = new Exceptions();
            StringBuilder sb = new StringBuilder();
            string[] list = Directory.GetDirectories(path);
            if (list == null) return;
            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    Walk(dirpath, destinationPath, ref validRows, ref skippedRows);
                }
            }
            string[] fileList = Directory.GetFiles(path, "*.csv");
            foreach (string filepath in fileList)
            {
                new SimpleCSVParser().Parse(filepath, ref validRows, ref skippedRows,ref sb);
            }
            //check if SB is non emprty
            FileStream stream = new FileStream(destinationPath, FileMode.Append);
            using (StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.UTF8))
            {
                sw.Write(sb);
                sb = new StringBuilder();
            }
        }
        public static void Main(String[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            int validRows = 0;
            int skippedRows = 0;
            string path = @"E:\SMU Classes\5510 Software Development  Neil Williams & Dan Penny\Assignments\Directory Traversal\Sample Data\Sample Data\";
            watch.Start();
            string rootfolder = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            if (!Directory.Exists(rootfolder + "\\Output"))
            {
                Directory.CreateDirectory(rootfolder + "\\Output");
            }
            if (File.Exists(rootfolder + "\\Output\\Output.csv"))
            {
                File.Delete(rootfolder + "\\Output\\Output.csv");
            }
            FileStream stream = new FileStream(rootfolder + "//Output//Output.csv", FileMode.OpenOrCreate);
            using (StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.UTF8))
            {
                sw.WriteLine("First Name, Last Name,Street Number, Street, City, Province, Postal Code,Country,Phone Number, email Address");
            }
            DirWalker fw = new DirWalker();
            fw.Walk(path, rootfolder + "\\Output\\Output.csv", ref validRows, ref skippedRows);
            watch.Stop();
            FileStream stream1 = new FileStream(rootfolder + "//Output//Output.csv", FileMode.Append);
            using (StreamWriter sw = new StreamWriter(stream1, System.Text.Encoding.UTF8))
            {
                sw.WriteLine("Total number of valid rows: " + validRows +
            " \nTotal number of skipped rows: " + skippedRows +
            " \nTotal execution time: " + (float)(watch.ElapsedMilliseconds/1000) + " seconds");
            }



        }
    }
}
