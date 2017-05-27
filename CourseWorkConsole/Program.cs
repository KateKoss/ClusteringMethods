using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    //int Cnt = 0;
                    foreach (Point P in Unclustered)//Считаем новый центр масс
                        if (Dist2(Center, P) < R)
                        {
                            for (int i = 0; i < P.getDimension(); i++)
                            {
                                sumCoordTemp[i] += P.getCoordinates()[i];
                            }
                            //x += P.getX();
                            //y += P.getY();
                            //Cnt++;
                        }
                    for (int i = 0; i < sumCoordTemp.Length; i++)
                    {
                        sumCoordTemp[i] /= Unclustered.Count;
                    }
                    //NewCenter = new Point(x / Cnt, y / Cnt);
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

        //Проверка на совпадение центров окружностей(точек)
        //private static bool centersCoincide(Point P1, Point P2)
        //{
        //    if (P1.getX() == P2.getX() && P1.getY() == P2.getY())
        //    {
        //        return true;
        //    }
        //    else return false;
        //}

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
                        if (j != point.getDimension()-1) Console.Write("{0},  ", point.getCoordinates()[j]); 
                        else Console.WriteLine("{0})", point.getCoordinates()[j]);
                    }
                }
                Console.WriteLine();
            }
        }

        const int _DIMENSION = 2; //размерность задачи(_DIMENSION = 2 - координатная плоскость)
        //static double radius;
        static void Main(string[] args)
        {
            List<Point> listOfPoints = new List<Point>();
            listOfPoints.Add(new Point(new double[] { 7, 5, 0 }));
            listOfPoints.Add(new Point(new double[] { 7, 5, 2 }));
            listOfPoints.Add(new Point(new double[] { 8, 4, -1 }));
            listOfPoints.Add(new Point(new double[] { 8, 5, 0 }));

            listOfPoints.Add(new Point(new double[] { 1, 3, 0 }));
            listOfPoints.Add(new Point(new double[] { 2, 2, 0 }));
            listOfPoints.Add(new Point(new double[] { 2, 3, 0 }));

            //listOfPoints.Add(new Point(1, 3));
            //listOfPoints.Add(new Point(2, 2));
            //listOfPoints.Add(new Point(2, 3));
            //listOfPoints.Add(new Point(2, 4));
            //listOfPoints.Add(new Point(3, 2));
            //listOfPoints.Add(new Point(3, 3));
            //listOfPoints.Add(new Point(3, 4));
            //listOfPoints.Add(new Point(4, 4));
            //
            ////listOfPoints.Add(new Point(1.23435, 10003));
            ////listOfPoints.Add(new Point(2.432, 10002));
            ////listOfPoints.Add(new Point(2.3465, 10003));
            ////listOfPoints.Add(new Point(2.342, 10004));
            ////listOfPoints.Add(new Point(3.435435, 10002));
            ////listOfPoints.Add(new Point(3.32443, 10003));
            ////listOfPoints.Add(new Point(3.546, 10004));
            ////listOfPoints.Add(new Point(4.4365787654, 10004));
            //
            //listOfPoints.Add(new Point(7, 5));
            //listOfPoints.Add(new Point(8, 4));
            //listOfPoints.Add(new Point(8, 5));
            //listOfPoints.Add(new Point(8, 6));
            //listOfPoints.Add(new Point(9, 4));
            //listOfPoints.Add(new Point(9, 5));
            //listOfPoints.Add(new Point(9, 6));
            //listOfPoints.Add(new Point(10, 5));
            //listOfPoints.Add(new Point(10, 7));
            //listOfPoints.Add(new Point(3, 8));
            //listOfPoints.Add(new Point(3, 9));
            //listOfPoints.Add(new Point(4, 8));
            //listOfPoints.Add(new Point(4, 9));
            //listOfPoints.Add(new Point(4, 10));
            //listOfPoints.Add(new Point(5, 8));
            //listOfPoints.Add(new Point(5, 9));
            //listOfPoints.Add(new Point(5, 10));
            printClasters(Recluster(listOfPoints, 4));
            //printClasters(Recluster(createNormalizedPoints(listOfPoints, radius), Program.radius));
            Console.ReadKey();
        }
    }

    //
}
