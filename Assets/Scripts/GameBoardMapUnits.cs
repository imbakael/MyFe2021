using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameBoard
{
    private MapUnitsCollection mapUnitsCollection;

    public List<MapUnit> GetAllOtherUnits(TeamType ignoreTeam) => mapUnitsCollection.GetAllOtherUnits(ignoreTeam);

    public void AllNextTurn() => mapUnitsCollection.AllNextTurn();

    public void NextTurn(TeamType teamType) => mapUnitsCollection.NextTurn(teamType);

    public void CreateMapUnits(Vector2Int[] allCellPos, TeamType team) {
        for (int i = 0; i < allCellPos.Length; i++) {
            Vector2Int cellPos = allCellPos[i];
            int index = walkMap.size.x * cellPos.y + cellPos.x;
            LogicTile tile = allLogicTiles[index];
            MapUnit unit = Instantiate(playerPrefabs[(int)team]);
            unit.Init(team, tile);
            unit.transform.position = GetWorldPos(tile) + new Vector3(0.5f, 0, 0);
            mapUnitsCollection.AddUnit(team, unit);
        }
    }

    public bool IsAllEnemyDie() => mapUnitsCollection.IsAllUnitsDie(TeamType.ENEMY);

    public bool IsMainRoleDie() => mapUnitsCollection.IsMainRoleDie(TeamType.MY_ARMY);
}
