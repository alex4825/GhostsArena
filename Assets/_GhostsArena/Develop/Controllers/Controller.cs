public abstract class Controller
{
    public abstract bool HasInput { get; }

    public bool IsEnabled { get; set; }

    public void Update()
    {
        if (IsEnabled == false)
            return;

        UpdateLogic();
    }

    protected abstract void UpdateLogic();
}
