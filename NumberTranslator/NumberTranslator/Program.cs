using System.Numerics;

namespace NumberTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Графический интерфейс
            while (true)
            {
                Console.Write("Введите число : ");
                Console.WriteLine(GoNum(Console.ReadLine()) + "\n");
            }
        }

        static string GoNum(string str)
        {
            // Улавливает ошибку при введении строки
            try
            {
                BigInteger num = BigInteger.Parse(str);
                return NumberTranslator(num);
            }
            catch
            {
                return "Введите число!";
            }
        }

        static string NumberTranslator(BigInteger num)
        {
            // Логика
            try
            {
                // Числа с 0 по 9
                BigInteger[] mas_first = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                Dictionary<BigInteger, string> first = new Dictionary<BigInteger, string>
                {
                    [0] = "zero",
                    [1] = "one",
                    [2] = "two",
                    [3] = "three",
                    [4] = "four",
                    [5] = "five",
                    [6] = "six",
                    [7] = "seven",
                    [8] = "eight",
                    [9] = "nine",
                };

                // Числа с 10 по 19
                BigInteger[] mas_second = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
                Dictionary<BigInteger, string> second = new Dictionary<BigInteger, string>
                {
                    [10] = "ten",
                    [11] = "eleven",
                    [12] = "twelve",
                    [13] = "thriteen",
                    [14] = "fourteen",
                    [15] = "fifteen",
                    [16] = "sixteen",
                    [17] = "seventeen",
                    [18] = "eighteen",
                    [19] = "nineteen",
                };

                // Числа с 20 по 90
                BigInteger[] mas_thrid = { 20, 30, 40, 50, 60, 70, 80, 90 };
                Dictionary<BigInteger, string> thrid = new Dictionary<BigInteger, string>
                {
                    [20] = "twenty",
                    [30] = "thirty",
                    [40] = "forty",
                    [50] = "fifty",
                    [60] = "sixty",
                    [70] = "seventy",
                    [80] = "eighty",
                    [90] = "ninety"
                };

                // Степени десятки
                Dictionary<BigInteger, string> infinity = new Dictionary<BigInteger, string>
                {
                    [BigInteger.Pow(10, 2)] = "hundred",
                    [BigInteger.Pow(10, 3)] = "thousand",
                    [BigInteger.Pow(10, 6)] = "million",
                    [BigInteger.Pow(10, 9)] = "billion",
                    [BigInteger.Pow(10, 12)] = "trillion",
                    [BigInteger.Pow(10, 15)] = "quadrillion",
                    [BigInteger.Pow(10, 18)] = "quintillion",
                    [BigInteger.Pow(10, 21)] = "sextillion",
                    [BigInteger.Pow(10, 24)] = "septillion",
                    [BigInteger.Pow(10, 27)] = "octillion",
                    [BigInteger.Pow(10, 30)] = "nonillion",
                    [BigInteger.Pow(10, 33)] = "decillion",
                    [BigInteger.Pow(10, 36)] = "undecillion",
                    [BigInteger.Pow(10, 39)] = "duodecillion",
                };

                // Количество символов в числе
                int key = Convert.ToString(num).Length;

                // Проверка на отрицание
                if (BigInteger.Abs(num) != num)
                {
                    return "negative " + NumberTranslator(BigInteger.Abs(num));
                }
                // Числа с 0 по 9
                else if (Array.IndexOf(mas_first, num) >= 0)
                {
                    return first[num];
                }
                // Числа с 10 по 19
                else if (Array.IndexOf(mas_second, num) >= 0)
                {
                    return second[num];
                }
                // Числа из thrid
                else if (Array.IndexOf(mas_thrid, num) >= 0)
                {
                    return thrid[num];
                }
                // Числа с 20 по 99
                else if (key == 2)
                {
                    return thrid[num / 10 * 10] + " " + NumberTranslator(num % 10);
                }
                // Числа со 100 по 900
                else if (key == 3)
                {
                    return NumberTranslator(num / 100) + " " + infinity[100] + (num / 100 * 100 != num ? " and " + NumberTranslator(num % 100) : "");
                }
                // Все остальные числа
                else
                {
                    int k = (key - 1) / 3 * 3;
                    return NumberTranslator(num / BigInteger.Pow(10, k)) + " " + infinity[BigInteger.Pow(10, k)] + (num / BigInteger.Pow(10, k) * BigInteger.Pow(10, k) != num ? (" " + NumberTranslator(num % BigInteger.Pow(10, k))) : "");
                }
            }
            catch
            {
                // Если число не входит в словарь infinity, то оно слишком большое
                return "Число слишком большое!";
            }
        }
    }
}