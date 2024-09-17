using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;
using TMPro;

public class targetStatus : MonoBehaviour
{
    public TMP_Text label; 
    private AreaTargetBehaviour[] areaTargets;
    
    void Start()
    {
        areaTargets = FindObjectsOfType<AreaTargetBehaviour>();
    }

    void Update()
    {
        foreach (AreaTargetBehaviour target in areaTargets) {
            if (target.enabled &&
                    (target.TargetStatus.Status == Status.TRACKED ||
                     target.TargetStatus.Status == Status.EXTENDED_TRACKED)){
                
                label.text="Target: "+target.TargetName;

            } else {
                label.text="Target: Not Init...";
            }
        }
    }
}
