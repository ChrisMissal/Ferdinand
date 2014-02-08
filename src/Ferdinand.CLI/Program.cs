namespace Ferdinand.CLI
{
    using Ferdinand;

    class Program
    {
        static void Main(string[] args)
        {
            var explorer = new Explorer();
            explorer.GetReport(args[0]);
        }
    }
}
