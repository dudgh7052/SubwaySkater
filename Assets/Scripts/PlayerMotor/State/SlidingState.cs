using UnityEngine;

public class SlidingState : BaseState
{
    [SerializeField] float m_sliderDuration = 1.0f;

    // Collider Logic
    Vector3 m_initialCenter;
    float m_initialSize = 0.0f;
    float m_slideStart = 0.0f;

    public override void Enter()
    {
        m_slideStart = Time.time;

        m_initialSize = m_motor.m_controller.height;
        m_initialCenter = m_motor.m_controller.center;

        m_motor.m_controller.center = m_initialCenter * 0.5f;
        m_motor.m_controller.height = m_initialSize * 0.5f;
    }

    public override void Tick()
    {
        if (InputManager.Instance.IsSwipeLeft) m_motor.ChangeLane(-1); // ���� �̵�
        if (InputManager.Instance.IsSwipeRight) m_motor.ChangeLane(1); // ������ �̵�

        if (!m_motor.m_isGrounded) m_motor.ChangeState(GetComponent<FallingState>());

        if (InputManager.Instance.IsSwipeUp) m_motor.ChangeState(GetComponent<JumpingState>());

        // ���� �ð� - �����̵� ���۽ð��� �����̵� ���ɽð����� Ŭ ���
        if (Time.time - m_slideStart > m_sliderDuration) m_motor.ChangeState(GetComponent<RunningState>());
    }

    public override void Exit()
    {
        m_motor.m_controller.center = m_initialCenter;
        m_motor.m_controller.height = m_initialSize;
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 _moveVector = Vector3.zero;

        _moveVector.x = m_motor.SnapToLane(); // �̵��ؾ� �� x ����
        _moveVector.y = -1.0f;
        _moveVector.z = m_motor.m_baseRunSpeed;

        return _moveVector;
    }
}
