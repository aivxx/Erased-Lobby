using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


[ExecuteInEditMode]
public class HandControl : MonoBehaviour
{


    ActionBasedController controller;
    public Hand hand;

    
    public void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    public void Update()
    {
        hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }

    public static void AlignHandToAttachment(Transform handroot, Transform handGrabAttach, Transform interactableAttach)
    {
        handroot.rotation = interactableAttach.rotation * Quaternion.Inverse(handGrabAttach.localRotation);
        handroot.position = interactableAttach.position + (handroot.position - handGrabAttach.position);
    }


    public bool hideHand = false;
    public bool disableCollisions = false;
    public HandPose leftHandPose = null;
    public HandPose rightHandPose = null;
    public Transform fixedAttachment = null;

    public HandPose GetHandPose(HandType type)
    {
        switch(type)
        {
            case HandType.Left: return leftHandPose;
            case HandType.Right: return rightHandPose;
            default: return null;
        }
    }


}
