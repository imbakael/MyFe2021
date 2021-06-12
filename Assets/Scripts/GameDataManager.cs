using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    private static string SavePath = string.Empty;

    private void Awake() {
        SavePath = Application.persistentDataPath + "/SaveData.txt";
    }

    public void Init() {
        if (File.Exists(SavePath)) {
            byte[] bytes = File.ReadAllBytes(SavePath);
            string json = Encoding.UTF8.GetString(bytes);
            var allSaveData = JsonUtility.FromJson<AllSaveData>(json);
            CreateMapUnits(allSaveData.data);
            Debug.Log("File Exists" + json);
        } else {
            var mapUnitData = new List<MapUnitData> {
                //                                                                         hp/str/def/ski/luck/d/spd 
                new MapUnitData(TeamType.ENEMY, 10, 10, 5, 0, new Role(TeamType.ENEMY, 10, 40, 8, 3, 5, 2, 20, 4)),
                new MapUnitData(TeamType.ENEMY, 10, 13, 3, 0, new Role(TeamType.ENEMY, 10, 40, 8, 3, 5, 2, 20, 4)),
                new MapUnitData(TeamType.ENEMY, 11, 15, 1, 0, new Role(TeamType.ENEMY, 11, 44, 9, 5, 4, 3, 20, 4)),
                new MapUnitData(TeamType.ENEMY, 11, 10, 0, 0, new Role(TeamType.ENEMY, 11, 44, 9, 5, 4, 3, 20, 4)),
                new MapUnitData(TeamType.ENEMY, 12, 11, 4, 0, new Role(TeamType.ENEMY, 12, 33, 7, 3, 6, 4, 40, 6)),
                new MapUnitData(TeamType.ENEMY, 12, 15, 7, 0, new Role(TeamType.ENEMY, 12, 33, 7, 3, 6, 4, 40, 6)),
                new MapUnitData(TeamType.ENEMY, 12, 16, 4, 0, new Role(TeamType.ENEMY, 12, 33, 7, 3, 6, 4, 40, 6)),
                new MapUnitData(TeamType.ENEMY, 13, 11, 9, 0, new Role(TeamType.ENEMY, 13, 60, 14, 6, 8, 5, 20, 5)),

                new MapUnitData(TeamType.My, 0, 0, 8, 0, new Role(TeamType.My, 0, 35, 10, 4, 13, 7, 40, 11)),
                new MapUnitData(TeamType.My, 1, 1, 9, 0, new Role(TeamType.My, 1, 30, 9, 3, 8, 5, 40, 10)),
                new MapUnitData(TeamType.My, 2, 1, 7, 0, new Role(TeamType.My, 2, 42, 10, 5, 4, 4, 40, 4)),
                new MapUnitData(TeamType.My, 3, 2, 5, 0, new Role(TeamType.My, 3, 33, 9, 4, 6, 4, 40, 6)),
                new MapUnitData(TeamType.My, 4, 0, 6, 0, new Role(TeamType.My, 4, 45, 12, 7, 9, 8, 40, 7)),
            };
            CreateMapUnits(mapUnitData);
            //SaveAll(new AllSaveData(mapUnitData));
            //LevelScriptController.Instance.StartScript();
        }
    }

    private void CreateMapUnits(List<MapUnitData> mapUnitData) {
        foreach (MapUnitData data in mapUnitData) {
            GameBoard.instance.CreateMapUnits(data);
        }
    }

    public void SaveAll() {
        var mapUnitData = new List<MapUnitData>();
        List<MapUnit> my = GameBoard.instance.GetTeam(TeamType.My);
        List<MapUnit> enemy = GameBoard.instance.GetTeam(TeamType.ENEMY);
        mapUnitData.AddRange(GetOneTeamData(my));
        mapUnitData.AddRange(GetOneTeamData(enemy));
        var data = new AllSaveData(mapUnitData);
        SaveAll(data);
    }

    private List<MapUnitData> GetOneTeamData(List<MapUnit> mapUnits) {
        var result = new List<MapUnitData>();
        foreach (MapUnit item in mapUnits) {
            if (item.IsDead) {
                continue;
            }
            result.Add(new MapUnitData(item.Team, item.classId, item.Tile.X, item.Tile.Y, item.GetMapState(), item.Role));
        }
        return result;
    }

    private void SaveAll(AllSaveData data) {
        string json = JsonUtility.ToJson(data);
        File.WriteAllBytes(SavePath, Encoding.UTF8.GetBytes(json));
        Debug.Log("SaveAll " + json);
    }

    public static void DeleteFile() {
        string path = SavePath == string.Empty ? Application.persistentDataPath + "/SaveData.txt" : SavePath;
        if (File.Exists(path)) {
            File.Delete(path);
            Debug.Log("存在文件，删除成功！");
        } else {
            Debug.Log("不存在文件，删除失败！");
        }
    }

    public static bool ExistFile() => File.Exists(SavePath == string.Empty ? Application.persistentDataPath + "/SaveData.txt" : SavePath);

}

[System.Serializable]
public class AllSaveData {
    public List<MapUnitData> data;

    public AllSaveData(List<MapUnitData> data) {
        this.data = data;
    }
}

[System.Serializable]
public class MapUnitData {
    public TeamType team;
    public int classId;
    public int x;
    public int y;
    public int state;
    public Role role;

    public MapUnitData(TeamType team, int classId, int x, int y, int state, Role role) {
        this.x = x;
        this.y = y;
        this.state = state;
        this.role = role;
        this.team = team;
        this.classId = classId;
    }
}
