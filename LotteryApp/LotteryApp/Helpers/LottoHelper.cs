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
            List<int> NumberList = numberString.Select(x => Int32.Parse(x)).ToList();
            NumberList.GroupBy(n => n);

            foreach (int number in NumberList)
            {
                if (NumberList.Count != 6)
                {
                    throw new Exception("You have not entered 6 numbers. Try again...");
                }

                else if (number > 49 || number < 1)
                {
                    throw new Exception("Please  keep your numbers between 1 to 49. Try again...");
                }

                List<int> numberLists = new List<int>();

                foreach (int numbers in NumberList)
                {
                    if (numberLists.Contains(numbers))
                    {
                        throw new Exception("Please do not repeat your numbers! Try again...");
                    }

                    else
                    {
                        numberLists.Add(numbers);
                    }
                }
            }
            return NumberList;
        }

        public static List<int> GetLuckyDip(Random rnd)
        {
            List<int> lotteryLine = new List<int>();

            for (int a = 1; a < 7; a++)
            {
                int LuckyDip = LineGenerator(rnd);

                if (lotteryLine.Contains(LuckyDip))
                {
                    GetWinningNumbers(rnd);
                }

                else
                {
                    lotteryLine.Add(LuckyDip);
                }
            }

            lotteryLine.Sort();
            return lotteryLine;
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
            List<int> winningNumbers = new List<int>();

            for (int a = 1; a < 7; a++)
            {
                int winningNumber = WinnerNumbersGenerator(rnd);

                if (winningNumbers.Contains(winningNumber))
                {
                    GetWinningNumbers(rnd);
                }

                else
                {
                    winningNumbers.Add(winningNumber);
                }
            }

            winningNumbers.Sort();
            return winningNumbers;
        }

        public static int WinnerNumbersGenerator(Random rnd)
        {
            int WinningNumbers = rnd.Next(1, 50);
            return WinningNumbers;
        }

        public static List<int> WinningCalculator(List<int> oneLine, List<int> winningLine)
        {
            List<int> winners = new List<int>();

            foreach (int number in oneLine)
            {
                if (winningLine.Contains(number))
                {
                    winners.Add(number);
                }
            }

            return winners;
        }

        public static int PrizeCalculator(List<int> winners)
        {
            int Prize = 0;
            switch (winners.Count)
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
