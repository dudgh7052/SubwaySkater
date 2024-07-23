using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] float m_verticalDistance = 25.0f;
    [SerializeField] float m_immunityTime = 1.0f;

    float m_startTime;

    public override void Enter()
    {
        m_startTime = Time.time;

        m_motor.m_controller.enabled = false;
        m_motor.transform.position = new Vector3(0.0f, m_verticalDistance, m_motor.transform.position.z);
        m_motor.m_controller.enabled = true;

        m_motor.m_verticalVelocity = 0.0f;
        m_motor.m_currentLane = 0;
        m_motor.m_animator?.SetTrigger("Respawn");
    }

    public override void Tick()
    {
        // �� ���� �� ���׻��·�
        if (m_motor.m_isGrounded && (Time.time - m_startTime) > m_immunityTime)
        {
            m_motor.ChangeState(GetComponent<RunningState>());
        }

        if (InputManager.Instance.IsSwipeLeft) m_motor.ChangeLane(-1); // ���� �̵�
        if (InputManager.Instance.IsSwipeRight) m_motor.ChangeLane(1); // ������ �̵�
    }

    public override void Exit()
    {
        GameManager.Instance.ChangeCamera(CameraType.Game);
    }

    public override Vector3 ProcessMotion()
    {
        // �߷� ����
        m_motor.ApplyGravity();

        // �ǵ��ƿ��� ���� ����
        Vector3 _moveVector = Vector3.zero;

        _moveVector.x = m_motor.SnapToLane(); // �̵��ؾ� �� x ����
        _moveVector.y = m_motor.m_verticalVelocity;
        _moveVector.z = m_motor.m_baseRunSpeed;

        return _moveVector;
    }
}