using UnityEngine;
using System.Collections;

public class MovingTile : MonoBehaviour {

	public int digit;
	public int slot = 0;

	public float smooth = 0.95f;

	public BoardManager board;

	public float tickTime;
	public float step_interval;
	public Vector3 old_position;
	public Vector3 next_position;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// todo: make this an increasing ramp, not smooth slowdown
		float delta = (Time.realtimeSinceStartup - tickTime) / step_interval;
		transform.position = Vector3.Lerp(old_position, next_position, delta * delta);
	}

	// on button click: attempt to insert into board
	public void Fire()
	{
		// this is weird...
		board.FireTile(this); 
	}

	public IEnumerator Kill()
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(this.gameObject);
	}
}
