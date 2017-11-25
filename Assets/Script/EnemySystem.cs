using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySystem : MonoBehaviour {

	public float HP = 5.0f;
	public GameObject target;
	public float attackDistance = 2f;
	public GameObject attackParticle;

	Animator _anim;
	GameObject nowTarget;
	NavMeshAgent nma;

	const int WALK = 0;     // アニメーション:walk
	const int ATTACK = 1;   // アニメーション:attack
	const int DOWN = 2;     // アニメーション:down
	const string animState = "animState";

	// Use this for initialization
	void Start() {

		nowTarget = target;
		nma = GetComponent<NavMeshAgent>();
		nma.destination = target.transform.position;
		_anim = GetComponent<Animator>();
		_anim.SetInteger(animState, WALK);

		StartCoroutine(myUpdate());
	}

	IEnumerator attackCharacter() {

		GameObject ap = Instantiate(attackParticle);
		ap.transform.position = nowTarget.transform.position;
		Destroy(ap.gameObject, 2);
		yield return new WaitForSeconds(1.3f);

	}

	// Update is called once per frame
	IEnumerator myUpdate() {

		while(true) {

			Debug.Log(nowTarget.name);

			nma.destination = nowTarget.transform.position;

			float distance = (transform.position - nowTarget.transform.position).sqrMagnitude;


			while(attackDistance * attackDistance > distance) {
				_anim.SetInteger(animState, ATTACK);

				yield return StartCoroutine(attackCharacter());

				yield return null;

			}

			_anim.SetInteger(animState, WALK);

			yield return null;

		}
	}

	/*
	 * このモンスターにダメージを与える
	 */
	public void damage(float dmg) {

		HP -= dmg;
		if(HP <= 0)
			StartCoroutine(down());

	}

	private IEnumerator down() {

		_anim.SetInteger(animState, DOWN);

		yield return new WaitForSeconds(1f);

		Destroy(this.gameObject);
	}


	/*
	public void setTarget(GameObject newTarget) {

		/*
		 * 今のターゲットが目標でない
		 * 即ち、自キャラを狙っている場合は
		 * ターゲットを変更せず今のターゲットに向かう
		 *
		if(nowTarget != target)
			return;


		

	}
*/

	private void OnTriggerStay(Collider other) {

		/*
		 * 身近に敵がいる かつ 今の目標は敵キャラクターである場合
		 */


		if(other.gameObject.tag != "Ally" || nowTarget != target)
			return;


		nowTarget = other.gameObject;
		nma.destination = other.gameObject.transform.position;
	}

}
