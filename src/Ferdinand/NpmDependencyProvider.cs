namespace Ferdinand
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class NpmDependencyProvider : IDependencyProvider
    {
        private readonly IFileFinder _fileFinder = new NpmFileFinder();
        private readonly string _baseDirectory;

        public NpmDependencyProvider(string baseDirectory)
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
            var jsonText = File.ReadAllText(fileInfo.FullName);

            dynamic package = SimpleJson.DeserializeObject(jsonText);

            if (package.ContainsKey("dependencies"))
            {
                foreach (var devDependency in package["dependencies"])
                {
                    yield return new Dependency
                    {
                        Type = "node",
                        Name = devDependency.Key,
                        Version = devDependency.Value,
                    };
                }
            }

            if (package.ContainsKey("devDependencies"))
            {
                foreach (var devDependency in package["devDependencies"])
                {
                    yield return new Dependency
                    {
                        Type = "node-dev",
                        Name = devDependency.Key,
                        Version = devDependency.Value,
                    };
                }
            }
        }
    }
}