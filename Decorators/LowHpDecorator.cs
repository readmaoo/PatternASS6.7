using Godot;
public class LowHpDamageX2Decorator : IAttack
{
    private readonly IAttack _baseAttack;
    private readonly int _hpThreshold;

    public LowHpDamageX2Decorator(IAttack baseAttack, int hpThreshold = 50)
    {
        _baseAttack = baseAttack;
        _hpThreshold = hpThreshold;
    }
    public void Tick(Intent i, Unit unit, float dt)
    {
        float prev = unit.DamageMultiplier;
        if (unit.Hp <= _hpThreshold)
        {
           unit.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D")?.Play("hit");
            unit.DamageMultiplier = 2f;
        }
        else
        {
            unit.DamageMultiplier = 1f;
        }
        _baseAttack.Tick(i, unit, dt);
        unit.DamageMultiplier = prev;
    }
}
