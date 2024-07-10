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
        // 그라운드 체크
        m_isGrounded = m_controller.isGrounded;

        // 상태에 따른 움직임
        m_moveVector = m_state.ProcessMotion();

        // 상태 전환 체크
        m_state.Tick();

        // 플레이어 이동
        m_controller.Move(m_moveVector * Time.deltaTime);
    }

    public float SnapToLane()
    {
        float _r = 0.0f; // 리턴 할 값

        // 현재 위치가 현재 레인과 같은지 체크
        if (transform.position.x != (m_currentLane * m_distanceInBetweenLanes))
        {
            // 원하는 위치까지의 거리 계산 (레인 위치 - 현재 위치)
            float _deltaToDesiredPosition = (m_currentLane * m_distanceInBetweenLanes) - transform.position.x;

            _r = (_deltaToDesiredPosition > 0) ? 1 : -1; // 이동 방향 정하기
            _r *= m_baseSidewaySpeed; // 속도 곱하기

            float _actualDistance = _r * Time.deltaTime; // 실제 이동해야 할 거리 계산

            // 이동해야 할 거리가 목표 위치보다 클 경우
            if (Mathf.Abs(_actualDistance) > Mathf.Abs(_deltaToDesiredPosition))
            {
                _r = _deltaToDesiredPosition * (1 / Time.deltaTime); // _r을 줄여서 목표 위치를 벗어나지 않게 조정
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
        m_currentLane = Mathf.Clamp(m_currentLane + argDir, -1, 1); // 범위 제한
    }

    public void ChangeState(BaseState argState)
    {
        m_state.Exit();
        m_state = argState;
        m_state.Enter();
    }
}
