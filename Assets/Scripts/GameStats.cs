using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get; private set; } = null;

    // Score
    public float m_score;
    public float m_highScore;
    public float m_distanceModifire = 1.5f;

    // Fish
    public int m_totalFish;
    public int m_fishCollectedThisSession;
    public float m_pointsPerFish = 10.0f;

    // Internal Cooldown 
    float m_lastScoreUpdate;
    float m_scoreUpdateDelta = 0.2f;

    // Action
    public Action<int> OnCollectFish; // UI같은것들 업데이트 할 액션
    public Action<float> OnScoreChange;

    void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        float _score = GameManager.Instance.IsMotor.transform.position.z * m_distanceModifire;
        _score += m_fishCollectedThisSession * m_pointsPerFish;

        if (_score > m_score)
        {
            m_score = _score;

            // Internal Cooldown
            if (Time.time - m_lastScoreUpdate > m_scoreUpdateDelta)
            {
                m_lastScoreUpdate = Time.time;
                OnScoreChange?.Invoke(m_score);
            }
        }
    }

    public void ResetSession()
    {
        m_score = 0;
        m_fishCollectedThisSession = 0;

        OnCollectFish?.Invoke(m_fishCollectedThisSession);
        OnScoreChange?.Invoke(m_score);
    }

    public void CollectFish()
    {
        m_fishCollectedThisSession++;
        OnCollectFish?.Invoke(m_fishCollectedThisSession); // 구독한 메서드들한테 생성 카운트 보내기
    }

    public string ScoreToText()
    {
        return m_score.ToString("000000");
    }

    public string FishToText()
    {
        return m_fishCollectedThisSession.ToString("000");
    }
}
