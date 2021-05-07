using System.Collections;

public interface IAction {
    bool IsOver { get; set; }
    void Action(); // 包含对话推进、大地图角色行为（移动、攻击等）
}
