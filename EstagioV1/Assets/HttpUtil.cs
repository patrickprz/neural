using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SimpleJson;

public class HttpUtil : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		JsonTraining dataTraining = new JsonTraining ();
		dataTraining.inputs = new List<int[]>();
		dataTraining.outputs = new List<int[]>();
		int[] array1Test = new int[4] {1,0,1,0}; 
		int[] array2Test = new int[1] {1}; 
		dataTraining.inputs.Add (array1Test);
		dataTraining.outputs.Add (array2Test);

		string jsonTraning = GameDevWare.Serialization.Json.SerializeToString (dataTraining);
		jsonTraning = jsonTraning.Replace (" ", string.Empty);

		string urlTraining = "http://127.0.0.1:5000/training/" + jsonTraning;
		//Debug.Log (urlTraining);
		WWW wwwTraining = new WWW(urlTraining);
		StartCoroutine(WaitForRequest(wwwTraining));

		//DIVISÃO ENTRE TREINAMENTO E PREDIÇÃO####################################################################

		JsonPredict dataPrediction = new JsonPredict ();
		dataPrediction.inputs = new List<int[]> ();
		int[] arrayAtk = new int[4] { 1, 0, 1, 0 };

		dataPrediction.inputs.Add (arrayAtk);
		dataPrediction.inputs.Add (arrayAtk);
		string jsonPrediction = GameDevWare.Serialization.Json.SerializeToString (dataPrediction);
		jsonPrediction = jsonPrediction.Replace (" ", string.Empty);

		string urlPrediction = "http://127.0.0.1:5000/prediction/" + jsonPrediction;
		//Debug.Log (urlPrediction);
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
	
