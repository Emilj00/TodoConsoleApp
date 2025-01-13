using TodoConsoleApp.Data;
using TodoConsoleApp.Utility;
using TodoConsoleApp.Views.Creation;
using Task = TodoConsoleApp.Data.Task;

namespace TodoConsoleApp.Views;

public class TaskListView(TaskList taskList) : ViewBase
{
    private TaskList taskList = taskList;

    private const int ADDITIONAL_SPACE_COUNT = 20;
    private const int VISIBLE_TASK_COUNT = 30;
    
    private int selectedTaskIndex = 0;
    private int scrollOffset = 0;
    
    protected override void Display()
    {
        Console.Clear();
        
        RenderingUtility.RenderHeader(taskList.Name, ADDITIONAL_SPACE_COUNT);

        RenderTasks();
        
        ColoredText.RenderNewLine();
        ColoredText.RenderNewLine();

        RenderingUtility.RenderFooter(["ARROWS - Moving around", "A - Add new task", "D - Delete task", "SPACE - Mark as finished (toggle)", "S - Save list", "C - Close list"]);
    }
    
    private void RenderTasks()
    {
        ColoredText.RenderNewLine();
        ColoredText.RenderNewLine();

        if (taskList.Tasks.Count == 0)
        {
            ColoredText.Create().Next("No task in the list. Create one using 'A' key.").Render();
            return;
        }
        
        int endIndex = Math.Min(scrollOffset + VISIBLE_TASK_COUNT, taskList.Tasks.Count);

        for (var i = scrollOffset; i < endIndex; i++)
        {
            var task = taskList.Tasks[i];

            ConsoleColor backgroundColor = i == selectedTaskIndex ? ConsoleColor.DarkGray : (ConsoleColor)(-1);
            string taskFinishIndicator = $" [{(task.IsCompleted ? "X" : " ")}]";

            ColoredText.Create().Next(taskFinishIndicator).BackgroundColor(backgroundColor).Next($" {task.Name} ").BackgroundColor(backgroundColor).NewLine().Render();
        }
    }

    protected override void GetUserInput()
    {
        var pressedKey = Console.ReadKey().Key;

        if (pressedKey == ConsoleKey.UpArrow)
        {
            selectedTaskIndex--;
            
            if (selectedTaskIndex < scrollOffset)
            {
                scrollOffset--;
            }
        }
        else if (pressedKey == ConsoleKey.DownArrow)
        {
            selectedTaskIndex++;
            if (selectedTaskIndex >= scrollOffset + VISIBLE_TASK_COUNT)
            {
                scrollOffset++;
            }
        }
        else if (pressedKey == ConsoleKey.A)
        {
            TaskCreationView taskCreationView = new TaskCreationView(taskList.Tasks);
            taskCreationView.Start();
        }
        else if (pressedKey == ConsoleKey.D)
        {
            if (taskList.Tasks.Count == 0)
            {
                return;
            }
            
            taskList.Tasks.RemoveAt(selectedTaskIndex);
        }
        else if (pressedKey == ConsoleKey.Spacebar)
        {
            if (taskList.Tasks.Count == 0)
            {
                return;
            }

            taskList.Tasks[selectedTaskIndex].IsCompleted = !taskList.Tasks[selectedTaskIndex].IsCompleted;
        }
        else if (pressedKey == ConsoleKey.S)
        {
            taskList.Save();
        }
        else if (pressedKey == ConsoleKey.C)
        {
            Stop();
        }

        if (selectedTaskIndex > taskList.Tasks.Count - 1)
        {
            selectedTaskIndex = Math.Max(0, taskList.Tasks.Count - 1);
        }
        else if (selectedTaskIndex < 0)
        {
            selectedTaskIndex = 0;
        }

        if (scrollOffset < 0)
        {
            scrollOffset = 0;
        }
        else if (scrollOffset > taskList.Tasks.Count - VISIBLE_TASK_COUNT)
        {
            scrollOffset = Math.Max(0, taskList.Tasks.Count - VISIBLE_TASK_COUNT);
        }
    }
}