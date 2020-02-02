using System;
using UnityEngine;

public class LineBehaviour : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer = null;

    [SerializeField]
    private float totalSimulatedTime = 1.0f;

    [SerializeField, Min(1)]
    private int maxPoints = 15;

    [SerializeField]
    private LayerMask layerMask = default;

    [SerializeField]
    private Transform impactTransform = null;
    
    private Ray ray;

    private RaycastHit[] hits;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ray = new Ray();
        hits = new RaycastHit[1];
        impactTransform.gameObject.SetActive(false);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        impactTransform.gameObject.SetActive(active);
    }
    
    public void UpdateWithForce(Vector3 force)
    {
        lineRenderer.positionCount = maxPoints;

        var lastLocalPosition = Vector3.zero;
        lineRenderer.SetPosition(0,lastLocalPosition);
        for (int i = 1; i < maxPoints; i++)
        {
            var time = ((float)i / maxPoints) * totalSimulatedTime;
            Vector3 localPosition = ProjectileMotion(force, time);
            
            var lastNextVector = transform.rotation * (localPosition - lastLocalPosition);
            ray.origin = transform.rotation * lastLocalPosition + transform.position;
            ray.direction = lastNextVector.normalized;
            var length = lastNextVector.magnitude;
            
            // Debug.DrawRay(ray.origin, (ray.direction * length), Color.green, 0.1f);
            var hitCount = Physics.RaycastNonAlloc(ray, hits, length, layerMask, QueryTriggerInteraction.Ignore);
            if (hitCount > 0)
            {
                var hit = hits[0];
                var localHitPosition = Quaternion.Inverse(transform.rotation) * (hit.point - transform.position);
                lineRenderer.SetPosition(i,localHitPosition);
                lineRenderer.positionCount = i + 1;
                PositionCollisionGizmo(hit);
                return;
            }
            lineRenderer.SetPosition(i,localPosition);
            lastLocalPosition = localPosition;
        }
        
        impactTransform.gameObject.SetActive(false);
    }

    private void PositionCollisionGizmo(RaycastHit hit)
    {
        impactTransform.gameObject.SetActive(true);
        impactTransform.position = hit.point;
    }

    public Vector3 ProjectileMotion(Vector3 force, float timeToDistance){        
        Vector3 position = force * timeToDistance + Physics.gravity * (0.5f * timeToDistance * timeToDistance);
        return position;
    }
    
    #if UNITY_EDITOR

    private void Reset()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    #endif
}
