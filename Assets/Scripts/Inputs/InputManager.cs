using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    // Action schemes
    RunnerInputAction m_actionScheme;

    // Configuration
    [SerializeField] float m_sqrSwipeDeadzone = 50.0f;

    #region Properties
    public bool IsTap { get; private set; }
    public bool IsSwipeLeft { get; private set; }
    public bool IsSwipeRight { get; private set; }
    public bool IsSwipeUp { get; private set; }
    public bool IsSwipeDown { get; private set; } 
    public Vector2 IsTouchPostion { get; private set; }
    #endregion

    Vector2 m_startDrag;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SetupControl();
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void LateUpdate()
    {
        ResetInputs();
    }

    void ResetInputs()
    {
        IsTap = IsSwipeLeft = IsSwipeRight = IsSwipeUp = IsSwipeDown = false;
    }

    void SetupControl()
    {
        m_actionScheme = new RunnerInputAction();

        // action 등록
        m_actionScheme.Gameplay.Tap.performed += context => OnTap(context);
        m_actionScheme.Gameplay.TouchPosition.performed += context => OnPosition(context);
        m_actionScheme.Gameplay.StartDrag.performed += context => OnStartDrag(context);
        m_actionScheme.Gameplay.EndDrag.performed += context => OnEndDrag(context);
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        IsTap = true;
    }

    private void OnPosition(InputAction.CallbackContext context)
    {
        IsTouchPostion = context.ReadValue<Vector2>();
    }

    private void OnStartDrag(InputAction.CallbackContext context)
    {
        m_startDrag = IsTouchPostion;
    }

    private void OnEndDrag(InputAction.CallbackContext context)
    {
        Vector2 _delta = IsTouchPostion - m_startDrag;
        float _sqrDistance = _delta.sqrMagnitude;

        // 스와이프 시
        if (_sqrDistance > m_sqrSwipeDeadzone)
        {
            float _x = Mathf.Abs(_delta.x);
            float _y = Mathf.Abs(_delta.y);

            if (_x > _y) // 왼쪽, 오른쪽
            {
                if (_delta.x < 0) IsSwipeLeft = true;
                else IsSwipeRight = true;
            }
            else // 위, 아래
            {
                if (_delta.y < 0) IsSwipeDown = true;
                else IsSwipeUp = true;
            }
        }

        m_startDrag = Vector2.zero;
    }

    void OnEnable()
    {
        m_actionScheme.Enable();
    }

    void OnDisable()
    {
        m_actionScheme.Disable();
    }
}