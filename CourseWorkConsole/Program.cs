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
        Dictionary<Point, Point> association = new Dictionary<Point, Point>();
        //private static List<Point> createNormalizedPoints(List<Point> pointList, double radius)
        //{
        //    double[] max = new double[_DIMENSION]; //max[0] - for x, max[1] - for y
        //    for (int i = 0; i < max.Length; i++)
        //    {
        //        max[i] = 0.0;
        //    }
        //    double[] min = new double[_DIMENSION];
        //    for (int i = 0; i < max.Length; i++)
        //    {
        //        min[i] = Double.PositiveInfinity;
        //    }

        //    double[] aNew = new double[pointList.Count];
        //    foreach (var point in pointList)
        //    {

        //        if (point.getX() > max[0])
        //            max[0] = point.getX();
        //        if (point.getX() < min[0])
        //            min[0] = point.getX();

        //        if (point.getY() > max[1])
        //            max[1] = point.getY();
        //        if (point.getY() < min[1])
        //            min[1] = point.getY();

        //    }


        //    List<Point> normalizedlistOfPoint = new List<Point>();
        //    foreach (var point in pointList)
        //    {
        //        normalizedlistOfPoint.Add(new Point((point.getX() - min[0]) / (max[0] - min[0]),
        //            (point.getY() - min[1]) / (max[1] - min[1])
        //            ));
        //    }
        //    //Program.radius = (radius - min[1]) / (max[1] - min[1]);
        //    return normalizedlistOfPoint;
        //}

        ////создаем ассоциацию нормализованной точки и нормальной точки
        //private static Dictionary<Point, Point> createAssociation(List<Point> pointList)
        //{
        //    Dictionary<Point, Point> association = new Dictionary<Point, Point>();
        //    foreach (Point point in pointList)
        //    {
        //        association.Add(createNormalizedPoint(point), point);
        //    }
        //    return association;
        //}


        /// <summary>Разбивает список точек на несколько кластеров, которые тоже хранятся как списки.</summary>
        /// <param name="Points">Список точек</param>
        /// <param name="Radius">Радиус кластеров</param>
        /// <returns>Список кластеров (список списков точек)</returns>
        public static List<List<Point>> Recluster(List<Point> Points, double Radius)
        {
            List<List<Point>> Result = new List<List<Point>>();
            Random RND = new Random();
            double R = Radius;
            List<Point> Unclustered = new List<Point>(Points);
            
            while (Unclustered.Count > 0)
            {
                Point NewCenter = Unclustered[RND.Next(Unclustered.Count)];
                Point Center;
                do //Двигаем центр, пока он не устаканится
                {
                    Center = NewCenter;
                    //double x = 0.0, y = 0.0;
                    double[] sumCoordTemp = new double[Points[0].getDimension()];
                    //int Count = 0;
                    foreach (Point P in Unclustered)//Считаем новый центр масс
                        if (Dist2(Center, P) < R)
                        {
                            for (int i = 0; i < P.getDimension(); i++)
                            {
                                sumCoordTemp[i] += P.getCoordinates()[i];//суммируем соответсвующие координаты кажой точки
                            }
                            //x += P.getX();
                            //y += P.getY();
                            //Count++;
                        }
                    for (int i = 0; i < sumCoordTemp.Length; i++)
                    {
                        sumCoordTemp[i] /= Unclustered.Count;//и делим на количество этих точек(не кластеризированных)
                    }
                    //NewCenter = new Point(x / Count, y / Count);
                    NewCenter = new Point(sumCoordTemp);
                }
                while (!Center.equals(NewCenter));//!centersCoincide(Center, NewCenter)
                List<Point> Cluster = new List<Point>(); //Переносим точки в новый кластер
                for (int i = Unclustered.Count - 1; i >= 0; i--)
                    if (Dist2(Center, Unclustered[i]) < R)
                    {
                        Cluster.Add(Unclustered[i]);
                        Unclustered.RemoveAt(i);
                    }
                Result.Add(Cluster);
            }
            return Result;
        }

        /// <summary>Квадрат расстояния между точками</summary>
        private static double Dist2(Point P1, Point P2)
        {
            double temp = 0;
            if (P1.getDimension() == P2.getDimension())
            {
                for (int i = 0; i < P1.getDimension(); i++)
                {
                    temp += Math.Pow(P1.getCoordinates()[i] - P2.getCoordinates()[i], 2);
                }
                return Math.Sqrt(temp);
            }
            else
            {
                Console.WriteLine("Error!\nDimension of two points are different!!!");
                return Double.PositiveInfinity;
            }
        }
                      
        public static void printClasters(List<List<Point>> clasters)
        {
            int i = 1;
            foreach (var claster in clasters)
            {
                Console.WriteLine("-----------Claster #{0}-----------", i++);
                foreach (var point in claster)
                {
                    Console.Write("(");
                    for (int j = 0; j < point.getDimension(); j++)
                    {
                        if (j != point.getDimension()-1) Console.Write("{0};  ", point.getCoordinates()[j]); 
                        else Console.WriteLine("{0})", point.getCoordinates()[j]);
                    }
                }
                Console.WriteLine();
            }
        }

        public static void createPointsFromConcole()
        {
            Console.Write("Enter the dimention of problem(x): ");
            try
            {
                int _DIMENSION = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter amount of points(x): ");
                int size = Convert.ToInt32(Console.ReadLine());
                double[] coord = new double[_DIMENSION];

                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine("------------Point #{0}------------", i + 1);
                    for (int j = 0; j < _DIMENSION; j++)
                    {
                        Console.Write("Enter the coordinates[{0}] of the point : ", j + 1);
                        if (!Double.TryParse(Console.ReadLine(), out coord[j]))
                        {
                            Console.WriteLine("Error! Enter double number.");
                            j--;
                        }
                    }
                    listOfPoints.Add(new Point(coord));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error! Please, enter integer numbers.\nReload the programm.");
            }
        }

        public static void createPointsFromFile()
        {
            const string fileName = "Points.txt";
            StreamReader myReadStream = new StreamReader(fileName);     //создаем поток для чтения
            string[] temp = myReadStream.ReadLine().Split(' ');         //считываем строку, разделяем на числа
            _DIMENSION = Convert.ToInt32(temp[0]);
            double[] coordTemp = new double[_DIMENSION];
            for (int i = 0; myReadStream.Peek() >= 0; i++)   //repeat rows= ... times
            {
                temp = myReadStream.ReadLine().Split(' ');
                for (int j = 0; j < _DIMENSION; j++)
                {
                    coordTemp[j] = Convert.ToDouble(temp[j]);
                }
                listOfPoints.Add(new Point(coordTemp));
            }
        }

        static int _DIMENSION = 2; //размерность задачи(_DIMENSION = 2 - координатная плоскость)
        static List<Point> listOfPoints = new List<Point>();
        static void Main(string[] args)
        {
            createPointsFromConcole();
            //Program.createPointsFromFile();
            printClasters(Recluster(listOfPoints, 4));
            //printClasters(Recluster(createNormalizedPoints(listOfPoints, radius), Program.radius));
            Console.ReadKey();
        }
    }

    //
}
