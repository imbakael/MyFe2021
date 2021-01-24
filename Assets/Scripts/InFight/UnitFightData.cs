using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFightData
{
    public int attack;
    public int defend;
    public int magicDefend;
    public int attackSpeed;
    public int hit;
    public int dodge;
    public int cri;
    public int criDodge = 8;

    public UnitFightData(int attack, int defend, int magicDefend, int attackSpeed, int hit, int dodge, int cri, int criDodge) {
        this.attack = attack;
        this.defend = defend;
        this.magicDefend = magicDefend;
        this.attackSpeed = attackSpeed;
        this.hit = hit;
        this.dodge = dodge;
        this.cri = cri;
        this.criDodge = criDodge;
    }
}
