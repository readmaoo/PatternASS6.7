using Godot;
using System;

public partial class Node2d : Node2D
{

	Button wizard, archer, knight;
	public static string unittype;

	public override void _Ready(){
		knight = GetNode<Button>("Button");
		wizard = GetNode<Button>("Button2");
		archer = GetNode<Button>("Button3");
		knight.Pressed += () => OnButtonPress("Knight");
		wizard.Pressed += () => OnButtonPress("Vizard");
		archer.Pressed += () => OnButtonPress("Archer");
	}

	public void OnButtonPress(string type){
		unittype = type;
		var node = GD.Load<PackedScene>("res://node_2d_2.tscn").Instantiate();
		GetParent().AddChild(node);
		QueueFree();
	}
}
