using System.Text.Json;

namespace TodoConsoleApp.Data;

public static class ListOfTaskLists
{
    public static List<TaskList> listOfTaskLists { get; } = [];
    
    public static readonly string LISTS_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "tasklist");

    static ListOfTaskLists()
    {
        Directory.CreateDirectory(LISTS_FOLDER);
        LoadAllLists();
    }

    private static void LoadAllLists()
    {
        string[] files = Directory.GetFiles(LISTS_FOLDER);

        foreach (string file in files)
        {
            TaskList? tasklist = Load(file);
            if (tasklist != null)
            {
                listOfTaskLists.Add(tasklist);
            }
        }
    }
    
    public static TaskList? Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }
        
        return JsonSerializer.Deserialize<TaskList>(File.ReadAllText(filePath));
    }
}