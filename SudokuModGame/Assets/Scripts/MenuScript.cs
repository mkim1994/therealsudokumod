using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{
	public GameObject music;

	void Start(){
		//Screen.SetResolution (750,500, true);
	//	Screen.SetResolution(750,500,false);
	}


	void Update(){

		if(Input.GetKey (KeyCode.Escape)){
			Application.Quit ();
		}
	}

	public void playbutton(){
		Invoke("loadlevel", 0.1f);
	}
	void loadlevel(){
		Application.LoadLevel("1");
	}

	/*public void quitbutton(){
		Application.Quit ();
	}
*/
	public void howtobutton(){
		Invoke("howto", 0.1f);
		//DontDestroyOnLoad(music);
	}
	void howto(){
		Application.LoadLevel("Instructions");
	}


}