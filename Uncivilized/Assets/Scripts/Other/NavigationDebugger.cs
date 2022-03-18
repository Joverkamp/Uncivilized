using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class NavigationDebugger : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agentToDebug;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (agentToDebug != null)
        {
            if (agentToDebug.hasPath)
            {
                lineRenderer.positionCount = agentToDebug.path.corners.Length;
                lineRenderer.SetPositions(agentToDebug.path.corners);
                lineRenderer.enabled = true;
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
