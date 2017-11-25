using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationTest : MonoBehaviour {

	public GameObject m_girl;

	// Use this for initialization
	void Start () {
		Instantiate (m_girl, new Vector3 (0, 0, 0), Quaternion.identity);
	}

}
