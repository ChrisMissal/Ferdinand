namespace Ferdinand
{
    using System;
    using System.IO;
    using System.Linq;

    public class Explorer
    {
        public Report GetReport(string directory, string reportFileName = "ferdinand-report.txt")
        {
            var dependencyProviders = from type in typeof(IDependencyProvider).Assembly.GetTypes()
                                      where typeof(IDependencyProvider).IsAssignableFrom(type)
                                         && !type.IsAbstract
                                         && !type.IsInterface
                                      select (IDependencyProvider)Activator.CreateInstance(type, directory);

            var dependencies = from finder in dependencyProviders
                               from dependency in finder.GetDependencies()
                               orderby dependency.Name, dependency.Version descending
                               select dependency;

            var report = new Report();

            File.WriteAllLines(reportFileName, dependencies
                .Distinct()
                .Do(x => report.AddDependency(x))
                .OrderBy(x => x.Name)
                .ThenByDescending(x => x.Version)
                .Select(x => x.ToString())
                );

            return report;
        }
    }
}