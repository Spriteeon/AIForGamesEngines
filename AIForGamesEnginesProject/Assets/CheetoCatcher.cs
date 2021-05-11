using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheetoCatcher : MonoBehaviour
{
    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log("Destroyed escaped cheeto");
        }
    }
}
