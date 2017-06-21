using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public List<int[]> atkList = new List<int[]>();
	public List<int> auxList = new List<int> ();	

	public List<int[]> nextAtkList = new List<int[]>();
	public List<int> nextAuxList = new List<int> ();

	public List<int[]> auxForPredict = new List<int[]>();

	public bool prediction = false;
	public bool canAttack = true;

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
			atk1 ();
		}

		if (Input.GetKeyDown (KeyCode.X)) 
		{
			atk2 ();
		}
	}

	void addAtk (int atk)
	{
		auxList.Add (atk);
		if (auxList.Count == 4) 
		{
			atkList.Add (auxList.ToArray());
			if (prediction == true) 
			{
				Debug.Log ("Modo predict");
				auxForPredict.Add (auxList.ToArray ());
				canAttack = false;
				HttpUtil.instance.Predict(auxForPredict); 
				auxForPredict.Clear ();
			}
			prediction = true;
		}
		if (auxList.Count == 5) 
		{
			nextAuxList.Add (atk);
			nextAtkList.Add (nextAuxList.ToArray());
			auxList.Clear ();
			nextAuxList.Clear ();
			canAttack = false;
			HttpUtil.instance.Training(atkList, nextAtkList);
		}
	}

	void atk1()
	{
		addAtk (0);
		Debug.Log ("Atk1");
	}

	void atk2()
	{
		addAtk (1);
		Debug.Log ("Atk2");
	}
}
