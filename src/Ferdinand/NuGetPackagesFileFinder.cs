namespace Ferdinand
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class NuGetPackagesFileFinder : IFileFinder
    {
        public IEnumerable<FileInfo> GetFiles(string baseDirectory)
        {
            var directoryInfo = new DirectoryInfo(baseDirectory);

            return from files in directoryInfo.EnumerateFiles("packages.config", SearchOption.AllDirectories)
                   select files;
        }
    }
}
