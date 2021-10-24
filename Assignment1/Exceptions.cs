using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace Assignment1
{
    public class Exceptions
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void WriteLog(string Error)
        {
            var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepo, new FileInfo("log4net.config"));

            log.Error(Error);
        }

        static StreamWriter OpenStream(string path)
        {
            Exceptions ex = new Exceptions();
            if (path is null)
            {
                ex.WriteLog("You did not supply a file path.");
                return null;
            }
            try
            {
                var fs = new FileStream(path, FileMode.CreateNew);
                
                return new StreamWriter(fs);
            }
            catch (FileNotFoundException)
            {
                ex.WriteLog("The file or directory cannot be found.");
            }
            catch (DirectoryNotFoundException)
            {
                ex.WriteLog("The file or directory cannot be found.");
            }
            catch (DriveNotFoundException)
            {
                ex.WriteLog("The drive specified in 'path' is invalid.");
            }
            catch (PathTooLongException)
            {
                ex.WriteLog("'path' exceeds the maxium supported path length.");
            }
            catch (UnauthorizedAccessException)
            {
                ex.WriteLog("You do not have permission to create this file.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
            {
                ex.WriteLog("There is a sharing violation.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
            {
                ex.WriteLog("The file already exists.");
            }
            catch (IOException e)
            {
                ex.WriteLog($"An exception occurred:\nError code: " +
                                  $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}");
            }
            return null;
        }
    }
}
