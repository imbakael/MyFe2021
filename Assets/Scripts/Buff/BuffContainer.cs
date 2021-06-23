using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffContainer
{
    private Role role;
    private List<BuffBase> buffs;

    public BuffContainer(Role role) {
        buffs = new List<BuffBase>();
        this.role = role;
    }

    public void Add(BuffBase buff) {
        buffs.Add(buff);
        if (buff is IModify modify) {
            modify.Apply();
        }
        // 如果此buff的效果是驱散所有负面buff，则需要对所有buff进行遍历
    }

    public void Remove(BuffBase buff) {
        if (buffs.Contains(buff)) {
            buff.OnRemove();
            buffs.Remove(buff);
        }
    }

    public List<BuffBase> GetBuffsByTag(string tag) {
        return buffs.Where(t => t.tag.Contains(tag)).ToList();
    }

    // 攻击者对伤害信息进行处理
    // 假设this属于攻击者A，A攻击B时调用A身上的所有buff的OnHit方法，来强化伤害
    // 如A本来最终物理攻击 = 10，B的最终物理攻击 = 7，damge = 3，但是A有个buff可以提升20%物理伤害，则经过此步骤后damge = 3(base) + 3 * 0.2(offset) = 3.6
    public void HandleDamgeInfoOnHit(DamageInfo damageInfo) {
        for (int i = 0; i < buffs.Count; i++) {
            BuffBase buff = buffs[i];
            buff.OnHit(damageInfo);
        }
    }

    // 受击者对伤害信息进行处理
    // 接上面的伤害信息，比如B身上有个buff可以降低90%物理伤害（至少1点），则经过此步骤后damge = 3 + 3 * 0.2 - 3 * 0.9 = 0.9 （最终 = 1）
    public void HandleDamgeInfoBeHurt(DamageInfo damageInfo) {
        for (int i = 0; i < buffs.Count; i++) {
            BuffBase buff = buffs[i];
            buff.BeHurt(damageInfo);
        }
        // 如果要杀死role则触发每个buff的BeKilled方法
        float totalDamage = damageInfo.damage.GetTotalDamage();
        if (totalDamage >= role.Hp) {
            for (int i = 0; i < buffs.Count; i++) {
                BuffBase buff = buffs[i];
                buff.OnBeforeDead(damageInfo);
            }
        }
    }
}
