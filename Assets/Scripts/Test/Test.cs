using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Linq;
using System.Collections;

public class Test : MonoBehaviour
{
    string example1 = @"
        {
            'id':1,
            'name':'铁剑',
            'number':40,
            'desc':'普通的剑'
        }
    ";

    string example2 = @"
        [
            {'name':'艾瑞柯'},
            {'name':'玛丽卡'}
        ]
    ";

    private Dictionary<int, int> testDic = new Dictionary<int, int>();

    private void Awake() {
        // json读取
        //string s = File.ReadAllText(Application.dataPath + "/Json/Items.json");
        //JsonData jd = JsonMapper.ToObject(s);
        //foreach (JsonData j in jd) {
        //    Debug.Log(j["name"]);
        //}

        //// json写入
        //Item item1 = new Item(0, "铁剑", 40, "铁制的剑");
        //Item item2 = new Item(0, "细剑", 20, "威力较小的剑");
        //List<Item> items = new List<Item> {
        //    item1, item2
        //};
        //File.WriteAllText(Application.dataPath + "/Json/Item2.json", JsonMapper.ToJson(items));

        MyContainer c = new MyContainer();
        c.Register<Talent>((temp) => { return new Talent(); });
        Talent t = c.Create<Talent>();
        //Debug.LogError("t.count = " + t.count);

        //string tag = @"
        //    {
        //        'type':'fire',
        //        'fixed':'def_-3.2|str_2',
        //        'percent':'def_-0.1|luck_0.3'
        //    }
        //";

        //var baseAbility = new Ability {
        //    str = 10,
        //    def = 100,
        //    luck = 3
        //};

        //var dic = MyTools.GetPropertyDic(tag, "fixed");
        //var dic1 = MyTools.GetPropertyDic(tag, "percent");

        //Debug.LogError("0 str = " + baseAbility.str + ", def = " + baseAbility.def + ", luck = " + baseAbility.luck);

        //foreach (var item in dic) {
        //    MyTools.ChangeFieldValue(baseAbility, item.Key, item.Value);
        //}

        //Debug.LogError("1 str = " + baseAbility.str + ", def = " + baseAbility.def + ", luck = " + baseAbility.luck);

        //foreach (var item in dic1) {
        //    float baseValue = MyTools.GetFieldValue<float>(baseAbility, item.Key);
        //    MyTools.ChangeFieldValue(baseAbility, item.Key, baseValue * item.Value);
        //}

        //Debug.LogError("2 str = " + baseAbility.str + ", def = " + baseAbility.def + ", luck = " + baseAbility.luck);
    }

    private void Start() {
        //TestBattle();
    }

    private void TestBattle() {
        MapUnit active = GameBoard.instance.GetTeam(TeamType.My)[0];
        MapUnit passive = GameBoard.instance.GetTeam(TeamType.ENEMY)[0];
        MapBattleController.Instance.StartMapBattle(active, passive);
    }

}
