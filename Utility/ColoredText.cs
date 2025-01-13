namespace TodoConsoleApp.Utility;

public class ColoredText
{
    private ColoredText? _parrent = null;

    private string _text = "";
    
    private ConsoleColor _foregroundColor = (ConsoleColor)(-1);
    private ConsoleColor _backgroundColor = (ConsoleColor)(-1);

    public static ColoredText Create(string text = "")
    {
        ColoredText coloredText = new ColoredText();
        coloredText._text = text;
        
        return coloredText;
    }

    public ColoredText Text(string text)
    {
        _text = text;
        return this;
    }

    public ColoredText BackgroundColor(ConsoleColor color)
    {
        _backgroundColor = color;
        return this;
    }

    public ColoredText ForegroundColor(ConsoleColor color)
    {
        _foregroundColor = color;
        return this;
    }

    public ColoredText Colors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        return BackgroundColor(backgroundColor).ForegroundColor(foregroundColor);
    }

    public ColoredText Next(string text)
    {
        ColoredText coloredText = new ColoredText();
        
        coloredText._parrent = this;
        coloredText._text = text;
        
        return coloredText;
    }

    public ColoredText NewLine()
    {
        return Next("\n");
    }

    public void Render()
    {
        Stack<ColoredText> stack = new Stack<ColoredText>();
        ColoredText? current = this;

        while (current != null)
        {
            stack.Push(current);
            current = current._parrent;
        }

        while (stack.Count > 0)
        {
            ColoredText coloredText = stack.Pop();

            Console.BackgroundColor = coloredText._backgroundColor;
            Console.ForegroundColor = coloredText._foregroundColor;

            Console.Write(coloredText._text);
            Console.ResetColor();
        }

        Console.ResetColor();
    }

    public static void RenderNewLine()
    {
        Create("").NewLine().Render();
    }
}
