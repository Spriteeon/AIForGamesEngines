using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformMovement : MonoBehaviour
{
    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }
}
