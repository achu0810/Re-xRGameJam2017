using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationTest : MonoBehaviour {

	public GameObject m_girl;
	public GameObject m_girl2;

	// Use this for initialization
	void Start () {
		Instantiate (m_girl, new Vector3 (0, 0, 0), Quaternion.identity);
		StartCoroutine (ins());
	}

	IEnumerator ins() {
		yield return new WaitForSeconds (2.5f);
		Instantiate (m_girl2);
	}

}
