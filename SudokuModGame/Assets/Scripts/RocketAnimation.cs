using UnityEngine;
using System.Collections;

public class RocketAnimation : MonoBehaviour {

	private Animator animator;
	public BoardManager board;

	// Use this for initialization
	void Start () {
	//	animator.SetBool ("gorocket", false);
		//animation.playAutomatically = false;
		//this.animation.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (animator.GetBool (Animator.StringToHash ("gorocket")));
		Debug.Log (board.gameWin);
		if(board.gameWin == true){
			animator.SetTrigger("Hit");
			
		}
	
	}

	void Awake()
	{
		animator = GetComponent<Animator>();

	}
}
