using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Collider checkpointCollider;

    [SerializeField]
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        checkpointCollider = GetComponent<Collider>();

        if (!player)
        {
            throw new System.Exception("Checkpoint missing player reference!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit checkpoint!");
            player.SetCurrentCheckpoint(this.gameObject);
        }
    }
}
