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
