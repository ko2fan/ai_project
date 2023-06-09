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
        }
        else
        {
            Vector2 bankPosition = entity.GetParent<Node2D>().GetNode<Node2D>("bank")?.Position ?? Vector2.Zero;
            entity.Target = bankPosition;
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
        else
        {
            entity.SetTimer(0.5f);
            AnimationPlayer anim = (AnimationPlayer)entity.GetNode("AnimationPlayer");
            anim.Play("mine");
        }
    }

    public override void Exit(BaseEntity entity)
    {
        GD.Print("Exiting MineState");
        AnimationPlayer anim = (AnimationPlayer)entity.GetNode("AnimationPlayer");
        anim.Stop();
    }
}