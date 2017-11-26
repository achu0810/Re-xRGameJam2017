using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class HammerEffect : MonoBehaviour {

	private Animator animator;
	private AnimatorStateInfo animatorState;
	public GameObject groundEff;
	public float insSec;
	public GameObject muzzle;

	void Start () {
		animator = GetComponent<Animator> ();
		this.UpdateAsObservable ()
			.Subscribe (_ => animatorState = this.animator.GetCurrentAnimatorStateInfo (0));

		this.UpdateAsObservable ()
			.Where (_ => animatorState.normalizedTime >= insSec)
			.Where(_ => animatorState.IsName("standing_melee_combo_attack_ver_2"))
			.ThrottleFirst (System.TimeSpan.FromSeconds (3f))
			.Subscribe (_ => {
				GameObject obj = Instantiate(groundEff, muzzle.transform.position, muzzle.transform.rotation);
				Destroy(obj.gameObject, 0.5f);
		});
	}
		
}
