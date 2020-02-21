using System;

namespace FireFinder.Main
{
    class Program
    {
        static void Main(string[] args)
        {


            int dis = 12;
            int? district = 12;

            if (district.Equals(dis) || district == null)
            {
                Console.WriteLine("Hello Value");
            }
        }
    }
}
