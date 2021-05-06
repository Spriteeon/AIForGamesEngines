using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
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
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += agent.transform.position - item.position;
            }
        }
        
        if(nAvoid > 0) // If theres objects to avoid
        {
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }
}
