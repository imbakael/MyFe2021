using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 实现此接口表示会产生实际伤害
public interface IDamageable {
    DamageInfo DoDamage();
}

