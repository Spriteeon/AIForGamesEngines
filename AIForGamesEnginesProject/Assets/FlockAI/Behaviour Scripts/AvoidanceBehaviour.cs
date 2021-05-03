using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0) // No neighbours, no adjustment
        {
            return Vector3.zero;
        }

        // Add all points and average out
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0; // How many objects in avoidance radius
        foreach (Transform item in context)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += agent.transform.position - item.position;
            }
        }
        
        {
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }
}
