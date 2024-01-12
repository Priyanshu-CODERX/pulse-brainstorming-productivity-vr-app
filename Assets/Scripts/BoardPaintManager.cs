using System.Collections.Generic;
using UnityEngine;

public class BoardPaintManager : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private List<Vector3> pointsList;
    private bool isMousePressed;

    void Start()
    {
        // Initialize Line Renderer
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // Initialize points list
        pointsList = new List<Vector3>();

        // Set initial state
        isMousePressed = false;
    }

    void Update()
    {
        // Check for mouse button press
        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;
            lineRenderer.positionCount = 0;
            pointsList.Clear();
        }

        // Check for mouse button release
        if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }

        // Draw line while mouse button is pressed
        if (isMousePressed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // Add the current mouse position to the points list
            pointsList.Add(mousePos);

            // Update the Line Renderer positions
            lineRenderer.positionCount = pointsList.Count;
            lineRenderer.SetPositions(pointsList.ToArray());
        }
    }
}
