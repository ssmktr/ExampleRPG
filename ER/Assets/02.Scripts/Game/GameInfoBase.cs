using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfoBase : MonoBehaviour {

    public Transform UnitGroup;
    public FollowCamera _FollowCamera;

    public virtual void Init(cGame game)
    {
        _FollowCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>();
        UnitGroup = transform.FindChild("UnitGroup").transform;

        for (int i = 0; i < GameManager.Instance.HeroCnt; ++i)
        {
            GameObject hero = GameHelper.UnitLoad(1001, UnitGroup, Vector3.zero);
            if (hero != null && _FollowCamera != null)
                _FollowCamera.Target = hero.transform;

            Unit unit = hero.GetComponent<Unit>();
            if (unit != null)
            {
                // 체력바 생성
                if (game != null)
                    game.CreateHpSlider(unit, unit._HpParent, hero.name);

                GameManager.Instance.AddUnit(UnitType.Hero, unit);
            }
        }

        Vector3 RandPos = Vector3.zero;
        for (int i = 0; i < GameManager.Instance.EnemyCnt; ++i)
        {
            RandPos = new Vector3(Random.Range(-35f, 35f), 0f, Random.Range(-35f, 35f));
            GameObject enemy = GameHelper.UnitLoad(2001, UnitGroup, RandPos);

            Unit unit = enemy.GetComponent<Unit>();
            if (unit != null)
            {
                // 체력바 생성
                if (game != null)
                    game.CreateHpSlider(unit, unit._HpParent, enemy.name);

                GameManager.Instance.AddUnit(UnitType.Enemy, unit);
            }
        }

        for (int i = 0; i < GameManager.Instance.BossCnt; ++i)
        {
            RandPos = new Vector3(Random.Range(-35f, 35f), 0f, Random.Range(-35f, 35f));
            GameObject boss = GameHelper.UnitLoad(3001, UnitGroup, RandPos, (obj) => { }, 3);

            Unit unit = boss.GetComponent<Unit>();
            if (unit != null)
            {
                // 체력바 생성
                if (game != null)
                    game.CreateHpSlider(unit, unit._HpParent, boss.name);

                GameManager.Instance.AddUnit(UnitType.Boss, unit);
            }
        }

        GameManager.Instance.AllUnitDicAdd();
    }
}
