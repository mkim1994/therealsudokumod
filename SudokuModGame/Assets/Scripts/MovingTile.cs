using UnityEngine;
using System.Collections;

public class MovingTile : MonoBehaviour {

	public int digit;
	public int slot = 0;

	public float smooth = 0.95f;

	public BoardManager board;

	public Vector3 old_position;
	public Vector3 next_position;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// todo: make this an increasing ramp, not smooth slowdown
		transform.position = Vector3.Lerp(transform.position, next_position, smooth * Time.deltaTime);
	}

	// on button click: attempt to insert into board
	public void Fire()
	{
		// this is weird...
		board.FireTile(this); 

		Debug.Log("DID SOMETHING");
	}

	public IEnumerator Kill()
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(this.gameObject);
	}
}
