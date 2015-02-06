using UnityEngine;
using System.Collections;

public class SudokuBoard : MonoBehaviour {
	
	public enum Direction : byte { NONE, UP, DOWN, LEFT, RIGHT };
	
	private class Tile {
		public bool locked;
		public int digit;		
		public Direction direction;
		public bool hasMoved; // used while updating the board
		public Tile(){
			this.locked = false;
			this.digit = 0;
			this.direction = Direction.NONE;
		}
	}
	
	private Tile[,] board;
	public int size = 5;
	
	private float step_interval = 1.0f; // seconds between moves
	private float last_step = 0.0f; // time of last move
	
	// Use this for initialization
	void Start () {
		board = new Tile[size, size];
		for (int r = 0; r < size; r++)
		{
			for (int c = 0; c < size; c++)
			{
				board[r, c] = new Tile();
				if(r == 0){
					board[r,c].direction = Direction.RIGHT;
				}
				else if(r == 4){
					board[r,c].direction = Direction.RIGHT;
				}
				else if(c == 0){
					board[r,c].direction = Direction.UP;
				}
				else if(c == 4){
					board[r,c].direction = Direction.DOWN;
				}

			}
		}

		board[0,1].digit = 1;
		board[0,2].digit = 2;
		board[0,3].digit = 3;

	}
	
	void FixedUpdate () {
		if (Time.fixedTime - last_step > step_interval)
		{
			MoveTiles();
			last_step = Time.fixedTime;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	// check if the board is completely filled (might be invalid!)
	public bool IsFull()
	{
		for (int r = 0; r < size; r++)
		{
			for (int c = 0; c < size; c++)
			{
				if (board[r, c].digit == 0)
					return false;
			}
		}
		return true;
	}
	
	// check if the board is finished correctly
	// requires a complete board (no zeroes)
	// THIS MUST CHANGE IF TILES ARE DESTROYED/REPLACED
	public bool IsValid()
	{
		for (int r = 0; r < size; r++)
		{
			int sumr = 0;
			int sumc = 0;
			for (int c = 0; c < size; c++)
			{
				sumr += board[r, c].digit;
				sumc += board[c, r].digit;
			}
			if (sumr != 6 || sumc != 6) return false;
		}
		return true;
	}
	
	void MoveTiles() {
		
		// mark all tiles that need to move
		for (int i = 0; i < size; i++) 
		{
			for (int j = 0; j < size; j++)
			{			
				if(board[i, j].digit > 0 && board[i, j].direction != Direction.NONE)
				{
					board[i, j].hasMoved = false;
				}
				else
				{
					board[i, j].hasMoved = true;
				}
			}
		}
		
		// make tiles on the edge rotate around
		board[0, 0].direction = Direction.RIGHT;
		board[0, size - 1].direction = Direction.DOWN;
		board[size - 1, size - 1].direction = Direction.LEFT;
		board[size - 1, 0].direction = Direction.UP;
		
		bool changed;
		Debug.Log ("starting move");
		do 
		{
			changed = false;
			for (int r = 0; r < size; r++)
			{
				for (int c = 0; c < size; c++)
				{
					Tile tile = board[r, c];
					// if the tile hasn't moved yet and wants to
					if (!tile.hasMoved)
					{	
						int rdst = r + (tile.direction == Direction.UP ? -1 : (tile.direction == Direction.DOWN ? 1 : 0));
						int cdst = c + (tile.direction == Direction.LEFT ? -1 : (tile.direction == Direction.RIGHT ? 1 : 0));
						
						// stop tiles from moving out of the board
						if ((rdst < 0 || rdst > size - 1 || (c > 0 && c < size - 1 && (rdst == 0 || rdst == size - 1)))
						    || (cdst < 0 || cdst > size - 1 || (r > 0 && r < size - 1 && (cdst == 0 || cdst == size - 1))))
						{
							tile.direction = Direction.NONE;
							tile.hasMoved = true;
						}
						// move a tile if its destination is empty
						else if (board[rdst, cdst].digit == 0)
						{
							// move the tile to its destination
							board[rdst, cdst].digit = tile.digit;
							board[rdst, cdst].direction = tile.direction;
							board[rdst, cdst].locked = tile.locked;
							// mark it as fixed
							board[rdst, cdst].hasMoved = true;
							// clear its old location so another tile can move there
							board[r, c].digit = 0;
							board[r, c].hasMoved = true;
							changed = true; // iterate until all dependencies have resolved
						}
					}
				}
			}
		} while (changed);
		Debug.Log ("finished move");
	}
	
	public int GetTileDigit(int r, int c)
	{
		if (0 <= r && r < size && 0 <= c && c < size)
			return board[r, c].digit;
		else
			throw new System.Exception("E_IndexOutOfRange");
	}
	
	public void SetDirection(int r, int c, Direction d)
	{
		if (0 <= r && r < size && 0 <= c && c < size)
			board[r, c].direction = d;
		else
			throw new System.Exception("E_IndexOutOfRange");
	}
	
	public void SlideTopEdge()
	{
		for (int c = 1; c < size - 1; c++)
		{
			SetDirection(0, c, Direction.DOWN);
		}
	}
	
	public void SlideBottomEdge()
	{
		for (int c = 1; c < size - 1; c++)
		{
			SetDirection(size - 1, c, Direction.UP);
		}
	}
	
	public void SlideLeftEdge()
	{
		for (int r = 1; r < size - 1; r++)
		{
			SetDirection(r, 0, Direction.RIGHT);
		}
	}
	
	public void SlideRightEdge()
	{
		for (int r = 1; r < size - 1; r++)
		{
			SetDirection(r, size - 1, Direction.LEFT);
		}
	}
}
