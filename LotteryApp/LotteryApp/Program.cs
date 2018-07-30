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
            Random rnd = new Random();
            Console.WriteLine("Welcome to Jack Fagan's lottery. Changing lives since 2001");

            while (true)
            {
                int tickets = NumberOfTickets();

                var LotteryLines = new List<List<int>>();
                List<string> input = new List<string>();
                List<int> LotteryLine = new List<int>();
                int Ticket = 0;
                int TotalPrize = 0;
                int ticketno = 0;

                for (int i = 0; i < tickets; i++)
                {
                    ticketno = ticketno + 1;
                    List<int> OneLine = new List<int>();
                    Console.WriteLine($"Please enter ticket {ticketno}'s numbers (1-49) or enter 'Lucky Dip' for us to generate your 6 numbers");
                    OneLine = InputHandler(rnd, LotteryLine);
                    LotteryLines.Add(OneLine);
                }

                List<int> WinningLine = PrintWinningNumbers(rnd);
                LottoResult(LotteryLines, Ticket, WinningLine, TotalPrize);
            }
        }

        public static int NumberOfTickets()
        {
            int tickets = 0;

            while (tickets == 0)
            {
                Console.WriteLine("How many tickets would you like?");
                string ticketstr = Console.ReadLine().Trim();

                if (int.TryParse(ticketstr, out tickets))
                {
                    Console.WriteLine($"You have brought {tickets} tickets");
                    break;
                }

                else
                {
                    Console.WriteLine("Please enter a valid number of tickets");
                }
            }

            return tickets;
        }

        public static List<int> InputHandler(Random rnd, List<int> lotteryLine)
        {
            while (lotteryLine.Count == 0)
            {
                string input = Console.ReadLine().Replace(" ", "").ToLower();

                if (input == "luckydip")
                {
                    lotteryLine = LottoHelper.GetLuckyDip(rnd);
                    Console.WriteLine($"Your lucky dip numbers are");
                    lotteryLine.ForEach(Console.WriteLine);
                    break;
                }

                else
                {
                    try
                    {
                        lotteryLine = LottoHelper.UsersChosenNumbers(input);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            return lotteryLine;
        }

        private static List<int> PrintWinningNumbers(Random rnd)
        {
            List<int> winningNumbers = LottoHelper.GetWinningNumbers(rnd);
            Console.WriteLine("This rounds winning numbers are...");

            foreach(int number in winningNumbers)
            {
                Task.Delay(1500).Wait(); Console.WriteLine(number);
            }

            return winningNumbers;
        }

        private static void OutputResult(int prize, int ticket, List<int> winners)
        {

            if (prize > 0)
            {
                Task.Delay(800).Wait(); Console.Write("--WINNER--");
                Task.Delay(800).Wait(); Console.Write("--WINNER--");
                Task.Delay(800).Wait(); Console.Write("--WINNER--");
                Task.Delay(800).Wait(); Console.WriteLine($"You have matched {winners.Count} numbers on ticket {ticket} You have won £{prize}");
            }

            else
            {
                Task.Delay(800).Wait(); Console.WriteLine($"Unlucky! You matched 0 numbers on ticket {ticket}");
            }
        }

        private static void LottoResult(List<List<int>> LotteryLines, int ticket, List<int> WinningLine, int totalPrize)
        {
            foreach (List<int> OneLine in LotteryLines)
            {
                ticket = ticket + 1;
                List<int> Winners = LottoHelper.WinningCalculator(OneLine, WinningLine);
                int prize = LottoHelper.PrizeCalculator(Winners);
                OutputResult(prize, ticket, Winners);
                totalPrize = totalPrize + prize;
            }

            Console.WriteLine($"Your total prize money is £{totalPrize}");
            Task.Delay(500).Wait(); Console.WriteLine("Play again!");
        }
    }
}
