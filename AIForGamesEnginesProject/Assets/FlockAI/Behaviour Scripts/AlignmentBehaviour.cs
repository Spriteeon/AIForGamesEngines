using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0) // No neighbours, maintain current alignment
        {
            return agent.transform.up;
        }

        // Add all points and average out
        Vector3 alignmentMove = Vector3.zero;
        foreach (Transform item in context)
        {
            alignmentMove += item.transform.up;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
