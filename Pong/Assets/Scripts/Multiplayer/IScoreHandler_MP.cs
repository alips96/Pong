namespace Pong.MP
{
    public interface IScoreHandler_MP
    {
        byte PlayerScored(int winScore);
        byte OpponentScored(int winScore);
    }
}