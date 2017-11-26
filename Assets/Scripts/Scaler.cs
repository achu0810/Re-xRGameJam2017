using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scaler : MonoBehaviour {

	public GameObject obj1;
	public GameObject obj2;

	public GameObject obj3;

	public GameObject obj4;

	public GameObject targetObj;

	public Vector3 scale {
		get {
			var zScale1 = (obj2.transform.position - obj1.transform.position).magnitude;
			var xScale1 = (obj3.transform.position - obj2.transform.position).magnitude;
			var zScale2 = (obj4.transform.position - obj3.transform.position).magnitude;
			var xScale2 = (obj1.transform.position - obj4.transform.position).magnitude;
			var z = (zScale1 + zScale2) / 2;
			var x = (xScale1 + xScale2) / 2;
			return new Vector3(x, 1, z);
		}
	}

	public Vector3 halfPos {
		get {
			return (obj1.transform.position + obj2.transform.position + obj3.transform.position + obj4.transform.position) / 4;
		}
	}

	void Update() {
		targetObj.transform.position = halfPos;
		targetObj.transform.localScale = scale;
		targetObj.transform.rotation = obj1.transform.rotation;
	}

}
