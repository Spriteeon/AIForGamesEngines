using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Collider agentCollider;
    public Collider AgentCollider {  get { return agentCollider; } }

    private bool isInArea = false;

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Move(Vector3 velocity)
    {
        //velocity = new Vector3(velocity.x, 0, velocity.z);
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
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
}
