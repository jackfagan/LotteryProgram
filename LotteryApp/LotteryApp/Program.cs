﻿using System;
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
                int ticket = 0;
                int TotalPrize = 0;
                int ticketno = 0;

                for (int i = 0; i < tickets; i++)
                {
                    ticketno = ticketno + 1;
                    List<int> OneLine = new List<int>();
                    Console.WriteLine($"Please enter ticket {ticketno}'s numbers or enter 'Lucky Dip' for us to generate your 6 numbers");
                    OneLine = InputHandler(rnd, LotteryLine);
                    LotteryLines.Add(OneLine);
                }

                List<int> WinningLine = PrintWinningNumbers(rnd);
                LottoResult(LotteryLines, ticket, WinningLine, TotalPrize);
            }
        }

        public static List<int> InputHandler(Random rnd, List<int> LotteryLine)
        {
            string input = Console.ReadLine().Replace(" ", "").ToLower();

            if (input == "luckydip")
            {
                LotteryLine = LottoHelper.GetLuckyDip(rnd);
                Console.WriteLine($"Your lucky dip numbers are");
                LotteryLine.ForEach(Console.WriteLine);
            }

            else
            {
                try
                {
                    LotteryLine = LottoHelper.UsersChosenNumbers(input);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    InputHandler(rnd, LotteryLine);
                }
            }

            return LotteryLine;
        }

        public static int NumberOfTickets()
        {
            int tickets = 0;
            Console.WriteLine("How many tickets would you like?");
            string ticketstr = Console.ReadLine().Trim();

            if (int.TryParse(ticketstr, out tickets))
            {
                Console.WriteLine($"You have brought {tickets} tickets");
            }

            else
            {
                NumberOfTickets();
            }
            return tickets;

        }

        private static List<int> PrintWinningNumbers(Random rnd)
        {
            List<int> WinningNumbers = LottoHelper.GetWinningNumbers(rnd);
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

        private static void LottoResult(List<List<int>> LotteryLines, int ticket, List<int> WinningLine, int TotalPrize)
        {
            foreach (List<int> OneLine in LotteryLines)
            {
                ticket = ticket + 1;
                List<int> Winners = LottoHelper.WinningCalculator(OneLine, WinningLine);
                int Prize = LottoHelper.PrizeCalculator(Winners);
                OutputResult(Prize, ticket, Winners);
                TotalPrize = TotalPrize + Prize;
            }

            Console.WriteLine($"Your total prize money is £{TotalPrize}");
            Task.Delay(500).Wait(); Console.WriteLine("Play again!");
        }

        private static void OutputResult(int Prize, int ticket, List<int> Winners)
        {

            if (Prize > 0)
            {
                Task.Delay(800).Wait(); Console.Write("--WINNER--");
                Task.Delay(800).Wait(); Console.Write("--WINNER--");
                Task.Delay(800).Wait(); Console.Write("--WINNER--");
                Task.Delay(800).Wait(); Console.WriteLine($"You have matched {Winners.Count} numbers on ticket {ticket} You have won £{Prize}");
            }
            else
            {
                Task.Delay(800).Wait(); Console.WriteLine($"Unlucky! You matched 0 numbers on ticket {ticket}");
            }
        }
    }
}
