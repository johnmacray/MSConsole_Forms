using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

