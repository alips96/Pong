using Pong.General;

public class ScoreHandler_SP : IScoreHandler_SP
{
    private int score;
    private EventMaster eventMaster;

    public ScoreHandler_SP(EventMaster _eventMaster)
    {
        eventMaster = _eventMaster;
    }

    public int AddPoint()
    {
        return ++score;
    }

    public void PerformGameOver()
    {
        eventMaster.CallEventGameOver(score);
        score = 0;
    }
}
