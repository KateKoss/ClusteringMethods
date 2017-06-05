using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CourseWorkConsole
{
    class ForelAlgorithm
    {
        private int _DIMENSION; //размерность задачи(_DIMENSION = 2 - координатная плоскость)
        private List<Point> listOfPoints = new List<Point>();
        private double radius;
        List<List<Point>> clasters;
        //private int clasterIdToFind;
        public void findClasters()
        {
            if (listOfPoints.Count != 0)
            {
                printClasters(Recluster(listOfPoints));
            }
            else Console.WriteLine("At first you need have points!");
        }

        public void findTopTen(int pointIdForFinding)
        {
            int clasterIndex = -1;
            int pointIndex = -1;
            foreach (var claster in clasters)
            {
                if (clasterIndex != -1) break;
                else
                {
                    foreach (var point in claster)
                    {
                        if (point.getPointId() == pointIdForFinding)
                        {
                            pointIndex = claster.IndexOf(point);
                            clasterIndex = clasters.IndexOf(claster);
                            break;
                        }
                    }
                }
            }
            foreach (var point in clasters[clasterIndex])
            {
                point.setDistanceToSomePoint(Dist2(point, clasters[clasterIndex][pointIndex]));
            }
            var orderedClaster = clasters[clasterIndex].OrderBy(s => s.getDistanceToSomePoint());
            int count = 0;
            foreach (var point in orderedClaster)  //вывод топ 10 ближайших точек
            {
                if (point.getPointId() != pointIdForFinding)
                {
                    count++;
                    Console.Write("id = {0} | (", point.getPointId());
                    for (int j = 0; j < point.getDimension(); j++)
                    {
                        if (j != point.getDimension() - 1) Console.Write("{0};  ", point.getCoordinates()[j]);
                        else Console.WriteLine("{0})", point.getCoordinates()[j]);
                    }
                    if (count == 10) break;
                }
            }
        }
        /// <summary>Разбивает список точек на несколько кластеров, которые тоже хранятся как списки.</summary>
        /// <param name="Points">Список точек</param>
        /// <param name="Radius">Радиус кластеров</param>
        /// <returns>Список кластеров (список списков точек)</returns>
        public List<List<Point>> Recluster(List<Point> Points)
        {
            List<List<Point>> Result = new List<List<Point>>();
            Random RND = new Random();
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
                    int Count = 0;
                    foreach (Point P in Unclustered)//Считаем новый центр масс
                        if (Dist2(Center, P) < radius) //Если расстояние от текущего центра до точки(некластеризированной) меньше радиуса
                        {
                            for (int i = 0; i < P.getDimension(); i++)
                            {
                                sumCoordTemp[i] += P.getCoordinates()[i];//суммируем соответсвующие координаты каждой точки
                            }
                            //x += P.getX();
                            //y += P.getY();
                            Count++;
                        }
                    if (isPointsAroundCenter(sumCoordTemp, Center))
                    {
                        for (int i = 0; i < sumCoordTemp.Length; i++)
                        {
                            sumCoordTemp[i] /= Count;//и делим на количество этих точек(не кластеризированных, которые попали в радиус)
                        }

                        //NewCenter = new Point(x / Count, y / Count);
                        NewCenter = new Point(sumCoordTemp);
                    }
                    else break;
                }
                while (!Center.equals(NewCenter));//!centersCoincide(Center, NewCenter)
                List<Point> Cluster = new List<Point>(); //Переносим точки в новый кластер
                for (int i = Unclustered.Count - 1; i >= 0; i--)
                    if (Dist2(Center, Unclustered[i]) < radius)
                    {
                        Cluster.Add(Unclustered[i]);
                        Unclustered.RemoveAt(i);
                    }
                Result.Add(Cluster);

                //Console.WriteLine("-----------Claster #{0}-----------", Result.Count);
                //foreach (var point in Cluster)
                //{
                //    Console.Write("(");
                //    for (int j = 0; j < point.getDimension(); j++)
                //    {
                //        if (j != point.getDimension() - 1) Console.Write("{0};  ", point.getCoordinates()[j]);
                //        else Console.WriteLine("{0})", point.getCoordinates()[j]);
                //
                //    }
                //}
            }
            clasters = Result;
            return Result;
        }

        /// <summary>Расстояние между точками</summary>
        private double Dist2(Point P1, Point P2)
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

        private bool isPointsAroundCenter(double[] sumCoordTemp, Point Center)
        {
            for (int i = 0; i < sumCoordTemp.Length; i++)
            {
                if (sumCoordTemp[i] != Center.getCoordinates()[i])// если хоть одна координа не совпадает - return true
                {                                                 //это означает, что кроме этой точки(sumCoordTemp), в радиус попали другие точки
                    return true;
                }
            }
            return false;
        }

        public void printClasters(List<List<Point>> clasters)
        {
            int i = 1;
            foreach (var claster in clasters)
            {
                Console.WriteLine("-----------Claster #{0}-----------", i++);
                foreach (var point in claster)
                {
                    Console.Write("id = {0} | (", point.getPointId());
                    for (int j = 0; j < point.getDimension(); j++)
                    {
                        if (j != point.getDimension() - 1) Console.Write("{0};  ", point.getCoordinates()[j]);
                        else Console.WriteLine("{0})", point.getCoordinates()[j]);
                    }
                }
                Console.WriteLine();
            }
        }


        public void createRandomPoints()
        {
            Console.WriteLine("Random input information:");
            Random rnd = new Random();
            int _DIMENSION = rnd.Next(2, 10);
            Console.WriteLine("Dimension of point(random): {0}", _DIMENSION);
            int amountOfPoints = rnd.Next(10, 50);
            Console.WriteLine("Amount of points(random): {0}", amountOfPoints);
            this.radius = rnd.Next(500, 600);
            Console.WriteLine("Claster radius(random): {0}", this.radius);
            double[] coord = new double[_DIMENSION];
            Console.WriteLine();
            for (int i = 0; i < amountOfPoints; i++)
            {
                Console.WriteLine("------------Point #{0}------------", i + 1);
                for (int j = 0; j < _DIMENSION; j++)
                {
                    coord[j] = rnd.Next(1, 1000) - 200;
                    Console.WriteLine("Coordinate[{0}] of the point : {1}", j + 1, coord[j]);
                }
                listOfPoints.Add(new Point(coord));
            }
        }
        public void createPointsFromConcole()
        {
            Console.Write("Enter the dimention of problem: ");
            try
            {
                int _DIMENSION = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter amount of points: ");
                int amountOfPoints = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter claster radius: ");
                this.radius = Convert.ToDouble(Console.ReadLine());
                double[] coord = new double[_DIMENSION];

                for (int i = 0; i < amountOfPoints; i++)
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

        public void createPointsFromFile()
        {
            try
            {
                const string fileName = "Points.txt";
                StreamReader myReadStream = new StreamReader(fileName);     //создаем поток для чтения
                string[] temp = myReadStream.ReadLine().Split(' ');         //считываем строку, разделяем на числа
                _DIMENSION = Convert.ToInt32(temp[0]);
                temp = myReadStream.ReadLine().Split(' ');
                myReadStream.Peek();
                radius = Convert.ToDouble(temp[0]);
                Console.WriteLine("Dimension of point(random): {0}", _DIMENSION);
                Console.WriteLine("Claster radius(random): {0}", radius);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        

    }
}
