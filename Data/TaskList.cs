using System.Text.Json;

namespace TodoConsoleApp.Data;

public class TaskList
{
    public string Name { get; set; }
    public List<Task> Tasks { get; set; } = [];

    public bool IsTaskListCompleted => Tasks.Count > 0 && Tasks.All(x => x.IsCompleted);

    public void Save()
    {
        string taskListPath = Path.Combine(ListOfTaskLists.LISTS_FOLDER, $"{Name}.json");
        File.WriteAllText(taskListPath, JsonSerializer.Serialize(this));
    }

    public void Delete()
    {
        string taskListPath = Path.Combine(ListOfTaskLists.LISTS_FOLDER, $"{Name}.json");
        File.Delete(taskListPath);
    }
}