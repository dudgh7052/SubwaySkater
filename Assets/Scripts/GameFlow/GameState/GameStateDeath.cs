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
        m_brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.IsMotor.RespawnPlayer();
    }

    public void ToMenu()
    {
        m_brain.ChangeState(GetComponent<GameStateInit>());
        GameManager.Instance.IsMotor.ResetPlayer();
        GameManager.Instance.IsWorldGeneration.ResetWorld();
        GameManager.Instance.IsSceneChunkGeneration.ResetWorld();
    }
}