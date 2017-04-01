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

        GameObject hero = null;
        if (GameManager.Instance.AllUnitDic.ContainsKey(UnitType.Hero))
            hero = GameHelper.UnitLoad(GameManager.Instance.AllUnitDic[UnitType.Hero][0].id, UnitGroup, Vector3.zero);
        if (hero != null && _FollowCamera != null)
            _FollowCamera.Target = hero.transform;

        // 체력바 생성
        if (game != null)
            game.CreateHpSlider(hero.GetComponent<Unit>(), hero.GetComponent<Unit>()._HpParent, hero.name);

        Vector3 RandPos = Vector3.zero;
        if (GameManager.Instance.AllUnitDic.ContainsKey(UnitType.Enemy))
        {
            for (int i = 0; i < GameManager.Instance.AllUnitDic[UnitType.Enemy].Count; ++i)
            {
                RandPos = new Vector3(Random.Range(-35f, 35f), 0f, Random.Range(-35f, 35f));
                GameObject enemy = GameHelper.UnitLoad(GameManager.Instance.AllUnitDic[UnitType.Enemy][i].id, UnitGroup, RandPos);

                // 체력바 생성
                if (game != null)
                {
                    game.CreateHpSlider(enemy.GetComponent<Unit>(), enemy.GetComponent<Unit>()._HpParent, enemy.name);
                }
            }
        }

        if (GameManager.Instance.AllUnitDic.ContainsKey(UnitType.Boss))
        {
            for (int i = 0; i < GameManager.Instance.AllUnitDic[UnitType.Boss].Count; ++i)
            {
                RandPos = new Vector3(Random.Range(-35f, 35f), 0f, Random.Range(-35f, 35f));
                GameObject boss = GameHelper.UnitLoad(GameManager.Instance.AllUnitDic[UnitType.Boss][i].id, UnitGroup, RandPos, (obj) => { }, 3);

                // 체력바 생성
                if (game != null)
                    game.CreateHpSlider(boss.GetComponent<Unit>(), boss.GetComponent<Unit>()._HpParent, boss.name);
            }
        }
    }
}
