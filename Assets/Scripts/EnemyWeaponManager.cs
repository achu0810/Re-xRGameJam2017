using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponManager : MonoBehaviour {

	void Start () {
			
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Ally") {
			other.GetComponent<AllyStatus> ().Damage (2);
		}
	}

}
