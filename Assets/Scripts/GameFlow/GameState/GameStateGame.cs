public class GameStateGame : GameState
{
    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.IsMotor.ResumePlayer();
    }
}
