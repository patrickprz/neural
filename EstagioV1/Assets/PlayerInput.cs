using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	private List<int[]> atkList = new List<int[]>();
	private List<int> auxList = new List<int> ();	

	private List<int[]> nextAtkList = new List<int[]>();
	private List<int> nextAuxList = new List<int> ();

	private List<int[]> auxForPredict = new List<int[]>();

	private bool prediction = false;
	private bool canAttack = true;

	public GameObject prefab;

	private Vector3 pos0 = new Vector3(-7.3f,2.7f,90.0f);
	private Vector3 pos1 = new Vector3(-7.3f,-1.3f,90.0f);

	private Quaternion quarternion = Quaternion.Euler(new Vector3(0,0,-90));

	public EnemyController enemyController;

	public float timeBetween = 1f;
	private float timestamp;

	// Use this for initialization
	void Start () {
		HttpUtil.instance.OnFinishedProcessing += () => canAttack = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (!canAttack)
			return;
		
		if (Input.GetKeyDown (KeyCode.Z)) 
		{
			atk0 ();
		}

		if (Input.GetKeyDown (KeyCode.X)) 
		{
			atk1 ();
		}
	}

	void addAtk (int atk)
	{
		auxList.Add (atk);
		if (auxList.Count == 3) 
		{
			atkList.Add (auxList.ToArray());
			if (prediction == true) 
			{
				auxForPredict.Add (auxList.ToArray ());
				canAttack = false;
				HttpUtil.instance.Predict(auxForPredict); 
				auxForPredict.Clear ();
			}
			prediction = true;
		}
		if (auxList.Count == 4) 
		{
			nextAuxList.Add (atk);
			nextAtkList.Add (nextAuxList.ToArray());
			auxList.Clear ();
			nextAuxList.Clear ();
			canAttack = false;
			HttpUtil.instance.Training(atkList, nextAtkList);
			enemyController.prepareEnemy ();
		}
	}

	public void atk0()
	{
		if (!canAttack)
			return;
		
		if (Time.time >= timestamp) {
			addAtk (0);
			Instantiate (prefab, pos0, quarternion);
			timestamp = Time.time + timeBetween;
		}

		//Debug.Log ("Atk0");
	}

	public void atk1()
	{
		if (!canAttack)
			return;
		if (Time.time >= timestamp) {
			addAtk (1);
			Instantiate (prefab, pos1, quarternion);
			timestamp = Time.time + timeBetween;
		}
		//Debug.Log ("Atk1");
	}
		
}
