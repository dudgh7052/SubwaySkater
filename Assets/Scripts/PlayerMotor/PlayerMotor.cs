using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 m_moveVector;
    [HideInInspector] public Vector3 m_verticalVelocity;
    [HideInInspector] public bool m_isGrounded;
    [HideInInspector] public int m_currentLane;

    public float m_distanceInBetweenLanes = 3.0f;
    public float m_baseRunSpeed = 5.0f;
    public float m_gravity = 14.0f;
    public float m_baseSidewaySpeed = 10.0f;
    public float m_terminalVelocity = 20.0f;

    public CharacterController m_controller;

    BaseState m_state;

    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_state = GetComponent<RunningState>();
        m_state.Enter();
    }

    void Update()
    {
        UpdateMotor();
    }

    void UpdateMotor()
    {
        // �׶��� üũ
        m_isGrounded = m_controller.isGrounded;

        // ���¿� ���� ������
        m_moveVector = m_state.ProcessMotion();

        // ���� ��ȯ üũ
        m_state.Tick();

        // �÷��̾� �̵�
        m_controller.Move(m_moveVector * Time.deltaTime);
    }

    public float SnapToLane()
    {
        float _r = 0.0f; // ���� �� ��

        // ���� ��ġ�� ���� ���ΰ� ������ üũ
        if (transform.position.x != (m_currentLane * m_distanceInBetweenLanes))
        {
            // ���ϴ� ��ġ������ �Ÿ� ��� (���� ��ġ - ���� ��ġ)
            float _deltaToDesiredPosition = (m_currentLane * m_distanceInBetweenLanes) - transform.position.x;

            _r = (_deltaToDesiredPosition > 0) ? 1 : -1; // �̵� ���� ���ϱ�
            _r *= m_baseSidewaySpeed; // �ӵ� ���ϱ�

            float _actualDistance = _r * Time.deltaTime; // ���� �̵��ؾ� �� �Ÿ� ���

            // �̵��ؾ� �� �Ÿ��� ��ǥ ��ġ���� Ŭ ���
            if (Mathf.Abs(_actualDistance) > Mathf.Abs(_deltaToDesiredPosition))
            {
                _r = _deltaToDesiredPosition * (1 / Time.deltaTime); // _r�� �ٿ��� ��ǥ ��ġ�� ����� �ʰ� ����
            }
        }
        else
        {
            _r = 0.0f;
        }

        return _r;
    }

    public void ChangeLane(int argDir)
    {
        m_currentLane = Mathf.Clamp(m_currentLane + argDir, -1, 1); // ���� ����
    }

    public void ChangeState(BaseState argState)
    {
        m_state.Exit();
        m_state = argState;
        m_state.Enter();
    }
}
