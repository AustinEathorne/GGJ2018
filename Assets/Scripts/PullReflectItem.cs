using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullReflectItem : MonoBehaviour 
{
	private delegate void PlayerAction();

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			PlayerAction playerAction = this.gameObject.tag == "Sun" ?  new PlayerAction(this.Pull) : new PlayerAction(this.Reflect);
			playerAction();
		}
	}

	private void Pull()
	{
		Debug.Log("Pull Action");
	}

	private void Reflect()
	{
		Debug.Log("Reflect Action");
	}
}
