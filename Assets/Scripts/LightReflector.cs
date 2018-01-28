using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReflector : MonoBehaviour 
{
	
	
	public void OnCollisionEnter(Collision col)
	{
		Debug.Log("EnterCol");
		if(col.gameObject.tag == "SpawnedBeam")
		{
			Debug.Log("Enter - Redraw line");
			col.gameObject.GetComponentInParent<LightBeam>().StartCoroutine(col.gameObject.GetComponentInParent<LightBeam>().DrawLightBeam());
		}
	}

	public void OnCollisionExit(Collision col)
	{
		Debug.Log("ExitCol");
		if(col.gameObject.tag == "SpawnedBeam")
		{
			Debug.Log("Exit - Redraw line");
			col.gameObject.GetComponentInParent<LightBeam>().StartCoroutine(col.gameObject.GetComponentInParent<LightBeam>().DrawLightBeam());
		}
	}
}
