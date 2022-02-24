using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeleportingSystem;

[RequireComponent(typeof(Teleporting))]
[RequireComponent(typeof(LineRenderer))]
public class TeleportCurve : MonoBehaviour
{
    LineRenderer lineRenderer;

    private Vector3[] curvePoints;
    private float playerElevation;
    [Space]
    [Range(0.1f, 0.9f)]
    [SerializeField] float forthRange = 0.5f;
    [Range(0.5f, 5.0f)]
    [SerializeField] float curveHeight = 1.0f;
    
    [SerializeField] Transform teleportObject = default;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (teleportObject == null) teleportObject = GameObject.FindGameObjectWithTag("Player").transform;
        if (teleportObject == null) teleportObject = GameObject.Find("Player").transform;

    }

    public void DrawCurve(Vector3 from, Vector3 to)
    {
        DiscreteCurve(from, to, lineRenderer.positionCount);
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, curvePoints[i]);
        }
    }


    private void DiscreteCurve(Vector3 startPos, Vector3 destPos,  int density)
    {
        Vector2 peak = new Vector2(forthRange, curveHeight);

        curvePoints = new Vector3[0];       // curve poinst array reset
        curvePoints = new Vector3[density]; // curve poinst array initialization
        curvePoints[0] = startPos;          // setting curve start point

        if (density > 2) //drawing curve only when line renderer points count
            //is greater than 2
        {

            // adjust curve height to players elevation (if not on the ground)
            playerElevation = teleportObject.transform.position.y;
            if (destPos.y != playerElevation)
                peak.y += destPos.y;
            if (destPos.y < playerElevation)
                peak.y += (playerElevation - destPos.y);
            // discretization vector to divide curve into equal parts
            Vector3 interVector = (destPos - startPos) / (density - 1);

            for (int i = 1; i < density - 1; i++)
            {
                float pointHeight = startPos.y;

                float progres = ((float)i / (float)density);

                // Curve is just connection of two sinus cuts
                // to generate curve we need to calculate it's height
                // in every discrete section step:
                if (progres < peak.x) //discretization before curve's peak
                    pointHeight += (peak.y - startPos.y/* + playerElevation*/) 
                                    * Mathf.Sin(progres / peak.x * Mathf.PI / 2);
                else // discretization after peak is reached
                    pointHeight = (peak.y/* + playerElevation*/ - destPos.y) 
                                    * Mathf.Sin(((progres - peak.x) / (1 - peak.x)) 
                                    * Mathf.PI / 2 + Mathf.PI / 2) 
                                    + destPos.y;

                // putting 'interVector' and 'pointHeight' to build curve point
                curvePoints[i] = new Vector3(
                    startPos.x + i * interVector.x,
                    pointHeight,
                    startPos.z + i * interVector.z);
            }
        }
        // last curve point is always teleport destination point
        curvePoints[density - 1] = destPos;
    }
}
