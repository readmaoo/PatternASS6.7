using Godot;
public partial class Projectile : Area2D
{
    public float Damage;
    public float OwnerIndex;
    public float Lifetime = 2.0f;
    public Vector2 Velocity;
    public Projectile InitOwner(Unit owner) { OwnerIndex = owner.PlayerIndex; return this; }

    private void OnBodyEntered(Node body)
    {
        if (body is Unit unit)
        {
            GD.Print($"[Projectile] target Unit, owner={OwnerIndex} vs target={unit.PlayerIndex}, dmg={Damage}");
            if (unit.PlayerIndex == OwnerIndex) return;
            unit.TakeDamage((int)Damage);
            GD.Print($"[Projectile] hit {body.Name}");
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
        if (Lifetime <= 0)
        {
            QueueFree();
        }
    }

}