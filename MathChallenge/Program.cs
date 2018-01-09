using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathChallenge
{
	class Program
	{
		public static string num1;
		public static string num2;
		
		public static void Main(string[] args)
		{
			GetNumbers();
		}

		private static void QuitConsoleApp()
		{
			Console.WriteLine("\n\nPress R to repeat or any other key to exit the app.");
			ConsoleKeyInfo quitKey = Console.ReadKey();

			if (quitKey.Key.ToString().ToLower() == "r")
			{
				Console.Clear();
				GetNumbers();
			}
		}

		private static void GetNumbers()
		{
			Console.WriteLine("Please enter two ints with the same number of digits: ");

			Console.Write("Number 1: ");
			num1 = Console.ReadLine();

			Console.Write("Number 2: ");
			num2 = Console.ReadLine();

			CheckNumbersLength(num1, num2);
		}

		private static void CheckNumbersLength(string num1, string num2)
		{
			List<int> num1List = new List<int>();
			List<int> num2List = new List<int>();

			if (num1.Length == num2.Length)
			{
				GetDigits(num1, num1List);
				GetDigits(num2, num2List);
				GetEachDigitTotal(num1List, num2List);
			}
			else
			{
				NumbersDontMatchError();
			}
		}

		private static void GetEachDigitTotal(List<int> num1List, List<int> num2List)
		{
			List<int> answerList = new List<int>();

			for (int i = 0; i < num1.Length; i++)
			{
				answerList.Add(num1List[i] + num2List[i]);
			}
			CheckDigitTotalsAreSame(answerList, num1List, num2List);
		}

		private static void CheckDigitTotalsAreSame(List<int> answerList, List<int> num1List, List<int> num2List)
		{
			int val;
			var query = answerList.GroupBy(x => x)
			  .Where(g => g.Count() > 1)
			  .ToDictionary(x => x.Key, y => y.Count());

			query.TryGetValue(num1List[0] + num2List[0], out val);

			if (answerList.Count == val)
				BuildAnswerString(true, num1List, num2List);
			else
				BuildAnswerString(false, num1List, num2List);
		}

		private static void BuildAnswerString(bool bit, List<int> num1List, List<int> num2List)
		{
			string answer = string.Format("Number 1 = {0}, Number 2 = {1} => ", num1, num2);

			for (int i = 0; i < num1List.Count; i++)
			{
				if (i == 0)
					answer += string.Format("{0}+{1}", num1List[i], num2List[i]);
				else if(bit == true)
					answer += string.Format(" = {0}+{1}", num1List[i], num2List[i]);
				else if (bit == false)
					answer += string.Format(" != {0}+{1}", num1List[i], num2List[i]);
			}

			Console.WriteLine(string.Format("\n{0}", answer));
			Console.WriteLine(bit);
			QuitConsoleApp();
		}

		private static void GetDigits(string num, List<int> numList)
		{
			for(int i = 0; i < num.Length; i++)
			{
				numList.Add(int.Parse(num[i].ToString()));
			}
		}

		private static void NumbersDontMatchError()
		{
			Console.Clear();
			Console.WriteLine("*The numbers you have entered do not have the same number of digits.* \n");
			GetNumbers();
		}
	}
}
