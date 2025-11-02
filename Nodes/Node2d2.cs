using Godot;


public partial class Node2d2 : Node2D
{

    Button wizard, archer, knight;
	public override void _Ready(){
        knight = GetNode<Button>("Button");
		wizard = GetNode<Button>("Button2");
		archer = GetNode<Button>("Button3");
		knight.Pressed += () => Join("Knight");
		wizard.Pressed += () => Join("Vizard");
        archer.Pressed += () => Join("Archer");
        
	}
    void Join(string name){
        var node = GD.Load<PackedScene>("res://level.tscn").Instantiate();
            GetParent().AddChild(node);
            QueueFree();
            var p1 = node.GetNode<Marker2D>("Spawn1");
            var p2 = node.GetNode<Marker2D>("Spawn2");
            var u1 = (Node2D)UnitFactory.CreateUnit(Node2d.unittype,2);
            u1.GlobalPosition = p1.GlobalPosition;
            node.AddChild(u1);
            var u2 = (Node2D)UnitFactory.CreateUnit(name,1);
            u2.GlobalPosition = p2.GlobalPosition;
            node.AddChild(u2);

    }
}
