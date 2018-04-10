namespace CleverbotDiscord
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Startup.RunAsync(args).GetAwaiter().GetResult();
        }
    }
}