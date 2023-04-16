using AIProj1.Entities;
using Godot;

namespace AIProj1.States;

public class DrinkState : State
{
    public static DrinkState Instance => _instance ??= new DrinkState();
    private static DrinkState _instance;
    
    public override void Execute(BaseEntity entity)
    {
        GD.Print("Executing DrinkState");
        if (!entity.IsSatiated())
        {
            entity.Drink();
        }
        else
        {
            entity.ChangeState(IdleState.Instance);
        }
    }

    public override void Enter(BaseEntity entity)
    {
        GD.Print("Entering DrinkState");
        
        if (!entity.AtTarget())
        {
            MoveState.Instance.PreviousState = this;
            entity.ChangeState(MoveState.Instance);
        }
    }

    public override void Exit(BaseEntity entity)
    {
        GD.Print("Exiting DrinkState");
    }
}