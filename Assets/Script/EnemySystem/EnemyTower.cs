using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour {

	public List<GameObject> respawn;

	public List<GameObject> Enemies;

	// Use this for initialization
	void Start () {

		StartCoroutine(myUpdate());

	}

	// Update is called once per frame
	IEnumerator myUpdate() {

		while(true) {

			yield return new WaitForSeconds(5f);


			GameObject newEnemy = Instantiate(Enemies[Random.Range(0, Enemies.Count)]);
			newEnemy.transform.position = respawn[Random.Range(0, respawn.Count)].transform.position;

		}

	}


}
