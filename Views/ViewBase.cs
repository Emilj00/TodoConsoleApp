namespace TodoConsoleApp.Views;

public abstract class ViewBase
{
    private bool IsRunning { get; set; } = false;
 
    public void Start()
    {
        IsRunning = true;
    
        while (IsRunning)
        {
            Display();
            GetUserInput();
        }
    }

    public void Stop()
    {
        IsRunning = false;
    }

    protected abstract void Display();
    protected virtual void GetUserInput()
    {

    }
}