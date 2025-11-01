using Godot;

public class Player2Input : IController
{
    public Intent GetIntent()
    {
        Intent intent = new Intent();
        intent.MoveX = Input.GetActionStrength("u2_right") - Input.GetActionStrength("u2_left");
        intent.Jump = Input.IsActionJustPressed("u2_jump");
        intent.Attack = Input.IsActionJustPressed("attack2");
        return intent;
    }
}