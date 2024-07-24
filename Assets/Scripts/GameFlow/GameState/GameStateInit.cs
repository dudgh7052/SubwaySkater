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

        m_hiscoreText.text = "Highscore : " + "TBD";
        m_fishCountText.text = "Fish : " + "TBD";

        m_menuUI.SetActive(true);
    }

    public override void Exit()
    {
        m_menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        m_brain.ChangeState(GetComponent<GameStateGame>());
    }

    public void OnShopClick()
    {
        //m_brain.ChangeState(GetComponent<GameStateShop>());
        Debug.Log("Shop Button");
    }
}