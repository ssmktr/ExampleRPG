using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public bool IsGameStart = false;

    public int HeroCnt = 0;
    public int EnemyCnt = 0;
    public int BossCnt = 0;

    public Dictionary<UnitType, List<Unit>> AllUnitDic = new Dictionary<UnitType, List<Unit>>();
    List<Unit> ListHero = new List<Unit>();
    List<Unit> ListEnemy = new List<Unit>();
    List<Unit> ListBoss = new List<Unit>();
    public bool IsAutoBattle = false;

    public void AllUnitClear(UnitType type = UnitType.None)
    {
        switch (type)
        {
            case UnitType.None:
                ListHero.Clear();
                ListEnemy.Clear();
                ListBoss.Clear();
                AllUnitDic.Clear();
                break;

            case UnitType.Hero:
                ListHero.Clear();
                if (AllUnitDic.ContainsKey(type))
                    AllUnitDic.Remove(type);
                break;
            case UnitType.Enemy:
                ListEnemy.Clear();
                if (AllUnitDic.ContainsKey(type))
                    AllUnitDic.Remove(type);
                break;
            case UnitType.Boss:
                ListBoss.Clear();
                if (AllUnitDic.ContainsKey(type))
                    AllUnitDic.Remove(type);
                break;
        }
    }

    // 유닛 추가
    public void AddUnit(UnitType unitType, Unit unit)
    {
        switch (unitType)
        {
            case UnitType.Hero:
                ListHero.Add(unit);
                break;

            case UnitType.Enemy:
                ListEnemy.Add(unit);
                break;

            case UnitType.Boss:
                ListBoss.Add(unit);
                break;
        }
    }

    // 모든 리스트 딕셔너리에 추가
    public void AllUnitDicAdd()
    {
        if (ListHero.Count > 0)
            AllUnitDic.Add(UnitType.Hero, ListHero);

        if (ListEnemy.Count > 0)
            AllUnitDic.Add(UnitType.Enemy, ListEnemy);

        if (ListBoss.Count > 0)
            AllUnitDic.Add(UnitType.Boss, ListBoss);
    }
}
