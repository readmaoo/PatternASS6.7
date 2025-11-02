using Godot;

public static class KnightFactory
{
    public static Node CreateKnight(int playerIndex)
    {
        var knightScene = GD.Load<PackedScene>("res://knight.tscn");
        var node = knightScene.Instantiate<Unit>();
        node.Init(200, 50);
        node.SetController(playerIndex == 1 ? new Player1Input() : new Player2Input());
        node.PlayerIndex = playerIndex;
        node.SetMovement(new GroundMove());   
        IAttack attack = new MeleeAttack();
        attack = new LowHpDamageX2Decorator(attack,50); 
        node.SetAttack(attack);
        return node;
    }

    
}