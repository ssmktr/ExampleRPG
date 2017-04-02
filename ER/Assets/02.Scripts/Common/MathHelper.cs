using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper {

    // 각도 체크
    public static bool InAngle(Vector3 dir, Vector3 startPos, Vector3 endPos, float angle)
    {
        if (angle >= 360)
            return true;

        float ac = Mathf.Acos(Vector3.Dot(dir.normalized, (endPos - startPos).normalized));
        ac = (float.IsNaN(ac)) ? 1 : ac;

        float Rad = 180f / Mathf.PI;
        if (ac == 1 || (ac * Rad) <= angle / 2f)
            return true;

        return false;
    }

    // 거리 체크
    public static bool InDistance(Vector3 startPos, Vector3 endPos, float dist)
    {
        if (Vector3.Distance(startPos, endPos) <= dist)
            return true;

        return false;
    }
}
