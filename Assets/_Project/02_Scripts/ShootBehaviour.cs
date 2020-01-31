using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField]
    public float force = 100.0f;

    public Transform transform;
    Vector3 forceVector;
    // Start is called before the first frame update
    void Start(){
        //rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        float yDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");
        float rotDirection = yDirection;
        RotatePivot(rotDirection);
        ShootObject(zDirection);
    }
    void RotatePivot(float rotDirection){
        transform.rotation *= Quaternion.Euler(0.0f,rotDirection*1.5f,0.0f);        
    }
    void ShootObject(float zDirection){
        rb.AddForce(new Vector3(0.0f,0.0f,zDirection));
        rb.velocity *= 1.0f;
    }

}
