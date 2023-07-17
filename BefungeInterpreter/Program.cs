namespace BefungeInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            BefungeInterpreter i = new BefungeInterpreter();
            i.LoadFile("befunge.befunge");
            try
            {
                i.Execute();
            } catch(Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("X: " + i.x + " Y: " + i.y);
            }
        }   
    }
}

