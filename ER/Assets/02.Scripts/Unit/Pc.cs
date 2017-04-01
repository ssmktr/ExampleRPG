using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pc : Unit {

    private void Update()
    {
        InputControl();
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
}
