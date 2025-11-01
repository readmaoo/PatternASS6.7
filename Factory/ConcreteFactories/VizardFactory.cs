using Godot;
public static class VizardFactory
{
    public static Node CreateVizard(int playerIndex)
    {
        var vizardScene = GD.Load<PackedScene>("res://vizard.tscn");
        var node = vizardScene.Instantiate<Unit>();
        node.Init(100, 100);
        node.SetController(playerIndex == 1 ? new Player1Input() : new Player2Input());
        node.SetMovement(new GroundMove());   
        return node;
    }

}