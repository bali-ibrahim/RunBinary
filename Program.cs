namespace RunBinary
{
    public class Program
    {
        public static int Main(string[] _)
        {
            var job = new Job();
            job.Execute(null).Wait();
            return 0;
        }
    }
}