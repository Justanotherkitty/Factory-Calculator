using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Factory_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i<args.Length; i ++) 
            {
                switch (args[i])
                {
                    case "/f":
                        if (args.Length > i + 1)
                        {
                            Console.WriteLine($"Reading file: {args[i + 1]}");
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("Error: /f Flag used but no file name given!");
                            Environment.Exit(1);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
