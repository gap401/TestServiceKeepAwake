using System.IO;
using Topshelf.Logging;

namespace TestServiceKeepAwake
{
    public class ConverterService
    {

        private FileSystemWatcher _watcher;
        private static readonly LogWriter _log = HostLogger.Get<ConverterService>();

        public bool Start()
        {
            _watcher = new FileSystemWatcher(@"c:\temp\a", "*_in.txt");

            _watcher.Created += FileCreated;

            _watcher.IncludeSubdirectories = false;

            _watcher.EnableRaisingEvents = true;

            _log.InfoFormat("Starting Service ... 'TestServiceKeepAwake.exe' ");

            return true;

        }

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            _log.InfoFormat("Starting conversion of '{0}'", e.FullPath);

            string content = File.ReadAllText(e.FullPath);

            string upperContent = content.ToUpperInvariant();

            var dir = Path.GetDirectoryName(e.FullPath);

            var convertedFileName = Path.GetFileName(e.FullPath) + ".converted";

            var convertedPath = Path.Combine(dir, convertedFileName);

            File.WriteAllText(convertedPath, upperContent);

        }



        public bool Stop()
        {
            _watcher.Dispose();

            return true;

        }


    }
}
