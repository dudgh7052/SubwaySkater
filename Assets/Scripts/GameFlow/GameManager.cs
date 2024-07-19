using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field: SerializeField] public PlayerMotor IsMotor { get; set; }

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
}
