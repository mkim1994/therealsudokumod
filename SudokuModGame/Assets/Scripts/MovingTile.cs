using UnityEngine;
using System.Collections;

public class MovingTile : MonoBehaviour {

	public int digit;
	public int slot = 0;

	public enum State { Ring, Firing, Sleep };
	public State state = State.Ring;

	public BoardManager board;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: INTERPOLATE TRANSFORM
	}

	public void move_clockwise(){
		slot += 1;
		if (slot >= board.Size() * 4)
			slot = 0;
	}

	public void move_counterclockwise(){
		slot -= 1;
		if (slot < 0)
			slot = board.Size() * 4 - 1;
	}

	// on button click: attempt to insert into board
	public void Fire()
	{
		//TODO
	}
}
