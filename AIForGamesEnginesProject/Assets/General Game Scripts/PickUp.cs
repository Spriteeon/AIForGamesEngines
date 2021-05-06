using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private AudioClip pointSound;

    [SerializeField]
    private int pointsAmount = 50;

    private Vector3 startPos = new Vector3();
    private Vector3 tempPos = new Vector3();
    private float freq = 0.75f;
    private float amp = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameController)
        {
            throw new System.Exception("PickUp: GameController null!");
        }

        startPos = transform.position;
    }

    private void Update()
    {
        // Makes obj float up and down
        tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * freq) * amp;
        transform.position = tempPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.AddScore(pointsAmount);
            AudioSource.PlayClipAtPoint(pointSound, transform.position);
            Destroy(this.gameObject);
        }
    }
}
