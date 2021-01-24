using UnityEngine;

// 首先 常量，static，SerializeField，属性，字段；
// 其次 Awake(), Update(), 公共方法，私有方法
public class PlayerTest : MonoBehaviour
{
    // 首先私有的属性、字段、方法前不能忽略private

    private const int MAX_PLAYERS = 3; // 常量
    
    private static int totalPlayersSpawned; // static

    [SerializeField]
    private int damageModifier = 4; // SerializeField

    public int HealRemaining { get; } // Public属性（默认没有set;）

    private int Damage => damageModifier; // Private属性

    public int test; // Public Fields（尽量不要用，而是改成Pulic属性）

    private Weapon weapon; // Private Fields
    private int health;

    private void Awake() { } // Awake/Start/Constructor

    private void Update() { } // Update

    public void TakeDamage(int amount) { // Public Methods
        health -= amount;
        if (health <= 0) {
            Die();
        }
    } 

    private void Die() => SetWeapon(null); // Private Methods

    private void SetWeapon(Weapon weapon) { }

}

public class Weapon {

}
