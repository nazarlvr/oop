using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace task3
{
    class Converter
    {
        public Dictionary <string, double> valuta = new Dictionary<string, double>();
        public Converter (double x, double y)
        {
            valuta.Add ("usd", x);
            valuta.Add("euro", y);
        }
        public void Add(string name, double x)
        {
            valuta.Add(name, x);
        }
        public double ToUa(string name, double value)
        {
            return value * valuta[name];
        }

        public double ToForeign(string name, double value)
        {
            if(valuta[name] == 0) { Console.WriteLine("No Data"); return -1; }
            else
            return value / valuta[name];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Converter change = new Converter(25.1, 32.05);
            change.Add("pounds", 36.6);
            Console.WriteLine(change.ToUa("pounds", 50.3) + " is 50.3 pounds in uah");
            Console.WriteLine(change.ToForeign("usd", 100) + " is 100 uah in usd");
            Console.WriteLine(change.ToUa("euro", 0.5) + " is 0.5 euro in uah");
            Console.WriteLine(change.ToForeign("pounds", 366) + " is 366 uah in pounds");
        }
    }
}