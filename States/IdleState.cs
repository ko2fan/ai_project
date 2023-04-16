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
        entity.Target = new Vector2(400, 360);
        entity.ChangeState(MineState.Instance);
    }

    public override void Enter(BaseEntity entity)
    {
        GD.Print("Entering IdleState");
    }

    public override void Exit(BaseEntity entity)
    {
        GD.Print("Exiting IdleState");
    }
}