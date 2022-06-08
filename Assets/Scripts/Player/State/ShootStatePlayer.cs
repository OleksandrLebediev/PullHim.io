using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStatePlayer : BaseState
{
    public ShootStatePlayer(IStationStateSwitcher stateSwitcher,
        IEnumerable<IShooter> shooters) : base(stateSwitcher)
    {
        _shooters = shooters;
    }

    private IEnumerable<IShooter> _shooters;

    public override void Enter()
    {
        foreach (var shooter in _shooters)
        {
            if (shooter.IsShooting == true)
                _stateSwitcher.SwitchState<IdelStatePlayer>();
        }


        foreach (var shooter in _shooters)
        {
            shooter.Shoot();
        }

        _stateSwitcher.SwitchState<IdelStatePlayer>();
    }
}
