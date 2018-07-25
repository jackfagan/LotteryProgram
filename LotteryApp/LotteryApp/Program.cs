using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LotteryApp.Helpers;

namespace LotteryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Jack Fagan's lottery. Changing lives since 2001");

            while (true)
            {
                int tickets = NumberOfTickets();
                List<int> lotteryNumbers = new List<int>();
                List<string> input = new List<string>();

                for (int i = 0; i < tickets; i++)
                {
                    Console.WriteLine("Please enter this tickets numbers or enter 'Lucky Dip' for us to generate your 6 numbers (100% legit)");
                    lotteryNumbers = InputHandler(input);
                }

                List<int> WinningLine = PrintWinningNumbers();

                int ticket = 0;
                for (int i = 0; i < tickets; i++)
                {
                    ticket = ticket + 1;
                    List<int> Winners = LottoHelper.WinningCalculator(lotteryNumbers, WinningLine);

                    foreach (string numberline in input)
                    {
                        int Prize = LottoHelper.PrizeCalculator(Winners);

                        if (Prize > 0)
                        {
                            Console.WriteLine($"Congratulations! You have matched {Winners.Count} numbers on ticket {ticket} You have won £{Prize}");
                        }
                        else
                        {
                            Console.WriteLine("Unlucky! You matched 0 numbers");
                        }

                    }
                }
            }
        }

        public static List<int> InputHandler(List<string> input)
        {
            string inputstr = Console.ReadLine();
            inputstr = LottoHelper.InputValidator(inputstr);
            input.Add(inputstr);
            List<int> LotteryLine = new List<int>();

            if (inputstr == "luckydip")
            {
                LotteryLine = LottoHelper.GetLuckyDip();
                Console.WriteLine($"Your lottery numbers are");
                LotteryLine.ForEach(Console.WriteLine);
            }

            //else
            //{
            //    LotteryLine = LottoHelper.UsersChosenNumbers(input);
            //}

            return LotteryLine;
        }

        public static int NumberOfTickets()
        {
            int tickets = 0;
            Console.WriteLine("How many tickets would you like?");
            string ticketstr = Console.ReadLine().Trim();
            int.TryParse(ticketstr, out tickets);
            return tickets;
        }

        private static List<int> PrintWinningNumbers()
        {
            List<int> WinningNumbers = LottoHelper.GetWinningNumbers();
            Console.WriteLine("This rounds winning numbers are...");
            foreach(int number in WinningNumbers)
            {
                Task.Delay(1500).Wait(); Console.WriteLine(number);
            }
            return WinningNumbers;
        }

        public static void ErrorMessage()
        {
            Console.WriteLine("Uh oh! A number has been entered multiple times");
        }

        private static void LottoResult(List<int> lotteryNumbers)
        {
        }

    }
}
