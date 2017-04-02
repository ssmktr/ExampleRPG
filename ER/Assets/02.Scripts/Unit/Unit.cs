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

    // 공격 상태 체크
    public bool IsAttack = false;
    // 스턴 상태 체크
    public bool IsStun { get { return _UnitState == UnitState.Stun; } }
    // 사망 상태 체크
    public bool IsDeath { get { return _UnitState == UnitState.Dead; } }

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

    // 초기화
    public virtual void Init(UnitInfo unitinfo)
    {
        _UnitInfo = unitinfo;

        _Animation = GetComponentInChildren<Animation>();
        _NavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        _NavMeshAgent.speed = _UnitInfo.movespeed;

        _HpParent = transform.FindChild("HpParent");
        _HpParent.transform.localPosition = new Vector3(0, 1 / transform.localScale.y + _UnitInfo.collidersize, 0);

        // 능력치
        if (_UnitInfo != null)
        {
            _MaxHp = _UnitInfo.hp; ;
            _CurHp = _MaxHp;

            _Atk = _UnitInfo.atk;
        }
    }

    // 이동
    protected void SetMoveDest(Vector3 dest)
    {
        if (_NavMeshAgent != null)
            _NavMeshAgent.SetDestination(dest);
    }

    // 애니메이션 재생
    protected void PlayAnimation(AniState eAni)
    {
        if (_Animation != null)
            _Animation.Play(eAni.ToString(), PlayMode.StopAll);
    }

    // 공격
    protected bool _IsAttack = false;
    protected virtual void Attack()
    {
        if (_IsAttack)
            return;

        _IsAttack = true;
        StartCoroutine(_Attack());
        PlayAnimation(AniState.Attack);
    }

    IEnumerator _Attack()
    {
        yield return new WaitForSeconds(1f);
        _IsAttack = false;
    }

    // 대미지 입음
    public virtual void TakeDamage(float damage)
    {
        _CurHp -= damage;
        SetHpProgressBar();
    }

    // 체력 게이지
    void SetHpProgressBar()
    {
        if (_CurHp < 0)
        {
            _CurHp = 0;
            _UnitState = UnitState.Dying;
            PlayAnimation(AniState.Dead);
            _HpProgressBar._slider.value = 0f;

            StartCoroutine(_Death());
            return;
        }

        if (_MaxHp > 0)
            _HpProgressBar._slider.value = (_CurHp / _MaxHp);
        else
            _HpProgressBar._slider.value = 0f;
    }

    IEnumerator _Death()
    {
        yield return new WaitForSeconds(1f);
        _UnitState = UnitState.Dead;
        gameObject.SetActive(_UnitState != UnitState.Dead);
        _HpProgressBar.gameObject.SetActive(_UnitState != UnitState.Dead);
    }

    #region STATUS
    float _CurHp = 0f;
    float _MaxHp = 0f;
    protected float CurHp { get { return _UnitInfo.hp; } }

    int _Atk = 0;
    protected int Atk { get { return _Atk; } }

    #endregion
}
