using AIProj1.Entities;

namespace AIProj1.States;

public abstract class State
{
    public abstract void Execute(BaseEntity entity);

    public abstract void Enter(BaseEntity entity);

    public abstract void Exit(BaseEntity entity);
}