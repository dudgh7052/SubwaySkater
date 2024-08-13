using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateShop : GameState
{
    public GameObject m_shopUI;
    public TextMeshProUGUI m_fishCountText;
    public TextMeshProUGUI m_currentHatName;
    public HatLogic m_hatLogic;

    // Shop Item
    public GameObject m_hatPrefab;
    public Transform m_hatContainer;
    Hat[] m_hats;

    void Start()
    {
        m_hats = Resources.LoadAll<Hat>("Hat"); // 나중에 Addressable로 바꾸기
        PopulateShop();
    }

    public override void Enter()
    {
        GameManager.Instance.ChangeCamera(CameraType.Shop);

        m_fishCountText.text = SaveManager.Instance.m_saveState.Fish.ToString("000");

        m_shopUI.SetActive(true);
    }

    public override void Exit()
    {
        m_shopUI.SetActive(false);
    }

    void PopulateShop()
    {
        for (int i = 0; i < m_hats.Length; i++)
        {
            int _index = i;
            GameObject _obj = Instantiate(m_hatPrefab, m_hatContainer);
            // Button
            _obj.GetComponent<Button>().onClick.AddListener(() => OnHatClick(_index));
            // Thumnail
            _obj.transform.GetChild(0).GetComponent<Image>().sprite = m_hats[i].m_thumbnail;
            // ItemName
            _obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_hats[i].m_itemName;
            // Price
            _obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = m_hats[i].m_itemPrice.ToString();
        }
    }

    void OnHatClick(int argIndex)
    {
        m_currentHatName.text = m_hats[argIndex].m_itemName;
        m_hatLogic.SelectHat(argIndex);
    }

    public void OnHomeClick()
    {
        m_brain.ChangeState(GetComponent<GameStateInit>());
    }
}