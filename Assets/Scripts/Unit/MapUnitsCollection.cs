using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapUnitsCollection
{
    private Dictionary<TeamType, List<MapUnit>> collection;

    public MapUnitsCollection() {
        collection = new Dictionary<TeamType, List<MapUnit>> {
            {TeamType.MY_ARMY, new List<MapUnit>() },
            {TeamType.ALLY, new List<MapUnit>() },
            {TeamType.ENEMY, new List<MapUnit>() },
            {TeamType.NEUTRAL, new List<MapUnit>() }
        };
    }

    public void AddUnit(TeamType team, MapUnit unit) => collection[team].Add(unit);

    public bool IsAllUnitsDie(TeamType team) {
        List<MapUnit> units = collection[team];
        return !units.Any(t => !t.IsDead);
    }

    public bool IsMainRoleDie(TeamType team) {
        List<MapUnit> units = collection[team];
        return units[0].IsDead;
    }

    public void NextTurn(TeamType team) {
        List<MapUnit> units = collection[team];
        foreach (MapUnit item in units) {
            item.NextTurn();
        }
    }

    public void AllNextTurn() {
        foreach (var item in collection) {
            foreach (MapUnit mapUnit in item.Value) {
                mapUnit.NextTurn();
            }
        }
    }

}
