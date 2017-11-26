using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MagicInstantiate : MonoBehaviour {

	private Animator animator;
	private AnimatorStateInfo animatorState;
	public GameObject magicEff;
	public float insSec1;
	public float insSec2;
	public GameObject muzzle;
	
	void Start () {

		animator = GetComponent<Animator> ();
		this.UpdateAsObservable ()
			.Subscribe (_ => animatorState = this.animator.GetCurrentAnimatorStateInfo (0));

		this.UpdateAsObservable ()
			.Where(_ => animatorState.IsName("standing_melee_combo_attack_ver_3"))
			.Where (_ => animatorState.normalizedTime >= insSec1)
			.ThrottleFirst(System.TimeSpan.FromSeconds(2f))
			.Subscribe(_ => {
				Instantiate(magicEff, muzzle.transform.position, muzzle.transform.rotation);
			});

		this.UpdateAsObservable ()
			.Where(_ => animatorState.IsName("standing_melee_combo_attack_ver_3"))
			.Where (_ => animatorState.normalizedTime >= insSec2)
			.ThrottleFirst(System.TimeSpan.FromSeconds(2f))
			.Subscribe(_ => {
				Instantiate(magicEff, muzzle.transform.position, muzzle.transform.rotation);
			});
		
	}
}
