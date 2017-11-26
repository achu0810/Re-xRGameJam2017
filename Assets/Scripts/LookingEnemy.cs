using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class LookingEnemy : MonoBehaviour {

	private Transform m_trans;
	[SerializeField]private float funcResult;
	public GameObject enemyObject;
	public float m_speed;
	
	void Start () {
		Destroy (this.gameObject, 5f);

		this.UpdateAsObservable ()
			.Subscribe (x => {
				SearchEnemies();
				transform.LookAt(enemyObject.transform);
				transform.position += Time.deltaTime * m_speed * transform.forward;
			});
	}

	void SearchEnemies() {
		GameObject[] enemies;
		float minDistance = float.MaxValue;
		float tmpDistance;
		enemies = GameObject.FindGameObjectsWithTag("Enemy") as GameObject[];
		foreach(GameObject enemy in enemies) {
			tmpDistance = Vector3.Distance(this.transform.position, enemy.transform.position);
			if(tmpDistance < minDistance) {
				minDistance = tmpDistance;
				enemyObject = enemy.gameObject;
			}
		}
	}

}
