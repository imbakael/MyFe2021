using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Exp {

    public static IEnumerator Calculate(MapUnit active, MapUnit passive) {
        Role levelUpRole = AddExp(active, passive);
        if (levelUpRole != null) {
            string levelUpData = GetLevelUpValue(levelUpRole);
            LevelUpPanel panel = UIManager.Instance.CreateLevelUpPanel(levelUpRole, levelUpData);
            while (!panel.isDestroyed) {
                yield return null;
            }
        }
    }

    private static Role AddExp(MapUnit active, MapUnit passive) {
        if (active.Team == TeamType.My) {
            bool isLevelUp = active.Role.AddExp(50);
            if (isLevelUp) {
                return active.Role;
            }
        } else if (passive.Team == TeamType.My) {
            bool isLevelUp = passive.Role.AddExp(50);
            if (isLevelUp) {
                return passive.Role;
            }
        }
        return null;
    }

    private static string GetLevelUpValue(Role role) {
        // 等级，血量，力量，技术，速度，幸运，守备，魔防，体格
        int level = 1;
        int hp = Random.Range(0, 3);
        int str = Random.Range(0, 2);
        int ski = Random.Range(1, 3);
        int spd = Random.Range(1, 3);
        int luck = Random.Range(0, 2);
        int def = Random.Range(0, 2);
        int res = Random.Range(0, 2);
        int con = Random.Range(0, 2);
        return level + "|" + hp + "|" + str + "|" + ski + "|" + spd + "|" + luck + "|" + def + "|" + res + "|" + con;
    }
}
