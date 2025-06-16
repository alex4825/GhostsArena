using UnityEngine;

public class ShooterController : Controller
{
    private IShootable _shootable;

    public ShooterController(IShootable shootable)
    {
        _shootable = shootable;
    }

    public override bool HasInput => Input.GetButtonDown("Fire1");

    protected override void UpdateLogic()
    {
        if (HasInput)
        {
            _shootable.Shoot();
        }
    }
}
