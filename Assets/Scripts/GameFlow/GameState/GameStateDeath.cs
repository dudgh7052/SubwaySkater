using UnityEngine;

public class GameStateDeath : GameState
{
    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.IsMotor.PausePlayer();
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.IsSwipeDown) ToMenu();
        if (InputManager.Instance.IsSwipeUp) ResumeGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.IsMotor.RespawnPlayer();
        m_brain.ChangeState(GetComponent<GameStateGame>());
    }

    public void ToMenu()
    {
        m_brain.ChangeState(GetComponent<GameStateInit>());
    }
}