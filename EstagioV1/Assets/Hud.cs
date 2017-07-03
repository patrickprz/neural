using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

	public InputField input;
	//public Text nextHit;
	public Text training;
	public Text counter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//nextHit.text = HttpUtil.instance.nextHit;
		training.text = HttpUtil.instance.training;
		counter.text = PlayerInput.cont.ToString();
	}

	public void changeUrl()
	{
		HttpUtil.instance.apiUrl = input.text;
	}
}
