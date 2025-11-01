using Godot;
public interface IMovement
{
    void Apply(Intent i, CharacterBody2D body, float dt);
}