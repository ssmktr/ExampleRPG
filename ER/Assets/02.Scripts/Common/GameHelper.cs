using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelper : MonoBehaviour{

    public static GameObject UnitLoad(int id, Transform parent, Vector3 pos, System.Action<GameObject> callback = null, float size = 1)
    {
        // 유닛 정보가 있는지 체크 후 있으면 생성
        UnitInfo unitinfo = DataManager.Instance.GetUnitInfo(id);
        if (unitinfo != null)
        {
            // 기본이되는 최상위 오브젝트 생성
            GameObject UnitBase = (GameObject)Instantiate(Resources.Load("Unit/UnitBase"));
            UnitBase.transform.parent = parent;
            UnitBase.transform.localScale = Vector3.one * size;

            // 해당 유닛 생성
            string path = string.Format("Unit/{0}/{1}", unitinfo.playertype.ToString(), unitinfo.modelname);
            switch (unitinfo.playertype)
            {
                case PlayerState.Hero:
                    UnitBase.name = "HERO_" + id;
                    //UnitBase.AddComponent<HeroBase>().FirstInit(id);
                    break;

                case PlayerState.Enemy:
                    UnitBase.name = "ENEMY_" + id;
                    //UnitBase.AddComponent<EnemyBase>().FirstInit(id);
                    break;

                case PlayerState.Boss:
                    UnitBase.name = "BOSS_" + id;
                    //UnitBase.AddComponent<EnemyBase>().FirstInit(id);
                    break;
            }

            GameObject Unit = (GameObject)Instantiate(Resources.Load(path));
            Unit.transform.parent = UnitBase.transform;
            Unit.transform.localPosition = Vector3.zero;
            Unit.transform.localScale = Vector3.one;

            // 콜리더 입히기
            SphereCollider col = UnitBase.GetComponent<SphereCollider>();
            if (col == null)
                col = UnitBase.AddComponent<SphereCollider>();
            col.radius = unitinfo.collidersize;
            col.center = new Vector3(0, unitinfo.collidersize, 0);

            // 포지션 적용
            UnitBase.transform.localPosition = new Vector3(pos.x, pos.y + col.radius, pos.z);

            // 콜백
            if (callback != null)
                callback(UnitBase);

            return UnitBase;
        }

        return null;
    }

    // 개발 일때만 로그 남기기
    public static void DevDebugLog(object _msg, LOGSTATE _logState = LOGSTATE.NORMAL)
    {
        if (GameDefine.IsDevMode)
        {
            switch (_logState)
            {
                case LOGSTATE.WARRING:
                    Debug.LogWarning(_msg);
                    break;

                case LOGSTATE.ERROR:
                    Debug.LogError(_msg);
                    break;

                default:
                    Debug.Log(_msg);
                    break;
            }
        }
    }
}

