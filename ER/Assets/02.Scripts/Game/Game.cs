using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public Transform UnitGroup;
    public GameObject UI, HpProgressBar;
    public FollowCamera _FollowCamera;

    private void Awake()
    {
        DataManager.Instance.AllLoadTable();
    }

    private void Start()
    {
        GameObject hero = GameHelper.UnitLoad(1001, UnitGroup, Vector3.zero);
        if (hero != null)
            _FollowCamera.Target = hero.transform;

        Vector3 RandPos = Vector3.zero;
        //for (int i = 0; i < 10; ++i)
        //{
        //    RandPos = new Vector3(Random.Range(-35f, 35f), 0f, Random.Range(-35f, 35f));
        //    GameHelper.UnitLoad(2001, UnitGroup, RandPos);
        //}

        RandPos = new Vector3(Random.Range(-35f, 35f), 0f, Random.Range(-35f, 35f));
        GameHelper.UnitLoad(3001, UnitGroup, RandPos, (obj) => { }, 3);
    }
}

public enum AniState
{
    Wait = 0,
    Walk,
    Damage,
    Dead,
    Attack,
}

public enum PlayerState
{
    Hero = 0,
    Enemy,
    Boss,
}

public enum FSMKey
{
    None = 0,
    Move,
    Saw,
    Meet,
    NoHp,
}

public enum FSMValue
{
    None = 0,
    Move,
    Follow,
    Attack,
    Death,
}