using System.Collections.Generic;
using UnityEngine;
//using LitJson;
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
        ////foreach (JsonData j in jd) {
        ////    Debug.Log(j["name"]);
        ////}

        //// json写入
        //Item item1 = new Item(0, "铁剑", 40, "铁制的剑");
        //Item item2 = new Item(0, "细剑", 20, "威力较小的剑");
        //List<Item> items = new List<Item> {
        //    item1, item2
        //};
        //File.WriteAllText(Application.dataPath + "/Json/Item2.json", JsonMapper.ToJson(items));

        Dictionary<int, List<int>> a = new Dictionary<int, List<int>> {
            { 8, new List<int> { 33, 44 } },
            { 9, new List<int> { -1, 98 } },
        };

        List<int> b = a[8];
        b.RemoveAt(1);
        foreach (var item in a) {
            //Debug.Log(item.Value.Count);
        }

        MyContainer c = new MyContainer();
        c.Register<Talent>((temp) => { return new Talent(); });
        Talent t = c.Create<Talent>();
        //Debug.LogError("t.count = " + t.count);

        //TestInsersect();
    }

    private void TestInsersect() {
        List<int> a = new List<int> { 99, -1, 2, -45 };
        List<int> b = new List<int> { 0, 44, -23, -1, 99, 4, 2 };
        var insersect = a.Intersect(b);
        foreach (var item in insersect) {
            Debug.Log("inter = " + item);
        }
    }

    
}
