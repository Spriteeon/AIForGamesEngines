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
	public GameObject finish;

	float distanceToFinish;
	float oldDistanceToFinish;

	public override void Initialize()
	{
		startingPosition = transform.position;
		Rb = GetComponent<Rigidbody>();
	}

	public override void OnEpisodeBegin()
	{
		onEnvironmentReset?.Invoke();

		Debug.Log(message: "Episode Begin");
		transform.position = startingPosition;
		Rb.velocity = Vector3.zero;

		distanceToFinish = Vector3.Distance(gameObject.transform.position, finish.transform.position);
		oldDistanceToFinish = distanceToFinish;
	}

	private void FixedUpdate()
	{
		distanceToFinish = Vector3.Distance(gameObject.transform.position, finish.transform.position);
		if(distanceToFinish < oldDistanceToFinish)
        {
			AddReward(1f);
		}
        else
        {
			AddReward(-1f);
		}
		oldDistanceToFinish = distanceToFinish;
	}

	public override void OnActionReceived(float[] vectorAction)
	{
		Rb.velocity = new Vector3(vectorAction[0] * speed, 0f, vectorAction[1] * speed);
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		sensor.AddObservation(Vector3.Distance(finish.transform.position, gameObject.transform.position));
		//base.CollectObservations(sensor);
	}

	public override void Heuristic(float[] actionsOut)
	{
		actionsOut[0] = Input.GetAxis("Horizontal");
		actionsOut[1] = Input.GetAxis("Vertical");
	}

	void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.CompareTag("enemy"))
        {
			AddReward(-1f);
			//EndEpisode();
        }
		if (collision.gameObject.CompareTag("wall"))
		{
			AddReward(-1f);
			//EndEpisode();
		}
		//if (collision.gameObject.CompareTag("die"))
		//{
		//	AddReward(-0.5f);
		//	EndEpisode();
		//}
		if (collision.gameObject.CompareTag("finish"))
        {
			AddReward(100f);
			EndEpisode();
        }
	}

	private void Reset()
	{
		Debug.Log(message: "Reset");
	}

	#region Debug

	#endregion
}
