public class GameStateInit : GameState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.IsTap)
        {
            m_brain.ChangeState(GetComponent<GameStateGame>());
        }
    }
}