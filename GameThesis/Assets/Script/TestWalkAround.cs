using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWalkAround : MonoBehaviour
{
    public Transform pivot;

    public bool fight;

    public float waitDis;
    public float fightDis;

    void Update()
    {
        //Transform
        if (fight)
        {
            Vector3 fightPos = pivot.position - (transform.forward * fightDis);
            transform.position = Vector3.Lerp(transform.position, fightPos, 2 * Time.deltaTime);
        }
        else
        {
            Vector3 waitPos = pivot.position - (transform.forward * waitDis);
            Vector3 moveAround = waitPos + transform.right;
            transform.position = Vector3.Lerp(transform.position, moveAround, 2 * Time.deltaTime);

        }

        //Rotation
        Vector3 lookDir = pivot.position - transform.position;
        lookDir = lookDir.normalized;
        if (lookDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            Quaternion rot = Quaternion.Slerp(transform.rotation, targetRot, 10 * Time.deltaTime);
            rot.x = 0f;
            rot.z = 0f;
            transform.rotation = rot;
        }
    }
}
