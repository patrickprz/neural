using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SimpleJson;

public class HttpUtil : MonoBehaviour {

	// Use this for initialization
	void Start () {
		JsonData data = new JsonData ();

		data.inputs = new List<int[]>();
		data.outputs = new List<int[]>();

		int[] array1Test = new int[4] {1,0,1,0}; 
		int[] array2Test = new int[1] {1}; 

		data.inputs.Add (array1Test);
		data.outputs.Add (array2Test);
		data.inputs.Add (array1Test);
		data.outputs.Add (array2Test);

		Debug.Log (data);

		//string json = JsonUtility.ToJson(data);

		Debug.Log (JsonUtility.FromJson<JsonData>(json));

		//json = '[{"inputs":[[0,0,1,0],[0,1,1,1],[1,0,1,0],[1,1,1,0]],"outputs":[[0],[1],[1],[0]]}]';
			

		string url = "http://127.0.0.1:5000/import/" + json;
		Debug.Log (url);
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
	// Update is called once per frame
	void Update () {
		
	}
}


[System.Serializable]
public class JsonData
{
	public List<int[]> inputs;
	public List<int[]> outputs;
}
	
