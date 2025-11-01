using Godot;

public partial class Unit : CharacterBody2D
{
    public int MaxHp { get; private set; }
    public int Hp    { get; private set; }
    public int Damage { get; private set; }
    private bool _inited;
    private IController _controller;
    private IMovement   _movement;
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
    public void SetMovement(IMovement m)     => _movement   = m;
    public override void _PhysicsProcess(double delta)
    {
        if (_controller == null || _movement == null) return;

        var i = _controller.GetIntent();
        _movement.Apply(i, this, (float)delta);
    }
}