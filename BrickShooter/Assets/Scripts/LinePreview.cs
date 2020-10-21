using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePreview : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 dragStartPoint;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.SetPosition(0, Vector3.zero);
        // lineRenderer.SetPosition(1, Vector3.zero);
    }
    public void SetStartPosition(Vector3 worldPosition)
    {
        dragStartPoint = worldPosition;
        lineRenderer.SetPosition(0, worldPosition);
    }
    public void SetEndPosition(Vector3 worldPosition)
    {

        Vector3 offset = worldPosition - dragStartPoint;
        Vector3 endPosition = transform.position + 10 * offset;
        lineRenderer.SetPosition(1, endPosition);

        // offset = worldPosition - endPosition;
        // Vector3 temp = endPosition + 2 * offset;
        // lineRenderer.SetPosition(2, temp);

    }
    public void ClearLine()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }
}
