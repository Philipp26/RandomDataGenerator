using System;
using System.Linq;
using System.IO;
using System.Text;
using NumbericDataGenerator;

namespace RandomNumberGenerator
{
    class Program
    {
        #region Текст справочной информации
        public static readonly string helpText =
            "1. -h - call help\n" +
            "2. -c - data count\n" +
            "3. -seed - init generator\n" +
            "4. -o - path to output file\n" +
            "5. -d - distribution with option -normal, default -uniform" +
            "5.1 -";
        #endregion

        /// <summary>
        /// Метод для отображения справочной информации
        /// </summary>
        static void GetHelp()
        {
            Console.WriteLine(helpText);
            Environment.Exit(0);
        }

        
        static void Main(string[] args)
        {
            //args = new string[] { "-h", "-seed", "100", "-c", "25", "-o", @"D:\datas.txt"};
            //args = new string[] { "-seed", "100", "-c", "25", "-o", @"D:\datas.txt", "-d", "-normal" };

            var data = new int[] { };
            var arrDouble = new double[] { };

            if (args.Contains("-h"))
                GetHelp();

            if (args.Contains("-seed") && args.Contains("-c"))
            {
                var seed = Convert.ToInt32(args[Array.IndexOf(args, "-seed") + 1]);
                var dataCount = Convert.ToInt32(args[Array.IndexOf(args, "-c") + 1]);

                data = new NumbericDataGenerator.NumbericDataGenerator().GetUniformDistributionData(seed, dataCount);

                if (args.Contains("-d") && args.Contains("-normal"))
                {
                    arrDouble = Array.ConvertAll(data, Convert.ToDouble);
                    arrDouble = new NumbericDataGenerator.NumbericDataGenerator().GetNormalDistributionData(seed, dataCount);
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
                
                else
                {
                    if (arrDouble.Length == 0)
                        foreach (var element in data)
                            Console.WriteLine(element);
                    else
                        foreach (var element in arrDouble)
                            Console.WriteLine(element);
                }
            }
        }
    }
}
