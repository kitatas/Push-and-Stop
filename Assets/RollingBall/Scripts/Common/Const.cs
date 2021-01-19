namespace RollingBall.Common
{
    /// <summary>
    /// 定数管理
    /// </summary>
    public sealed class Const
    {
        public const float POP_ANIMATION_SPEED = 0.25f;

        public const float FADE_TIME = 0.7f;

        public const float UI_ANIMATION_TIME = 0.5f;
        public const float CORRECT_TIME = 0.3f;

        public static string GetKeyName(int stageIndex) => $"stage{stageIndex}";

        public const int MAX_STAGE_COUNT = 30;
    }
}