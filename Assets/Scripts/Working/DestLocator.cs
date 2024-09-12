using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestLocator : MonoBehaviour
{
    public GameObject destination;
    public GameObject body;

    void Start()
    {
        body.transform.position = destination.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
