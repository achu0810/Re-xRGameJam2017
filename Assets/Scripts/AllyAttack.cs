using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AI;

public class AllyAttack : MonoBehaviour {

	public float attackDistance;
	public float valueOfSearchingForTheEnemy; 
	[SerializeField]private GameObject enemyObject;
	[SerializeField]private float funcResult;
	private Animator animator;
	private AnimatorStateInfo animatorState;
	private NavMeshAgent agent;
	[SerializeField]private Vector3 targetPos = Vector3.zero;
	public GameObject swordObject;

	void Start () {

		// add collision to sword 
		this.UpdateAsObservable ()
			.Subscribe (_ => {
				if(animatorState.IsName("standing_melee_attack_horizontal")) {
					if(animatorState.normalizedTime >= 0.25f && animatorState.normalizedTime <= 0.4f) {
						swordObject.GetComponent<Collider>().enabled = true;
					} else {
						swordObject.GetComponent<Collider>().enabled = false;
					}
				}
		});

		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();

		// get sword
		this.UpdateAsObservable ()
			.TakeWhile (_ => swordObject == null)
			.Subscribe (_ => swordObject = this.transform.Find ("Sword").gameObject);

		// EnemyTowerタグがついたものを探す
		this.UpdateAsObservable ()
			.TakeWhile (x => targetPos == Vector3.zero)
			.Subscribe (x => targetPos = GameObject.FindWithTag ("EnemyTower").transform.position);

		// 1番近い敵を探す
		this.UpdateAsObservable ()
			.Subscribe (x => {
				SearchEnemies();
				funcResult = AttackDistance();
				animatorState = this.animator.GetCurrentAnimatorStateInfo(0);
			});

		// searching for the enemy
		this.UpdateAsObservable ()
			.Where (_ => funcResult > valueOfSearchingForTheEnemy)
			.Subscribe (_ => {
				agent.destination = targetPos;
				agent.stoppingDistance = 0;
		});
		this.UpdateAsObservable ()
			.Where (_ => funcResult <= valueOfSearchingForTheEnemy)
			.Where (_ => funcResult > attackDistance)
			.Subscribe (_ => {
				agent.stoppingDistance = 2.5f;
				agent.destination = enemyObject.transform.position;
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
