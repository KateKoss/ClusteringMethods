using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkConsole
{
    class Point
    {
        public Point(double[] coordinates)
        {
            this.dimension = coordinates.Length;
            this.coordinates = new double[this.dimension];
            for (int i = 0; i < coordinates.Length; i++)
            {
                this.coordinates[i] = coordinates[i];
            }
        }
        private int dimension;//размерность
        private double[] coordinates;
        public void setCoordinates(double[] coordinates)
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                this.coordinates[i] = coordinates[i];
            }
        }
        public double[] getCoordinates() { return this.coordinates; }
        public int getDimension() { return this.dimension; }

        public bool equals(Point point)
        {
            for (int i = 0; i < point.getDimension(); i++)
            {
                if (this.coordinates[i] != point.getCoordinates()[i])
                {
                    return false;
                }
            }
            return true;
        }
        //---------------------------------
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        private double x;
        private double y;
        public double getX() { return this.x; }
        public double getY() { return this.y; }
    }
}


//listOfPoints.Add(new Point(new double[] { 7.1, 5, 0 }));
//listOfPoints.Add(new Point(new double[] { 7, 5, 2 }));
//listOfPoints.Add(new Point(new double[] { 8, 4, -1 }));
//listOfPoints.Add(new Point(new double[] { 8, 5, 0 }));
//
//listOfPoints.Add(new Point(new double[] { 1, 3, 0 }));
//listOfPoints.Add(new Point(new double[] { 2, 2, 0 }));
//listOfPoints.Add(new Point(new double[] { 2, 3, 0 }));

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