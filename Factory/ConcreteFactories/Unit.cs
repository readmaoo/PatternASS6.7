using Godot;

public partial class Unit : CharacterBody2D
{
    [Signal] public delegate void HealthChangedEventHandler(int hp, int maxHp);
    [Signal] public delegate void DiedEventHandler();

    public int MaxHp { get; private set; }
    public int PlayerIndex { get; set; }
    public int Hp { get; private set; }
    public int Damage { get; private set; }
    public float DamageMultiplier { get; set; } = 1f;

    private bool _inited;
    private bool _dead;                
    private IController _controller;
    private IMovement _movement;
    private IAttack _attack;

    public Unit Init(int maxHp, int damage)
    {
        if (_inited) return this;
        MaxHp = maxHp;
        Hp = maxHp;
        Damage = damage;
        _inited = true;
        return this;
    }

    public void SetController(IController c) => _controller = c;
    public void SetMovement(IMovement m) => _movement = m;
    public void SetAttack(IAttack a) => _attack = a;

    public override void _PhysicsProcess(double delta)
    {
        if (_dead) return;              
        if (_controller == null || _movement == null) return;

        var i = _controller.GetIntent();
        _movement.Apply(i, this, (float)delta);
        _attack?.Tick(i, this, (float)delta);
    }

    public void TakeDamage(int amount)
    {
        if (_dead) return;
        GD.Print($"[Unit {Name}] HP before={Hp}, dmg={amount}");
        Hp = Mathf.Clamp(Hp - amount, 0, MaxHp);
        EmitSignal(SignalName.HealthChanged, Hp, MaxHp);

        if (Hp == 0)
        {
            _dead = true;
            var anim = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
            if (anim != null)
            {
                anim.AnimationFinished += () =>
                {
                    GD.Print($"[Unit {Name}] DIED");
                    EmitSignal(SignalName.Died);
                    QueueFree();
                };
                anim.Play("death");
            }
        }
    }
}
