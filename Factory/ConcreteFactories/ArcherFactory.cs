using Godot;

public class ArcherFactory
{
    public static Node CreateArcher(int playerIndex)
    {
        var archerScene = GD.Load<PackedScene>("res://archer.tscn");
        var node = archerScene.Instantiate<Unit>();
        node.Init(150, 75);
        node.SetController(playerIndex == 1 ? new Player1Input() : new Player2Input());
        node.SetMovement(new GroundMove());         
        return node;
    }

}