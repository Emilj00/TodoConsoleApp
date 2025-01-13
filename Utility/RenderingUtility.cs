namespace TodoConsoleApp.Utility;

public static class RenderingUtility
{
    public static void RenderHeader(string text, int additionalSpaceCount)
    {
        ColoredText line = ColoredText.Create(new string('-', text.Length + additionalSpaceCount)).BackgroundColor(ConsoleColor.DarkGray).ForegroundColor(ConsoleColor.DarkGray).NewLine();
        string spaceOnOneSide = new string(' ', additionalSpaceCount / 2);
        
        line.Render();
        ColoredText.Create($"{spaceOnOneSide}{text}{spaceOnOneSide}").BackgroundColor(ConsoleColor.DarkGray).ForegroundColor(ConsoleColor.Yellow).NewLine().Render();
        line.Render();
    }

    public static void RenderFooter(string[] footerLines)
    {
        ColoredText coloredText = new ColoredText();

        foreach (var text in footerLines)
        {
            coloredText = coloredText.Next($" {text} ").Colors(ConsoleColor.Yellow, ConsoleColor.DarkGray).Next(" ");
        }

        coloredText.Render();
    }
}