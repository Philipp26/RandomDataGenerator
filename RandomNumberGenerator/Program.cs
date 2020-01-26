using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace RandomNumberGenerator
{
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
        /// Метод для генерации последовательнсоти псевдослучайных чисел
        /// </summary>
        /// <param name="seed">Значение ининциализатора пседвослучайных чисел</param>
        /// <param name="dataCount">Количество генерируемых данных</param>
        /// <returns>Сгенерированный массив</returns>
        static int[] GetRandomNumberInitializer(int seed, int dataCount)
        {
            var rnd = new Random(seed);
            var rndArray = new int[dataCount];

            for (int i = 0; i < dataCount; i++)
                rndArray[i] = rnd.Next(1, 100);

            return rndArray;
        }

        static void Main(string[] args)
        {
            //args = new string[] { "-h", "-seed", "100", "-c", "25", "-o", @"D:\datas.txt"};
            args = new string[] {"-seed", "100", "-c", "25", "-o", @"D:\datas.txt" };

            var data = new int[] { };

            if (args.Contains("-h"))
                GetHelp();

            if (args.Contains("-seed") && args.Contains("-c"))
            {
                var seed = Convert.ToInt32(args[Array.IndexOf(args, "-seed") + 1]);
                var dataCount = Convert.ToInt32(args[Array.IndexOf(args, "-c") + 1]);

                data = GetRandomNumberInitializer(seed, dataCount);
            }

            if(args.Contains("-o"))
            {
                var path = args[Array.IndexOf(args, "-o") + 1];

                if (!String.IsNullOrWhiteSpace(args[Array.IndexOf(args, "-o") + 1]))
                {
                    path = args[Array.IndexOf(args, "-o") + 1];
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                        {
                            foreach (var element in data)                            
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
