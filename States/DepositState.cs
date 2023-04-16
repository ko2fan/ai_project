using AIProj1.Entities;
using Godot;

namespace AIProj1.States;

public class DepositState : State
{
    public static DepositState Instance => _instance ??= new DepositState();
    private static DepositState _instance;
    
    public override void Execute(BaseEntity entity)
    {
        GD.Print("Executing DepositState");
        if (entity.HasGold())
        {
            entity.DepositResource();
            GD.Print("Depositing gold");
        }
        else
        {
            entity.ChangeState(IdleState.Instance);
        }
    }

    public override void Enter(BaseEntity entity)
    {
        GD.Print("Entering DepositState");
        
        if (!entity.AtTarget())
        {
            MoveState.Instance.PreviousState = this;
            entity.ChangeState(MoveState.Instance);
        }
    }

    public override void Exit(BaseEntity entity)
    {
        GD.Print("Exiting DepositState");
    }
}