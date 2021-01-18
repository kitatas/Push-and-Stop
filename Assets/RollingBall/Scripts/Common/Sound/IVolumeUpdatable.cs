namespace RollingBall.Common.Sound
{
    public interface IVolumeUpdatable
    {
        float GetVolume();
        void SetVolume(float value);
    }
}