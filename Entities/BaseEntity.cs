namespace AIProj1.Entities;
using Godot;
using AIProj1.States;

public partial class BaseEntity : Node2D
{
    public static int nextId = 0;
    public int uid { get; private set; }

    public BaseEntity()
    {
        uid = nextId;
        ++nextId;
    }

    public Vector2 Target { get; set; }
    
    [ExportGroup("Entity Maximums")]

    [Export] private int maxCarryWeight;
    [Export] private float maxThurst;
    [Export] private float maxFatigue;
    [Export] private float thurstLevel;

    [ExportGroup("Entity Speed")]
    [Export] private float moveSpeed;
    [Export] private float thurstSpeed;
    [Export] private float fatigueSpeed;
    
    private State _currentState;
    
    [ExportGroup("Entity amounts")]
    [Export] private int _totalGold;
    [Export] private int _totalMoney;

    [Export] private float _currentThurst;
    [Export] private float _currentFatigue;
    [Export] private float _currentCarryWeight;
	
    public void ChangeState(State state)
    {
        if (state == null)
        {
            GD.PrintErr("Tried to change to invalid state");
            return;
        }
        _currentState.Exit(this);
        _currentState = state;
        _currentState.Enter(this);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _currentThurst = 0;
        _currentFatigue = 0;
        _currentState = IdleState.Instance;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        _currentThurst += (float)(thurstSpeed * delta);
        if (IsThirsty())
        {
            Target = new Vector2(600, 500);
            ChangeState(DrinkState.Instance);
        }
        _currentState.Execute(this);
    }

    public bool AtTarget()
    {
        return !(Position.DistanceSquaredTo(Target) > 144.0f);
    }

    public bool CanCarry() => _currentCarryWeight < maxCarryWeight;

    public bool IsThirsty() => _currentThurst >= thurstLevel;
    public bool IsSatiated() => _currentThurst <= 10;

    public bool HasGold() => _totalGold > 0;

    public void Drink()
    {
        _currentThurst -= 10;
        if (_currentThurst <= 0.0f)
            _currentThurst = 0f;
    }

    public void DepositResource()
    {
        _totalGold -= 1;
    }

    public void MoveTowards(Vector2 position)
    {
        Position = Position.MoveToward(position, moveSpeed);
    }

    public void MineResource()
    {
        _totalGold += 1;
        _currentCarryWeight += 10;
    }
}