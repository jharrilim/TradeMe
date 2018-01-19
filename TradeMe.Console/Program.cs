using System;
using static System.Console;
using TradeMe.Actor;
using TradeMe.Trade;

namespace TradeMe.Console
{
    class Program
    {
		private static bool exit = false;
		private static readonly Exchange exchange = new Exchange("JooTrade");
        static void Main(string[] args)
        {
			while(!exit)
			{
				PrintMenu();
			}
			WriteLine("Done.");
        }

		static void PrintHeader()
		{
			WriteLine("+-------------------------+");
			WriteLine("|         TradeMe         |");
			WriteLine("+-------------------------+");
		}

		static void PrintMenu()
		{
			Clear();
			PrintHeader();
			WriteLine("| 1) Add Security         |");
			WriteLine("| 2) View Securities      |");
			WriteLine("| 3) Add Shareholder      |");
			WriteLine("| 4) View Shareholders    |");
			WriteLine("| 5) Select Shareholder   |");
			WriteLine("| Q) Quit                 |");
			WriteLine("+-------------------------+");
			WriteLine("| Enter option: ");
			SetCursorPosition(16, 10);
			string key = ReadLine();
			ParseMenuOption(key);
		}

		static void ParseMenuOption(string option)
		{
			switch (option)
			{
				case "q":
				case "Q":
					exit = true;
					break;
				case "1":
					AddSecurityPrompt();
					break;
				case "2":
					ViewSecurities();
					break;
				case "3":
					AddShareholder();
					break;
				case "4":
					ViewShareholders();
					break;
				case "5":
					SelectShareholder();
					break;
			}
		}

		static void AddSecurityPrompt()
		{
			Clear();
			PrintHeader();
			WriteLine("| Security Name:          |");
			WriteLine("|                         |");
			WriteLine("| Security Symbol:        |");
			WriteLine("|                         |");
			WriteLine("+-------------------------+");
			SetCursorPosition(2, 4);
			string name = ReadLine();
			SetCursorPosition(2, 6);
			string symbol = ReadLine();
			Security security = new Security(name, symbol);
			exchange.EnlistSecurity(security);
			SetCursorPosition(0, 8);
			WriteLine("{0} - ({1}) added.", name, symbol);
			WriteLine("Press any key to continue.");
			ReadKey();
		}

		static void ViewSecurities()
		{
			Clear();
			PrintHeader();
			int i = 1;
			foreach(Security s in exchange.Securities)
			{
				WriteLine($"{i++}. {s.ToString()}");
			}
			WriteLine("Press any key to continue.");
			ReadKey();
		}

		static void AddShareholder()
		{
			Clear();
			PrintHeader();
			WriteLine("| Shareholder Name:       |");
			WriteLine("|                         |");
			WriteLine("| Initial Balance:        |");
			WriteLine("|                         |");
			WriteLine("+-------------------------+");
			SetCursorPosition(2, 4);
			string name = ReadLine();
			SetCursorPosition(2, 6);
			decimal initialBalance = decimal.Parse(ReadLine());
			Shareholder shareholder = new Shareholder(name, initialBalance);
			exchange.AddShareholder(shareholder);
			SetCursorPosition(2, 8);
			WriteLine("{0}, {1:c2} added.", name, initialBalance);
			WriteLine("Press any key to continue.");
			ReadKey();
		}

		private static void PrintShareholders()
		{
			Clear();
			PrintHeader();
			int i = 1;
			foreach (Shareholder s in exchange.Shareholders)
			{
				WriteLine($"{i++}. {s.Name}");
			}
		}

		private static void ViewShareholders()
		{
			PrintShareholders();
			WriteLine("Press any key to continue.");
			ReadKey();
		}

		private static void SelectShareholder()
		{
			PrintShareholders();
			WriteLine("Select Shareholder: ");
			string idx = ReadLine();
		}
	}
}
