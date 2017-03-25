using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public bool IsGameStart = false;

    public Dictionary<UnitType, List<UnitInfo>> AllUnitDic = new Dictionary<UnitType, List<UnitInfo>>();
    List<UnitInfo> ListHero = new List<UnitInfo>();
    List<UnitInfo> ListEnemy = new List<UnitInfo>();
    List<UnitInfo> ListBoss = new List<UnitInfo>();
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
    public void AddUnit(UnitType unitType, UnitInfo unitInfo)
    {
        switch (unitType)
        {
            case UnitType.Hero:
                ListHero.Add(unitInfo);
                break;

            case UnitType.Enemy:
                ListEnemy.Add(unitInfo);
                break;

            case UnitType.Boss:
                ListBoss.Add(unitInfo);
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
