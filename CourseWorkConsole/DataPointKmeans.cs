using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkConsole
{
    public class DataPointKmeans
    {
        private double n { get; set; }
        public double[] a { get; set; }
        public int Cluster { get; set; }
        public int pointId { get; set; }
        private static Random r = new Random();

        public DataPointKmeans(int n)
        {
            this.n = n;
            a = new double[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = r.Next(0, 100);                
            }           
            
            Cluster = 0;
        }

        public DataPointKmeans()
        {

        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < n; i++)
            {
                str += "{" + a[i].ToString("f" + 1) + "}; ";
            }
            return str;
        }

    }
}
