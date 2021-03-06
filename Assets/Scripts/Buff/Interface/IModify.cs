﻿
/*  1、实现此接口表示会对角色属性进行修改，比如武器、部分buff、可消耗道具等
    2、注意此方法会立即修改角色部分属性，不会再进行动态计算。而DamageInfo在进行buff过滤时可能会动态计算伤害值
 
    举例:攻击者A的力量10，B的防御6；A身上buff效果为：增加50%基础力量，同时物理攻击伤害+20%；B身上buff效果为：防御+2，受到物理伤害降低(5 + 30%)，反弹20%伤害，则步骤如下：
        base damage（物理） = (10 + 10 * 0.5 - 6 - 2) = 7
        过滤后damge = 7 + 7 * 0.2 - 7 * 0.3 - 5 = 1.3
        反弹buff会再生成一个DamageInfo，基础值为1.3 * 20% = 0.26，此伤害不会再被反弹，不会触发暴击
*/
public interface IModify {
    // 执行属性方面的计算，如力量 + 20%；每次玩家基础属性改变、升级等都会从自身所有buff中重新计算apply，以刷新offset值
    void Apply();
}
