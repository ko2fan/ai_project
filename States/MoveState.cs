using AIProj1.Entities;
using Godot;

namespace AIProj1.States;

public class MoveState : State
{
    public static MoveState Instance => _instance ??= new MoveState();
    private static MoveState _instance;

    public State PreviousState;
    
    public override void Execute(BaseEntity entity)
    {
        //GD.Print("Executing MoveState");
        entity.MoveTowards(entity.Target);
        entity.GetNode<Sprite2D>("Sprite2D").FlipH = entity.FacingLeft();
        //GD.Print("Moving " + entity.Position + " towards " + entity.Target);
        if (entity.AtTarget())
        {
            entity.ChangeState(PreviousState);
        }
    }

    public override void Enter(BaseEntity entity)
    {
        GD.Print("Entering MoveState");
        AnimationPlayer anim = (AnimationPlayer)entity.GetNode("AnimationPlayer");
        anim.Play("walk");
    }

    public override void Exit(BaseEntity entity)
    {
        GD.Print("Exiting MoveState");
        AnimationPlayer anim = (AnimationPlayer)entity.GetNode("AnimationPlayer");
        anim.Stop();
    }
}