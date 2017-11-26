using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class AnimatorTest : MonoBehaviour {

	private Animator animator;
	[SerializeField]private string animationName;

	void Start () {
		animator = GetComponent<Animator> ();

		this.UpdateAsObservable ()
			.Where (x => Input.GetKeyDown (KeyCode.Space))
			.Subscribe (x => {
				animator.SetTrigger(animationName);
		});
	}
	
}
