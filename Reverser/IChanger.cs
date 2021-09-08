namespace Reverser
{
    public interface IChanger
    {
        void ChangeBack(ContentChange change);
        void ChangeForward(ContentChange change);
    }
}