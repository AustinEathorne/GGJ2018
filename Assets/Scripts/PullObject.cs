using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour 
{
	[SerializeField]
	private float pullSpeed = 10.0f;

	[SerializeField]
	private float pullRange = 10.0f;

	private bool isPulling = false;
	private bool isrunningCoroutine = false;

	private IEnumerator Start()
	{
		this.StartCoroutine(this.PullObjectIn());
		yield return null;
	}


	public IEnumerator PullObjectIn()
	{
		if(this.isrunningCoroutine)
			yield break;

		this.isrunningCoroutine = true;

		GameObject[] pullableList = GameObject.FindGameObjectsWithTag("ReflectiveSurface");
		List<Transform> pullableInfrontList = new List<Transform>();

		//check if object is infront of you
		for(int i = 0; i < pullableList.Length; i++)
		{
			Vector3 distance = this.transform.localPosition - pullableList[i].transform.localPosition;
			if(Vector3.Dot(distance, pullableList[i].transform.forward) > 0.0f)
			{
				pullableInfrontList.Add(pullableList[i].transform);
			}
		}

		if(pullableInfrontList != null)
		{
			int closestIndex = 0;
			float closestDistance = Vector3.Distance(this.transform.localPosition, pullableInfrontList[0].localPosition);

			// find the closest
			for(int i  = 0; i < pullableInfrontList.Count; i++)
			{
				float distance = Vector3.Distance(this.transform.localPosition, pullableList[i].transform.localPosition);

				if(distance <= closestDistance)
				{
					closestIndex = i;
					closestDistance = distance;
				}
			}

			while(this.isPulling)
			{
				pullableInfrontList[closestIndex].transform.position = Vector3.MoveTowards(pullableInfrontList[closestIndex].transform.position, this.transform.localPosition, this.pullSpeed * Time.deltaTime);

				yield return null;	
			}
		}
		this.StartCoroutine(this.PullObjectIn());
	}

	public void SetIsPulling(bool value)
	{
		this.isPulling = value;
	}



}
