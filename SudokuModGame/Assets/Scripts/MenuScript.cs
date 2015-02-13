using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{

	void Start(){
		//Screen.SetResolution (750,500, true);
	//	Screen.SetResolution(750,500,false);
	}


	void Update(){
	}

	public void playbutton(){
		Invoke("loadlevel", 0.0f);
	}
	void loadlevel(){
		Application.LoadLevel("MelTest");
	}

	public void quitbutton(){
		Application.Quit ();
	}

}