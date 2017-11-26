using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameoverManager : MonoBehaviour {

	public int m_hp;
	public GameObject m_gameoverText;
	private GameObject[] m_enemies;
	private GameObject[] m_allies;

	void Start () {
		this.ObserveEveryValueChanged(_ => m_hp)
			.Where(x => x <= 0)
			.Subscribe(_ => {
				m_enemies = GameObject.FindGameObjectsWithTag("Enemy") as GameObject[];
				m_allies = GameObject.FindGameObjectsWithTag("Ally") as GameObject[];
				foreach(GameObject tmp in m_enemies) {
					Destroy(tmp.gameObject);				
				}
				foreach(GameObject tmp in m_allies) {
					Destroy(tmp.gameObject);
				}
				m_gameoverText.SetActive(true);
			});
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			m_hp--;
			Destroy (other.gameObject);
		}
	}
}
