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
		if (auxList.Count == 4) 
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

	public void atk0()
	{
		if (!canAttack)
			return;
		
		addAtk (0);
		Debug.Log ("Atk0");
	}

	public void atk1()
	{
		if (!canAttack)
			return;
		
		addAtk (1);
		Debug.Log ("Atk1");
	}
}
