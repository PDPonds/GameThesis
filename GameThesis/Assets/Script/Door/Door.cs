using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField] private bool IsRotationDoor = true;
    [SerializeField] private float speed = 1f;

    [SerializeField] private float rotationAmount = 90f;
    [SerializeField] private float ForwardDirection = 0f;

    private Vector3 startRotation;
    private Vector3 forward;

    private Coroutine animationCoroutine;

    private void Awake()
    {
        startRotation = transform.rotation.eulerAngles;
        forward = transform.right;
    }

    public void OpenDoor(Vector3 UserPosition)
    {
        if(animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        if(IsRotationDoor)
        {
            float dot = Vector3.Dot(forward, (UserPosition - transform.position).normalized);
            Debug.Log($"Dot: {dot.ToString("N3")}");
            animationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    public void CloseDoor()
    {
        if (IsOpen)
        {
            if(animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            if (IsRotationDoor)
            {
                animationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }

    private IEnumerator DoRotationOpen(float forwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (forwardAmount >= ForwardDirection)
        {
            Debug.Log("1");
            endRotation = Quaternion.Euler(new Vector3(-90, startRotation.y - rotationAmount, -90));
        }
        else
        {
            Debug.Log("2");
            endRotation = Quaternion.Euler(new Vector3(-90, startRotation.y + rotationAmount, -90));
        }

        Debug.Log("start rotation: " + startRotation);
        Debug.Log("end rotation: " + endRotation);

        IsOpen = true;
        float time = 0;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion _startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation);
        
        IsOpen = false;

        float time = 0;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(_startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}
