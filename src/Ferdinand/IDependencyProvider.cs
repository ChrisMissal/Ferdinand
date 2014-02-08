namespace Ferdinand
{
    using System.Collections.Generic;

    internal interface IDependencyProvider
    {
        IEnumerable<Dependency> GetDependencies();
    }
}