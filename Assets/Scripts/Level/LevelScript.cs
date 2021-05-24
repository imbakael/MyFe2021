using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 关卡剧本子集
public class LevelScript
{
    // 剧本的行为包括角色对话（有时会包含选项等待玩家选择）、角色在大地图上移动or攻击等行为、生成角色

    private List<IAction> actions;
    private int index;

    public LevelScript() {
        index = 0;
        actions = new List<IAction> {
            new ConversationAction(PosType.Left, "喂，三点几嚟#"),
            new ConversationAction(PosType.Right, "做撚啊做，饮茶先啦，三点几嚟，饮茶先#"),
            new ConversationAction(PosType.Left, "做咁多都冇用嘅，老细唔锡你嘅嚟#"),
            new ConversationAction(PosType.Right, "饮茶先，饮茶先！#")
        };
    }

    public IAction GetAction() => actions.Count > index ? actions[index++] : null;

    public void Skip() {
        // 比如结束对话、设置一些单位的最终位置、属性等

    }
}
