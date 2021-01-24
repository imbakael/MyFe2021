using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestLinq : MonoBehaviour
{
    private List<Person> people = new List<Person> {
        new Person { Name = "张三", Age = 15, Id = 5 },
        new Person { Name = "李四", Age = 13, Id = 1},
        new Person { Name = "王五", Age = 13, Id = 0 },
    };
    
    private void Awake() {
        // Where
        Person p = people.Where(t => t.Name == "王五").FirstOrDefault();
        Debug.Log("王五.age = " + p.Age);

        // All
        Debug.Log("All = " + people.All(t => t.Age > 13));

        // Any
        Debug.Log("Any = " + people.Any(t => t.Name == "张三"));

        // ToDictionary
        var dic = people.ToDictionary(k => k.Name, v => v.Age);
        foreach (var item in dic) {
            Debug.Log("k = " + item.Key + ", v = " + item.Value);
        }

        // ToList
        var list = people.Where(t => t.Name == "王五").ToList();
        list.ForEach(t => Debug.Log("age = " + t.Age));

        var dic1 = people.OrderBy(t => t.Age).ThenBy(t => t.Id).ToDictionary(k => k.Name, v => v.Age);
        foreach (var item in dic1) {
            Debug.LogError("item = " + item.Key + ", v = " + item.Value);
        }
    }

}

public class Person {
    public string Name { get; set; }
    public int Age { get; set; }
    public int Id { get; set; }
}
