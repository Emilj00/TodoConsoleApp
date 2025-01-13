using TodoConsoleApp.Data;
using TodoConsoleApp.Utility;
using TodoConsoleApp.Views.Creation;

namespace TodoConsoleApp.Views;

public class MainMenuView : ViewBase
{
    private const string CREATE_TASK_TEXT = "Creating task list";
    private const int ADDITIONAL_SPACE_COUNT = 20;
    
    private const int VISIBLE_LIST_COUNT = 30;

    private int selectedListIndex = 0;
    private int scrollOffset = 0;
    
    protected override void Display()
    {
        Console.Clear();

        RenderingUtility.RenderHeader(CREATE_TASK_TEXT, ADDITIONAL_SPACE_COUNT);

        RenderLists();
        
        ColoredText.RenderNewLine();
        ColoredText.RenderNewLine();

        RenderingUtility.RenderFooter(["ARROWS - Moving around", "A - Add new list", "D - Delete list", "SPACE - Open task list", "C - Close list"]);
    }

    private void RenderLists()
    {
        ColoredText.RenderNewLine();
        ColoredText.RenderNewLine();

        var listOfTaskList = ListOfTaskLists.listOfTaskLists;

        if (listOfTaskList.Count == 0)
        {
            ColoredText.Create().Next("No task lists available. Create one using 'A' key.").Render();
            return;
        }

        int endIndex = Math.Min(scrollOffset + VISIBLE_LIST_COUNT, listOfTaskList.Count);

        for (var i = scrollOffset; i < endIndex; i++)
        {
            var task = listOfTaskList[i];

            ConsoleColor backgroundColor = i == selectedListIndex ? ConsoleColor.DarkGray : (ConsoleColor)(-1);
            string taskFinishIndicator = $" [{(task.IsTaskListCompleted ? "X" : " ")}]";

            ColoredText.Create().Next(taskFinishIndicator).BackgroundColor(backgroundColor).Next($" {task.Name} ").BackgroundColor(backgroundColor).NewLine().Render();
        }
    }

    protected override void GetUserInput()
    {
        ConsoleKey pressedKey = Console.ReadKey().Key;

        var listOfTaskList = ListOfTaskLists.listOfTaskLists;
        
        if (pressedKey == ConsoleKey.UpArrow)
        {
            selectedListIndex--;
            
            if (selectedListIndex < scrollOffset)
            {
                scrollOffset--;
            }
        }
        else if (pressedKey == ConsoleKey.DownArrow)
        {
            selectedListIndex++;
            if (selectedListIndex >= scrollOffset + VISIBLE_LIST_COUNT)
            {
                scrollOffset++;
            }
        }
        else if (pressedKey == ConsoleKey.A)
        {
            ListCreationView taskCreationView = new ListCreationView(listOfTaskList);
            taskCreationView.Start();
        }
        else if (pressedKey == ConsoleKey.D)
        {
            if (listOfTaskList.Count == 0)
            {
                return;
            }

            listOfTaskList[selectedListIndex].Delete();
            listOfTaskList.RemoveAt(selectedListIndex);
        }
        else if (pressedKey == ConsoleKey.Spacebar)
        {
            TaskListView taskListView = new TaskListView(listOfTaskList[selectedListIndex]);
            taskListView.Start();
        }
        else if (pressedKey == ConsoleKey.C)
        {
            Stop();
        }

        if (selectedListIndex > listOfTaskList.Count - 1)
        {
            selectedListIndex = Math.Max(0, listOfTaskList.Count - 1);
        }
        else if (selectedListIndex < 0)
        {
            selectedListIndex = 0;
        }

        if (scrollOffset < 0)
        {
            scrollOffset = 0;
        }
        else if (scrollOffset > listOfTaskList.Count - VISIBLE_LIST_COUNT)
        {
            scrollOffset = Math.Max(0, listOfTaskList.Count - VISIBLE_LIST_COUNT);
        }
    }
}