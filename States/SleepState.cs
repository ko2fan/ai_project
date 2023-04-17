using AIProj1.Entities;
using Godot;

namespace AIProj1.States;

public class SleepState : State
{
    public static SleepState Instance => _instance ??= new SleepState();
    private static SleepState _instance;
    
    public override void Execute(BaseEntity entity)
    {
        GD.Print("Executing SleepState");
        if (entity.CanWake())
        {
            entity.ChangeState(IdleState.Instance);
        }
        else
        {
            entity.Sleep();
        }
    }

    public override void Enter(BaseEntity entity)
    {
        GD.Print("Entering SleepState");
        
        if (!entity.AtTarget())
        {
            MoveState.Instance.PreviousState = this;
            entity.ChangeState(MoveState.Instance);
        }
    }

    public override void Exit(BaseEntity entity)
    {
        GD.Print("Exiting SleepState");
        AnimationPlayer anim = (AnimationPlayer)entity.GetNode("AnimationPlayer");
        anim.Stop();
    }
}