using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ObstacleAgent : Agent
{
	public float speed = 8f;
	public float rotationSpeed = 3f;

	private Vector3 startingPosition;
	private Rigidbody Rb;
	private EnvironmentParameters environmentParameters;

	public event Action onEnvironmentReset;

	private void FixedUpdate()
	{
		
	}

	public override void OnActionReceived(float[] vectorAction)
	{
		Rb.velocity = new Vector3(vectorAction[0] * speed, 0f, vectorAction[1] * speed);
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		base.CollectObservations(sensor);
	}

	public override void Initialize()
	{
		startingPosition = transform.position;
		Rb = GetComponent<Rigidbody>();
	}

	public override void Heuristic(float[] actionsOut)
	{
		actionsOut[0] = Input.GetAxis("Horizontal");
		actionsOut[1] = Input.GetAxis("Vertical");
	}

	public override void OnEpisodeBegin()
	{
		onEnvironmentReset?.Invoke();

		Debug.Log(message: "Episode Begin");
		transform.position = startingPosition;
		Rb.velocity = Vector3.zero;
	}

	private void Reset()
	{
		Debug.Log(message: "Reset");
	}

	#region Debug

	#endregion
}
