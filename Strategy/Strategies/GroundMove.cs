using Godot;

public class GroundMove : IMovement
{
    private const float Speed = 200f;
    private const float JumpVelocity = -400f;
    private const float Gravity = 980f;
    public void Apply(Intent i, CharacterBody2D body, float dt)
    {
        var anim = body.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
        var v = body.Velocity;
        v.X = i.MoveX * Speed;
        if (!body.IsOnFloor())
            v.Y += Gravity * dt;
        else if (v.Y > 0)
            v.Y = 0f;
        if (i.Jump && body.IsOnFloor())
            v.Y = JumpVelocity;
        body.Velocity = v;
        body.MoveAndSlide();
        if ((anim.Animation == "attack" || anim.Animation == "hit") && anim.IsPlaying())
        {
            const float dirEpsA = 5f;
            if (v.X > dirEpsA) anim.FlipH = false;
            else if (v.X < -dirEpsA) anim.FlipH = true;
            return;
        }
        if (i.Attack)
        {
            anim.Play("attack");          
            return;                       
        }
        string target = Mathf.Abs(v.X) > 0.1f
            ? "run"
            : (body.IsOnFloor() ? "idle" : "jump");

        if (anim.Animation != target)
            anim.Play(target);

        const float dirEps = 5f;
        if (v.X > dirEps) anim.FlipH = false;
        else if (v.X < -dirEps) anim.FlipH = true;
    }
}