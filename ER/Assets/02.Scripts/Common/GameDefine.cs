
public class GameDefine {

    // 개발인지 라이브인제 
    public static bool IsDevMode = true;

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