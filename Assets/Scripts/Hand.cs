using System;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Animations.Rigging;

public enum HandType
{
    Left,
    Right
};

public class Hand : MonoBehaviour
{
    public HandType type = HandType.Left;
    public bool isHidden { get; private set; } = false;

    [SerializeField] InputAction trackedAction = null;
    [SerializeField] InputAction gripAction = null;
    [SerializeField] InputAction triggerAction = null;

    bool m_isCurrentlyTracked = false;

    List<MeshRenderer> m_currentRenderers = new List<MeshRenderer>();

    Collider[] m_colliders = null;

    public bool isCollisionEnabled { get; private set; } = false;

    public XRBaseInteractor interactor = null;

    public bool hideOnTrackingLoss = true;
    public float poseAnimationSpeed = 10.0f;//meters per second


    HandPose m_currentPose = null;
    Rig m_handPosingRig = null;
    float m_targetPoseWeight = 0.0f;
    Coroutine m_poseAnimationCoroutine = null;
    FingerPoser[] m_fingers = null;


    public void Awake()
    {

        if(interactor == null)
        {
            interactor = GetComponentInParent<XRBaseInteractor>();
        }

        if(m_handPosingRig == null)
        {
            m_handPosingRig = GetComponentInChildren<Rig>();
        }
    }

    [Obsolete]
    private void OnEnable()
    {
        interactor?.onSelectEntered.AddListener(OnGrab);
        interactor?.onSelectExited.AddListener(OnRelease);


    }

    [Obsolete]
    private void OnDisable()
    {
        interactor?.onSelectEntered.RemoveListener(OnGrab);
        interactor?.onSelectExited.RemoveListener(OnRelease);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_colliders = GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        trackedAction.Enable();
        gripAction.Enable();
        triggerAction.Enable();

        if (m_handPosingRig != null) m_handPosingRig.weight = 0.0f;
        m_fingers = GetComponentsInChildren<FingerPoser>();

        if(hideOnTrackingLoss) Hide();
    }

    // Update is called once per frame
    void Update()
    {
        float isTracked = trackedAction.ReadValue<float>();
        if (isTracked == 1.0f && !m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = true;
            Show();

        }else if
            (isTracked == 0 && m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = false;
            if(hideOnTrackingLoss) Hide();
        }
        
    }

    public void Show()
    {
        foreach (MeshRenderer renderer in m_currentRenderers)
        {
            renderer.enabled = true;
            
        }
        isHidden = false;
        EnableCollisions(true);
    }

    public void Hide()
    {
        m_currentRenderers.Clear();
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
            m_currentRenderers.Add(renderer);
        }
        isHidden = true;
        EnableCollisions(false);
    }

    public void EnableCollisions(bool enabled)
    {

        if (isCollisionEnabled == enabled) return;

        isCollisionEnabled = enabled;
        foreach(Collider collider in m_colliders)
        {
            collider.enabled = isCollisionEnabled;
        }
    }

    void OnGrab(XRBaseInteractable grabbedObject)
    {
       
        HandControl ctrl = grabbedObject.GetComponent<HandControl>();
        if(ctrl != null)
        {
            if(ctrl.hideHand)
            { 
                Hide();
            }
            else
            {
                HandPose pose = ctrl.GetHandPose(type);
                if(pose != null)
                {
                    m_currentPose = pose;
                    AnimateHandPoseWeightTo(1.0f);
                }
            }
        } 
        
    }

    void OnRelease(XRBaseInteractable releasedObject)
    {
       
        HandControl ctrl = releasedObject.GetComponent<HandControl>();
        if (ctrl != null)
        {
            if (ctrl.hideHand)
            {
                Show();
            }
            else if(m_currentPose !=null)
            {
                AnimateHandPoseWeightTo(0.0f);
                m_currentPose = null;
            }
        } 
        
    }

    IEnumerator AnimateHand()
    {
        while(!Mathf.Approximately(m_handPosingRig.weight, m_targetPoseWeight))
        {
            m_handPosingRig.weight = Mathf.MoveTowards(m_handPosingRig.weight, m_targetPoseWeight, poseAnimationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void AnimateHandPoseWeightTo(float targetWeight)
    {
        if (m_poseAnimationCoroutine != null)
        {
            StopCoroutine(m_poseAnimationCoroutine);
        }
        m_targetPoseWeight = targetWeight;
        m_poseAnimationCoroutine = StartCoroutine(AnimateHand());
    }

   void SyncTransform(Transform finger, Pose fingerPose)
    {

        Vector3 position = interactor.attachTransform.TransformPoint(fingerPose.position);
        Quaternion rotation = interactor.attachTransform.rotation * fingerPose.rotation;

        finger.SetPositionAndRotation(position, rotation);
    }

    void SyncRigToPose()
    {
        if (interactor == null || m_currentPose == null) return;
        if(interactor.attachTransform == null)
        {
            Debug.LogError("Interactor is missing an attach transform.");
        }

        foreach(var finger in m_fingers)
        {
            switch(finger.finger)
            {
                case FingerId.Thumb:
                    SyncTransform(finger.transform, m_currentPose.thumb);
                    break;
                    case FingerId.Index:
                    SyncTransform(finger.transform, m_currentPose.index);
                    break;
                    case FingerId.Middle:
                    SyncTransform(finger.transform, m_currentPose.middle);
                    break;
                    case FingerId.Ring:
                    SyncTransform(finger.transform, m_currentPose.ring);
                    break;
                    case FingerId.Pinky:
                    SyncTransform(finger.transform, m_currentPose.pinky);
                    break;

            }
        }
    }
}
