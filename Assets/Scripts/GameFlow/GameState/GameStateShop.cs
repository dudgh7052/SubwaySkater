using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GameStateShop : GameState
{
    public GameObject m_shopUI;
    public TextMeshProUGUI m_fishCountText;
    public TextMeshProUGUI m_currentHatName;
    public HatLogic m_hatLogic;
    bool IsInit = false;

    // Shop Item
    public GameObject m_hatPrefab;
    public Transform m_hatContainer;
    Hat[] m_hats;
    int m_unlockedHatCount = 0;

    // Completion Circle
    public Image m_completionCircle;
    public TextMeshProUGUI m_completionText;

    public override void Enter()
    {
        GameManager.Instance.ChangeCamera(CameraType.Shop);
        m_hats = Resources.LoadAll<Hat>("Hat"); // 나중에 Addressable로 바꾸기
        m_shopUI.SetActive(true);

        if (!IsInit)
        {
            m_fishCountText.text = SaveManager.Instance.m_saveState.Fish.ToString();
            m_currentHatName.text = m_hats[SaveManager.Instance.m_saveState.CurrentHatIndex].m_itemName;
            PopulateShop();
            IsInit = true;
        }

        ResetCompletionCircle();
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
            if (SaveManager.Instance.m_saveState.UnlockedHatFlag[i] == 0)
                _obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = m_hats[i].m_itemPrice.ToString();
            else
            {
                _obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                m_unlockedHatCount++;
            }          
        }
    }

    void OnHatClick(int argIndex)
    {
        if (SaveManager.Instance.m_saveState.UnlockedHatFlag[argIndex] == 1)
        {
            SaveManager.Instance.m_saveState.CurrentHatIndex = argIndex;
            m_currentHatName.text = m_hats[argIndex].m_itemName;
            m_hatLogic.SelectHat(argIndex);
            SaveManager.Instance.Save();
        }
        // 없는데 살 수 있는 경우
        else if (m_hats[argIndex].m_itemPrice <= SaveManager.Instance.m_saveState.Fish)
        {
            SaveManager.Instance.m_saveState.Fish -= m_hats[argIndex].m_itemPrice;
            SaveManager.Instance.m_saveState.UnlockedHatFlag[argIndex] = 1;
            SaveManager.Instance.m_saveState.CurrentHatIndex = argIndex;
            m_currentHatName.text = m_hats[argIndex].m_itemName;
            m_hatLogic.SelectHat(argIndex);
            m_fishCountText.text = SaveManager.Instance.m_saveState.Fish.ToString("");
            SaveManager.Instance.Save();
            m_hatContainer.GetChild(argIndex).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            m_unlockedHatCount++;
            ResetCompletionCircle();
        }
        else // 못 사는 경우
        {
            Debug.Log("Not enough fish");
        }
    }

    void ResetCompletionCircle()
    {
        int _hatCount = m_hats.Length - 1;
        int _curUnlockedCount = m_unlockedHatCount - 1;

        m_completionCircle.fillAmount = (float)_curUnlockedCount / (float)_hatCount;
        m_completionText.text = _curUnlockedCount + "/" + _hatCount;
    }

    public void OnHomeClick()
    {
        m_brain.ChangeState(GetComponent<GameStateInit>());
    }
}