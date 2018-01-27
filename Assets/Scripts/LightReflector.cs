using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReflector : MonoBehaviour 
{
	[SerializeField]
	private LineRenderer lineRenderer;

	[SerializeField]
	private Transform targetTransform;

	[SerializeField]
	private float lightStep = 0.1f;

	[SerializeField]
	private float stoppingDistance = 0.25f;


	public void Start()
	{
		this.StartCoroutine(this.StartReflection(45));
	}

	public IEnumerator StartReflection(float angle)
	{
		
		this.lineRenderer.SetPosition(0, Vector3.zero);
		this.lineRenderer.SetPosition(1, Vector3.zero);
		//this.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, -angle, 0.0f));



		this.lineRenderer.enabled = true;

		//Vector3 targetDirection = (this.transform.TransformPoint(this.lineRenderer.GetPosition(1)) - this.targetTransform.position).normalized;
		float targetDistance = Vector3.Distance(this.transform.TransformVector(this.lineRenderer.GetPosition(1)), this.targetTransform.localPosition);

		while(targetDistance >= this.stoppingDistance)
		{
			targetDistance = Vector3.Distance(this.transform.TransformVector(this.lineRenderer.GetPosition(1)), this.targetTransform.position);

			this.lineRenderer.SetPosition(1, Vector3.MoveTowards(this.transform.TransformVector(this.lineRenderer.GetPosition(1)), this.targetTransform.localPosition, this.lightStep * Time.deltaTime));

			yield return null;
		}

		yield return null;
	}
}
