namespace Losenordshanterare
{
    class Program
    {
        static void Main(string[] args)
        {
            FileService.WriteToFile("LUKAS");
            FileService.Execute();
            Console.ReadLine();
        }
    }
}