using System;
using System.Collections.Generic;
using System.Linq;

namespace LotteryApp.Helpers
{
    class LottoHelper
    {
        public static string InputValidator(string inputstr)
        {
            inputstr = inputstr.ToLower().Replace(" ", "");
            return inputstr;
        }

        public static List<int> UsersChosenNumbers(string input)
        {
            List<int> LotteryLine = NumberValidator(input);
            return LotteryLine;
        }

        public static List<int> NumberValidator(string input)
        {
            input = input.Replace(" ", "").Replace("and", ",").Trim();
            string[] numberString = input.Split(',', '+', '&');
            List<int> numberList = numberString.Select(x => Int32.Parse(x)).ToList();
            numberList.GroupBy(n => n);

            foreach (int number in numberList)
            {
                if (numberList.Count != 6)
                {
                    throw new Exception("You have not entered 6 numbers. Try again...");
                }

                else if (number > 49 || number < 1)
                {
                    throw new Exception("Please  keep your numbers between 1 to 49. Try again...");
                }

                else if (numberList.Contains(number))
                {
                    throw new Exception("Please do not repeat your numbers! Try again...");
                }
            }
            return numberList;
        }

        public static List<int> GetLuckyDip(Random rnd)
        {
            List<int> LotteryLine = new List<int>();

            for (int a = 1; a < 7; a++)
            {
                int LuckyDip = LineGenerator(rnd);

                if (LotteryLine.Contains(LuckyDip))
                {
                    GetWinningNumbers(rnd);
                }
                else
                {
                    LotteryLine.Add(LuckyDip);
                }
            }

            LotteryLine.Sort();
            return LotteryLine;
        }

        public static int LineGenerator(Random rnd)
        {
            int GeneratedNumber = rnd.Next(1, 50);
            return GeneratedNumber;
        }

        internal static int WinningCalculator(object lotteryLine)
        {
            throw new NotImplementedException();
        }

        public static List<int> GetWinningNumbers(Random rnd)
        {
            List<int> WinningNumbers = new List<int>();

            for (int a = 1; a < 7; a++)
            {
                int WinningNumber = WinnerNumbersGenerator(rnd);

                if (WinningNumbers.Contains(WinningNumber))
                {
                    GetWinningNumbers(rnd);
                }
                else
                {
                    WinningNumbers.Add(WinningNumber);
                }
            }
            WinningNumbers.Sort();
            return WinningNumbers;
        }

        public static int WinnerNumbersGenerator(Random rnd)
        {
            int WinningNumbers = rnd.Next(1, 50);
            return WinningNumbers;
        }

        public static List<int> WinningCalculator(List<int> OneLine, List<int> WinningLine)
        {
            List<int> Winners = new List<int>();

            foreach (int number in OneLine)
            {
                if (WinningLine.Contains(number))
                {
                    Winners.Add(number);
                }
            }

            return Winners;
        }

        public static int PrizeCalculator(List<int> Winners)
        {
            int Prize = 0;
            switch (Winners.Count)
            {
                case 1:
                    Prize = 10;
                   break;

                case 2:
                    Prize = 30;
                   break;

                case 3:
                    Prize = 100;
                   break;

                case 4:
                    Prize = 5000;
                    break;

                case 5:
                    Prize = 30000;
                    break;

                case 6:
                    Prize = 1000000;
                    break;

                default:
                    Prize = 0;
                    break;
            }

            return Prize;
        }
    }
}
