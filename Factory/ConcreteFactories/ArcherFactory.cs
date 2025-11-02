using Godot;
public class ArcherFactory
{
    public static Node CreateArcher(int playerIndex)
    {
        var archerScene = GD.Load<PackedScene>("res://archer.tscn");
        var node = archerScene.Instantiate<Unit>();
        node.Init(150, 20);
        node.SetController(playerIndex == 1 ? new Player1Input() : new Player2Input());
        node.PlayerIndex = playerIndex;
        node.SetMovement(new GroundMove());
        IAttack attack = new ArcherAttack();
        attack = new LowHpDamageX2Decorator(attack,50); 
        node.SetAttack(attack);
        return node;
    }
}
