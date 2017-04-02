using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pc : Unit {

    public override void Init(UnitInfo unitinfo)
    {
        base.Init(unitinfo);
    }

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

    protected override void Attack()
    {
        base.Attack();

        // 적 대미지 체크
        List<Unit> listEnemy = GameManager.Instance.AllUnitDic[UnitType.Enemy];
        for (int i = 0; i < listEnemy.Count; ++i)
        {
            if (GameHelper.IsAttackRange(transform.forward, transform.position, listEnemy[i].transform.position, 90, 1))
                listEnemy[i].TakeDamage(Atk);
        }

        // 보스 대미지 체크
        List<Unit> listBoss = GameManager.Instance.AllUnitDic[UnitType.Boss];
        for (int i = 0; i < listBoss.Count; ++i)
        {
            if (GameHelper.IsAttackRange(transform.forward, transform.position, listBoss[i].transform.position, 90, 3))
                listBoss[i].TakeDamage(Atk);
        }
    }

    // 내 캐릭 조종하기
    void InputControl()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_IsAttack)
                return;

            Attack();
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _UnitInfo.movespeed);
            PlayAnimation(AniState.Walk);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * _UnitInfo.movespeed);
            PlayAnimation(AniState.Walk);
        }
        else
        {
            PlayAnimation(AniState.Wait);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * 120);
            PlayAnimation(AniState.Walk);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 120);
            PlayAnimation(AniState.Walk);
        }
    }

    // 픽킹
    RaycastHit hit;
    void InputRaycast()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            SetMoveDest(hit.point);
        }
    }
}

