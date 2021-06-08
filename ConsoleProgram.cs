using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
class ConsoleProgram
{
	public void Run()
	{
		Page pg = new Page("qweqwdq");
		//Thread.Sleep(Timeout.Infinite);
	}
}

class Tools
{

	public static int MaxHorizontalLenght = 80;
	public static int DontScrollMaxVerticalValue = 25;

	public static int LeftCursorBuffer;
	public static int TopCursorBuffer;

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

class Page
{
	public List<ActiveElement> ActiveElementsList = new List<ActiveElement>();

	public ConsoleColor PageBackgroundColor = ConsoleColor.Black;
	public ConsoleColor GlobalTextColor = ConsoleColor.White;
	public (int, int) ConsoleSize = (80, 30);
	public string Title;
	public bool NavigationOn = false;
	public int clickCount = 0;

	ConsoleKeyInfo key;
	public Page(string title, ConsoleColor backColor = ConsoleColor.Black)
	{
		this.Title = title;

		content();
		Console.CursorVisible = false;

		while(true)
		{
			key = Console.ReadKey(intercept: true);
			pageNavigation();
			keyboardEvents();
		}
	}

	void content()
	{
		Tools.SkipLines(1);
		Console.Write("=========Welcome to PowerfulConsole (ver. DevelopmentBuild - 0.1)=========");
		ActiveElement text1 = new ActiveElement((0, 3), ActiveElementsList);
		ActiveElement text2 = new ActiveElement((0, 5), ActiveElementsList);
		ActiveElement text3 = new ActiveElement((0, 6), ActiveElementsList);
		ActiveElement text4 = new ActiveElement((0, 7), ActiveElementsList);
		ActiveElement text5 = new ActiveElement((0, 8), ActiveElementsList);

		ActiveElement text6 = new ActiveElement((0, 9), ActiveElementsList);
		ActiveElement text7 = new ActiveElement((0, 10), ActiveElementsList);
		ActiveElement text8 = new ActiveElement((0, 11), ActiveElementsList);
		ActiveElement text9 = new ActiveElement((0, 12), ActiveElementsList);
		ActiveElement text10 = new ActiveElement((0, 13), ActiveElementsList);
		ActiveElement text11 = new ActiveElement((0, 14), ActiveElementsList);
		Tools.SkipLines(2);
		Button button1 = new Button((Console.CursorLeft, Console.CursorTop), "Enter", (10, 3));
		
		Tools.SkipLines(2); // УСТАНОВКА СТАНДАРТНЫХ ЦВЕТОВ
		Console.Write(new string(' ', 1), Console.ForegroundColor = this.GlobalTextColor, Console.BackgroundColor = this.PageBackgroundColor);
		
	}

	void pageNavigation()
	{
		if (NavigationOn == false)
		{
			if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow)
			{
				NavigationOn = true;
				Console.CursorVisible = true;
				Tools.SetPosition(ActiveElementsList[clickCount].Position.Item1, ActiveElementsList[clickCount].Position.Item2);
			}
		}

		if (NavigationOn == true)
		{
			if (key.Key == ConsoleKey.DownArrow)
			{
				clickCount++;
				if (clickCount > ActiveElementsList.Count - 1)
				{
					clickCount = 0;
					Tools.SetPosition(ActiveElementsList[clickCount].Position.Item1, ActiveElementsList[clickCount].Position.Item2);
				}
				Tools.SetPosition(ActiveElementsList[clickCount].Position.Item1, ActiveElementsList[clickCount].Position.Item2);
			}
			if (key.Key == ConsoleKey.UpArrow)
			{
				clickCount--;
				if (clickCount < 0)
				{
					clickCount = ActiveElementsList.Count - 1;
					Tools.SetPosition(ActiveElementsList[ActiveElementsList.Count - 1].Position.Item1, ActiveElementsList[ActiveElementsList.Count - 1].Position.Item2);
				}
				Tools.SetPosition(ActiveElementsList[clickCount].Position.Item1, ActiveElementsList[clickCount].Position.Item2);
			}
		}

		if (key.Key == ConsoleKey.Escape)
		{
			NavigationOn = false;
			Console.CursorVisible = false;
			foreach (ActiveElement i in ActiveElementsList)
			{
				i.Event_Normal();
			}
		}



		try
		{
			selectEffect();
		}
		catch (Exception ex)
		{

		}

	}

	void selectEffect()
	{
		if (ActiveElementsList[clickCount].Position.Item2 == Console.CursorTop)
		{
			ActiveElementsList[clickCount].Event_Selected();
			if (clickCount == 0)
			{
				if (ActiveElementsList[ActiveElementsList.Count - 1].ElementSelected == true)
				{
					ActiveElementsList[ActiveElementsList.Count - 1].Event_Normal();
					Tools.SetPosition(ActiveElementsList[0].Position);
				}
				if (ActiveElementsList[1].ElementSelected == true)
				{
					ActiveElementsList[1].Event_Normal();
					Tools.SetPosition(ActiveElementsList[clickCount].Position);
				}
			}

			if (clickCount == ActiveElementsList.Count - 1)
			{
				if (ActiveElementsList[0].ElementSelected == true)
				{
					ActiveElementsList[0].Event_Normal();
					Tools.SetPosition(ActiveElementsList[ActiveElementsList.Count - 1].Position);
				}
			}

			if (ActiveElementsList[clickCount - 1].ElementSelected == true)
			{
				ActiveElementsList[clickCount - 1].Event_Normal();
				Tools.SetPosition(ActiveElementsList[clickCount].Position);
			}

			if (ActiveElementsList[clickCount + 1].ElementSelected == true)
			{
				ActiveElementsList[clickCount + 1].Event_Normal();
				Tools.SetPosition(ActiveElementsList[clickCount].Position);
			}
		}
	}

	void keyboardEvents()
	{
		if (NavigationOn == true)
		{
			if (key.Key == ConsoleKey.Enter)
			{
				ActiveElementsList[clickCount].Event_ChangeTextColor(ConsoleColor.Red);
			}
		}
	}
}

class ActiveElement
{
	public (int, int) Position;  //Кортеж с значениями left и top
	public bool ElementSelected = false;
	public string TextContent;
	public int TextStringLenght;
	public ActiveElement((int, int) position, List<ActiveElement> list, string content = "default content(text)")
	{
		this.Position = position;
		Tools.SetPosition(position.Item1, position.Item2);
		Console.Write(content);
		this.TextContent = content;
		this.TextStringLenght = content.Length;
		object o = this.MemberwiseClone();
		list.Add((ActiveElement)o);
	}

	public void Event_Message()
	{
		MessageBox.Show(TextContent);
	}

	public void Event_ChangeTextColor(ConsoleColor textcolor)
	{
		Tools.SetPosition(Position.Item1, Position.Item2);
		Console.Write(TextContent, Console.ForegroundColor = textcolor);
	}

	public void Event_Selected()
	{
		ElementSelected = true;
		Tools.SetPosition(this.Position.Item1, this.Position.Item2);
		Console.Write(TextContent, Console.ForegroundColor = ConsoleColor.Yellow);
		Tools.SetPosition(this.Position.Item1, this.Position.Item2);
	}

	public void Event_Normal()
	{
		ElementSelected = false;
		Tools.SetPosition(this.Position.Item1, this.Position.Item2);
		Console.Write(TextContent, Console.ForegroundColor = ConsoleColor.Gray);
		Tools.SetPosition(this.Position.Item1, this.Position.Item2);
	}
}

class Button
{
	public (int, int) Position;
	public (int, int) Size;
	public string Content;

	ConsoleColor ContentColor;
	ConsoleColor NormalButtonColor;
	ConsoleColor PushedButtonColor;
	ConsoleColor SelecteButtonColor;

	public bool ButtonPush = false;

	public Button((int, int) position, string content, (int, int) size, ConsoleColor normal = ConsoleColor.White,
														ConsoleColor push = ConsoleColor.White,
														ConsoleColor select = ConsoleColor.White,
														ConsoleColor contcolor = ConsoleColor.Black)
	{
		this.Position = position;
		this.Size = size;
		this.Content = content;
		this.NormalButtonColor = normal;
		this.PushedButtonColor = push;
		this.SelecteButtonColor = select;
		this.ContentColor = contcolor;

		drawButton();
	}

	void drawButton()
	{
		Tools.SetPosition(Position.Item1, Position.Item2);
		for (int i = 0; i < Size.Item2; i++)
		{
			Console.WriteLine(new string(' ', Size.Item1), Console.BackgroundColor = NormalButtonColor);
		}
		if ((Size.Item2 % 2) != 0)
		{
			Tools.SetPosition(Position.Item1, Position.Item2 + (Size.Item2 / 2));
			Console.Write(Content, Console.ForegroundColor = ContentColor, Console.BackgroundColor = NormalButtonColor);
		}
		if ((Size.Item2 % 2) == 0)
		{
			Tools.SetPosition(Position.Item1, Position.Item2);
			Console.Write(Content, Console.ForegroundColor = ContentColor, Console.BackgroundColor = NormalButtonColor);
		}



	}


}