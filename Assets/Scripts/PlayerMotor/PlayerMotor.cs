using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 m_moveVector;
    [HideInInspector] public float m_verticalVelocity;
    [HideInInspector] public bool m_isGrounded;
    [HideInInspector] public int m_currentLane;

    public float m_distanceInBetweenLanes = 3.0f;
    public float m_baseRunSpeed = 5.0f;
    public float m_gravity = 14.0f;
    public float m_baseSidewaySpeed = 10.0f;
    public float m_terminalVelocity = 20.0f; // �ִ� �߷�

    public CharacterController m_controller;
    public Animator m_animator;

    BaseState m_state;
    bool m_isPause = false;

    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();

        m_state = GetComponent<RunningState>();
        m_state.Enter();

        m_isPause = true;
    }

    void Update()
    {
        if (m_isPause) return;

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

        // �ִϸ����� �� ����
        m_animator?.SetBool("IsGrounded", m_isGrounded);
        m_animator?.SetFloat("Speed", Mathf.Abs(m_moveVector.z));
        

        // �÷��̾� �̵�
        m_controller.Move(m_moveVector * Time.deltaTime);
    }

    /// <summary>
    /// �÷��̾� ĳ���� x ����
    /// </summary>
    /// <returns>�̵� ��</returns>
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

    /// <summary>
    /// ���� �̵�
    /// </summary>
    /// <param name="argDir">�̵� �� ����</param>
    public void ChangeLane(int argDir)
    {
        m_currentLane = Mathf.Clamp(m_currentLane + argDir, -1, 1); // ���� ����
    }

    public void ApplyGravity()
    {
        m_verticalVelocity -= m_gravity * Time.deltaTime;
        if (m_verticalVelocity < -m_terminalVelocity) m_verticalVelocity = -m_terminalVelocity;
    }

    public void ChangeState(BaseState argState)
    {
        m_state.Exit();
        m_state = argState;
        m_state.Enter();
    }

    public void PausePlayer()
    {
        m_isPause = true;
    }

    public void ResumePlayer()
    {
        m_isPause = false;
    }

    public void ResetPlayer()
    {
        m_currentLane = 0;
        transform.position = Vector3.zero;
        m_animator?.SetTrigger("Idle");
        ChangeState(GameManager.Instance.IsMotor.GetComponent<RunningState>());
        PausePlayer();
    }

    public void RespawnPlayer()
    {
        ChangeState(GetComponent<RespawnState>());
        GameManager.Instance.ChangeCamera(CameraType.Respawn);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string _hitLayerName = LayerMask.LayerToName(hit.gameObject.layer);

        if (_hitLayerName == "Death") ChangeState(GetComponent<DeathState>());
    }

    
}
