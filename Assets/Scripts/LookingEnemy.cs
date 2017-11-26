using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class LookingEnemy : MonoBehaviour {

	private AllyAttack allyAttack;
	
	void Start () {
		this.UpdateAsObservable ()
			.TakeWhile (_ => allyAttack == null)
			.Subscribe (_ => allyAttack = GetComponent<AllyAttack> ());

		this.UpdateAsObservable ()
			.SkipWhile (_ => allyAttack == null)
			.Subscribe (_ => transform.LookAt(allyAttack.enemyObject.transform));

	}

}
