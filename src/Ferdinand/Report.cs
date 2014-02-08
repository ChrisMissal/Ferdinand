namespace Ferdinand
{
    using System.Collections.Generic;
    using System.Linq;

    public class Report
    {
        private readonly IList<Dependency> _dependencies = new List<Dependency>();

        public string Text { get; set; }

        internal Report AddDependency(Dependency dependency)
        {
            if (_dependencies.Contains(dependency))
                return this;

            _dependencies.Add(dependency);
            return this;
        }

        public IEnumerable<string> Dependencies
        {
            get { return _dependencies.Select(x => x.ToString()); }
        }
    }
}