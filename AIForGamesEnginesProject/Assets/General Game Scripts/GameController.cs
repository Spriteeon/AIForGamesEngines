using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (!player) // player null
        {
            throw new System.Exception("Player Null!");
        }
    }   

    public void RespawnPlayer()
    {
        Debug.Log("Player respawning");
        GameObject checkpoint = player.GetComponent<PlayerController>().GetCurrentCheckpoint();

        if (checkpoint)
        {
            Debug.Log(checkpoint.transform.position);
            player.GetComponent<FirstPersonController>().m_CharacterController.enabled = false;
            player.GetComponent<FirstPersonController>().m_CharacterController.transform.position = checkpoint.transform.position + new Vector3(-10,5,0); // Spawns player above spawn point so they fall to platform
            player.GetComponent<FirstPersonController>().m_CharacterController.enabled = true;
        }
        else
        {
            // Spawn at start??
        }
    }
}
