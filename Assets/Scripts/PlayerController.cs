using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private float jumpForce = 50.0f;

    private XROrigin _xrRig;
    private Rigidbody _playerRb;

    private bool IsGrounded => Physics.Raycast(new Vector2(transform.position.x, transform.position.y + 2.0f), Vector3.down, 2.0f);

    private void Start()
    {
        _xrRig = GetComponent<XROrigin>();
 
        _playerRb = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        var center = _xrRig.CameraInOriginSpacePos;
        jumpActionReference.action.performed += OnJump;
    }

    void OnJump(InputAction.CallbackContext obj)
    {
        if (!IsGrounded) return;
        _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

}
