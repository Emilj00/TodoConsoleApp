using TodoConsoleApp.Data;
using TodoConsoleApp.Utility;

namespace TodoConsoleApp.Views.Creation;

public class ListCreationView(List<TaskList> list) : ViewBase
{
    private const string CREATE_TASK_TEXT = "Creating task list";
    private const int ADDITIONAL_SPACE_COUNT = 20;

    private List<TaskList> taskList = list;
    
    protected override void Display()
    {
        Console.Clear();

        RenderingUtility.RenderHeader(CREATE_TASK_TEXT, ADDITIONAL_SPACE_COUNT);
        
        ShowNamingRules();

        ColoredText.RenderNewLine();
        ColoredText.Create("Enter list name: ").ForegroundColor(ConsoleColor.Yellow).Render();
    }

    private void ShowNamingRules()
    {
        ColoredText.RenderNewLine();
        ColoredText.Create("The list name have at least 3 characters").Render();
        ColoredText.RenderNewLine();
    }

    protected override void GetUserInput()
    {
        string taskListName = Console.ReadLine()!;
        if (taskListName.Length < 3)
        {
            return;
        }

        TaskList newTaskList = new TaskList()
        {
            Name = taskListName,
        };

        newTaskList.Save();
        taskList.Add(newTaskList);

        Stop();
    }
}