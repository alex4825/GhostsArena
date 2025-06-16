public class CompositeController : Controller
{
    private Controller[] _controllers;

    public CompositeController(params Controller[] controllers)
    {
        _controllers = controllers;

        foreach (var controller in _controllers)
            controller.IsEnabled = true;
    }

    public override bool HasInput
    {
        get
        {
            foreach (var controller in _controllers)
                if (controller.IsEnabled)
                    return true;

            return false;
        }
    }

    protected override void UpdateLogic()
    {
        foreach (var controller in _controllers)
            controller.Update();
    }
}
