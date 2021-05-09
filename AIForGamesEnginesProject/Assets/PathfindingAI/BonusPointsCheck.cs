using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPointsCheck : MonoBehaviour
{
    Collider worldCollider;


    [SerializeField]
    private GameController controller;

    [SerializeField]
    private AudioClip beatAISound;

    [SerializeField]
    private AStar astar;

    bool hasBeenTriggered;

    // Start is called before the first frame update
    void Start()
    {
        worldCollider = GetComponent<Collider>();
        hasBeenTriggered = false;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (!hasBeenTriggered && other.CompareTag("Player"))
        {
            controller.AddScore(50);
            hasBeenTriggered = true;
            AudioSource.PlayClipAtPoint(beatAISound, transform.position);
            Debug.Log("Player1 beat the bean!");
            
            //kills the algorithm update if maze has been completed
            if(astar != null)
            {
                astar.canRunAlgorithm = false;
            }

        }
        if (!hasBeenTriggered && other.CompareTag("enemy"))
        {
            hasBeenTriggered = true;
            Debug.Log("The Bean beat player1");

            //kills the algorithm update if maze has been completed
            if (astar != null)
            {
                astar.canRunAlgorithm = false;
            }
        }
        
    }
}