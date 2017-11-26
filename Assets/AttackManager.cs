using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour {

	public float m_damage;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyHPManager> ().damage (m_damage);
		}
	}
}
