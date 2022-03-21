using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class Hands : MonoBehaviour
{
    [SerializeField] HandType handType = HandType.Left;

    public bool isHidden { get; private set; } = false;

    [SerializeField] InputAction trackedAction = null;
    [SerializeField] InputAction gripAction = null;
    [SerializeField] InputAction triggerAction = null;
   // [SerializeField] Animator handAnimator = null;
    // int m_gripAmountParameter = 0;
  //  int m_pointAmountParameter = 0;


    private bool m_isCurrentlyTracked = false;

    private List<SkinnedMeshRenderer> m_currentRenderers = new List<SkinnedMeshRenderer>();

    Collider[] m_colliders = null;

    public bool isCollisionEnabled { get; private set; } = false;

    [SerializeField] XRBaseInteractor interactor = null;


    private void Awake()
    {
        if (interactor == null)
        {
            interactor = GetComponentInParent<XRBaseInteractor>();
        }
    }

    [Obsolete]
    private void OnEnable()
    {
        interactor.onSelectEntered.AddListener(OnGrab);
        interactor.onSelectExited.AddListener(OnRelease);
    }

    [Obsolete]
    private void OnDisable()
    {
        interactor.onSelectEntered.RemoveListener(OnGrab);
        interactor.onSelectExited.RemoveListener(OnRelease);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_colliders = GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        trackedAction.Enable();
       // m_gripAmountParameter = Animator.StringToHash("GripAmount");
       // m_pointAmountParameter = Animator.StringToHash("PointAmount");
        gripAction.Enable();
        triggerAction.Enable();

        Hide();
    }

 /*  void UpdateAnimations()
    {
        float pointAmout = triggerAction.ReadValue<float>();
        handAnimator.SetFloat(m_pointAmountParameter, pointAmout);

        float gripAmount = gripAction.ReadValue<float>();
        handAnimator.SetFloat(m_gripAmountParameter, Mathf.Clamp01(gripAmount + pointAmout));


    } */

    // Update is called once per frame
    void Update()
    {
        float isTracked = trackedAction.ReadValue<float>();

        if (isTracked >= 1.0f && !m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = true;
            Show();
        }
        else if (isTracked == 0.0f && m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = false;
            Hide();
        }

        //UpdateAnimations();
    }

    public void Show()
    {
        foreach (SkinnedMeshRenderer renderer in m_currentRenderers)
        {
            renderer.enabled = true;
        }

        isHidden = false;
        EnableCollisions(true);
    }

    public void Hide()
    {
        m_currentRenderers.Clear();
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
            m_currentRenderers.Add(renderer);
        }

        isHidden = true;
        EnableCollisions(false);
    }

    public void EnableCollisions(bool enabled)
    {
        if (isCollisionEnabled == enabled)
        {
            return;
        }

        isCollisionEnabled = enabled;
        foreach (Collider collider in m_colliders)
        {
            collider.enabled = isCollisionEnabled;
        }
    }

    void OnGrab(XRBaseInteractable grabbedObject)
    {
        HandControl ctrl = grabbedObject.GetComponent<HandControl>();

        if (ctrl != null)
        {
            if (ctrl.hideHand == true)
            {
                Hide();
            }
        }
    }

    void OnRelease(XRBaseInteractable releasedObject)
    {
        HandControl ctrl = releasedObject.GetComponent<HandControl>();

        if (ctrl != null)
        {
            if (ctrl.hideHand == true)
            {
                Show();
            }
        }
    }

    [Obsolete]
    public void InteractionCleanup()
    {
        var directInteractor = interactor as XRDirectInteractor;

        if (directInteractor != null)
        {
            List<XRBaseInteractable> validTargets = new List<XRBaseInteractable>();

            directInteractor.GetValidTargets(validTargets);

            directInteractor.interactionManager.ClearInteractorSelection(directInteractor);
            directInteractor.interactionManager.ClearInteractorHover(directInteractor, validTargets);

            foreach (var target in validTargets)
            {
                if (target.gameObject.scene != interactor.gameObject.scene)
                {
                    target.transform.position = new Vector3(100, 100, 100);
                }
            }
        }
    }
}