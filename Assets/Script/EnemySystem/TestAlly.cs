using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAlly : MonoBehaviour {

	public float HP = 15f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void damage(float dmg) {

		HP -= dmg;

		if(HP <= 0)
			Destroy(this.gameObject);

	}



}
