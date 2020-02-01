using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehaviour : MonoBehaviour
{
    LineRenderer lineRenderer;
    public GameObject player;
    public float timeToDistance;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    public void UpdateWithForce(Vector3 force){     
        Debug.Log(force);
        //y(t) = v*t - g/2 * t^2
        timeToDistance = 1.0f;
        //Vector3 flyingCurve = ProjectileMotion(force, 5.0f);
        for(timeToDistance = 10.0f; timeToDistance >= 0.0f ; timeToDistance *= timeToDistance/10){
            Vector3 flyingCurve = ProjectileMotion(force, timeToDistance);
            lineRenderer.SetPosition((int)timeToDistance,flyingCurve * timeToDistance);
        }
    }
    public Vector3 ProjectileMotion(Vector3 force, float timeToDistance){        
        Vector3 currentVelocity = force * timeToDistance + Physics.gravity * (0.5f * timeToDistance * timeToDistance);
        return currentVelocity;
    }
}
