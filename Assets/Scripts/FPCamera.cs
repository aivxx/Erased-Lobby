using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPCamera : MonoBehaviour
{
    // Camera attached to player
    public Camera playerCamera;

    //Container variables for the mouse delta values each frame
    public float deltaX;
    public float deltaY;

    //Container variables for the player's rotation around the X and Y axis
    public float xRot; //rotation x in degrees
    public float yRot; //rotation y in degrees


  




    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main; // set player camera
        Cursor.visible = false; //hide cursor
    }

    // Update is called once per frame
    void Update()
    {

 if (PauseMenu.GamePaused) return;

        //Keep track of the player's x and y rotation
        yRot += deltaX;
        xRot -= deltaY;

        // Keep player's x rotation clamped
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        // rotate camera around x axis
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRot, 0);

       
    }

    // OnCameraLook event handler
    public void OnCameraLook(InputValue value)
    {
        // Reading mouse delta's as a vector 2 (delta x is x-component and delta y is y-component)
        Vector2 inputVector = value.Get<Vector2>();
        deltaX = inputVector.x;
        deltaY = inputVector.y;
    }

}
