using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pc : Unit {

    private void Update()
    {
        InputControl();

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            InputRaycast();
#elif UNITY_ANDROID
        if (Input.touchCount == 1)
            InputRaycast();
#endif

    }


    // 내 캐릭 조종하기
    void InputControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _UnitInfo.movespeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * _UnitInfo.movespeed);
        }
        else
        {

        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * 120);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 120);
        }

    }

    // 픽킹
    RaycastHit hit;
    void InputRaycast()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            SetMoveDest(hit.point);
            Debug.Log(hit.point + " ===== " + Camera.main.WorldToScreenPoint(hit.point));
        }
    }
}

