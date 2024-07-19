using UnityEngine;

public class GameStateInit : GameState
{
    public override void UpdateState()
    {
        if (InputManager.Instance.IsTap)
        {
            m_brain.ChangeState(GetComponent<GameStateGame>());
        }
    }
}