using UnityEngine;

public class SwitcherController : Controller
{
    private Controller _mainController;
    private Controller _idleController;

    public SwitcherController(Controller mainController, Controller idleController)
    {
        _mainController = mainController;
        _idleController = idleController;

        _mainController.IsEnabled = false;
        _idleController.IsEnabled = true;
    }

    public override bool HasInput => _mainController.HasInput || _idleController.HasInput;

    protected override void UpdateLogic()
    {
        if (_mainController.HasInput)
        {
            _mainController.IsEnabled = true;
            _idleController.IsEnabled = false;
        }
        else
        {
            _mainController.IsEnabled = false;
            _idleController.IsEnabled = true;
        }

        _mainController.Update();
        _idleController.Update();
    }
}
