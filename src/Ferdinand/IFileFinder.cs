namespace Ferdinand
{
    using System.Collections.Generic;
    using System.IO;

    public interface IFileFinder
    {
        IEnumerable<FileInfo> GetFiles(string baseDirectory);
    }
}