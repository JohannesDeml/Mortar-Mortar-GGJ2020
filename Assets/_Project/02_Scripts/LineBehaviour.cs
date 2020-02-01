using UnityEngine;

public class LineBehaviour : MonoBehaviour
{
    LineRenderer lineRenderer;
    public GameObject player;

    [SerializeField]
    private float totalSimulatedTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    public void UpdateWithForce(Vector3 force){
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            var time = ((float)i / lineRenderer.positionCount) * totalSimulatedTime;
            Vector3 flyingPosition = ProjectileMotion(force, time);
            lineRenderer.SetPosition(i,flyingPosition);
        }
    }
    public Vector3 ProjectileMotion(Vector3 force, float timeToDistance){        
        Vector3 currentVelocity = force * timeToDistance + Physics.gravity * (0.5f * timeToDistance * timeToDistance);
        return currentVelocity;
    }
}
