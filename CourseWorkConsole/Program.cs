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
