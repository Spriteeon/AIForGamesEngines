using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class OutOfWorld : MonoBehaviour
{
    Collider worldCollider;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private AudioClip fallingSound;

    // Start is called before the first frame update
    void Start()
    {
        worldCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
       {
            AudioSource.PlayClipAtPoint(fallingSound, other.GetComponent<PlayerController>().GetCurrentCheckpoint().transform.position);
            gameController.RespawnPlayer();
       }
    }
}
