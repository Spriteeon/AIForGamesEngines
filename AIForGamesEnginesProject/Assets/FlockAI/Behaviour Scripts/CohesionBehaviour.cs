using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(context.Count == 0) // No neighbours, no adjustment
        {
            return Vector3.zero;
        }

        // Add all points and average out
        Vector3 cohesionMove = Vector3.zero;
        foreach(Transform item in context)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= context.Count;

        // Create offset
        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }
}
