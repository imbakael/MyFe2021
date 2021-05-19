using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameBoard
{
    private MapUnitsCollection mapUnitsCollection;

    public List<MapUnit> GetAllOtherUnits(TeamType ignoreTeam) => mapUnitsCollection.GetAllOtherUnits(ignoreTeam);

    public List<MapUnit> GetTeam(TeamType team) => mapUnitsCollection.GetTeam(team);

    public void AllNextTurn() => mapUnitsCollection.AllNextTurn();

    public void NextTurn(TeamType teamType) => mapUnitsCollection.NextTurn(teamType);

    public void CreateMapUnits(Vector2Int[] allCellPos, TeamType team, int[] classIds) {
        for (int i = 0; i < allCellPos.Length; i++) {
            Vector2Int cellPos = allCellPos[i];
            int index = walkMap.size.x * cellPos.y + cellPos.x;
            LogicTile tile = allLogicTiles[index];
            MapUnit prefab = GetMapUnitPrefab(team, classIds[i]);
            MapUnit unit = Instantiate(prefab);
            unit.Init(team, tile);
            unit.transform.position = GetWorldPos(tile) + new Vector3(0.5f, 0, 0);
            mapUnitsCollection.AddUnit(team, unit);
        }
    }

    private MapUnit GetMapUnitPrefab(TeamType team, int classId) {
        MapUnit[] units = team == TeamType.My ? myMapUnitPrefabs : enemyMapUnitPrefabs;
        return units.Where(t => t.classId == classId).FirstOrDefault();
    }

    public void RemoveMapUnit(MapUnit unit) => mapUnitsCollection.RemoveUnit(unit);

    public bool IsAllEnemyDie() => mapUnitsCollection.IsAllUnitsDie(TeamType.ENEMY);

    public bool IsMainRoleDie() => mapUnitsCollection.IsMainRoleDie(TeamType.My);
}
