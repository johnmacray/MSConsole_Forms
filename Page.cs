using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class Page
{
	public List<ActiveElement> ActiveElementsList = new List<ActiveElement>();

	public List<Button> ButtonList = new List<Button>(); // Для навигации по кнопкам нужно переделывать pageNavigation()

	public ConsoleColor PageBackgroundColor = ConsoleColor.Black;
	public ConsoleColor GlobalTextColor = ConsoleColor.White;
	public (int, int) ConsoleSize = (80, 30);
	public string Title;
	public bool NavigationOn = false;
	public int clickCount = 0;

	ConsoleKeyInfo key;
	public Page(string title, (int, int) size)
	{
		this.Title = title;
		this.ConsoleSize = (size.Item1, size.Item2);
		Console.SetWindowSize(this.ConsoleSize.Item1, this.ConsoleSize.Item2);

		content();
		Console.CursorVisible = false;
		update();
	}

	void update()
	{
		while (true)
		{
			key = Console.ReadKey(intercept: true);
			pageNavigation();
			keyboardEvents();
		}
	}

	void content()
	{
		Tools.SkipLines(1);
		Console.Write("===============Welcome to MSConsole Forms (ver. DevelopmentBuild - 0.1)=========");
		Tools.SkipLines(2);
		int left = 25;
		

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
			//Its looks weird. Anyway, it works
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
			if(key.Key == ConsoleKey.Enter)
			{
				
			}
		}
	}
}