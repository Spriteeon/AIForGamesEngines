using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

struct PlayAreaPositions
{
    public Vector3 minPos;
    public Vector3 maxPos;
}

public class Flock : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;
    [SerializeField]
    private Vector3 agentScaleSize = new Vector3(1.5f, 1.5f, 1.5f);

    PlayAreaPositions area = new PlayAreaPositions();
    [SerializeField]
    private GameObject SpawnAreaMax;
    [SerializeField]
    private GameObject SpawnAreaMin;

    [Range(10, 100)]
    public int startingCount = 40;
    const float AgentDensity = 1f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    private Vector3 resetHeight = new Vector3(1, 1, 1);

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        area.minPos = SpawnAreaMin.transform.position;
        area.maxPos = SpawnAreaMax.transform.position;

        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            // Spawn area
            Vector3 spawnPos = new Vector3(Random.Range(area.minPos.x, area.maxPos.x), 0, Random.Range(area.minPos.z, area.maxPos.z));

            FlockAgent newAgent = Instantiate(
                agentPrefab,
                spawnPos ,
                Quaternion.Euler(1,1,1),
                transform
                );
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);

            newAgent.transform.localScale = agentScaleSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent); // What exists in the neighbour radius
            Vector3 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;

            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
                move = new Vector3(move.x, 0, move.z);
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);

        foreach(Collider c in contextColliders)
        {
            if (c != agent.AgentCollider && c != player) 
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
