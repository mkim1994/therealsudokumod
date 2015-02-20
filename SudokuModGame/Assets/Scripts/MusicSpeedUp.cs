using UnityEngine;
using System.Collections;

public class MusicSpeedUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string currentLevel = Application.loadedLevelName;

		if (currentLevel == "TitleScreen"){
			audio.pitch+=-0.2f;

		}
		else{
			int levelNum = int.Parse (currentLevel);


			if (levelNum == 2 || levelNum == 5){
				audio.pitch+=0.25f;
			}
			else if(levelNum == 3 || levelNum == 6 || levelNum == 7){
				audio.pitch+=0.67f;
			}
			else if(levelNum == 4){
				audio.pitch+=0.12f;
			}
		}
	
	}


	// Update is called once per frame
	void Update () {
	
	}
}
