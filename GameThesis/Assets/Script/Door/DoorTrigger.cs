using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private float closeDelay = 1f;
    private float delayCounter;
    private bool shouldOpen = false;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Debug.Log("Found Door");
            shouldOpen = true;
            if (!door.IsOpen)
            {
                Debug.Log("Open Door");
                door.OpenDoor(other.transform.position);
            }
        }
        else { shouldOpen = false; }
    }

    private void Update()
    {
        if (door.IsOpen && !shouldOpen)
        {
            Debug.Log("Close Door1111");
            delayCounter += Time.deltaTime;
            if (delayCounter > closeDelay)
            {
                Debug.Log("Close Door");
                door.CloseDoor();
                delayCounter = 0;
            }
        }
    }
}
