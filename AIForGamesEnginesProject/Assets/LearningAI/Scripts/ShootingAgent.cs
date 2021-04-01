using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ShootingAgent : Agent
{
	public Transform shootingPoint;
	public int minStepsBetweenShots = 50;
	public int damage = 100;

	public int score = 0;

	public float speed = 3f;
	public float rotationSpeed = 3f;

	private bool shotAvailable = true;
	private int stepsUntilShotIsAvailable = 0;

	private Vector3 startingPosition;
	private Rigidbody Rb;
	private EnvironmentParameters environmentParameters;

	public event Action onEnvironmentReset;

	private void Shoot()
	{
		if (!shotAvailable)
			return;

		var layerMask = 1 << LayerMask.NameToLayer("Enemy");
		var direction = transform.right;

		Debug.Log(message: "Shot!");
		Debug.DrawRay(start: shootingPoint.position, dir: direction * 200.0f, Color.green, duration: 2.0f);

		if (Physics.Raycast(origin: shootingPoint.position, direction, out var hit, maxDistance: 200.0f, layerMask))
		{
			hit.transform.GetComponent<Enemy>().GetShot(damage, shooter: this);
		}
		else
		{
			AddReward(-0.033f);
		}

		shotAvailable = false;
		stepsUntilShotIsAvailable = minStepsBetweenShots;
	}

	private void FixedUpdate()
	{
		if (!shotAvailable)
		{
			stepsUntilShotIsAvailable--;
			if (stepsUntilShotIsAvailable <= 0)
			{
				shotAvailable = true;
			}
		}

		//AddReward(-1f / MaxStep);
	}

    public override void OnActionReceived(float[] vectorAction)
	{
		if (Mathf.RoundToInt(vectorAction[0]) >= 1)
		{
			Shoot();
		}

		Rb.velocity = new Vector3(vectorAction[1] * speed, 0f, vectorAction[2] * speed);
		//transform.Rotate(Vector3.up, vectorAction[3] * rotationSpeed);
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		//sensor.AddObservation(shotAvailable);
		base.CollectObservations(sensor);
	}

	public override void Initialize()
	{
		startingPosition = transform.position;
		Rb = GetComponent<Rigidbody>();

		//TODO: Delete
		Rb.freezeRotation = true;
		environmentParameters = Academy.Instance.EnvironmentParameters;
	}

	public override void Heuristic(float[] actionsOut)
	{
		actionsOut[0] = Input.GetKey(KeyCode.P) ? 1f : 0f;
		//transform.rotation.SetLookRotation();
		actionsOut[1] = Input.GetAxis("Vertical");
		actionsOut[2] = -Input.GetAxis("Horizontal");
		//actionsOut[1] = -Input.GetAxis("Vertical");
		//actionsOut[3] = Input.GetAxis("Vertical");
	}

	public override void OnEpisodeBegin()
	{
		onEnvironmentReset?.Invoke();

		Debug.Log(message: "Episode Begin");
		transform.position = startingPosition;
		Rb.velocity = Vector3.zero;
		shotAvailable = true;
	}

	private void Reset()
	{
		Debug.Log(message: "Reset");
	}

	public void RegisterKill()
	{
		AddReward(increment: 1.0f);
		EndEpisode();
	}

	#region Debug
	
	#endregion
}
