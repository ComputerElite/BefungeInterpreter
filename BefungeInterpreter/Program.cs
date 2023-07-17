namespace BefungeInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            BefungeInterpreter i = new BefungeInterpreter();
            i.LoadFile("befunge.befunge");
            i.Execute();
        }   
    }
}

