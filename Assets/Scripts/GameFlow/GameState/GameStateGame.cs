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

        GameStats.Instance.OnCollectFish += OnCollectFish;
        GameStats.Instance.OnScoreChange += OnScoreChange;

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

        GameStats.Instance.OnCollectFish -= OnCollectFish;
        GameStats.Instance.OnScoreChange -= OnScoreChange;
    }

    void OnScoreChange(float argScore)
    {
        m_scoreText.text = GameStats.Instance.ScoreToText();
    }

    void OnCollectFish(int argFishCount)
    {
        m_fishCountText.text = GameStats.Instance.FishToText();
    }
}
