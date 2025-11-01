using Godot;

public static class KnightFactory
{
    public static Node CreateKnight(int playerIndex)
    {
        var knightScene = GD.Load<PackedScene>("res://knight.tscn");
        var node = knightScene.Instantiate<Unit>();
        node.Init(200, 50);
        node.SetController(playerIndex == 1 ? new Player1Input() : new Player2Input());
        node.SetMovement(new GroundMove());   
        return node;
    }

    
}