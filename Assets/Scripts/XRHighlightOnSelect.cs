using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHighlightOnSelect : MonoBehaviour
{

    public Material hoverMaterial = null;

    XRBaseInteractable m_interactable = null;
    Renderer m_renderer = null;
    Material[] m_currentMaterials = null;


    void Awake()
    {
        m_renderer = GetComponent<Renderer>();
        m_interactable = GetComponent<XRBaseInteractable>();
    }

    [System.Obsolete]
    private void OnEnable()
    {
        m_interactable.onHoverEntered.AddListener(SwapInMaterial);
        m_interactable.onHoverExited.AddListener(SwapOutMaterial);
    }

    [System.Obsolete]
    private void OnDisable()
    {
        m_interactable.onHoverEntered.RemoveListener(SwapInMaterial);
        m_interactable.onHoverExited.RemoveListener(SwapOutMaterial);
    }

    void SwapInMaterial(XRBaseInteractor interactor)
    {
        m_currentMaterials = m_renderer.materials;
        Material[] hoverMats = new Material[m_currentMaterials.Length];
        for (int i = 0; i < m_currentMaterials.Length; ++i)
        {
            hoverMats[i] = hoverMaterial;
        }
        m_renderer.materials = hoverMats;
    }

    void SwapOutMaterial(XRBaseInteractor interactor)
    {
        m_renderer.materials = m_currentMaterials;
        m_currentMaterials = null;
    }


}
