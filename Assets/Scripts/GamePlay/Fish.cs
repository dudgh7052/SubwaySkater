using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Animator m_animator;

    void Start()
    {
        m_animator = GetComponentInParent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) PickupFish();
    }

    void PickupFish()
    {
        m_animator?.SetTrigger("Pickup");
        GameStats.Instance.CollectFish();
        // increment the fish count
        // increment the score
        // play sfx
        // trigger a animation
    }

    public void OnShowChunk()
    {
        m_animator?.SetTrigger("Idle");
    }
}