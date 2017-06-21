using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpUtil : MonoBehaviour {
	public event System.Action OnFinishedProcessing;
	public static HttpUtil instance;
	// Use this for initialization
	void Awake(){
		instance = this;	
	}

	void Start () {

	}

	public void Training(List<int[]> inputList, List<int[]> outputList)
	{
		JsonTraining dataTraining = new JsonTraining ();
		dataTraining.inputs = inputList;
		dataTraining.outputs = outputList;

		string jsonTraning = GameDevWare.Serialization.Json.SerializeToString (dataTraining);
		jsonTraning = jsonTraning.Replace (" ", string.Empty);

		string urlTraining = "http://127.0.0.1:5000/training/" + jsonTraning;

		WWW wwwTraining = new WWW(urlTraining);
		StartCoroutine(WaitForRequest(wwwTraining));
	}

	public void Predict(List<int[]> inputList)
	{
		JsonPredict dataPrediction = new JsonPredict ();
		dataPrediction.inputs = inputList;

		string jsonPrediction = GameDevWare.Serialization.Json.SerializeToString (dataPrediction);

		jsonPrediction = jsonPrediction.Replace (" ", string.Empty);

		string urlPrediction = "http://127.0.0.1:5000/prediction/" + jsonPrediction;
		WWW wwwPrediction = new WWW(urlPrediction);
		StartCoroutine(WaitForRequest(wwwPrediction));
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


		if (www.isDone == true) 
		{
			Debug.Log ("ACABOOOU");
			if (OnFinishedProcessing != null)
				OnFinishedProcessing ();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}


}


[System.Serializable]
public class JsonTraining
{
	public List<int[]> inputs;
	public List<int[]> outputs;
}

[System.Serializable]
public class JsonPredict
{
	public List<int[]> inputs;
}
	
