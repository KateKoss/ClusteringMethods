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
            int n = 2;
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
                            System.Console.Write("\nEnter n - number of point coordinates: ");
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
                            n = Int32.Parse(s);
                            //Console.ReadKey();
                            
                        } while (n <= 0);

                        do
                        {
                            string s = "";
                            System.Console.Write("\nEnter number of n-dimensional points [more than zero]: ");
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
                        

                        //K_MEANS
                        for (int i = 0; i < numberOfPoints; i++)
                        {
                            DataPointKmeans dp = new DataPointKmeans(n);
                            dp.pointId = i;
                            rawDataToCluster.Add(dp);
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
                            n = Convert.ToInt32(temp[0]);
                            temp = myReadStream.ReadLine().Split(' ');
                            myReadStream.Peek();


                            radius = Convert.ToDouble(temp[0]);
                            Console.WriteLine("Dimension of point(random): {0}", n);
                            Console.WriteLine("Claster radius(random): {0}", radius);
                            double[] coordTemp = new double[n];
                            for (int i = 0; myReadStream.Peek() >= 0; i++)   //repeat rows= ... times
                            {
                                temp = myReadStream.ReadLine().Split(' ');
                                for (int j = 0; j < n; j++)
                                {
                                    //FOREl
                                    coordTemp[j] = Convert.ToDouble(temp[j]);
                                }
                                listOfPoints.Add(new Point(coordTemp));

                                //K-MEANS
                                DataPointKmeans dp = new DataPointKmeans(n);
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
            ForelAlgorithm alg1 = new ForelAlgorithm();

            alg1.createPointsFromFile();
            //alg1.createRandomPoints();
            //alg1.createPointsFromConcole();
            alg1.findClasters();

            Console.WriteLine("\n\n-----------------------K-MEANS ALGORITHM----------------------\n\n");
            KMeansAlgorithm alg2 = new KMeansAlgorithm(n, numberOfClusters, rawDataToCluster);
            alg2.Init(numberOfPoints);
            //Console.Write("\nEnter point id for which you want to find simular points: ");
            //int pointId = Convert.ToInt32(Console.ReadLine());
            //alg1.findTopTen(pointId);

            Console.ReadKey();
        }
    }
}
