public class GameStateGame : GameState
{
    public override void Enter()
    {
        GameManager.Instance.IsMotor.ResumePlayer();
    }
}
