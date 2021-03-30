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

	private bool shotAvailable = true;
	private int stepsUntilShotIsAvailable = 0;

	private void Shoot(Vector3 direction)
	{
		if (!shotAvailable)
			return;

		int layerMask = 1 << LayerMask.NameToLayer("Enemy");

		Debug.Log(message: "Shot!");
		Debug.DrawRay(start: shootingPoint.position, dir: direction * 200.0f, Color.green, duration: 2.0f);

		if (Physics.Raycast(origin: shootingPoint.position, direction, out var hit, maxDistance: 200.0f, layerMask))
		{
			hit.transform.GetComponent<Enemy>().GetShot(damage);
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
	}

    public override void OnActionReceived(float[] vectorAction)
	{
		base.OnActionReceived(vectorAction);
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		base.CollectObservations(sensor);
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void Heuristic(float[] actionsOut)
	{
		//transform.rotation.SetLookRotation();
	}

	public override void OnEpisodeBegin()
	{
		base.OnEpisodeBegin();
	}

	#region Debug
	private void OnMouseDown()
	{
		Shoot(-transform.right);
	}
	#endregion
}
