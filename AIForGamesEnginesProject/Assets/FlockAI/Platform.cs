using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private FlockAgent agentObj;
    private GameObject player;
    private bool playerOn = false;

    private void Start()
    {
        player = GameObject.Find("FPSController");
    }

    private void FixedUpdate()
    {
        if (playerOn)
        {
            player.GetComponent<PlayerPlatformMovement>().Move(agentObj.moveVelocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOn = false;
        }
    }
}
