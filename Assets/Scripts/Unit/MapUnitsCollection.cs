using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapUnitsCollection
{
    private Dictionary<TeamType, List<MapUnit>> collection;

    public MapUnitsCollection() {
        collection = new Dictionary<TeamType, List<MapUnit>> {
            {TeamType.My, new List<MapUnit>() },
            {TeamType.ALLIANCE, new List<MapUnit>() },
            {TeamType.ENEMY, new List<MapUnit>() },
            {TeamType.NEUTRAL, new List<MapUnit>() }
        };
    }

    public void AddUnit(TeamType team, MapUnit unit) => collection[team].Add(unit);

    public List<MapUnit> GetTeam(TeamType team) => collection[team];

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
            item.SetIdle();
        }
    }

    public void AllNextTurn() {
        foreach (var item in collection) {
            foreach (MapUnit mapUnit in item.Value) {
                mapUnit.SetIdle();
            }
        }
    }

    public List<MapUnit> GetAllOtherUnits(TeamType ignoreTeam) {
        List<MapUnit> result = new List<MapUnit>();
        foreach (var item in collection) {
            if (item.Key != ignoreTeam) {
                result.AddRange(item.Value);
            }
        }
        return result;
    }
}
