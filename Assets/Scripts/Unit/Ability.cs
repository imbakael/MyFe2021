
public class Ability
{
    // 基础属性（可成长属性）
    public float str; // 力量
    public float def; // 守备
    public float mag; // 魔力
    public float res; // 魔防
    public float ski; // 技巧
    public float spd; // 速度
    public float luck; // 幸运
    public float con; // 体格
    public float move; // 移动力

    // 注：抗性只有加减法没有乘除法，比如初始物理抗性10%，盔甲增加20%物理抗性，则result = 10 % + 20%，而不是10% * (1 + 20%)
    public float physicsResist;
    public float lightResist;
    public float darkResist;
    public float fireResist;
    public float thunderResist;
    public float windResist;

    // 其他
    public float crit; // 暴击率
    public float critAvoid; // 暴击躲避率
    public float critTimes; // 暴击倍率

}
