using UnityEngine;

public class CompositeController : Controller
{
    private Controller _mainController;
    private Controller _idleController;

    private float _maxIdleTime;
    private float _idleTimer;

    public CompositeController(Controller mainController, Controller idleController, float maxIdleTime)
    {
        _mainController = mainController;
        _idleController = idleController;
        _maxIdleTime = maxIdleTime;

        _mainController.IsEnabled = true;
        _idleController.IsEnabled = false;
    }

    public override bool HasInput => _mainController.HasInput || _idleController.HasInput;

    protected override void UpdateLogic()
    {
        if (_mainController.HasInput)
        {
            _idleTimer = 0;
            _idleController.IsEnabled = false;
        }
        else
        {
            _idleTimer += Time.deltaTime;
        }

        if (_idleTimer >= _maxIdleTime)
        {
            _idleTimer = 0;
            _idleController.IsEnabled = true;
        }

        _mainController.Update();
        _idleController.Update();
    }
}
