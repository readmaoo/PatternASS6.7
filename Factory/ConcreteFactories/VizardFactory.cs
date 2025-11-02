using Godot;
public static class VizardFactory
{
    public static Node CreateVizard(int playerIndex)
    {
        var vizardScene = GD.Load<PackedScene>("res://vizard.tscn");
        var node = vizardScene.Instantiate<Unit>();
        node.Init(100, 35);
        node.SetController(playerIndex == 1 ? new Player1Input() : new Player2Input());
        node.PlayerIndex = playerIndex;
        node.SetMovement(new GroundMove());
        IAttack attack = new MageAttack();
        attack = new LowHpDamageX2Decorator(attack,50); 
        node.SetAttack(attack); 
        return node;
    }

}