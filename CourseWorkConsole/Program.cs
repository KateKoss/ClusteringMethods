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
            //alg1.findDecision();
            Algorithm start = new Algorithm();
            start.Init();

            
            Console.ReadKey();
        }
    }
}
