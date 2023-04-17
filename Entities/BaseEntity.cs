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
    [Export] private float fatigueLevel;

    [ExportGroup("Entity Rates")]
    [Export] private float moveSpeed;
    [Export] private float thurstSpeed;
    [Export] private float fatigueSpeed;
    
    private State _currentState;
    
    private int _totalGold;
    private int _totalMoney;

    private float _currentThurst;
    private float _currentFatigue;
    private float _currentCarryWeight;

    private double _timer;
    private double _timeElapsed;
    private double _delta;

    private Vector2 _direction;
	
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
        _totalGold = 0;
        _totalMoney = 0;
        _currentThurst = 0;
        _currentFatigue = 0;
        _currentCarryWeight = 0;
        _currentState = IdleState.Instance;
        _delta = 0;
        _timer = 0;
        _timeElapsed = 0;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        _delta = delta;
        _currentThurst += (float)(thurstSpeed * _delta);
        if (IsThirsty())
        {
            if (_currentState != MoveState.Instance || MoveState.Instance.PreviousState != DrinkState.Instance)
            {
                GD.Print("Thirsty");
                Vector2 saloonPosition = GetParent<Node2D>().GetNode<Node2D>("saloon")?.Position ?? Vector2.Zero;
                Target = saloonPosition;
                ChangeState(DrinkState.Instance);
            }
        }

        if (IsTired())
        {
            if (_currentState != MoveState.Instance || MoveState.Instance.PreviousState != SleepState.Instance)
            {
                GD.Print("Tired");
                Vector2 hotelPosition = GetParent<Node2D>().GetNode<Node2D>("hotel")?.Position ?? Vector2.Zero;
                Target = hotelPosition;
                ChangeState(SleepState.Instance);
            }
        }
        
        _currentState.Execute(this);
    }

    public bool AtTarget()
    {
        return !(Position.DistanceSquaredTo(Target) > 16.0f);
    }

    public bool CanCarry() => _currentCarryWeight < maxCarryWeight;

    public bool IsThirsty() => _currentThurst >= thurstLevel;
    public bool IsSatiated() => _currentThurst <= 10;

    public bool IsTired() => _currentFatigue >= fatigueLevel;
    public bool CanWake() => _currentFatigue <= 10;

    public bool HasGold() => _totalGold > 0;

    public void Drink()
    {
        _currentThurst -= (float)(10 * _delta);
        if (_currentThurst <= 0.0f)
            _currentThurst = 0f;
    }

    public void DepositResource()
    {
        _timeElapsed += _delta;
        if (_timeElapsed >= _timer)
        {
            _timeElapsed = 0;
            _currentCarryWeight -= 10;
            _totalGold -= 1;
        }
    }

    public void Sleep()
    {
        _currentFatigue -= (float)(10 * _delta);
        if (_currentFatigue <= 0.0f)
            _currentFatigue = 0f;
    }

    // public void Tire(float tiredness)
    // {
    //     _currentFatigue += tiredness;
    // }

    public void MoveTowards(Vector2 position)
    {
        Vector2 oldPosition = Position;
        Position = Position.MoveToward(position, moveSpeed);
        _direction = (Position - oldPosition).Normalized();
    }

    public bool FacingLeft() => _direction.X < 0;

    public void MineResource()
    {
        _timeElapsed += _delta;
        if (_timeElapsed >= _timer)
        {
            _timeElapsed = 0;
            _totalGold += 1;
            _currentCarryWeight += 10;
            _currentFatigue += fatigueSpeed;
        }
    }

    public void SetTimer(double time)
    {
        _timer = time;
    }
}