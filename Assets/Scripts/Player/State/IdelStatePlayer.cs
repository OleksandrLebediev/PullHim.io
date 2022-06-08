using System.Collections.Generic;

public class IdelStatePlayer : BaseState
{
    public IdelStatePlayer(IStationStateSwitcher stateSwitcher, IEnumerable<Character> characterList,
        Joystick joystick) : base(stateSwitcher)
    {
        _joystick = joystick;
        _characterList = characterList;
    }
    
    private Joystick _joystick;
    private IEnumerable<Character> _characterList;

    public override void Enter()
    {
        foreach (var character in _characterList)
        {
            character.SetAnimation(CharacterAnimationInfo.Idle, true);
        }
    }

    public override void Exit()
    {
        foreach (var character in _characterList)
        {
            character.SetAnimation(CharacterAnimationInfo.Idle, false);
        }
    }

    public override void UpdateLogic()
    {
        if (_joystick.Direction.magnitude > 0)
        {
            _stateSwitcher.SwitchState<MoveStatePlayer>();
        }
    }
}
