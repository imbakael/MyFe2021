using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PosTile {
    public int x;
    public int y;
    public PosTile(int x, int y) {
        this.x = x;
        this.y = y;
    }
}

public class SkillController : Singleton<SkillController>
{
    [SerializeField] private IceSpell icePrefab = default;

    private List<IceSpell> iceSpells = new List<IceSpell>();

    public void CreateIceSpells(int[] range) {
        var result = new List<LogicTile>();
        for (int i = 0; i < range.Length; i += 2) {
            LogicTile tile = GameBoard.instance.GetLogicTile(range[i], range[i + 1]);
            if (tile != null) {
                result.Add(tile);
            }
        }
        CreateIceSpells(result);
    }

    public void CreateIceSpells(List<PosTile> tiles) {
        var result = new List<LogicTile>();
        foreach (PosTile item in tiles) {
            LogicTile tile = GameBoard.instance.GetLogicTile(item.x, item.y);
            if (tile != null) {
                result.Add(tile);
            }
        }
        CreateIceSpells(result);
    }

    public void CreateIceSpells(List<LogicTile> range) {
        foreach (LogicTile tile in range) {
            Vector3 pos = GameBoard.instance.GetWorldPos(tile);
            IceSpell ice = Instantiate(icePrefab);
            ice.transform.position = pos + new Vector3(0.5f, 0, 0);
            ice.gameObject.SetActive(false);
            ice.Init(tile);
            iceSpells.Add(ice);
        }
        StartCoroutine(DelayShow());
    }

    private IEnumerator DelayShow() {
        foreach (IceSpell item in iceSpells) {
            item.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DestroyAllIceSpells() {
        for (int i = iceSpells.Count - 1; i >= 0; i--) {
            IceSpell ice = iceSpells[i];
            ice.DestroyMyself();
            iceSpells.RemoveAt(i);
        }
    }

    public void DestroyIceSpell(int x, int y) {
        IceSpell ice = iceSpells.Where(t => t.Tile.X == x && t.Tile.Y == y).FirstOrDefault();
        if (ice != null) {
            iceSpells.Remove(ice);
            ice.DestroyMyself();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            CreateIceSpells(new List<PosTile> {
                new PosTile(3, 5),
                new PosTile(4, 5),
                new PosTile(5, 5)
            });
        }
 
        if (Input.GetKeyDown(KeyCode.O)) {
            DestroyAllIceSpells();
        }
    }

    /// <summary>
    /// 根据目标位置x和y获得范围坐标
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="range"></param>
    /// <param name="minLength"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public int[][] GetAllRange(int x, int y, int[][] range, int minLength, int maxLength) {
        var result = new int[range.Length][];
        for (int i = 0; i < range.Length; i++) {
            result[i] = GetRange(x, y, range[i], minLength, maxLength);
        }
        return result;
    }

    private int[] GetRange(int x, int y, int[] range, int minLength, int maxLength) {
        var result = new List<int>();
        for (int i = minLength; i <= maxLength; i++) {
            int newX = x + range[0] * i;
            int newY = y + range[1] * i;
            result.Add(newX);
            result.Add(newY);
        }
        return result.ToArray();
    }
}
