using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameBoard
{
    private MapUnitsCollection mapUnitsCollection;

    public MapUnit GetMapUnitByRole(Role role) => mapUnitsCollection.GetMapUnitByRole(role);

    public List<MapUnit> GetAllOtherUnits(TeamType ignoreTeam) => mapUnitsCollection.GetAllOtherUnits(ignoreTeam);

    public List<MapUnit> GetTeam(TeamType team) => mapUnitsCollection.GetTeam(team);

    public void AllNextTurn() => mapUnitsCollection.AllNextTurn();

    public void NextTurn(TeamType teamType) => mapUnitsCollection.NextTurn(teamType);

    public void CreateMapUnits(MapUnitData data) {
        Vector2Int cellPos = new Vector2Int(data.x, data.y);
        int index = walkMap.size.x * cellPos.y + cellPos.x;
        LogicTile tile = allLogicTiles[index];
        MapUnit prefab = GetMapUnitPrefab(data.team, data.classId);
        MapUnit unit = Instantiate(prefab);
        unit.Init(data.role, tile, data.state);
        unit.transform.position = GetWorldPos(tile) + new Vector3(0.5f, 0, 0);
        mapUnitsCollection.AddUnit(data.team, unit);
    }

    private MapUnit GetMapUnitPrefab(TeamType team, int classId) {
        MapUnit[] units = team == TeamType.My ? myMapUnitPrefabs : enemyMapUnitPrefabs;
        return units.Where(t => t.classId == classId).FirstOrDefault();
    }

    public void RemoveMapUnit(MapUnit unit) => mapUnitsCollection.RemoveUnit(unit);

    public bool IsAllEnemyDie() => mapUnitsCollection.IsAllUnitsDie(TeamType.ENEMY);

    public bool IsMainRoleDie() => mapUnitsCollection.IsMainRoleDie(TeamType.My);
}
