using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TakeDamageData
{
    public Unit attacker;
    public float damageRatio;
    public float damage;
    public int AttackType = 0;
    public bool isSkillDamage;
    public float pushPower;
    public float through;
    public bool projecttile;

    public float TakeTime = 0;

    public TakeDamageData() { }
    public TakeDamageData(float takeTime, Unit attacker, float damageRatio, float damage, int AttackType, bool isSkillDamage, float pushPower = 0f, float through = 0f, bool projecttile = false)
    {
        this.attacker = attacker;
        this.damageRatio = damageRatio;
        this.damage = damage;
        this.AttackType = AttackType;
        this.isSkillDamage = isSkillDamage;
        this.pushPower = pushPower;
        this.through = through;
        this.projecttile = projecttile;

        this.TakeTime = takeTime;
    }
}

public class Unit : MonoBehaviour {

    public bool InitBase = false;

    public UnitType _UnitType = UnitType.None;
    public UnitState _UnitState = UnitState.None;
    public byte _TeamGroup = 0;
    public int Pc_uid = 0;
    public int AttackType = 0; // 1:근딜, 2:원딜, 3:마법
    public Transform _MyTransform = null, _HpParent = null;

    int CurCombo = 0;
    public bool IsAttack = false;

    // 네비 메시
    protected NavMeshAgent _NavMeshAgent = null;
    protected int _MovePathIndex = 0;

    protected UnitInfo _UnitInfo = null;

    // 체력 게이지
    public HpProgressBar _HpProgressBar = null;

    // 애니메이션 정보
    Animation _Animation = null;
    public Animation GetAnimation
    {
        get
        {
            if (_Animation != null)
            {

            }
            return null;
        }
    }

    // 스턴 상태인지 체크
    public bool IsStun { get { return _UnitState == UnitState.Stun; } }

    // 초기화
    public virtual void Init(UnitInfo unitinfo)
    {
        _UnitInfo = unitinfo;

        _Animation = GetComponentInChildren<Animation>();
        _NavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        _NavMeshAgent.speed = _UnitInfo.movespeed;

        _HpParent = transform.FindChild("HpParent");
        _HpParent.transform.localPosition = new Vector3(0, 1 / transform.localScale.y + _UnitInfo.collidersize, 0);
    }

    // 이동
    protected void SetMoveDest(Vector3 dest)
    {
        if (_NavMeshAgent != null)
            _NavMeshAgent.SetDestination(dest);
    }
}
