using UnityEngine;
using System.Collections;

public class Times : MonoBehaviour {

	private int currentLevel = 1;
	private float[] timesTaken = new float[7];
	Timer timeTaken;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 7; i++)
			timesTaken [i] = 0.0f;
		DontDestroyOnLoad (this);
		GameObject GO = GameObject.Find ("timer");
		timeTaken = GO.GetComponent<Timer> ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject GO = GameObject.Find ("timer");
		timeTaken = GO.GetComponent<Timer> ();
		if (!timeTaken.countTime) {
			string level = Application.loadedLevelName;
			LoadTime (level);
		}
	}

	void LoadTime(string level)
	{
		currentLevel = int.Parse (level);
		timesTaken[currentLevel-1] = timeTaken.gameTime;
	}

	public float[] GetResults ()
	{
		return timesTaken;
	}
}
