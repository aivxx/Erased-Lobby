using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class HandControl : MonoBehaviour
{

    public static void AlignHandToAttachment(Transform handroot, Transform handGrabAttach, Transform interactableAttach)
    {
        handroot.rotation = interactableAttach.rotation * Quaternion.Inverse(handGrabAttach.localRotation);
        handroot.position = interactableAttach.position + (handroot.position - handGrabAttach.position);
    }


    public bool hideHand = false;


}
