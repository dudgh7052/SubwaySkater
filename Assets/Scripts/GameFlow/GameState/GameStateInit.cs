using TMPro;
using UnityEngine;

public class GameStateInit : GameState
{
    public GameObject m_menuUI;
    [SerializeField] TextMeshProUGUI m_hiscoreText;
    [SerializeField] TextMeshProUGUI m_fishCountText;

    public override void Enter()
    {
        GameManager.Instance.ChangeCamera(CameraType.Init);

        m_hiscoreText.text = "Highscore: " + SaveManager.Instance.m_saveState.Highscore.ToString();
        m_fishCountText.text = "Fish: " + SaveManager.Instance.m_saveState.Fish.ToString();

        m_menuUI.SetActive(true);
    }

    public override void Exit()
    {
        m_menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        m_brain.ChangeState(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSession();
    }

    public void OnShopClick()
    {
        m_brain.ChangeState(GetComponent<GameStateShop>());
    }
}