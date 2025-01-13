using TodoConsoleApp.Utility;
using Task = TodoConsoleApp.Data.Task;

namespace TodoConsoleApp.Views.Creation;

public class TaskCreationView(List<Task> taskList) : ViewBase
{
    private const string CREATE_TASK_TEXT = "Creating task";
    private const int ADDITIONAL_SPACE_COUNT = 20;

    private List<Task> taskList = taskList;
    
    protected override void Display()
    {
        Console.Clear();

        
        RenderingUtility.RenderHeader(CREATE_TASK_TEXT, ADDITIONAL_SPACE_COUNT);
        
        ShowNamingRules();

        ColoredText.RenderNewLine();
        ColoredText.Create("Enter task name: ").ForegroundColor(ConsoleColor.Yellow).Render();
    }

    private void ShowNamingRules()
    {
        ColoredText.RenderNewLine();
        ColoredText.Create("The task name have at least 3 characters").Render();
        ColoredText.RenderNewLine();
    }

    protected override void GetUserInput()
    {
        string taskName = Console.ReadLine()!;
        if (taskName.Length < 3)
        {
            return;
        }
        
        taskList.Add(new Task
        {
            Name = taskName,
        });

        Stop();
    }
}