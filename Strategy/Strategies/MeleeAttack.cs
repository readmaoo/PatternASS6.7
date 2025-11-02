using Godot;
public class MeleeAttack : IAttack
{
    private float _attackCooldown = 0.5f;
    private float _timeSinceLastAttack = 0f;
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
    var scene = GD.Load<PackedScene>("res://MeleeHitbox.tscn");
    var hb = scene.Instantiate<MeleeHitbox>().InitOwner(unit);
    hb.Damage = (int)(unit.Damage * unit.DamageMultiplier);
    var sprite = unit.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
    bool facingRight = sprite != null ? !sprite.FlipH : (unit.Velocity.X >= 0f);
    var offset = new Vector2(facingRight ? 20 : -20, 0);
    hb.GlobalPosition = unit.GlobalPosition + offset;           
    hb.Velocity = new Vector2(facingRight ? 120 : -120, 0);       
    unit.GetParent().AddChild(hb);
    GD.Print($"[MeleeAttack] spawned {hb.GlobalPosition}, facingRight={facingRight}, owner={unit.PlayerIndex}");
}

}