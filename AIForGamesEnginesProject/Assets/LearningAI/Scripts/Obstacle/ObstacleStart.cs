using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStart : MonoBehaviour
{ 
    Collider checkpointCollider;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private GameObject obstacleAgent;

    // Start is called before the first frame update
    void Start()
    {
        checkpointCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Start Obstacle AI");

            obstacleAgent.GetComponent<ObstacleAgent>().SetSpeed(8f);
        }
    }
}
