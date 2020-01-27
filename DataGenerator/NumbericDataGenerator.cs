using System;

namespace NumbericDataGenerator
{
    public class NumbericDataGenerator
    {
        public class NormalRandom
        {
            private readonly Random random;
            private double z1 = double.NegativeInfinity;

            public NormalRandom(int seed)
            {
                random = new Random(seed);
            }

            /// <summary>
            /// Возвращает число, с учетом нормального распределения.
            /// </summary>
            /// <returns>Число типа double</returns>
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

        /// <summary>
        /// Метод для генерации последовательнсоти псевдослучайных чисел с учетом равномерного распредления
        /// </summary>
        /// <param name="seed">Значение ининциализатора пседвослучайных чисел</param>
        /// <param name="dataCount">Количество генерируемых данных</param>
        /// <returns>Сгенерированный массив</returns>
        public int[] GetUniformDistributionData(int seed, int dataCount)
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
        public double[] GetNormalDistributionData(int seed, int dataCount)
        {
            var rnd = new NormalRandom(seed);
            var rndArray = new double[dataCount];

            for (int i = 0; i < rndArray.Length; i++)
                rndArray[i] = rnd.Next(10, 2);

            return rndArray;
        }
    }
}
