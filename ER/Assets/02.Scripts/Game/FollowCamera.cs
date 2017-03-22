using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    float Dist = 6;
    float Height = 4;
    public Transform Target;

    private void LateUpdate()
    {
        if (Target != null)
        {
            transform.LookAt(Target);
            transform.position = (Target.position - (Vector3.forward * Dist)) + Target.up * Height;
        }
    }
}
