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
	private float lightLength = 10.0f;

	[SerializeField]
	private float lightSpeed = 0.1f;

	[SerializeField]
	private float stoppingDistance = 0.25f;

	private bool hasFired = false;

	[SerializeField]
	private bool hasNextTarget = false;

	//debug
	[SerializeField]
	private bool isFirstSource = false;
	private Vector3 initialLightDirection = Vector3.forward;

	public void Start()
	{
		if(isFirstSource)
		{
			this.initialLightDirection = (this.targetTransform.position - this.transform.position).normalized;
			this.StartCoroutine(this.StartReflection(this.initialLightDirection));
		}
	}

	public IEnumerator StartReflection(Vector3 lightDirection)
	{
		if(this.hasFired)
			yield break;

		//Debug.Log("Initial Direction: " + lightDirection);

		this.hasFired = true;

		// Setup line renderer
		this.lineRenderer.SetPosition(0, this.transform.position);
		this.lineRenderer.SetPosition(1, this.transform.position);
		this.lineRenderer.enabled = true;

		// Setup line collider

		BoxCollider lineCollider = new GameObject("LineCollider").AddComponent<BoxCollider>();
		lineCollider.transform.parent = this.lineRenderer.gameObject.transform;
		lineCollider.tag = "Line";
		lineCollider.isTrigger = true;
		float lineWidth = this.lineRenderer.endWidth;
		float lineLength = Vector3.Distance(this.lineRenderer.GetPosition(0), this.lineRenderer.GetPosition(1));
		lineCollider.size = new Vector3(lineLength, lineWidth, this.lineRenderer.endWidth);
		Vector3 midPoint = (this.lineRenderer.GetPosition(0) + this.lineRenderer.GetPosition(1)) * 0.5f;
		lineCollider.transform.position = midPoint;
		float angle = Mathf.Atan2((this.lineRenderer.GetPosition(1).z - this.lineRenderer.GetPosition(0).z), (this.lineRenderer.GetPosition(1).x - this.lineRenderer.GetPosition(0).x));
		angle *= Mathf.Rad2Deg;
		angle *= -1;
		lineCollider.transform.localRotation = Quaternion.Euler(0.0f, angle, 0.0f);

		float targetDistance = Vector3.Distance(this.lineRenderer.GetPosition(1), this.targetTransform.position);
		float lightStep = targetDistance / this.lightSpeed;
		lineLength = 0.0f;
		//Physics.IgnoreCollision(this.GetComponent<Collider>(), );

		while(targetDistance >= this.stoppingDistance && lineLength <= this.lightLength)
		{
		/*
			targetDistance = Vector3.Distance(this.lineRenderer.GetPosition(1), this.targetTransform.position);
			this.lineRenderer.SetPosition(1, Vector3.MoveTowards(this.transform.TransformVector(this.lineRenderer.GetPosition(1)), this.targetTransform.localPosition, this.lightStep * Time.deltaTime));

			lineLength = Vector3.Distance(this.lineRenderer.GetPosition(0), this.lineRenderer.GetPosition(1));
			lineCollider.size = new Vector3(lineLength, lineWidth, this.lineRenderer.endWidth);

			midPoint = (this.lineRenderer.GetPosition(0) + this.lineRenderer.GetPosition(1)) * 0.5f;
			lineCollider.transform.position = midPoint;

			angle = Mathf.Atan2((this.lineRenderer.GetPosition(1).z - this.lineRenderer.GetPosition(0).z), (this.lineRenderer.GetPosition(1).x - this.lineRenderer.GetPosition(0).x));
			angle *= Mathf.Rad2Deg;
			angle *= -1;
			lineCollider.transform.localRotation = Quaternion.Euler(0.0f, angle, 0.0f);

			lineLength = Vector3.Distance(this.lineRenderer.GetPosition(0), this.lineRenderer.GetPosition(1));
			targetDistance = Vector3.Distance(this.lineRenderer.GetPosition(1), lightDirection * this.lightLength);
			this.lineRenderer.SetPosition(1, Vector3.MoveTowards(this.lineRenderer.GetPosition(1), transform.TransformDirection(lightDirection * this.lightLength), lightStep * Time.deltaTime));//Vector3.MoveTowards(this.lineRenderer.GetPosition(1), this.targetTransform.localPosition, this.lightStep * Time.deltaTime));
			*/
			/*
			lineLength = Vector3.Distance(this.lineRenderer.GetPosition(0), this.lineRenderer.GetPosition(1));
			lineCollider.size = new Vector3(lineLength, lineWidth, this.lineRenderer.endWidth);

			midPoint = (this.lineRenderer.GetPosition(0) + this.lineRenderer.GetPosition(1)) * 0.5f;
			lineCollider.transform.position = midPoint;

			angle = Mathf.Atan2((this.lineRenderer.GetPosition(1).z - this.lineRenderer.GetPosition(0).z), (this.lineRenderer.GetPosition(1).x - this.lineRenderer.GetPosition(0).x));
			angle *= Mathf.Rad2Deg;
			angle *= -1;
			lineCollider.transform.localRotation = Quaternion.Euler(0.0f, angle, 0.0f);
			*/
			yield return null;
		}

		if(this.hasNextTarget)
		{
			Vector3 dir = (this.lineRenderer.GetPosition(1) - this.lineRenderer.GetPosition(0));
			Vector3 reflection = Vector3.Reflect(dir, transform.TransformDirection(this.targetTransform.forward)).normalized;
			Debug.Log("reflection: " + reflection.ToString());
			this.targetTransform.GetComponent<LightReflector>().StartCoroutine(this.targetTransform.GetComponent<LightReflector>().StartReflection(reflection));
		}

		yield return null;
	}
}
