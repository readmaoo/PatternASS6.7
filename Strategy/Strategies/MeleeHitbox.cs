using Godot;

public partial class MeleeHitbox : Area2D
{
    public int Damage;

    public int OwnerIndex { get; private set; } 
    public float Lifetime = 0.12f;
    public Vector2 Velocity;

    public MeleeHitbox InitOwner(Unit owner) { OwnerIndex = owner.PlayerIndex; return this; }

    private void OnBodyEntered(Node body)
    {
        if (body is Unit unit)
        {
            GD.Print($"[MeleeHitbox] owner={OwnerIndex} target={unit.PlayerIndex} dmg={Damage}");
            if (unit.PlayerIndex == OwnerIndex) return;
            unit.TakeDamage(Damage);
            QueueFree();
        }
    }

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        Lifetime -= (float)delta;
        GlobalPosition += Velocity * (float)delta; 
        if (Lifetime <= 0) QueueFree();
    }
}
