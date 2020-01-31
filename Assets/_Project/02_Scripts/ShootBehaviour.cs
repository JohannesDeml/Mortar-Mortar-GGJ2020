using System.Collections;
using System.Collections.Generic;
using Supyrb;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab = null;

    [SerializeField]
    private KeyCode fire = KeyCode.Space;
    
    [SerializeField]
    public float force = 100.0f;

    [SerializeField]
    private Transform projectileDirection = null;
    
    [SerializeField]
    private AnimationCurve shootAngleOverTime = AnimationCurve.EaseInOut(0f, 0f, 90f, 1f);
    
    public Transform pivotTransform;
    
    private Vector3 forceVector;
    private float shootAngle;
    private bool loadingShot;
    private float loadingShotStartTime;

    // Update is called once per frame
    void Update(){
        float yDirection = Input.GetAxis("Horizontal");
        RotatePivot(yDirection);

        UpdateShootInput();
        UpdateProjectileDirection();
    }

    private void UpdateProjectileDirection()
    {
        projectileDirection.localRotation = Quaternion.AngleAxis(shootAngle, Vector3.right);
    }

    private void UpdateShootInput()
    {
        if (loadingShot)
        {
            if (Input.GetKeyUp(fire))
            {
                ShootObject();
                loadingShot = false;
                return;
            }

            var loadingShotTime = Time.time - loadingShotStartTime;
            if (loadingShotTime >= shootAngleOverTime.Duration())
            {
                shootAngle = 0f;
                loadingShot = false;
            }

            shootAngle = - shootAngleOverTime.Evaluate(loadingShotTime);

            return;
        }

        if (Input.GetKey(fire))
        {
            loadingShot = true;
            loadingShotStartTime = Time.time;
        }
    }

    void RotatePivot(float rotDirection){
        pivotTransform.rotation *= Quaternion.Euler(0.0f,rotDirection*1.5f,0.0f);        
    }
    
    void ShootObject()
    {
        var shootDirection = Quaternion.AngleAxis(shootAngle, Vector3.right)* transform.forward;
        var instance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var rb = instance.GetComponent<Rigidbody>();
        
        shootDirection *= force;
        rb.AddForce(shootDirection, ForceMode.VelocityChange);
    }
}
