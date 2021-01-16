namespace RollingBall.Memento
{
    public interface ICaretakerPopable
    {
        void PopMementoStack();
        bool IsMementoStackEmpty();
    }
}