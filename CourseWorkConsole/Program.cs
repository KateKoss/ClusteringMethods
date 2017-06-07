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
            int numberOfPoints = 4;
            int _DIMENSION = 2;
            int numberOfClusters = 2;
            double radius = 1;
            int caseSwitch;
            StringBuilder str = new StringBuilder();
            List<Point> listOfPoints = new List<Point>();
            List<DataPointKmeans> rawDataToCluster = new List<DataPointKmeans>();

            System.Console.WriteLine("Corse work Kostycheva, Zinchenko IS-42 - Clustering Methods.");

            System.Console.WriteLine("\nChoose: ");
            System.Console.WriteLine("1 - generate points;");
            System.Console.WriteLine("2 - read from file.");

            do
            {
                System.Console.Write("Enter: ");
                string s = "";
                ConsoleKeyInfo key;

                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace)
                    {
                        double val = 0;
                        bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                        // Convert.ToInt32(key.KeyChar) == 46)//44-coma, 46-spot
                        if (_x)
                        {
                            s += key.KeyChar;
                            Console.Write(key.KeyChar);
                        }
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && s.Length > 0)
                        {
                            s = s.Substring(0, (s.Length - 1));
                            Console.Write("\b \b");
                        }
                    }
                }
                // Stops Receving Keys Once Enter is Pressed
                while (key.Key != ConsoleKey.Enter);

                Console.WriteLine();
                //Console.WriteLine("The Value You entered is : " + s);
                caseSwitch = Int32.Parse(s);
                //Console.ReadKey();

            } while (caseSwitch != 1 && caseSwitch != 2);
            
            switch (caseSwitch)
            {
                case 1:
                    {
                        do
                        {
                            string s = "";
                            System.Console.Write("\nEnter n - dimention(number of point coordinates): ");
                            ConsoleKeyInfo key;

                            do
                            {
                                key = Console.ReadKey(true);
                                if (key.Key != ConsoleKey.Backspace)
                                {
                                    double val = 0;
                                    bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                                   // Convert.ToInt32(key.KeyChar) == 46)//44-coma, 46-spot
                                    if (_x)
                                    {
                                        s += key.KeyChar;
                                        Console.Write(key.KeyChar);
                                    }
                                }
                                else
                                {
                                    if (key.Key == ConsoleKey.Backspace && s.Length > 0)
                                    {
                                        s = s.Substring(0, (s.Length - 1));
                                        Console.Write("\b \b");
                                    }
                                }
                            }
                            // Stops Receving Keys Once Enter is Pressed
                            while (key.Key != ConsoleKey.Enter);

                            Console.WriteLine();
                            //Console.WriteLine("The Value You entered is : " + s);
                            _DIMENSION = Int32.Parse(s);
                            
                        } while (_DIMENSION <= 0);

                        do
                        {
                            string s = "";
                            System.Console.Write("\nEnter amount of " + _DIMENSION + "-dimensional points [more than zero]: ");
                            ConsoleKeyInfo key;

                            do
                            {
                                key = Console.ReadKey(true);
                                if (key.Key != ConsoleKey.Backspace)
                                {
                                    double val = 0;
                                    bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                                    // Convert.ToInt32(key.KeyChar) == 46)//44-coma, 46-spot
                                    if (_x)
                                    {
                                        s += key.KeyChar;
                                        Console.Write(key.KeyChar);
                                    }
                                }
                                else
                                {
                                    if (key.Key == ConsoleKey.Backspace && s.Length > 0)
                                    {
                                        s = s.Substring(0, (s.Length - 1));
                                        Console.Write("\b \b");
                                    }
                                }
                            }
                            // Stops Receving Keys Once Enter is Pressed
                            while (key.Key != ConsoleKey.Enter);

                            Console.WriteLine();
                            //Console.WriteLine("The Value You entered is : " + s);
                            numberOfPoints = Int32.Parse(s);
                            //Console.ReadKey();

                        } while (numberOfPoints <= 0);
                                                
                       
                        System.Console.WriteLine("\nMethod K-means");
                        
                        do
                        {
                            string s = "";
                            System.Console.Write("\nEnter a desired number of clusters [must be 0< and <{0}]: ");
                            ConsoleKeyInfo key;

                            do
                            {
                                key = Console.ReadKey(true);
                                if (key.Key != ConsoleKey.Backspace)
                                {
                                    double val = 0;
                                    bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                                    // Convert.ToInt32(key.KeyChar) == 46)//44-coma, 46-spot
                                    if (_x)
                                    {
                                        s += key.KeyChar;
                                        Console.Write(key.KeyChar);
                                    }
                                }
                                else
                                {
                                    if (key.Key == ConsoleKey.Backspace && s.Length > 0)
                                    {
                                        s = s.Substring(0, (s.Length - 1));
                                        Console.Write("\b \b");
                                    }
                                }
                            }
                            // Stops Receving Keys Once Enter is Pressed
                            while (key.Key != ConsoleKey.Enter);

                            Console.WriteLine();
                            //Console.WriteLine("The Value You entered is : " + s);
                            numberOfClusters = Int32.Parse(s);
                        } while (numberOfClusters > numberOfPoints || numberOfClusters <= 0);

                        System.Console.WriteLine("\nMethod FOREL");
                        do
                        {
                            string s = "";
                            System.Console.Write("\nEnter claster radius: ");
                            ConsoleKeyInfo key;

                            do
                            {
                                key = Console.ReadKey(true);
                                if (key.Key != ConsoleKey.Backspace)
                                {
                                    double val = 0;
                                    bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                                    // Convert.ToInt32(key.KeyChar) == 46)//44-coma, 46-spot
                                    if (_x)
                                    {
                                        s += key.KeyChar;
                                        Console.Write(key.KeyChar);
                                    }
                                }
                                else
                                {
                                    if (key.Key == ConsoleKey.Backspace && s.Length > 0)
                                    {
                                        s = s.Substring(0, (s.Length - 1));
                                        Console.Write("\b \b");
                                    }
                                }
                            }
                            // Stops Receving Keys Once Enter is Pressed
                            while (key.Key != ConsoleKey.Enter);

                            Console.WriteLine();
                            //Console.WriteLine("The Value You entered is : " + s);
                            radius = Convert.ToDouble(s);
                        } while (radius <= 0);

                        Random rnd = new Random();
                        double[] tempCoord = new double[_DIMENSION];
                        //K_MEANS
                        for (int i = 0; i < numberOfPoints; i++)
                        {
                            DataPointKmeans dp = new DataPointKmeans(_DIMENSION);
                            dp.pointId = i;
                            rawDataToCluster.Add(dp);
                        }

                        //объеденить тут запись точек в наши классы!

                        //FOREL
                        for (int i = 0; i < numberOfPoints; i++)
                        {
                            Console.WriteLine("------------Point #{0}------------", i + 1);
                            for (int j = 0; j < _DIMENSION; j++)
                            {
                                tempCoord[j] = rnd.Next(1, 1000) - 200;
                                Console.WriteLine("Coordinate[{0}] of the point : {1}", j + 1, tempCoord[j]);
                            }
                            listOfPoints.Add(new Point(tempCoord));
                        }
                    }
                    break;
                case 2:
                    {
                        const string fileName = "Points.txt";
                        try
                        {
                            StreamReader myReadStream = new StreamReader(fileName);     //создаем поток для чтения
                            string[] temp = myReadStream.ReadLine().Split(' ');         //считываем строку, разделяем на числа
                            _DIMENSION = Convert.ToInt32(temp[0]);
                            temp = myReadStream.ReadLine().Split(' ');
                            myReadStream.Peek();
                            radius = Convert.ToDouble(temp[0]);

                            temp = myReadStream.ReadLine().Split(' ');
                            myReadStream.Peek();
                            numberOfClusters = Convert.ToInt32(temp[0]);
                            Console.WriteLine("Dimension of point: {0}", _DIMENSION);
                            Console.WriteLine("Claster radius: {0}", radius);
                            double[] coordTemp = new double[_DIMENSION];
                            for (int i = 0; myReadStream.Peek() >= 0; i++)   //repeat rows= ... times
                            {
                                temp = myReadStream.ReadLine().Split(' ');
                                for (int j = 0; j < _DIMENSION; j++)
                                {
                                    //FOREl
                                    coordTemp[j] = Convert.ToDouble(temp[j]);
                                }
                                listOfPoints.Add(new Point(coordTemp));

                                //K-MEANS
                                DataPointKmeans dp = new DataPointKmeans(_DIMENSION);
                                dp.pointId = i;
                                dp.a = coordTemp;
                                dp.Cluster = i;
                                rawDataToCluster.Add(dp);
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex); }
                    }
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            Console.WriteLine("\n-----------------------FOREL ALGORITHM----------------------\n\n");
            ForelAlgorithm alg1 = new ForelAlgorithm(_DIMENSION, listOfPoints, radius);

            //alg1.createPointsFromFile();
            //alg1.createRandomPoints();
            //alg1.createPointsFromConcole();
            alg1.findClasters();

            Console.WriteLine("\n\n-----------------------K-MEANS ALGORITHM----------------------\n\n");
            KMeansAlgorithm alg2 = new KMeansAlgorithm(_DIMENSION, numberOfClusters, rawDataToCluster);
            alg2.Init(numberOfPoints);
            //Console.Write("\nEnter point id for which you want to find simular points: ");
            //int pointId = Convert.ToInt32(Console.ReadLine());
            //alg1.findTopTen(pointId);

            Console.ReadKey();
        }
    }
}
