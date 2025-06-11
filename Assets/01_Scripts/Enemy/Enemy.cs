using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Health health { get; private set; }

    bool isCollision = false;

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
    }

    private void Start()
    {
        health.OnDie += OnDie;
    }

    void Update()
    {
        if (!isCollision)
        {
            transform.position += transform.forward * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollision = true;
        }
    }

    void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}