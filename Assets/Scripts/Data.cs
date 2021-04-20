using System.Collections;
using System.Collections.Generic;

public class Data {
    public static Dictionary<string, string> talentTips = new Dictionary<string, string> {
        { "剑之达人", "使用剑时单场战斗内攻击次数超过3次后不再损耗耐久" },
        { "连击", "战斗中攻击次数+1，发动率 = 技%" },
        { "月光", "无视敌人50%防御力, 发动率 = 技%" },
        { "天赋", "战斗内的被动技能" }
    };

    public static Dictionary<string, string> skillTips = new Dictionary<string, string> {
        { "剑舞", "战斗内攻击次数+2" }
    };

    public static Dictionary<string, string> attrTips = new Dictionary<string, string> {
        { "攻击力", "角色基础力量 + 武器、buff等提供的攻击" },
        { "防御力", "角色基础守备 + 武器、buff等提供的攻击" },
        { "魔攻", "角色基础魔攻 + 武器、buff等提供的魔攻" },
        { "魔防", "角色基础魔防 + 武器、buff等提供的魔防" },
        { "攻速", "角色基础速度 - (武器重量 - 体格)，其中括号内数值>=0" },
        { "命中", "受武器命中、技巧、幸运、buff等影响" },
        { "回避", "受攻速、幸运、地形效果、武器相克等影响" },
        { "必杀", "受武器必杀、技巧等影响" },
        { "必杀回避", "受幸运等影响" },

        { "抗性", "角色的抗性，最终伤害 = (攻 - 防) x (是否暴击 ？ 暴击系数 : 1) × (1 - 对应抗性) x (1 - 最终伤害免伤率)，抗性上限90%，下限-200%" },
        { "物理", "物理抗性，也就是对剑、枪、斧子、弓箭的抗性" },
        { "光", "光系魔法抗性，光抗达到50%后任何回血效果加成50%" },
        { "暗", "暗系魔法抗性，暗抗达到50%后最终伤害免伤率+10%" },
        { "火", "火抗性，火抗达到50%后免疫灼烧" },
        { "雷", "雷抗性，雷抗达到50%后免疫麻痹" },
        { "风", "风抗性，风抗达到50%后免疫任何减速效果" },
    };
}
