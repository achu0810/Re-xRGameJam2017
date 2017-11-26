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
	bool flag;

	const int WALK = 0;     // アニメーション:walk
	const int ATTACK = 1;   // アニメーション:attack
	const int DOWN = 2;     // アニメーション:down
	const string animState = "animState";

	// Use this for initialization
	void Start() {

		nowTarget = target;
		nma = GetComponent<NavMeshAgent>();
		
		_anim = GetComponent<Animator>();
		_anim.SetInteger(animState, WALK);
		flag = false;

		StartCoroutine(myUpdate());
	}

	public IEnumerator attackCharacter() {

		_anim.SetInteger(animState, ATTACK);
		GameObject ap = Instantiate(attackParticle);
		ap.transform.position = nowTarget.transform.position;
		Destroy(ap.gameObject, 2);

		TestAlly ta = nowTarget.GetComponent<TestAlly>();
		ta.damage(5f);

		yield return new WaitForSeconds(1.23f);



	}

	// Update is called once per frame
	IEnumerator myUpdate() {

		float distance;

		while(true) {

			nma.destination = nowTarget.transform.position;

			distance = (transform.position - target.transform.position).sqrMagnitude;
			if(attackDistance * attackDistance >= distance) 
				Destroy(this.gameObject);
				

			distance = (transform.position - nowTarget.transform.position).sqrMagnitude;

			while(attackDistance * attackDistance >= distance && nowTarget != null) 
				yield return StartCoroutine(attackCharacter());
			_anim.SetInteger(animState, WALK);
			nowTarget = target;

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


	private void OnTriggerStay(Collider other) {

		/*
		 * 身近に敵がいる かつ 今の目標は敵キャラクターである場合
		 */


		if(other.gameObject.tag != "Ally" || nowTarget != target)
			return;


		nowTarget = other.gameObject;
		nma.destination = other.gameObject.transform.position;
	}

	public void updateTarget(GameObject newT) {

		if(nowTarget != target)
			return;

		nowTarget = newT.gameObject;
		nma.destination = newT.gameObject.transform.position;
	}

	public void setTarget(GameObject t) {
		target = t;
	}

}
