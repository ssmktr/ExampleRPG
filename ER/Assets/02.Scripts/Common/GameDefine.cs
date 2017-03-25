
public class GameDefine {

    // 개발인지 라이브인제 
    public static bool IsDevMode = true;

    // 게임서능
    public const int GAME_FRAME = 30;
    public const float DEFAULT_TIMESCALE = 1f;

    public const float MOVE_DIST = 3f;
    public const float MOVE_CHANGE_DIST = 0.1f;
    public const float FOLLOW_DIST = 5f;
    public const float ATTACK_DIST = 1f;


}

public enum LOGSTATE
{
    NORMAL = 0,
    WARRING,
    ERROR,
}

public enum AniState
{
    Wait = 0,
    Walk,
    Damage,
    Dead,
    Attack,
}

public enum UnitType
{
    None = 0,
    Hero,
    Enemy,
    Boss,
}

public enum UnitState
{
    Idle,
    Stun,
    Move,
    Follow,
    Attack,
    Skill,
    MoveToSkill,
    Dying,
    Dead,
    Evasion,

    None,
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

public enum GAME_MODE
{
    None = 0,
    Game_Stage = 1,
}

public enum UIPANELTYPE
{
    NONE = 0,
    MAIN,
    SINGLEGAMEREADY,
    INGAMEHUD,
}