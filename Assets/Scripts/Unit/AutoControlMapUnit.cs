using System.Collections.Generic;

// 电脑控制的地图单位，也就是友军、敌人、中立单位
public class AutoControlMapUnit : MapUnit {

    public override void Click(LogicTile clickTile) {
        if (clickTile.UnitOnTile == this) {
            board.ShowMoveAndAttackTiles(tile, mapUnitAttr.movePower, mapUnitAttr.attackRange);
        } else {
            board.ClearUITiles();
        }
    }

    public override void MoveTo(LogicTile destination) {
        board.FindMovePaths(tile, mapUnitAttr.movePower);
        List<LogicTile> path = AStar.FindPath(lastStandTile, destination);
        StartCoroutine(Move(path));
    }
}
