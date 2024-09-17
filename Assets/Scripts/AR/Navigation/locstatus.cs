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
        switch (status.Status){
            case Status.EXTENDED_TRACKED:
                label.text = "Localized";
                label.color = new Color(0.25F,0.76F,0.22F,1F);
                break;
            case Status.LIMITED:
                label.text = "Localized(Limited)";
                label.color = new Color(0.94F,0.48F,0.0F,1F);
                break;
            default:
                label.text = "Please look around";
                label.color = new Color(0.94F,0.48F,0.0F,1F);
                break;
        }
    }
}
