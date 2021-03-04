using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Platform : MonoBehaviour
{
   // [SerializeField] private GameObject player;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
       // Debug.Log(player);
    }

    private void Update()
    {
        transform.rotation = new UnityEngine.Quaternion(0, transform.parent.rotation.y - transform.parent.rotation.y, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER ON");
            //other.transform.parent = transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    Debug.Log("PLAYER");
        //    UnityEngine.Vector3 offset = transform.position - other.transform.position;
        //    offset = new UnityEngine.Vector3(offset.x, 0, offset.z);
        //    other.GetComponent<FirstPersonController>().OnPlatform(offset);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER OFF");
            //other.transform.parent = null;
        }
    }
}
