using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkConsole
{
    class KMeansAlgorithm
    {
        List<DataPointKmeans> rawDataToCluster = new List<DataPointKmeans>();
        List<DataPointKmeans> normalizedDataToCluster = new List<DataPointKmeans>();
        List<DataPointKmeans> clusters = new List<DataPointKmeans>();
        private int numberOfClusters = 0;
        private int n = 0;
        private double radius = 0;

        public KMeansAlgorithm(int n, int numberOfClusters, List<DataPointKmeans> rawDataToCluster, double radius)
        {
            this.n = n;
            this.numberOfClusters = numberOfClusters;
            this.rawDataToCluster = rawDataToCluster;
            this.radius = radius;
        }

        public void Init(int numberOfPoints)
        {
            InitilizeRawData(numberOfPoints);

            for (int i = 0; i < numberOfClusters; i++)
            {
                clusters.Add(new DataPointKmeans(n) { Cluster = i });
            }
            //System.Console.WriteLine("Data BEFORE normalizing-------------------");
            //ShowData(rawDataToCluster);
            NormalizeData();

            //System.Console.WriteLine("Data AFTER normalizing-------------------");
            //ShowData(normalizedDataToCluster);
            Cluster(normalizedDataToCluster, numberOfClusters);
            StringBuilder sb = new StringBuilder();
            var group = rawDataToCluster.GroupBy(s => s.Cluster).OrderBy(s => s.Key);
            foreach (var g in group)
            {
                sb.AppendLine("Cluster # " + g.Key + ":");
                foreach (var value in g)
                {

                    sb.Append(value.pointId + " | " + value.ToString());
                    sb.AppendLine();
                }
                sb.AppendLine("------------------------------");
            }
            System.Console.WriteLine(sb);

            FindTopNearestPointsInClaster(group, numberOfPoints);


        }

        public void FindTopNearestPointsInClaster(IOrderedEnumerable<IGrouping<int, DataPointKmeans>> group, int numberOfPoints)
        {
            double[,] distances;
            int pointIdForFinding;
            int findTop;
            do
            {
                StringBuilder sb = new StringBuilder();
                do
                {
                    string s = "";
                    System.Console.Write("\nEnter point id for which you want to find simular points: ");
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
                    pointIdForFinding = Int32.Parse(s);
                } while (pointIdForFinding <= 0);



                int clusterIdForFinding = -1;
                DataPointKmeans centre = new DataPointKmeans(n);

                foreach (var g in group)
                {
                    foreach (var value in g)
                    {
                        if (value.pointId == pointIdForFinding)
                        {
                            clusterIdForFinding = g.Key;

                            centre.a = value.a;
                            centre.pointId = value.pointId;
                            centre.Cluster = value.Cluster;
                        }
                    }
                }

                int countOfPointInCluster = 0;


                if (clusterIdForFinding != -1)
                {
                    int k = 0;

                    var pointsInOneClaster = rawDataToCluster.FindAll(s => s.Cluster == clusterIdForFinding).OrderBy(s => s.pointId);

                    foreach (var value in pointsInOneClaster)
                    {
                        countOfPointInCluster++;
                    }
                    distances = new double[countOfPointInCluster, 2];
                    if (countOfPointInCluster != 0)
                    {
                        sb.AppendLine("\nCluster # " + clusterIdForFinding + ":");
                        foreach (var value in pointsInOneClaster)
                        {
                            DataPointKmeans point = new DataPointKmeans(n);
                            point.a = value.a;
                            point.Cluster = value.Cluster;
                            point.pointId = value.pointId;

                            distances[k, 0] = EuclideanDistance(point, centre);
                            distances[k, 1] = value.pointId;
                            sb.Append(value.pointId + " | " + value.ToString());
                            sb.AppendLine();
                            sb.AppendLine("------------------------------");

                            k++;
                        }

                        double[] temp = new double[2];
                        for (int j = 0; j < countOfPointInCluster; j++) // найпростіше бульбашкове сортування 
                            for (int i = 0; i < countOfPointInCluster - 1; i++)
                            {
                                if (distances[i, 0] > distances[i + 1, 0])
                                {
                                    temp[0] = distances[i, 0];
                                    temp[1] = distances[i, 1];
                                    distances[i, 0] = distances[i + 1, 0];
                                    distances[i, 1] = distances[i + 1, 1];
                                    distances[i + 1, 0] = temp[0];
                                    distances[i + 1, 1] = temp[1];
                                }
                            }

                        //Array.Sort(distances);
                    }
                    else sb.AppendLine("\nThere are no points in cluster # " + clusterIdForFinding + ".");
                    System.Console.WriteLine(sb);

                    do
                    {
                        string s = "";
                        System.Console.Write("\nEnter top to find (must be less then {0}): ", countOfPointInCluster);
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
                        findTop = Int32.Parse(s);
                    } while (findTop <= 0 || findTop > countOfPointInCluster);

                    sb.Clear();

                    if (distances.Length > 1)
                    {
                        if (countOfPointInCluster != 0)
                        {
                            DataPointKmeans top = new DataPointKmeans(n);
                            List<DataPointKmeans> topList = new List<DataPointKmeans>();
                            sb.AppendLine("\n----------------K-MEANS---------------\nTop " + findTop + " in cluster # " + clusterIdForFinding + ":");
                            for (int i = 0; i < findTop+1; i++)
                            {
                                top = rawDataToCluster.SingleOrDefault(s => s.pointId == distances[i, 1]);
                                topList.Add(top);
                            }
                            foreach (var value in topList)
                            {
                                if (value.pointId != pointIdForFinding)
                                {
                                    sb.Append(value.pointId + " | " + value.ToString());
                                    sb.AppendLine();
                                    sb.AppendLine("------------------------------");
                                }
                            }
                            System.Console.WriteLine(sb);
                            sb.Clear();


                           // double[,] tempCoord = new double[numberOfPoints, n];
                            List<Point> listOfPoints = new List<Point>();
                            //FOREL
                            for (int i = 0; i < numberOfPoints; i++)
                            {
                                double[] tempCoord2 = new double[n];
                                //Console.WriteLine("------------Point #{0}------------", i + 1);
                                for (int j = 0; j < n; j++)
                                {
                                    //tempCoord[i, j] = rawDataToCluster[i].a;
                                    //Console.WriteLine("Coordinate[{0}] of the point : {1}", j + 1, tempCoord[i, j]);
                                    tempCoord2[j] = rawDataToCluster[i].a[j];
                                }
                                listOfPoints.Add(new Point(tempCoord2));
                            }

                            sb.AppendLine("\n\n----------------FOREL---------------\nTop 10 in cluster # " + clusterIdForFinding + ":");
                            System.Console.WriteLine(sb);
                            ForelAlgorithm fa = new ForelAlgorithm(n,listOfPoints, radius);
                            fa.findTopTen(pointIdForFinding);
                        }
                    }
                    else System.Console.WriteLine("\nNot found.");
                }
                else System.Console.WriteLine("\nNot found.");


            } while (true);
        }

        private void InitilizeRawData(int numberOfPoints)
        {
            if (rawDataToCluster.Count == 0)
            {
                for (int i = 0; i < numberOfPoints; i++)
                {
                    DataPointKmeans dp = new DataPointKmeans(n);
                    dp.pointId = i;
                    rawDataToCluster.Add(dp);
                }
            }
        }

        private void ShowData(List<DataPointKmeans> data)
        {
            string str = "";
            foreach (DataPointKmeans dp in data)
            {
                str += dp.ToString() + Environment.NewLine;
            }
            System.Console.WriteLine(str);
        }


        private void NormalizeData()
        {
            double[] max = new double[n];
            for (int i = 0; i < max.Length; i++)
            {
                max[i] = 0.0;
            }
            double[] min = new double[n];
            for (int i = 0; i < min.Length; i++)
            {
                min[i] = Double.PositiveInfinity;
            }

            double[] aNew = new double[n];
            for (int i = 0; i < aNew.Length; i++)
            {
                aNew[i] = 0.0;
            }

            foreach (DataPointKmeans dataPoint in rawDataToCluster)
            {
                for (int i = 0; i < dataPoint.a.Length; i++)
                {
                    if (dataPoint.a[i] > max[i])
                        max[i] = dataPoint.a[i];
                    if (dataPoint.a[i] < min[i])
                        min[i] = dataPoint.a[i];
                }
            }

            foreach (DataPointKmeans dataPoint in rawDataToCluster)
            {
                for (int i = 0; i < aNew.Length; i++)
                {
                    aNew[i] = (dataPoint.a[i] - min[i]) / (max[i] - min[i]);
                }
                DataPointKmeans dp = new DataPointKmeans(n);
                for (int i = 0; i < n; i++)
                {
                    dp.a[i] = aNew[i];
                }

                normalizedDataToCluster.Add(dp);
            }
        }

        public void Cluster(List<DataPointKmeans> data, int numClusters)
        {
            bool changed = true;
            bool success = true;
            //визначаємо рандомно ценрт кожного класу
            InitializeCentroids();

            int maxIteration = data.Count * 10;
            //номер ітерацій (поріг н повинен перевищувати максимума)
            int threshold = 0;
            //поки жоден кластер не пустий і відбуваються переміщення даних з кластеру до кластеру
            while (success == true && changed == true && threshold < maxIteration)
            {
                ++threshold;
                success = UpdateDataPointMeans();
                changed = UpdateClusterMembership();
            }
        }

        private void InitializeCentroids()
        {
            Random random = new Random(numberOfClusters);
            for (int i = 0; i < numberOfClusters; ++i)//кожен кластер повинен мати хоч один член (точку, вектор)
            {
                normalizedDataToCluster[i].Cluster = rawDataToCluster[i].Cluster = i;
            }
            for (int i = numberOfClusters; i < normalizedDataToCluster.Count; i++)//дал робимо рандомно
            {
                normalizedDataToCluster[i].Cluster = rawDataToCluster[i].Cluster = random.Next(0, numberOfClusters);
            }
        }

        private bool UpdateDataPointMeans()
        {
            if (EmptyCluster(normalizedDataToCluster)) return false;

            //групуємо за номером кластеру
            var groupToComputeMeans = normalizedDataToCluster.GroupBy(s => s.Cluster).OrderBy(s => s.Key);
            int clusterIndex = 0;
            double[] a = new double[n];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = 0.0;
            }


            foreach (var item in groupToComputeMeans)
            {
                foreach (var value in item)
                {
                    for (int i = 0; i < n; i++)
                    {
                        a[i] += value.a[i];
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    clusters[clusterIndex].a[i] = a[i] / item.Count();
                }

                clusterIndex++;
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = 0.0;
                }
            }
            return true;
        }

        private bool EmptyCluster(List<DataPointKmeans> data)
        {
            var emptyCluster =
            data.GroupBy(s => s.Cluster).OrderBy(s => s.Key).Select(g => new { Cluster = g.Key, Count = g.Count() });

            foreach (var item in emptyCluster)
            {
                if (item.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private double EuclideanDistance(DataPointKmeans dataPoint, DataPointKmeans mean)
        {
            double diffs = 0.0;
            diffs = Math.Pow(dataPoint.a[0] - mean.a[0], 2);
            for (int i = 1; i < n; i++)
            {
                diffs += Math.Pow(dataPoint.a[i] - mean.a[i], 2);
            }
            return Math.Sqrt(diffs);
        }

        private bool UpdateClusterMembership()
        {
            bool changed = false;

            double[] distances = new double[numberOfClusters];

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < normalizedDataToCluster.Count; ++i)
            {

                for (int k = 0; k < numberOfClusters; ++k)
                    distances[k] = EuclideanDistance(normalizedDataToCluster[i], clusters[k]);

                int newClusterId = MinIndex(distances);
                if (newClusterId != normalizedDataToCluster[i].Cluster)
                {
                    changed = true;
                    normalizedDataToCluster[i].Cluster = rawDataToCluster[i].Cluster = newClusterId;
                    string str = "";
                    for (int j = 0; j < n; j++)
                    {
                        str += "a" + j + 1 + ": " + rawDataToCluster[i].a[j] + ", ";
                    }
                    sb.AppendLine("Data Point >> " + str + " moved to Cluster # " + newClusterId);
                }
                else
                {
                    sb.AppendLine("No change.");
                }
                sb.AppendLine("------------------------------");
                //System.Console.WriteLine(sb);                
            }
            if (changed == false)
                return false;
            if (EmptyCluster(normalizedDataToCluster)) return false;
            return true;
        }

        private int MinIndex(double[] distances)
        {
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }
            return indexOfMin;
        }
    }
}
