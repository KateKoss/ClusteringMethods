using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CourseWorkConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ForelAlgorithm alg1 = new ForelAlgorithm();

            alg1.createPointsFromFile();
            //alg1.createRandomPoints();
            //alg1.createPointsFromConcole();
            alg1.findClasters();
            //Algorithm start = new Algorithm();
            //start.Init();
            Console.Write("\nEnter point id for which you want to find simular points: ");
            int pointId = Convert.ToInt32(Console.ReadLine());
            alg1.findTopTen(pointId);

            Console.ReadKey();
        }
    }
}
