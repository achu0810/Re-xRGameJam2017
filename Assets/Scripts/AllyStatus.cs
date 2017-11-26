using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class AllyStatus : MonoBehaviour {

	public float m_hp;
	
	void Start () {

		this.ObserveEveryValueChanged (_ => m_hp)
			.Where (x => x <= 0)
			.Subscribe (_ => {
				Destroy (this.gameObject);
			});

	}
}
