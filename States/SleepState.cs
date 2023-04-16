using AIProj1.Entities;
using Godot;

namespace AIProj1.States;

public class SleepState : State
{
    public static SleepState Instance => _instance ??= new SleepState();
    private static SleepState _instance;
    
    public override void Execute(BaseEntity entity)
    {
        GD.Print("Executing sleep");
    }

    public override void Enter(BaseEntity entity)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(BaseEntity entity)
    {
        throw new System.NotImplementedException();
    }
}