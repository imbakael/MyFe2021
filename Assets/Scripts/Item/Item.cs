[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public int number;
    public string desc;
    
    public Item(int id, string name, int number, string desc) {
        this.id = id;
        this.name = name;
        this.number = number;
        this.desc = desc;
    }
}
