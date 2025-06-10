using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f;
    private float verticalVelocity;
    
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
    public Vector3 dampingVelocity;
    public Vector3 impact;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity +=  Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }
    
    public void Reset()
    {
        verticalVelocity = 0;
        impact = Vector3.zero;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
