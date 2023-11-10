using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform t_doorMesh;
    Animator anim;

    public bool b_isOpen;

    public bool b_isForntDoor;

    public bool b_isLock;


    private void Awake()
    {
        if (b_isForntDoor) b_isLock = true;
        anim = t_doorMesh.GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetBool("isOpen", b_isOpen);
    }



    private void OnTriggerStay(Collider other)
    {
        if (!b_isLock)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 toOther = other.transform.position - transform.position;

                float dot = Vector3.Dot(forward, toOther);
                if (dot < 0)
                {
                    anim.SetBool("Behind", true);
                    anim.SetBool("Fornt", false);
                }
                else
                {
                    anim.SetBool("Behind", false);
                    anim.SetBool("Fornt", true);
                }
                b_isOpen = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        b_isOpen = false;
    }
}
