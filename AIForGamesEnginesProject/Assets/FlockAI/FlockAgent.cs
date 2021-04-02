using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Collider agentCollider;
    public Collider AgentCollider {  get { return agentCollider; } }
    public Vector3 moveVelocity;

    private bool isInArea = false;

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;

        moveVelocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Area"))
        {
            isInArea = true;
        }              
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Area"))
        {
            isInArea = false;
        }
    }

    public bool GetAreaStatus()
    {
        return isInArea;
    }

    public Transform GetAgentTransformParent()
    {
        return transform;
    }
}
