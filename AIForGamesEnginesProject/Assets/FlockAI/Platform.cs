using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    Debug.Log("PLAYER ON");
        //    other.transform.parent = transform.parent;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    Debug.Log("PLAYER OFF");
        //    other.transform.parent = null;
        //}
    }
}
