// 只保存数据（字段），没有方法，保证数据和逻辑分离
// 每个具体的单位类都持有一个此对象，此对象也是保存数据的重点
[System.Serializable]
public class UnitAttr
{
    public TeamType Team;
    public int Id;

    public int Name;
    public int ClassId; // 职业id，如剑士、战士、刺客，通过id查表即可获得职业名
    public int Lv;
    public int CurExp; // 经验值取值范围0-99，到达100时升级，到达等级上限时重置为0
    public int CurHp;
    public int Hp;

    #region 战斗时属性，会实时跟随角色当前装备、buff和debuff等而改变，所以不应该属于此类中
    //public int Attack { get; set; }
    //public int Defend { get; set; }
    //public int MagicDefend { get; set; }
    //public int AttackSpeed { get; set; }
    //public int Hit { get; set; }
    //public int Dodge { get; set; }
    //public int Cri { get; set; }
    //public int CriDodge { get; set; }
    #endregion

    #region 基础属性（可成长属性）
    public int Str; // 力量
    public int Def; // 物防
    public int Mag; // 魔力
    public int Res; // 魔防
    public int Ski; // 技巧
    public int Spd; // 速度
    public int Luck; // 幸运
    public int Con; // 体格
    public int Move; // 移动力
    #endregion

    #region 基础抗性
    public float PhysicsResist;
    public float LightResist;
    public float DarkResist;
    public float FireResist;
    public float ThunderResist;
    public float WindResist;
    #endregion

    #region 大地图属性
    public Vector2Int position = new Vector2Int(3, 3);
    public MapState mapState = MapState.IDLE;
    public LogicTile tile;
    #endregion

    // 装备
    public List<Item> items = new List<Item>();

    // buff

    public UnitAttr(Json json) {
        // 读取Json数据
    }

}
