using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Swimmer : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float swimForce = 2f;
    [SerializeField] float dragForce = 1f;
    [SerializeField] float minForce = 1.1f;
    [SerializeField] float minTimeBetweenStrokes = 0f;

    [Header("References")]
    [SerializeField] InputActionReference leftControllerSwimReference;
    [SerializeField] InputActionReference leftControllerVelocity;
    [SerializeField] InputActionReference rightControllerSwimReference;
    [SerializeField] InputActionReference rightControllerVelocity;

    [SerializeField] Transform trackingDReference;

    private Rigidbody _rigidBody;
    private float _cooldownTimer;
    private bool _swimEnabled = false;


    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
       
        _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        if (!_swimEnabled)
        {
            return;
        }
        _cooldownTimer += Time.deltaTime;

        if (_cooldownTimer > minTimeBetweenStrokes && leftControllerSwimReference.action.IsPressed() && rightControllerSwimReference.action.IsPressed())
        {
            var leftHandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
            var rightHandVelocty = rightControllerVelocity.action.ReadValue<Vector3>();

            Vector3 localVelocity = -1 * (leftHandVelocity + rightHandVelocty);

            if (localVelocity.sqrMagnitude > minForce * minForce)
            {
                Vector3 worldVelocity = trackingDReference.TransformDirection(localVelocity);
                _rigidBody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                _cooldownTimer = 0f;
            }
        }

        if (_rigidBody.velocity.sqrMagnitude > 0.01f)
        {
            _rigidBody.AddForce(-_rigidBody.velocity * dragForce, ForceMode.Acceleration);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Water")
        {
            _swimEnabled = true;
            _rigidBody.useGravity = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Water")
        {
            _swimEnabled = false;
            _rigidBody.useGravity = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
           
        }
    }
}
