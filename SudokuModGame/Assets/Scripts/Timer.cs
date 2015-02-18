using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float gameTime = 0.0f;
	int min = 0;
	int sec = 0;
	string timeFormat = "";
	Text timer;
	BoardManager board;
	public bool countTime = false;

	// Use this for initialization
	void Start () {
		timer = GetComponent<Text>();
		GameObject GO = GameObject.Find ("GameBoard");
		board = GO.GetComponent<BoardManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject GO = GameObject.Find ("GameBoard");
		board = GO.GetComponent<BoardManager> ();
		countTime = board.gameRunning;
		if (countTime)
			AddToTimer ();
	}

	void AddToTimer()
	{
		gameTime += 1 * Time.deltaTime;
		timeFormat = "";
		sec = (int)(gameTime % 60.0f);
		min = (int)(gameTime / 60.0f);
		timeFormat = min.ToString () + ":";
		if (sec < 10)
			timeFormat = timeFormat + "0" + sec.ToString ();
		else
			timeFormat = timeFormat + sec.ToString ();
		timer.text = timeFormat;
	}
}
