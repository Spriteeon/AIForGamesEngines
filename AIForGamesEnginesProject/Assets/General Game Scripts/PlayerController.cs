using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
    Collider playerCollider;
    Rigidbody rb;

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
        if(collision.gameObject.CompareTag("CrowdAgent"))
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
    }

    public GameObject GetCurrentCheckpoint()
    {
        return currentCheckpoint;
    }

    public void SetCurrentCheckpoint(GameObject checkpoint)
    {
        currentCheckpoint = checkpoint;
        AudioSource.PlayClipAtPoint(checkpointSound, transform.position);
        Debug.Log("Player set new checkpoint!");
    }
}
