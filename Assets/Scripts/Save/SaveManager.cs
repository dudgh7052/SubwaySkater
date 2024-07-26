using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; } = null;

    // Fields
    public SaveState m_saveState;
    const string m_saveFileName = "data.ss";
    BinaryFormatter m_formatter;

    // Actions
    public Action<SaveState> OnLoad;
    public Action<SaveState> OnSave;

    void Awake()
    {
        Instance = this;

        m_formatter = new BinaryFormatter();

        // �� ���̺� ������ ��������
        Load();
    }

    void Load()
    {
        Debug.Log("Saved File Path: " + Application.persistentDataPath + m_saveFileName);

        try
        {
            FileStream _file = new FileStream(Application.persistentDataPath + m_saveFileName, FileMode.Open, FileAccess.Read); // ���� ��������
            m_saveState = (SaveState)m_formatter.Deserialize(_file); // BinaryFormatter�� Deserialize �ϱ�
            _file.Close();
            OnLoad?.Invoke(m_saveState);
        }
        catch
        {
            Debug.Log("Save file not found, let's create a new one!");
            Save();
        }
    }

    public void Save()
    {
        if (m_saveState == null) m_saveState = new SaveState();

        m_saveState.LastSaveTime = DateTime.Now;

        FileStream _file = new FileStream(Application.persistentDataPath + m_saveFileName, FileMode.OpenOrCreate, FileAccess.Write); // ���� ����
        m_formatter.Serialize(_file, m_saveState); // ����ȭ�ϱ�
        _file.Close();

        OnSave?.Invoke(m_saveState);
    }
}
