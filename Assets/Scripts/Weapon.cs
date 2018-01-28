using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {


	private void Start()
	{
		GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");

		for(int i = 0; i < weapons.Length; i++)
		{
			Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), weapons[i].GetComponent<Collider>());
		}
	}

}
