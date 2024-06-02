using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public LayerMask SurfaceLayer;
    public Transform ExitPoint;
    public Collider SurfaceCollider;
    public Vector3 OutVectorNormal => -ExitPoint.transform.up;
    public Collider PortalCollider;

    private void Awake()
    {
        PortalCollider = GetComponent<Collider>();
    }

    public void Update()
    {
        Debug.DrawLine(ExitPoint.position, ExitPoint.position + ExitPoint.transform.up, Color.green);
        Debug.DrawLine(ExitPoint.position, ExitPoint.position + ExitPoint.transform.right, Color.red);
        Debug.DrawLine(ExitPoint.position, ExitPoint.position + ExitPoint.transform.forward, Color.blue);
    }

    void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(other, SurfaceCollider);
        GameObject clone = GameObject.Instantiate(other.gameObject);
        clone.gameObject.transform.position = ExitPoint.position;
        clone.GetComponent<Rigidbody>().velocity = other.attachedRigidbody.velocity.magnitude * OutVectorNormal;
        Vector3 exitEulers = ExitPoint.rotation.eulerAngles;
        Vector3 euler = other.transform.rotation.eulerAngles;
        
        Quaternion diff = PortalCollider.transform.rotation * ExitPoint.rotation;
        clone.transform.rotation =  diff * clone.transform.rotation * Quaternion.Euler(180, 180, 180);
    }

    void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, SurfaceCollider, false);
    }
    

    void OnTriggerStay(Collider other)
    {
        // if (PortalCollider.bounds.Contains(other.bounds.center))
        // {
        //     other.gameObject.transform.position = ExitPoint.position;
        //     other.attachedRigidbody.velocity = other.attachedRigidbody.velocity.magnitude * OutVectorNormal;
        //     other.transform.rotation = other.transform.rotation * ExitPoint.rotation;
        // }
    }
}
