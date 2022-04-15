using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private float jumpForce = 5.0f;

    private CharacterController _characterController;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private bool _jumpPressed = false;
    private float _gravityValue = -9.81f;
   

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementJump();
    }

    private void MovementJump()
    {
        _groundedPlayer = _characterController.isGrounded;

        //if on ground, stop verical movement
        if(_groundedPlayer)
        {
            _playerVelocity.y = 0.0f;
        }

        
        //if jump pressed and on ground, jump
        if(_jumpPressed && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpForce * -1.0f * _gravityValue);
            _jumpPressed = false;
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {

      
    }
}
