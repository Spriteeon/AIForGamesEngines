using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Collider playerCollider;

    [SerializeField]
    private GameObject currentCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<Collider>();
    }

    public GameObject GetCurrentCheckpoint()
    {
        return currentCheckpoint;
    }

    public void SetCurrentCheckpoint(GameObject checkpoint)
    {
        currentCheckpoint = checkpoint;
        Debug.Log("Player set new checkpoint!");
    }
}
