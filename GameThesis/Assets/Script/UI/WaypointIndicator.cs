using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaypointIndicator : MonoBehaviour
{
    public Image image;
    public Transform target;
    public TextMeshProUGUI distanceText;

    [Header("Waypoint Config")]
    public float visibleDistance = 10;
    public Vector3 offset;
    public bool isDistanceNeeded = true;
    public float safeAreaRadius = 2.5f; // Maximum distance for the circular safe area

    private void Update()
    {
        if (target != null)
        {
            CheckVisible();

            float minX = image.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;
            float minY = image.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

            if (Vector3.Dot((target.position - Camera.main.transform.position), Camera.main.transform.forward) < 0) // Check if the target is behind the player
            {
                if (pos.x < Screen.width / 2) // Target is on the left side
                {
                    pos.x = maxX;
                }
                else // Target is on the right side
                {
                    pos.x = minX;
                }
            }

            // Calculate the circular safe area
            Vector2 safeAreaCenter = new Vector2(Screen.safeArea.x + Screen.safeArea.width / 2, Screen.safeArea.y + Screen.safeArea.height / 2);
            float safeRadius = Mathf.Min(Screen.safeArea.width, Screen.safeArea.height) / safeAreaRadius;

            // Calculate the distance from the indicator to the center of the circular safe area
            float distanceToCenter = Vector2.Distance(pos, safeAreaCenter);

            if (distanceToCenter > safeRadius)
            {
                // If the indicator is outside the safe area, reposition it on the safe circle's edge
                float angle = Mathf.Atan2(pos.y - safeAreaCenter.y, pos.x - safeAreaCenter.x);
                pos.x = safeAreaCenter.x + safeRadius * Mathf.Cos(angle);
                pos.y = safeAreaCenter.y + safeRadius * Mathf.Sin(angle);
            }

            // Clamp the indicator within the screen boundaries
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            image.transform.position = pos;

            // Meter Text
            if (isDistanceNeeded)
            {
                distanceText.text = ((int)Vector3.Distance(target.position, Camera.main.transform.position)).ToString() + "m";
            }
        }
    }

    private void CheckVisible()
    {

        if (target.gameObject.GetComponent<Renderer>().isVisible && Vector3.Distance(Camera.main.transform.position, target.position) < visibleDistance)
        {
            image.enabled = false;
            distanceText.gameObject.SetActive(false);
        }
        else
        {
            image.enabled = true;
            distanceText.gameObject.SetActive(true);
        }


    }
}
