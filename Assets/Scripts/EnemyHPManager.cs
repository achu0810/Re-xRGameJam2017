using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyHPManager : MonoBehaviour {

	public float m_hp = 5;

	void Start () {
		this.ObserveEveryValueChanged (_ => m_hp)
			.Where (x => x <= 0)
			.Subscribe (_ => Destroy (this.gameObject.transform.root.gameObject));
	}

	public void damage(float dmg) {
		m_hp -= dmg;
	}
}
