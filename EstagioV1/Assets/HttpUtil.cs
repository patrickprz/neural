using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HttpUtil : MonoBehaviour {
	
	public event System.Action OnFinishedProcessing;
	public static HttpUtil instance;
	public string apiUrl = "http://127.0.0.1:5000";

	public Text nextHit;
	public Text training;

	void Awake(){
		instance = this;	
	}
		
	public void Training(List<int[]> inputList, List<int[]> outputList)
	{
		JsonTraining dataTraining = new JsonTraining ();
		dataTraining.inputs = inputList;
		dataTraining.outputs = outputList;

		string jsonTraning = GameDevWare.Serialization.Json.SerializeToString (dataTraining);
		jsonTraning = jsonTraning.Replace (" ", string.Empty);

		string urlTraining = apiUrl +"/training/" + jsonTraning;

		StartCoroutine(WaitForRequest(urlTraining));
	}

	public void Predict(List<int[]> inputList)
	{
		JsonPredict dataPrediction = new JsonPredict ();
		dataPrediction.inputs = inputList;

		string jsonPrediction = GameDevWare.Serialization.Json.SerializeToString (dataPrediction);
		jsonPrediction = jsonPrediction.Replace (" ", string.Empty);

		string urlPrediction = apiUrl + "/prediction/" + jsonPrediction;

		StartCoroutine(WaitForRequest(urlPrediction));
	}

	IEnumerator WaitForRequest(string url)
	{
		WWW www = new WWW(url);
		yield return www;

		// check for errors
		if (www.error == null)
		{
			//Debug.Log("WWW Ok!: " + www.text);
			double value;
			double.TryParse (www.text, out value);
			if (value != 0 && value != 1) {
				Debug.Log ("Treinamento realizado com erro de " + value + "%");
				training.text = ("Treinamento realizado com erro de " + value + "%");
			} else {
				Debug.Log ("Golpe previsto: " + value);
				nextHit.text = ("Golpe previsto: " + value);
			}

		} else {
			Debug.Log(www.error);
		}


		if (www.isDone == true) 
		{
			if (OnFinishedProcessing != null)
				OnFinishedProcessing ();
		}
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
	
