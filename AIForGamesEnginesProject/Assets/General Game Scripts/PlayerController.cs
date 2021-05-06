using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
    Collider playerCollider;
    Rigidbody rb;

    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject currentCheckpoint;

    [SerializeField]
    private AudioClip checkpointSound;

    private Vector3 bounceNormal = Vector3.zero;
    [SerializeField]    [Range(0f, 200f)]
    private float bounceForce = 100f;
    private bool knockBackActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameController)
        {
            throw new System.Exception("Player: Game controller null");
        }

        playerCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (knockBackActive)
        {
            ApplyKnockBack(bounceNormal);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) // Knock's back player when they hit a crowd agent
        {
            if (!knockBackActive)
            {
                GetComponent<FirstPersonController>().m_CharacterController.enabled = false;
                rb.isKinematic = false;
                bounceNormal = collision.contacts[0].normal;
                knockBackActive = true;
                Invoke("StopKnockBack", 0.3f);
            }
        }
    }

    void ApplyKnockBack(Vector3 normal)
    {
        rb.AddForce(normal * bounceForce);
    }
    
    void StopKnockBack()
    {
        knockBackActive = false;
        GetComponent<FirstPersonController>().m_CharacterController.enabled = true;
        rb.isKinematic = true;
        gameController.AddScore(-10); 
    }

    public GameObject GetCurrentCheckpoint()
    {
        return currentCheckpoint;
    }

    public void SetCurrentCheckpoint(GameObject checkpoint)
    {
        if (currentCheckpoint != checkpoint)
        {
            currentCheckpoint = checkpoint;
            AudioSource.PlayClipAtPoint(checkpointSound, transform.position);
            gameController.AddScore(250);

            Debug.Log("Player set new checkpoint!");
        }
    }
}
