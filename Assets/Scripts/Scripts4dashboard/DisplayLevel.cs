using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject objectiveGO;
    private LineRenderer lineRenderer;
    [SerializeField]
    private Material mat;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 50f;
        lineRenderer.endWidth = 50f;
        lineRenderer.positionCount = 2;
        Vector3 screenPos1 = gameObject.transform.position;
        Vector3 screenPos2 = objectiveGO.transform.position;
        Vector3[] positions = new Vector3[2] { screenPos1, screenPos2 };
        lineRenderer.SetPositions(positions);
        lineRenderer.material = mat;
    }

    
}
