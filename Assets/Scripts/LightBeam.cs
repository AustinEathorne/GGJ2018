using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : MonoBehaviour {

	[SerializeField]
	private float maxDistance = 25.0f;

	[SerializeField]
	private LineRenderer lineRenderer;

	[SerializeField]
	private float lightSpeed = 5.0f;

	private Vector3 nextPos = Vector3.zero;
	private Vector3 nextNormal = Vector3.zero;

	private int startIndex = 0;

	private bool hasHitPoint = false;


	private IEnumerator Start()
	{
		this.StartCoroutine(this.RunBeam());
		yield return null;
	}

	private IEnumerator RunBeam()
	{
		Debug.Log("Run Beam Routine");

		yield return this.StartCoroutine(this.CastRay());
		yield return this.StartCoroutine(this.MoveLightBeam());
		yield return this.StartCoroutine(this.CalculateNextBeam());

		this.hasHitPoint = false;
		this.StartCoroutine(this.RunBeam());
	}

	private IEnumerator CastRay()
	{
		Ray ray = new Ray(this.transform.position, this.transform.forward);
		RaycastHit hit;

		Debug.Log("Out");

		while(!hasHitPoint)
		{
			Debug.Log("In");
			Debug.DrawRay(this.transform.position, this.transform.forward, Color.green, 100.0f);
			if(Physics.Raycast(ray, out hit, 100.0f))
			{
				Debug.Log("Waiting");

				if(hit.transform.tag == "ReflectiveSurface")
				{
					Debug.Log("Hit Mirror at: " + hit.point);
					this.hasHitPoint = true;

					this.AddPositionToLine();

					nextPos = hit.point;
					this.nextNormal = hit.transform.forward;
				}
			}

			yield return null;
		}
	}

	private IEnumerator MoveLightBeam()
	{
		Debug.Log("Move Beam");

		this.lineRenderer.SetPosition(startIndex, this.transform.position);
		this.lineRenderer.SetPosition(startIndex + 1, this.transform.position);

		float distance = Vector3.Distance(this.transform.position, nextPos);
		float linelength = 0.0f;

		while(distance > 0.05f)
		{
			Debug.Log("Moving");

			this.lineRenderer.SetPosition(startIndex + 1, Vector3.MoveTowards(this.lineRenderer.GetPosition(startIndex + 1), this.nextPos, this.lightSpeed * Time.deltaTime));
			distance = Vector3.Distance(this.lineRenderer.GetPosition(startIndex + 1), this.nextPos);

			yield return null;
		}
		yield return null;
	}

	private IEnumerator CalculateNextBeam()
	{
		Debug.Log("Calculate Next Beam");

		Vector3 dir = (this.lineRenderer.GetPosition(startIndex + 1) - this.lineRenderer.GetPosition(startIndex)).normalized;
		Vector3 reflection = Vector3.Reflect(dir, this.nextNormal);

		this.transform.rotation = Quaternion.LookRotation(reflection);
		this.transform.position = this.nextPos;

		yield return null;
	}

	private void AddPositionToLine()
	{
		Vector3[] tempArray = new Vector3[this.lineRenderer.positionCount + 1];
		this.lineRenderer.GetPositions(tempArray);

		this.lineRenderer.SetPosition(startIndex, this.lineRenderer.GetPosition(this.lineRenderer.positionCount - 1));
		this.lineRenderer.positionCount += 1;

		for(int i = 0; i < this.lineRenderer.positionCount; i++)
		{
			this.lineRenderer.SetPosition(i, tempArray[i]);
		}

		this.startIndex += 1;
	}

}
