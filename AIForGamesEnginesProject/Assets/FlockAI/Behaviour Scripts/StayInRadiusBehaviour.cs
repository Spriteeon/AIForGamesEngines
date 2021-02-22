using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    public Vector3 centre;
    public float radius = 15f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 centreOffset = centre - agent.transform.position;
        float t = centreOffset.magnitude / radius;

        if (t < 0.75f)
        {
            return Vector3.zero;
        }

        return centreOffset * t * t;
    }
}
