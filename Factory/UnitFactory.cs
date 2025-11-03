using Godot;
using System;

public static class UnitFactory
{
    public static Node CreateUnit(string unitType, int playerIndex)
    {
        switch (unitType)
        {
            case "Knight": return KnightFactory.CreateKnight(playerIndex);
            case "Vizard": return VizardFactory.CreateVizard(playerIndex);
            case "Archer": return ArcherFactory.CreateArcher(playerIndex);
            default: throw new ArgumentException("Unknown unit type");
        }
    }

}
