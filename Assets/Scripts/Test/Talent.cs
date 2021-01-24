﻿
using System.Collections.Generic;

public class Talent
{
    List<string> allTalents = new List<string> {
        // 攻击次数相关
        "流星剑，当前回合攻击次数 + 3，每次己方回合初始判定，技%",
        "连续，当前回合攻击次数 + 1， 每次己方回合初始判定，技%",
        "追击，己方回合数 + 1，可与速度差产生的追击叠加",
        @"极限流[稀有]，每回合攻击次数+1，（只考虑命中的普攻）除第一次普攻外，每次普攻伤害降低15%，最低将至25%，但是最后一次
          附加所有伤害的差值的2倍，且必中
          （如一共打了8刀，分别是100%，85%，70%，55%，40%，25%，25%，？%，其中？ = 25% + 375% * 2）",

        // buff相关
        "生命汲取，触发时回复造成伤害30%的生命值，（力 + 魔力)/2 %",
        @"圣女xx的守护[稀有]，战斗开始时自身当前hp的50%会化作一个护盾(护盾本质还是自身)，护盾会吸收所有伤害，如果战后护盾hp>=80%则回满血，
            hp>0% && hp < 80%则恢复护盾初始值的80%，若护盾被击破则正常掉血
          (如当前hp30，护盾hp = 15，对面伤害10，则战后hp为27，如果对面伤害16，则战后hp为14)",
        "贯穿，触发时无视敌人50%物理防御力",
        "月光，触发时无视敌人50%物理抗性",
        "xx，受到致死伤害时生命降至1，持续到本场战斗结束，cd 2回合",

        // 属性相关
        "精神免疫，对任何控制类技能免疫",
        "魔法免疫，对所有减益魔法免疫"


    };

    List<string> allActiveSkills = new List<string> {
        "生命互换，自身hp百分比低于对面时，互换双方hp百分比，对非boss且级别不高于自己的单位有效",

    };

    List<string> sword = new List<string> {
        ""
    };

    public int count = -12;
}
