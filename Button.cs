using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

	public Button((int, int) position, string content, (int, int) size, List<Button> butList, ConsoleColor normal = ConsoleColor.White,
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

		object o = this.MemberwiseClone();
		butList.Add((Button) o);

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

