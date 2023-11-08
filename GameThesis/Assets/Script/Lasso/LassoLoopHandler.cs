using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoLoopHandler : MonoBehaviour
{
    public Transform centerTransform; // The transform to be the center of the circle
    public LassoHandler lassoHandler; // Reference to the LassoHandler script
    public int loopSegments = 10; // Number of segments for the loop
    public float minRadius = 0.1f; // Minimum radius of the loop
    public float maxRadius = 1.0f; // Maximum radius of the loop

    private LineRenderer lineRenderer;
    private float currentRadius;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        currentRadius = maxRadius; // Start with the maximum radius
        CreateLassoLoop();
    }

    void LateUpdate()
    {
        CreateLassoLoop();
    }

    void CreateLassoLoop()
    {
        if (centerTransform != null && lassoHandler != null)
        {
            Transform startPointObject = lassoHandler.endPointObject;
            Transform endPointObject = lassoHandler.startPointObject;

            if (startPointObject != null && endPointObject != null)
            {
                lineRenderer.positionCount = loopSegments + 1;
                Vector3 startPoint = startPointObject.position;
                Vector3 endPoint = endPointObject.position;
                Vector3 loopCenter = centerTransform.position;

                // Calculate the radius as the distance between the center of the circle and the end of the first line
                currentRadius = Vector3.Distance(endPoint, loopCenter);

                float angleIncrement = 360f / loopSegments;

                for (int i = 0; i <= loopSegments; i++)
                {
                    float angle = i * angleIncrement;
                    float x = loopCenter.x + currentRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
                    float z = loopCenter.z + currentRadius * Mathf.Sin(angle * Mathf.Deg2Rad);

                    lineRenderer.SetPosition(i, new Vector3(x, startPoint.y, z));
                }
            }
        }
    }

    // Method to update the circle's radius
    public void UpdateCircleRadius(float newRadius)
    {
        currentRadius = Mathf.Clamp(newRadius, minRadius, maxRadius);
    }
}