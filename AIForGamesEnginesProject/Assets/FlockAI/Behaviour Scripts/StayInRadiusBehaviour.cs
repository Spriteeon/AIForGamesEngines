using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    private Vector3 centre;

    private float[] distance;

    public float radius = 15f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (agent.GetAreaStatus())
        {
            return Vector3.zero;
        }

        centre = flock.transform.position;

        Vector3 centreOffset = centre - agent.transform.position;
        return centreOffset;

        //Vector3 centreOffset = centre - agent.transform.position;
        //float t = centreOffset.magnitude / radius;
        //if (t < 0.9)
        //{
        //    return Vector3.zero;
        //}

        //return centreOffset * t * t;
    }
}
