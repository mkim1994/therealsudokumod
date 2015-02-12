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
		digit = (int)(Random.value * (board.Size() - 2)) + 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void move_clockwise(){
		slot += 1;
	}
}
