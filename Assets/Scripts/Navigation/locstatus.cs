using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;
using TMPro;

public class locstatus : MonoBehaviour
{
    public TMP_Text label; 
    private AreaTargetBehaviour areaTarget;
    private bool isLocalized = false;

    void Start()
    {
        areaTarget = FindObjectOfType<AreaTargetBehaviour>();
        if (areaTarget == null) return;
        else areaTarget.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    void Update()
    {
        
    }
    
    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        isLocalized = (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED);
        if (isLocalized){
            label.text = "Localized";
            label.color = new Color(0.25F,0.76F,0.22F,1F);
        }
        else {
            label.text = "Calibrating...";
            label.color = new Color(0.94F,0.48F,0.0F,1F);
        }
    }
}