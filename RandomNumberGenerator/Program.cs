using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace RandomNumberGenerator
{
    public class NormalRandom
    {
        private Random random;
        private double z1 = double.NegativeInfinity;

        public NormalRandom(int seed)
        {
            random = new Random(seed);
        }

        public double Next()
        {
            double result;
            if (!double.IsNegativeInfinity(z1))
            {
                result = z1;
                z1 = double.NegativeInfinity;
            }
            else
            {
                double x, y, s;
                do
                {
                    x = random.NextDouble() * 2 - 1;
                    y = random.NextDouble() * 2 - 1;
                    s = x * x + y * y;
                } while (s <= 0 || s > 1);

                result = x * Math.Sqrt(-2.0 * Math.Log(s) / s);
                z1 = y * Math.Sqrt(-2.0 * Math.Log(s) / s);
            }
            return result;
        }

        public double Next(double mean, double stddev)
        {
            return Next() * stddev + mean;
        }
    }

    class Program
    {
        #region Текст справочной информации
        public static readonly string helpText =
            "1. -h - call help\n" +
            "2. -c - data count\n" +
            "3. -seed - init generator\n" +
            "4. -o - path to output file\n";
        #endregion

        /// <summary>
        /// Метод для отображения справочной информации
        /// </summary>
        static void GetHelp()
        {
            Console.WriteLine(helpText);
            Environment.Exit(0);
        }

        /// <summary>
        /// Метод для генерации последовательнсоти псевдослучайных чисел с учетом равномерного распредления
        /// </summary>
        /// <param name="seed">Значение ининциализатора пседвослучайных чисел</param>
        /// <param name="dataCount">Количество генерируемых данных</param>
        /// <returns>Сгенерированный массив</returns>
        static int[] GetUniformDistributionData(int seed, int dataCount)
        {
            var rnd = new Random(seed);
            var rndArray = new int[dataCount];

            for (int i = 0; i < dataCount; i++)
                rndArray[i] = rnd.Next(1, 100);

            return rndArray;
        }

        /// <summary>
        /// Метод для генерации последовательнсоти псевдослучайных чисел с учетом номрального распредления
        /// </summary>
        /// <param name="seed">Значение ининциализатора пседвослучайных чисел</param>
        /// <param name="dataCount">Количество генерируемых данных</param>
        /// <returns>Сгенерированный массив</returns>
        static double[] GetNormalDistributionData(int seed, int dataCount)
        {
            var rnd = new NormalRandom(seed);
            var rndArray = new double[dataCount];

            for (int i = 0; i < rndArray.Length; i++)            
                rndArray[i] = rnd.Next(10, 2);

            return rndArray;
        }

        static void Main(string[] args)
        {
            //args = new string[] { "-h", "-seed", "100", "-c", "25", "-o", @"D:\datas.txt"};
            args = new string[] {"-seed", "100", "-c", "25", "-o", @"D:\datas.txt", "-d", "-normal"};

            var data = new int[] { };
            var arrDouble = new double[] { };

            if (args.Contains("-h"))
                GetHelp();

            if (args.Contains("-seed") && args.Contains("-c"))
            {
                var seed = Convert.ToInt32(args[Array.IndexOf(args, "-seed") + 1]);
                var dataCount = Convert.ToInt32(args[Array.IndexOf(args, "-c") + 1]);

                data = GetUniformDistributionData(seed, dataCount);

                if (args.Contains("-d") && args.Contains("-normal"))
                {
                    arrDouble = Array.ConvertAll(data, Convert.ToDouble);
                    arrDouble = GetNormalDistributionData(seed, dataCount);
                }
            }

            if(args.Contains("-o"))
            {
                var path = args[Array.IndexOf(args, "-o") + 1];

                if (!String.IsNullOrWhiteSpace(args[Array.IndexOf(args, "-o") + 1]))
                {
                    path = args[Array.IndexOf(args, "-o") + 1];

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
                        {
                            if (arrDouble.Length == 0)
                                foreach (var element in data)
                                    sw.WriteLine(element);
                            else
                                foreach (var element in arrDouble)
                                    sw.WriteLine(element);
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }               
            }
        }
    }
}
