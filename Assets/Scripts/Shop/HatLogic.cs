using System.Collections.Generic;
using UnityEngine;

public class HatLogic : MonoBehaviour
{
    [SerializeField] Transform m_hatContainer;
    List<GameObject> m_hatModels = new List<GameObject>();
    Hat[] m_hats;

    void Awake()
    {
        m_hats = Resources.LoadAll<Hat>("Hat"); // 나중에 Addressable로 바꾸기
        SpawnHats();
        SelectHat(SaveManager.Instance.m_saveState.CurrentHatIndex);
    }

    void SpawnHats()
    {
        for (int i = 0; i < m_hats.Length; i++)
        {
            m_hatModels.Add(Instantiate(m_hats[i].m_model, m_hatContainer));
        }
    }

    public void DisableAllHats()
    {
        for (int i = 0; i < m_hatModels.Count; i++)
        {
            m_hatModels[i].SetActive(false);
        }
    }

    public void SelectHat(int argIndex)
    {
        DisableAllHats();
        m_hatModels[argIndex].SetActive(true);
    }
}
