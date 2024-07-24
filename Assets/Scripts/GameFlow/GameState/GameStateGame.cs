using TMPro;
using UnityEngine;
public class GameStateGame : GameState
{
    public GameObject m_gameUI;
    [SerializeField] TextMeshProUGUI m_fishCountText;
    [SerializeField] TextMeshProUGUI m_scoreText;

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.IsMotor.ResumePlayer();
        GameManager.Instance.ChangeCamera(CameraType.Game);

        m_fishCountText.text = "xTBD";
        m_scoreText.text = "TBD";

        m_gameUI.SetActive(true);
    }

    public override void UpdateState()
    {
        GameManager.Instance.IsWorldGeneration.ScanPosition();
        GameManager.Instance.IsSceneChunkGeneration.ScanPosition();
    }

    public override void Exit()
    {
        m_gameUI.SetActive(false);
    }
}
