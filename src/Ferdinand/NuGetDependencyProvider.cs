namespace Ferdinand
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    internal class NuGetDependencyProvider : IDependencyProvider
    {
        private readonly IFileFinder _fileFinder = new NuGetPackagesFileFinder();
        private readonly string _baseDirectory;

        public NuGetDependencyProvider(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
        }

        public IEnumerable<Dependency> GetDependencies()
        {
            return from file in _fileFinder.GetFiles(_baseDirectory)
                   from dependencies in GetDependenciesFrom(file)
                   select dependencies;
        }

        private IEnumerable<Dependency> GetDependenciesFrom(FileSystemInfo fileInfo)
        {
            var xmlText = File.ReadAllText(fileInfo.FullName);
            var xdoc = XDocument.Parse(xmlText);

            return from dep in xdoc.Descendants("package")
                select new Dependency
                {
                    Name = dep.Attribute("id").Value,
                    Version = dep.Attribute("version").Value,
                    Type = "NuGet",
                };
        }
    }
}