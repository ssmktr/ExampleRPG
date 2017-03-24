using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TakeDamageData
{
    public UnitBase attacker;
    public float damageRatio;
    public float damage;
    public int AttackType = 0;
    public bool isSkillDamage;
    public float pushPower;
    public float through;
    public bool projecttile;

    public float TakeTime = 0;

    public TakeDamageData() { }
    public TakeDamageData(float takeTime, UnitBase attacker, float damageRatio, float damage, int AttackType, bool isSkillDamage, float pushPower = 0f, float through = 0f, bool projecttile = false)
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

public class UnitBase : MonoBehaviour {

    public bool InitBase = false;

    public UnitType _UnitType = UnitType.None;
    public UnitState _UnitState = UnitState.None;
    public byte _TeamGroup = 0;
    public int Pc_uid = 0;
    public int AttackType = 0; // 1:근딜, 2:원딜, 3:마법
    public Transform _MyTransform = null;

    int CurCombo = 0;
    public bool IsAttack = false;

    // 컨트롤러
    UnitController _Ower = null;

    // 네비 메시
    public NavMeshAgent _NavMeshAgent = null;
    protected NavMeshPath _MovePath = new NavMeshPath();
    protected int _MovePathIndex = 0;

    // 스턴 상태인지 체크
    public bool IsStun { get { return _UnitState == UnitState.Stun; } }

    // 애니메이션 정보
    Animation _Animation = null;
    public Animation GetAnimation
    {
        get {
            if (_Animation != null)
            {

            }
            return null;
        }
    }
    
    // 죽었는지 체크
    public bool IsDie
    {
        get
        {
            if (_UnitState == UnitState.Dying || _UnitState == UnitState.Dead)
                return true;

            return false;
        }
    }

    // 사용 가능한지 체크
    public bool IsUsable
    {
        get
        {
            if (gameObject == null || gameObject.activeSelf == false || _UnitState == UnitState.Dying || _UnitState == UnitState.Dead)
                return true;

            return true;
        }
    }

    // 네비 메시 사용 가능 여부
    public bool IsNavMeshAgent { get { return _NavMeshAgent != null && _NavMeshAgent.enabled == true; } }

    // 적 타겟
    UnitBase _ForcedTarget = null;
    public UnitBase ForcedTarget
    {
        get
        {
            if (_ForcedTarget == null || !_ForcedTarget.enabled)
                _ForcedTarget = null;

            return _ForcedTarget;
        }
        set { _ForcedTarget = value; }
    }

    public void Init(params object[] args)
    {
        _MyTransform = transform;

        Init_SyncData(args);
        Init_FSM();
        Init_Datas();
        Init_Controller();
        Init_Model();
        SetupComponents();
    }

    // 받아온 데이터 세팅
    protected virtual void Init_SyncData(params object[] args)
    {
        // 저장된 유닛중에 키값 으로 찾기
        if (_Ower == null)
        {
            int uid = (int)args[0];

        }

        _TeamGroup = (byte)args[1];
        _UnitType = (UnitType)args[2];
    }

    // 필요한 FSM 추가
    protected virtual void Init_FSM() { }

    // 현재 객체에 필요한 데이터 설정
    protected virtual void Init_Datas()
    {
    }

    // 스킬 및 버프 컨트롤 초기화
    protected virtual void Init_Controller()
    {
        if (_UnitType == UnitType.None)
            return;

        // 버프


        // 스킬
    }

    // 객체의 모델 초기화
    protected virtual void Init_Model()
    {
    }

    // 컴포너틑 링크 가져오기
    protected virtual void SetupComponents()
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // 컴포넌트 적용
    protected virtual void ComputeComponents()
    {
        SetAnimationSpeed();
    }

    // 애니메이션 스피드 설정
    public void SetAnimationSpeed()
    {

    }

    // 게임 모드 설정
    protected virtual void SetupForGameMode(GAME_MODE mode)
    {
    }

    // 로딩이 끝난 콜백
    public System.Action LoadingCompleteCallBack = null;
    protected virtual bool LoadingComplete()
    {
        ComputeComponents();
        SetupForGameMode(GAME_MODE.Game_Stage);

        if (LoadingCompleteCallBack != null)
            LoadingCompleteCallBack();

        return true;
    }

    // 이동
    public virtual bool MovePosition(Vector3 dest, float speedRatio = 1f, bool end = false)
    {
        if (_UnitState == UnitState.Skill)
            return false;

        bool success = CalculatePath(dest, end);
        if (success && _UnitState != UnitState.Skill)
        {
            bool changeState = ChangeState(UnitState.Move);
            //if (changeState)
            //    Move
        }
        return true;
    }

    public bool CalculatePath(Vector3 TargetPos, bool end = false)
    {
        if (!IsNavMeshAgent || float.IsNaN(TargetPos.x))
            return false;

        ClearPath();
        NavMeshHit navHit;
        // 8 => Terrain 레이어
        if (NavMesh.SamplePosition(TargetPos, out navHit, Vector3.Distance(TargetPos, transform.position), 8))
        {
            TargetPos = navHit.position;
        }

        bool find = _NavMeshAgent.CalculatePath(TargetPos, _MovePath);

        for (int i = 1; i < _MovePath.corners.Length - 1; ++i)
        {
            if (NavMesh.FindClosestEdge(_MovePath.corners[i], out navHit, 1))
            {
                if ((navHit.position - _MovePath.corners[i]).sqrMagnitude < 1f)
                {
                    _MovePath.corners[i] = _MovePath.corners[i] + navHit.normal * 1f;
                }
            }
        }

        if (_MovePath.corners.Length >= 2)
        {
            for (int i = 0; i < _MovePath.corners.Length - 1; ++i)
            {
                if ((_MovePath.corners[i] - _MovePath.corners[i + 1]).sqrMagnitude < 2f)
                {
                    _MovePath.corners[i + 1] = _MovePath.corners[i];
                }
            }
        }

        return find;
    }

    // 경로 삭제
    public void ClearPath()
    {

    }

    // 상태변경 함수
    public bool ChangeState(UnitState newState, bool Coercion = false)
    {
        // 상태가 같을때
        if (_UnitState == newState)
            return false;

        if (IsNavMeshAgent && _NavMeshAgent.hasPath)
            _NavMeshAgent.Stop();

        // 스턴, 상태거나, 상태 불가능한지 체크
        if (!Coercion && IsStun && !ChangeStateInStun(newState))
            return false;


        return true;
    }

    // 스턴 상태에서 변경 가능한지 체크
    bool ChangeStateInStun(UnitState state)
    {
        if (_UnitState != UnitState.Stun)
            return true;

        switch (state)
        {
            case UnitState.Idle:
            case UnitState.Dying:
            case UnitState.Dead:
                return true;
        }

        return false;
    }

    #region ATTACK

    public void AttackEvent(float damageRatio)
    {
        float atkAngle = 1f;
        DamageToTarget(damageRatio, atkAngle);
    }

    void DamageToTarget(float damageRatio, float angle)
    {
        if (angle <= 0f)
        {
            // 단일 공격
            UnitBase target = null;

        }
        else
        {
            // 범위 공격

            // 같은팀은 제외

        }
    }

    public List<TakeDamageData> TakeDamageQueues = new List<TakeDamageData>();
    public void AddTakeDamageData(float takeTime, UnitBase attacker, float damageRatio, float damage, int AttakType, bool isSkillDamage, float pushPower = 0f, float through = 0f, bool projecttile = false)
    {
        TakeDamageQueues.Add(new TakeDamageData(takeTime, attacker, damageRatio, damage, AttackType, isSkillDamage, pushPower, through, projecttile));
    }

    public System.Action<UnitBase, float> TakeDamageCallBack = null;
    protected bool NotDie = false;
    public bool NotDamMode = false;
    public virtual int TakeDamage(UnitBase attaker, float damageRatio, float damage, int AttackType, bool isSkillDamage, float pushPower = 0f, float through = 0f, bool projecttile = false, bool ignoreImmune = false)
    {
        damageRatio = 1;
        if (attaker == null)
            return 0;

        if (_NavMeshAgent == null || !_NavMeshAgent.enabled)
            return 0;

        if (!NotDie)
            return 0;

        // 헬퍼, 클론, PVP 라운드 체크중 대미지 무시
        float calcDamage = damage * damageRatio;

        return (int)calcDamage;
    }

    #endregion

    private void Update()
    {
        for (int i = 0; i < TakeDamageQueues.Count; ++i)
        {
            if (TakeDamageQueues[i] == null || TakeDamageQueues[i].attacker == null || TakeDamageQueues[i].attacker.IsDie || (Time.time - TakeDamageQueues[i].TakeTime) > 5)
            {
                TakeDamageQueues.RemoveAt(i);
                i--;
                continue;
            }

            if (TakeDamageQueues[i].TakeTime <= Time.time)
            {

                TakeDamageQueues.RemoveAt(i);
                i--;
                continue;
            }
        }
    }
}
