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
            new ConversationAction(PosType.Left, "今天天气不错啊，我累个个乖乖 #\n听说附近开始出现魔物了，老弟你怎么看？#"),
            new ConversationAction(PosType.Right, "我只会心疼gie gie...#\n话说最近欧冠你看了没#"),
            new ConversationAction(PosType.Left, "你搁这心疼尼玛呢，曼城2：1大巴黎，曼城牛逼，丁丁就是吊！#"),
            new ConversationAction(PosType.Right, "姆巴佩怎么毫无存在感，淦#")
        };
    }

    public IAction GetAction() => actions.Count > index ? actions[index++] : null;

    public void Skip() {
        // 比如结束对话、设置一些单位的最终位置、属性等

    }
}
