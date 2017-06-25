using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject prefab;
	private Quaternion quarternion = Quaternion.Euler(new Vector3(0,0,-90));

	public void prepareEnemy(){
		if (HttpUtil.instance.enemyPos == 0) {
			Instantiate (prefab, new Vector3(8f,2.7f,90f), quarternion);
		} else {
			Instantiate (prefab, new Vector3(8f,-1.3f,90f), quarternion);
		}

		Debug.Log(HttpUtil.instance.enemyPos);
	}

}
