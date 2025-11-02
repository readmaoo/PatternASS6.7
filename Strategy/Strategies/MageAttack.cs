using Godot;
public partial class MageAttack : IAttack
{
   private float _attackCooldown = 0.5f;
    private float _timeSinceLastAttack = 0f;
    private readonly float Speed = 520f;
    public void Tick(Intent i, Unit unit, float dt)
    {
        _timeSinceLastAttack += dt;
        if (i.Attack && _timeSinceLastAttack >= _attackCooldown)
        {
            SpawnHitbox(unit);
            _timeSinceLastAttack = 0f;
        }
    }
        private void SpawnHitbox(Unit unit)
    {
        var hitboxScene = GD.Load<PackedScene>("res://Fireball.tscn");
        var hitboxInstance = hitboxScene.Instantiate<Projectile>();
        hitboxInstance.Damage = unit.Damage * unit.DamageMultiplier;
        hitboxInstance.OwnerIndex = unit.PlayerIndex;
        var sprite = unit.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        bool facingRight;
        if (sprite != null)
        {
            facingRight = !sprite.FlipH;
        }
        else
        {
            facingRight = true; 
        }
        Vector2 offset = new Vector2(facingRight ? 12 : -12, -16);
        hitboxInstance.Velocity = new Vector2(facingRight ? Speed : -Speed, 0);
        hitboxInstance.Position = unit.Position + offset;
        unit.GetParent().AddChild(hitboxInstance);
    }
}