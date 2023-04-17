using AIProj1.Entities;
using Godot;

namespace AIProj1.States;

public class IdleState : State
{
    public static IdleState Instance => _instance ??= new IdleState();
    private static IdleState _instance;

    public override void Execute(BaseEntity entity)
    {
        GD.Print("Executing IdleState");
        Vector2 minePosition = entity.GetParent<Node2D>().GetNode<Node2D>("mine")?.Position ?? Vector2.Zero;
        
        entity.Target = minePosition;
        entity.ChangeState(MineState.Instance);
    }

    public override void Enter(BaseEntity entity)
    {
        GD.Print("Entering IdleState");
        AnimationPlayer anim = (AnimationPlayer)entity.GetNode("AnimationPlayer");
        anim.Play("idle");
    }

    public override void Exit(BaseEntity entity)
    {
        GD.Print("Exiting IdleState");
        AnimationPlayer anim = (AnimationPlayer)entity.GetNode("AnimationPlayer");
        anim.Stop();
    }
}