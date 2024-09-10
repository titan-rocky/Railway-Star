using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;
using TMPro;

public class targetStatus : MonoBehaviour
{
    public TMP_Text label; 
    private AreaTargetBehaviour areaTarget;
    
    void Start()
    {
        areaTarget = FindObjectOfType<AreaTargetBehaviour>();
        if (areaTarget == null) return;
        else label.text="Target: "+areaTarget.TargetName;
        
    }

    void Update()
    {
        
    }
}
