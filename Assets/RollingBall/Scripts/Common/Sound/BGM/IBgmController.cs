namespace RollingBall.Common.Sound.BGM
{
    public interface IBgmController
    {
        void PlayBgm(BgmType bgmType, bool isLoop = true);
        void StopBgm();
    }
}