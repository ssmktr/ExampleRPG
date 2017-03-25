using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfoBase : MonoBehaviour {

    public Transform UnitGroup;
    public FollowCamera _FollowCamera;

    public virtual void Init()
    {
        _FollowCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>();
        UnitGroup = transform.FindChild("UnitGroup").transform;

        GameObject hero = null;
        if (GameManager.Instance.AllUnitDic.ContainsKey(UnitType.Hero))
            hero = GameHelper.UnitLoad(GameManager.Instance.AllUnitDic[UnitType.Hero][0].id, UnitGroup, Vector3.zero);
        if (hero != null && _FollowCamera != null)
            _FollowCamera.Target = hero.transform;

        Vector3 RandPos = Vector3.zero;
        if (GameManager.Instance.AllUnitDic.ContainsKey(UnitType.Enemy))
        {
            for (int i = 0; i < GameManager.Instance.AllUnitDic[UnitType.Enemy].Count; ++i)
            {
                RandPos = new Vector3(Random.Range(-35f, 35f), 0f, Random.Range(-35f, 35f));
                GameHelper.UnitLoad(GameManager.Instance.AllUnitDic[UnitType.Enemy][i].id, UnitGroup, RandPos);
            }
        }

        if (GameManager.Instance.AllUnitDic.ContainsKey(UnitType.Boss))
        {
            for (int i = 0; i < GameManager.Instance.AllUnitDic[UnitType.Boss].Count; ++i)
            {
                RandPos = new Vector3(Random.Range(-35f, 35f), 0f, Random.Range(-35f, 35f));
                GameHelper.UnitLoad(GameManager.Instance.AllUnitDic[UnitType.Boss][i].id, UnitGroup, RandPos, (obj) => { }, 3);
            }
        }
    }
}
