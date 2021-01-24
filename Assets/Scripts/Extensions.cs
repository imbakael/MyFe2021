using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    // 用法 Vector3 v = v.With(y: 0);
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null) {
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }

    public static Vector2 With(this Vector2 original, float? x = null, float? y = null) {
        return new Vector2(x ?? original.x, y ?? original.y);
    }

    
}
