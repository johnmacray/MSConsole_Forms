using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class Tools
{
	public static int MaxHorizontalLenght = 80;
	public static int DontScrollMaxVerticalValue = 25;

	public static int LeftCursorBuffer;
	public static int TopCursorBuffer;

	public static (int, int) CurrentCursorPosition = (Console.CursorLeft, Console.CursorTop);

	/// <summary>
	///  Задает позицию курсора двумя параметрами: x(left), y(top).
	/// </summary>
	public static void SetPosition(int left, int top)
	{
		try
		{
			Console.SetCursorPosition(left, top);
		}
		catch (ArgumentOutOfRangeException ex)
		{
			MessageBox.Show("Uncorrect position value. Use only positive values");
		}

	}

	public static void SetPosition((int, int) position)
	{
		try
		{
			Console.SetCursorPosition(position.Item1, position.Item2);
		}
		catch (ArgumentOutOfRangeException ex)
		{
			MessageBox.Show("Uncorrect position value. Use only positive values");
		}
	}

	/// <summary>
	///  Создает горизонтальную строку из указанного символа указанное кол-во раз, с указанием на перенос строки
	/// </summary>
	public static string HorizontalLine(string symbol, int count, bool writeLine = false)
	{
		string line = null;
		for (int i = 0; i < count; i++)
		{
			line = symbol;
			Console.Write(symbol);
		}
		if (writeLine) Console.WriteLine();
		return null;
	}

	/// <summary>
	///  Создает вертикальную строку из указанного символа указанное кол-во раз, с указанием на перенос строки
	/// </summary>
	public static string VerticalLine(string symbol, int count, bool writeLine = false)
	{
		string line = null;
		int position = Console.CursorLeft;
		for (int i = 0; i < count; i++)
		{
			Console.SetCursorPosition(position, Console.CursorTop);
			Console.WriteLine(symbol);
		}
		if (writeLine) Console.WriteLine();
		Console.SetCursorPosition(position, Console.CursorTop);
		return line;
	}

	/// <summary>
	///  В РАЗРАБОТКЕ
	/// </summary>
	public static void TextField(int lenght, string symbol, ConsoleColor color, int positionLeft, int positionTop)
	{
		Console.ForegroundColor = color;
		SetPosition(positionLeft, positionTop - 1);
		HorizontalLine(symbol, lenght);
		SetPosition(positionLeft, positionTop + 1);
		HorizontalLine(symbol, lenght);
		//SetPosition(positionLeft, positionTop);

		Console.WriteLine();
		//Console.ReadLine();
	}

	public static void SkipLines(int skipCount)
	{
		for (int i = 0; i < skipCount; i++)
		{
			Console.WriteLine();
		}
	}

	/*	public static string ColorfulString()
		{
			return string a;
		}*/
}

