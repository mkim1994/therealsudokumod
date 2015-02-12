using UnityEngine;
using System.Collections;

public class UpdateTileImageALT : MonoBehaviour {
	
	public SudokuBoardALT board;
	
	public Sprite tile0;
	public Sprite tile1;
	public Sprite tile2;
	public Sprite tile3;
	
	public bool isBoard;
	
	SpriteRenderer sp;
	
	public int myrow, mycol;
	
	// Use this for initialization
	void Start () {
		board = GetComponentInParent<SudokuBoardALT>();
		sp = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		int mydigit = isBoard ? board.GetBoardDigit(myrow, mycol) : board.GetRingDigit(myrow, mycol);
		if (mydigit == 0)
			sp.color = new Color(0,0,0,0);
		else
		{
			sp.color = new Color(1,1,1,1);
			if (mydigit == 1)
				sp.sprite = tile1;
			else if (mydigit == 2)
				sp.sprite = tile2;
			else if (mydigit == 3)
				sp.sprite = tile3;
			else if (mydigit < 0){
				throw new System.Exception("wow");
			}
		}
	}
}
