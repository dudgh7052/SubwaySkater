using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameStateDeath : GameState, IUnityAdsShowListener
{
    public GameObject m_deathUI;
    [SerializeField] TextMeshProUGUI m_highscoreText;
    [SerializeField] TextMeshProUGUI m_curScoreText;
    [SerializeField] TextMeshProUGUI m_totalFishText;
    [SerializeField] TextMeshProUGUI m_curFishText;

    // Completion circle fields
    [SerializeField] Image m_completeionCircle;
    public float m_timetoDecision = 2.5f;
    float m_deathTime;

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.IsMotor.PausePlayer();

        m_deathTime = Time.time;
        m_deathUI.SetActive(true);

        // 필요하면 최고 점수 저장
        if (SaveManager.Instance.m_saveState.Highscore < (int)GameStats.Instance.m_score)
        {
            SaveManager.Instance.m_saveState.Highscore = (int)GameStats.Instance.m_score;
            m_curScoreText.color = Color.green;
        }
        else m_curScoreText.color = Color.white;

        SaveManager.Instance.m_saveState.Fish += GameStats.Instance.m_fishCollectedThisSession;

        SaveManager.Instance.Save();

        m_highscoreText.text = "Highscore : " + SaveManager.Instance.m_saveState.Highscore;
        m_curScoreText.text = GameStats.Instance.ScoreToText();
        m_totalFishText.text = "Total : " + SaveManager.Instance.m_saveState.Fish;
        m_curFishText.text = GameStats.Instance.FishToText();
    }

    public override void UpdateState()
    {
        float _ratio = (Time.time - m_deathTime) / m_timetoDecision;
        m_completeionCircle.color = Color.Lerp(Color.green, Color.red, _ratio);
        m_completeionCircle.fillAmount = 1 - _ratio;

        if (_ratio > 1)
        {
            m_completeionCircle.gameObject.SetActive(false);
        }
    }

    public override void Exit()
    {
        m_deathUI.SetActive(false);
    }

    public void TryResumeGame()
    {
        AdManager.Instance.ShowRewardedAd(this);
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

    public void EnableRevive()
    {
        m_completeionCircle.gameObject.SetActive(true);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Unity Ads show failed. {message}");
        ToMenu();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Unity Ads show started.");
    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Unity Ads show completed. {showCompletionState}");
        m_completeionCircle.gameObject.SetActive(false);
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                ResumeGame();
                break;
            default:
                break;
        }
    }
}