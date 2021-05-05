using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class StartAI : MonoBehaviour
{
    Collider worldCollider;

    [SerializeField]
    private AStar aStar;
    // Start is called before the first frame update
    void Start()
    {
        worldCollider = GetComponent<Collider>();
        //aStar = GetComponent<AStar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aStar.canRunAlgorithm = true;
        }
            
    }
}
