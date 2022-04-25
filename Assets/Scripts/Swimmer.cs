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
    [SerializeField] float minForce;
    [SerializeField] float minTimeBetweenStrokes;

    [Header("References")]
    [SerializeField] InputActionReference leftControllerSwimReference;
    [SerializeField] InputActionReference leftControllerVelocity;
    [SerializeField] InputActionReference rightControllerSwimReference;
    [SerializeField] InputActionReference rightControllerVelocity;
    [SerializeField] Transform trackingReference;

    Rigidbody _rigidbody;
    float _cooldownTimer;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

    }

    private void FixedUpdate()
    {

            
    
       


    }

    private void OnCollisionStay(Collision collision)
    {
          if (collision.gameObject.tag == "Water")
        {
            Debug.Log("Collision Enter");
            _cooldownTimer += Time.fixedDeltaTime;
            _rigidbody.useGravity = false;
            //add velocity when both controllers pressed
            if (_cooldownTimer > minTimeBetweenStrokes
                && leftControllerSwimReference.action.IsPressed()
                && rightControllerSwimReference.action.IsPressed())
            {
                var leftHandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
                var rightHandVelocity = rightControllerVelocity.action.ReadValue<Vector3>();
                Vector3 localVelocity = leftHandVelocity + rightHandVelocity;
                localVelocity *= -1;

                //swim forward
                if (localVelocity.sqrMagnitude > minForce * minForce)
                {
                    Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
                    _rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                    _cooldownTimer = 0f;
                }

            }

            //apply water drag force when player is moving
            if (_rigidbody.velocity.sqrMagnitude > 0.01f)
            {
                _rigidbody.AddForce(-_rigidbody.velocity * dragForce, ForceMode.Acceleration);
            }
           
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Water")
        {
            _rigidbody.useGravity = true;

        }
    }


}