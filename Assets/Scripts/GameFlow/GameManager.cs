using UnityEngine;

public enum CameraType
{
    Init,
    Game,
    Shop,
    Respawn
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field: SerializeField] public PlayerMotor IsMotor { get; set; }
    public GameObject[] m_cameras;

    GameState m_state;

    void Awake()
    {
        Instance = this;
        m_state = GetComponent<GameStateInit>();
        m_state.Enter();
    }

    void Update()
    {
        m_state.UpdateState();
    }

    public void ChangeState(GameState argState)
    {
        m_state.Exit();
        m_state = argState;
        m_state.Enter();
    }

    /// <summary>
    /// 시네머신 카메라 바꾸기
    /// </summary>
    /// <param name="argType">enum 타입</param>
    public void ChangeCamera(CameraType argType)
    {
        foreach (GameObject _obj in m_cameras)
        {
            _obj.SetActive(false);
        }

        m_cameras[(int)argType].SetActive(true);
    }
}
