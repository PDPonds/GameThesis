using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoHandler : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float textureScale = 1.0f;

    [Header("Lasso Config")]
    public Transform startPointObject;
    public Transform endPointObject;
    public int loopSegments = 10; // Number of segments for the loop

    void Start()
    {
        DrawLasso();
        SetTextureTiling();
    }

    void Update()
    {
        DrawLasso();
        SetTextureTiling();
    }

    private void DrawLasso()
    {
        if (startPointObject != null && endPointObject != null)
        {
            // Set the start and end positions of the line based on the objects' positions
            lineRenderer.SetPosition(0, startPointObject.transform.position);
            lineRenderer.SetPosition(1, endPointObject.transform.position);
        }
    }

    void SetTextureTiling()
    {
        // Calculate the texture tiling based on the LineRenderer's length
        float lineLength = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
        float newTiling = lineLength * textureScale;

        // Update the material's tiling settings
        lineRenderer.material.mainTextureScale = new Vector2(newTiling, 1f);
    }
}