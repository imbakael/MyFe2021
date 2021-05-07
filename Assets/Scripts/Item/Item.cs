[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public int count;
    public string desc;
    public int curCount;
    
    public Item(int id, string name, int count, int curCount, string desc) {
        this.id = id;
        this.name = name;
        this.count = count;
        this.curCount = curCount;
        this.desc = desc;
    }
}
