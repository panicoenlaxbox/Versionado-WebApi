using System;
using Microsoft.Owin.Hosting;

namespace Versionado
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:8082/";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Presione una tecla para continuar . . .");
                Console.ReadLine();
            }
        }
    }

}
