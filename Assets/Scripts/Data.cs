﻿using System.Collections;
using System.Collections.Generic;

public class Data {
    public static Dictionary<string, string> uiTips = new Dictionary<string, string> {
        { "天赋", "战斗内的被动技能" },
        { "技能", "主动技能，包括地图技能和战斗内技能" },
        { "等级", "角色等级，上限20级" },
        { "经验", "经验值达到100时升级" },
        { "生命", "角色生命值，降低到0时角色死亡" },

        
    };

    public static Dictionary<string, string> classTips = new Dictionary<string, string> {
        { "剑圣", "高阶职业，用剑的最高境界，速度和技巧都很出色" },
        { "剑士", "低阶职业，擅长使用剑" },
    };

    public static Dictionary<string, string> talentTips = new Dictionary<string, string> {
        { "剑之达人", "使用剑时单场战斗内攻击次数超过3次后不再损耗耐久" },
        { "连击", "每次己方回合时判定一次，战斗中攻击次数+1，发动率 = 技%" },
        { "月光", "无视敌人50%防御力, 发动率 = 技%" },

        // ----------恶魔族----------(普遍高攻低防)
        { "恶魔种族", "火抗50%，光抗-80%，暗抗30%，熔岩地形攻防+30%，寒冰地形攻防-30%" },
        { "天赋偷取", "大恶魔盖达瑞拉专属，击杀单位后可以手动选择1个他的天赋据为己有，不受章节影响，上限偷取1个，种族天赋or某些特定天赋不可偷取（如魅惑等）" },
        { "传送", "传送到移动范围内的任意位置，不受地形影响" },
        { "恶魔首领", "所有恶魔族士气+1" },
        // 火元素
        { "火焰传送", "可以传送到任意被灼烧的单位附近，无论敌我" },
        { "再行动", "击杀被灼烧单位后可再行动，一回合至多触发1次" },
        // 小恶魔
        { "厄运", "攻击有30%概率给目标附加厄运，受厄运英雄的单位幸运值清0，持续5回合" },
        { "厄运收集者", "场上每存在一个受厄运影响的单位（无论敌我），自身幸运+10，上限50" },
        // 独裁者
        { "裁决", "击杀目标后对周围5格所有人类or精灵造成恐惧效果，受恐惧影响的单位无法攻击比自己强大的单位" },
        { "血脉压制", "周围3格内比自己弱小的单位不会受到士气带来的增益" },

        // ----------精灵族----------(普遍具有光抗，擅长治疗，射术高)
        { "精灵族", "光抗50%，暗抗-30%，受到额外50%治疗效果加成，森林地形中士气+1" },
        { "隐匿", "在森林地形中待命时不会被敌人发现" },
        { "射术", "持弓时攻击力+30%，命中+50%，必杀率+25%" },
        { "精灵赞歌", "所有己方精灵族移动力+2，速度和技巧+5" },

        // ----------人族----------
        { "人族", "友方人族单位之和>=5时全体人族士气+1" },
        { "强援", "重甲专属，周围1格己方单位受到的第一次攻击由重甲承受，每回合生效一次" },
        { "圣骑士之盾", "圣骑士专属，战斗中与某一属性对抗时，其他属性基础抗性的10%会加到此属性抗性中" },

        // ----------亡灵族----------
        { "亡灵族", "光抗-80%，暗抗20%，不受中毒、睡眠、瘟疫等影响" },
        { "瘟疫", "攻击附加瘟疫，受瘟疫影响的单位生命上限-10%，每回合受到1点伤害" },

        // ----------中立种族（龙、一些野兽等）----------

    };

    public static Dictionary<string, string> skillTips = new Dictionary<string, string> {
        { "剑舞", "战斗内攻击次数+2" },
        { "冰封路径", "发射一道长8格的冰墙，冰墙上的单位受到施法者魔力150%的冰属性伤害，冰墙存在3回合，hp 1" },
        { "霜土", "在指定位置生成一个范围5格的寒冰地形，寒冰地形上的单位不会被灼烧，受到的火属性伤害-50%，如果有雷系魔法，则会对此区域的所有敌对非目标单位造成20%雷伤害" },
        { "恶魔之门1级", "随机召唤1个弱小恶魔单位(火元素or小恶魔or独裁者)" },
        { "恶魔之门2级", "随机召唤2个普通恶魔单位(火元素or小恶魔or独裁者)" },
        { "恶魔之门3级", "手动召唤2个强大恶魔单位(火元素or小恶魔or独裁者)" },
        { "属性吸取", "吸取己方单位20%攻防，持续2回合，如果是自身召唤的，可选择将其击杀，持续时间翻倍" },
    };

    public static Dictionary<string, string> attrTips = new Dictionary<string, string> {
        { "物理攻击力", "角色基础力量 + 武器、buff等提供的攻击" },
        { "物理防御力", "角色基础守备 + 武器、buff等提供的攻击" },
        { "力量", "角色力量，影响物理攻击力" },
        { "魔力", "角色魔力，影响魔法攻击力" },
        { "守备", "角色守备，越高受到的物理伤害越小" },
        { "魔防", "角色魔防，越高受到的魔防伤害越小" },
        { "技巧", "角色技巧，影响必杀率和一些天赋的触发几率" },
        { "体格", "角色体格，越高可以使用越重的武器" },
        { "速度", "角色速度，速度差值>=4时可以追击" },
        { "幸运", "角色幸运，越高则必杀回避率越高" },
        { "移动", "角色移动力，决定移动范围" },

        //{ "射程", "当前武器射程" },
        //{ "魔攻", "角色基础魔攻 + 武器、buff等提供的魔攻" },
        //{ "魔防", "角色基础魔防 + 武器、buff等提供的魔防" },
        //{ "攻速", "角色基础速度 - (武器重量 - 体格)，其中括号内数值>=0" },
        //{ "命中", "受武器命中、技巧、幸运、buff等影响" },
        //{ "回避", "受攻速、幸运、地形效果、武器相克等影响" },
        //{ "必杀", "受武器必杀、技巧等影响" },
        //{ "必杀回避", "受幸运等影响" },

        { "抗性", "角色的抗性，最终伤害 = (攻 - 防) x (是否暴击 ？ 暴击系数 : 1) × (1 - 对应抗性) x (1 - 最终伤害免伤率)，抗性上限90%，下限-200%" },
        { "物理", "物理抗性，也就是对剑、枪、斧子、弓箭的抗性" },
        { "光", "光系魔法抗性，光抗达到50%后任何回血效果加成50%" },
        { "暗", "暗系魔法抗性，暗抗达到50%后最终伤害免伤率+10%" },
        { "火", "火抗性，火抗达到50%后免疫灼烧" },
        { "雷", "雷抗性，雷抗达到50%后免疫麻痹" },
        { "风", "风抗性，风抗达到50%后免疫任何减速效果（包括移动力减速和攻速减速）" },
    };

    public static Dictionary<string, string> buffTips = new Dictionary<string, string> {
        { "灼烧", "每回合受到xx火焰伤害，计算抗性" },
        { "麻痹", "攻击时攻击次数-1（但至少可以攻击1次）" },
        { "减速", "移动力减半" },
        { "冰冻", "无法行动1回合，无法闪避" },
        { "沉默", "无法使用任何主动技和杖，3回合" },
        { "石化", "无法行动2回合，无法闪避，物抗+30%" },
        { "睡眠", "睡眠2回合，无法闪避，受到任意伤害后苏醒" },
    };

    public static Dictionary<string, string> storyTips = new Dictionary<string, string> {
        { "阿尔法", "来自XXX的剑圣" }
    };

    public static string GetDetail(string name) {
        if (uiTips.TryGetValue(name, out string result)) {
            return result;
        }
        if (classTips.TryGetValue(name, out result)) {
            return result;
        }
        if (talentTips.TryGetValue(name, out result)) {
            return result;
        }
        if (skillTips.TryGetValue(name, out result)) {
            return result;
        }
        if (attrTips.TryGetValue(name, out result)) {
            return result;
        }
        if (buffTips.TryGetValue(name, out result)) {
            return result;
        }
        if (storyTips.TryGetValue(name, out result)) {
            return result;
        }
        return "暂无介绍";
    }
}
