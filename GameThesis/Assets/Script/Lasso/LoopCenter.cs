using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LoopCenter : MonoBehaviour
{
    public GameObject target;
    public GameObject handPosition;
    public float lassoRadius;
    public float lassoRadiusMin = 0.5f;
    public float lassoRadiusMax = 1f;

    void Start()
    {
        
    }

    
    void Update()
    {
        //this.transform.rotation.SetLookRotation(handPosition.transform.position);
        //target.transform.position = (this.transform.position - handPosition.transform.position).normalized;

        Vector3 direction = (handPosition.transform.position - this.transform.position).normalized;
        target.transform.position = this.transform.position + direction * lassoRadius;
        

    }
}
