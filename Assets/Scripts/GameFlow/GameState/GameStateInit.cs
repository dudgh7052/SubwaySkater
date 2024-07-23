public class GameStateInit : GameState
{
    public override void Enter()
    {
        GameManager.Instance.ChangeCamera(CameraType.Init);
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.IsTap)
        {
            m_brain.ChangeState(GetComponent<GameStateGame>());
        }
    }
}