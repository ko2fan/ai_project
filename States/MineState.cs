using System.Reflection.Metadata.Ecma335;
using AIProj1.Entities;
using Godot;

namespace AIProj1.States;

public class MineState : State
{
    public static MineState Instance => _instance ??= new MineState();
    private static MineState _instance;
    
    public override void Execute(BaseEntity entity)
    {
        GD.Print("Executing MineState");
        if (entity.CanCarry())
        {
            entity.MineResource();
            GD.Print("Mining gold");
        }
        else
        {
            entity.Target = new Vector2(800, 360);
            entity.ChangeState(DepositState.Instance);
        }
    }

    public override void Enter(BaseEntity entity)
    {
        GD.Print("Entering MineState");
        
        if (!entity.AtTarget())
        {
            MoveState.Instance.PreviousState = this;
            entity.ChangeState(MoveState.Instance);
        }
    }

    public override void Exit(BaseEntity entity)
    {
        GD.Print("Exiting MineState");
    }
}