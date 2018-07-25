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

        //public static List<int> UsersChosenNumbers(List<string> input)
        //{
        //    //split input list and look through each string of numbers in input
        //    List<int> LotteryLine = NumberValidator(input);
        //    return LotteryLine;
        //}

        public static List<int> NumberValidator(string inputstr)
        {
            inputstr = inputstr.Replace(" ", "").Replace("and", ",").Trim();
            string[] numberList = inputstr.Split(',', '+', '&');

            var groupedNumbers = numberList.GroupBy(n => n);

            foreach (var grp in groupedNumbers)
            {
                if (grp.Count() > 1)
                {
                    Program.ErrorMessage();
                }
            }

            numberList.ToList();
            return numberList.Select(int.Parse).ToList();
        }

        public static List<int> GetLuckyDip()
        {
            List<int> LotteryLine = new List<int>();
            Random rnd = new Random();

            for (int a = 1; a < 7; a++)
            {
                int LuckyDip = LineGenerator(rnd);
                LotteryLine.Add(LuckyDip);
            }

            LotteryLine.Sort();
            return LotteryLine;
        }

        public static int LineGenerator(Random rnd)
        {
            int GeneratedNumber = rnd.Next(1, 50);
            return GeneratedNumber;
        }

        internal static int WinningCalculator(object lotteryLine, List<int> winningLine)
        {
            throw new NotImplementedException();
        }

        public static List<int> GetWinningNumbers()
        {
            List<int> WinningNumbers = new List<int>();
            Random rnd = new Random();
            for (int a = 1; a < 7; a++)
            {
                int WinningNumber = WinnerNumbersGenerator(rnd);
                bool alreadyExist = WinningNumbers.Contains(WinningNumber);

                if (alreadyExist == true)
                {
                    GetWinningNumbers();
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

        public static List<int> WinningCalculator(List<int> LotteryLine, List<int> WinningLine)
        {
            List<int> Winners = new List<int>();

            foreach (int number in LotteryLine)
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
