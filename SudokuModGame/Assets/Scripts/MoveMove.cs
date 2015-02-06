using UnityEngine;
using System.Collections;

public class MoveMove : MonoBehaviour {
	
	public int posIndex = 0;	
	public ArrayList positions = new ArrayList();
	
	// Use this for initialization
	void Start () {
		positions.Add (new Vector3(-4, 10, 0)); //A1
		positions.Add (new Vector3((float)0.7, 10, 0)); //A2
		positions.Add (new Vector3((float)5.4, 10, 0)); //A3
		positions.Add (new Vector3((float)10.2, (float)5.3, 0)); //B1
		positions.Add (new Vector3((float)10.2, (float)0.6, 0)); //B2
		positions.Add (new Vector3((float)10.2, (float)-4.1, 0)); //B3
		positions.Add (new Vector3((float)5.4, -9, 0)); //C1
		positions.Add (new Vector3((float)0.7, -9, 0)); //C2
		positions.Add (new Vector3(-4, -9, 0)); //C3
		positions.Add (new Vector3((float)-8.8, (float)-4.1, 0)); //D1
		positions.Add (new Vector3((float)-8.8, (float)0.6, 0)); //D2
		positions.Add (new Vector3((float)-8.8, (float)5.3, 0)); //D3
		
		//new positions within the grid: all 9 grid positions
		
		positions.Add (new Vector3(-4, (float)-4.1,0)); //C3's x-pos, D1's y-pos
		//etc.
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsInvoking ("move")){
			Invoke("move", 1);
		}
		
		//would need to check conditionals for if a spot is already occupied
		
		if(Input.GetKey (KeyCode.UpArrow)){
			
		}
		else if(Input.GetKey (KeyCode.DownArrow)){
		}
		else if(Input.GetKey (KeyCode.RightArrow)){
		}
		else if(Input.GetKey (KeyCode.LeftArrow)){
		}
		
	}
	
	void move () {
		print("move move");
		if (posIndex < positions.Count-1) {
			posIndex += 1;
		}
		else{
			posIndex = 0;
		}
		this.transform.position = (Vector3)positions[posIndex];
	}
}
