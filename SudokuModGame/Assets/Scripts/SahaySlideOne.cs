using UnityEngine;
using System.Collections;

public class SahaySlideOne : MonoBehaviour {

	public BenSudokuBoard board;

	void Start () {
		board = GetComponentInParent<BenSudokuBoard>();
	}

	public void SlideOne(string name)
	{
		string r = name.Substring (0, 1);
		string i = name.Substring (1, 1);
		int row = int.Parse (r);
		int idx = int.Parse (i);

		Debug.Log (row);
		Debug.Log (idx);

		board.FireRingTile (row, idx);
	}
}