using Godot;

public class Player1Input : IController
{
    public Intent GetIntent()
    {
        Intent intent = new Intent();
        intent.MoveX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        intent.Jump = Input.IsActionJustPressed("ui_up");
        intent.Attack = Input.IsActionJustPressed("attack1");
        return intent;
    }
}