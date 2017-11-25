using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class AllyAttack : MonoBehaviour {

	public float attackDistance;
	[SerializeField]private GameObject enemyObject;
	private float funcResult;
	private Animator animator;
	private AnimatorStateInfo animatorState;

	void Start () {
		animator = GetComponent<Animator> ();

		// 1番近い敵を探す
		this.UpdateAsObservable ()
			.Subscribe (x => {
				SearchEnemies();
				funcResult = AttackDistance();
				animatorState = this.animator.GetCurrentAnimatorStateInfo(0);
			});
		
		// Enemyが攻撃範囲にいれば攻撃
		this.UpdateAsObservable ()
			.Where (x => enemyObject != null)
			.Where (x => funcResult <= attackDistance)
			.Where (x => animatorState.IsName ("FreeVoxelGirl-idle"))
			.ThrottleFirst(System.TimeSpan.FromSeconds(0.3f))
			.Subscribe (x => {
				animator.SetTrigger("Attack");
		});
	}

	float AttackDistance() {
		float result;
		result = Vector3.Distance (this.transform.position, enemyObject.transform.position);
		return result;
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
