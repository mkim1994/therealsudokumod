using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public Sprite[] sprites;
	public Vector3[] board_positions; // seems like unity cant handle 2d arrays
	public Vector3[] ring_positions;

	public MovingTile prefab; // prefab to create clickable tiles
	private List<MovingTile> tiles; // references to active tiles
	private List<MovingTile> board_tiles; // dead tiles in the board
	public int spawn_slot = 1; // the ring slot of the spawn point

	public int size = 5;
	private int[,] board;	

	private int step_count = 0;
	public float step_interval = 1.0f;
	public float min_step_interval = 0.3f; //maximum spawn and rotate speed
	public float step_acceleration = 0.95f;
	public AudioClip blarg;

	// Use this for initialization
	void Start () {
		// create the board
		board = new int[size, size];
		for (int r = 0; r < size; r++)
		{
			for (int c = 0; c < size; c++)
			{
				board[r,c] = 0;
			}
		}

		tiles = new List<MovingTile>();
		board_tiles = new List<MovingTile>();

		// start the game
		Invoke("Step", step_interval);
	}
	
	// Update is called once per frame
	void Update () {
		// LEAVE EMPTY
	}

	// step the board state, spawning and updating tiles
	void Step()
	{
		step_count += 1;

		// signal all the tiles to rotate forward
		foreach (MovingTile tile in tiles)
		{
			tile.slot = (tile.slot + 1) % (4 * (size + 1));
			tile.transform.position = ring_positions[tile.slot];
			tile.old_position = ring_positions[tile.slot];
			if (tile.slot == 0)
			{
				StartCoroutine(tile.Kill());
			}
			else
			{
				tile.next_position = ring_positions[(tile.slot + 1) % (4 * (size + 1))];
			}				
		}

		// remove tiles that made it all the way around
		tiles.RemoveAll(tile => tile.slot == 0);

		// spawn a new tile on odd steps
		if (step_count % 2 == 1)
		{
			MovingTile tile = (MovingTile) Instantiate(prefab, ring_positions[spawn_slot], Quaternion.identity);
			tile.board = this; // give the tile a reference to query the board

			tile.slot = spawn_slot;
			tile.old_position = ring_positions[spawn_slot];
			tile.next_position = ring_positions[spawn_slot + 1];
			tile.digit = Random.Range(1, size);

			SpriteRenderer tileSprite = tile.GetComponent<SpriteRenderer>();
			tileSprite.sprite = sprites[tile.digit];

			tiles.Add(tile);
		}

		if (step_interval > min_step_interval && step_count > 15) 
		{
			step_interval = step_interval * step_acceleration;
		}

		audio.PlayOneShot(blarg, 0.7F);

		Invoke ("Step", step_interval);		
	}

	public int Size()
	{
		return size;
	}

	// argument: a "slot" index for a tile moving around the board
	// returns: (i need to both say yes/no can fire and give a transform)
	public bool FireTile(MovingTile tile)
	{
		// reject corners
		if (tile.slot % (size + 1) == 0)
		{
			return false;
		}

		// find the tile's position in grid coords
		int m = tile.slot % (size + 1), d = tile.slot / (size + 1);
		int x, y, dx, dy;
		if (d == 0)
		{
			x = m - 1;
			y = -1;
			dx = 0;
			dy = 1;
		}
		else if (d == 1)
		{
			x = size;
			y = m + 1;
			dx = -1;
			dy = 0;
		}
		else if (d == 2)
		{
			x = size - m;
			y = size + 1;
			dx = 0;
			dy = -1;
		}
		else if (d == 3)
		{
			x = -1;
			y = size - m;
			dx = 1;
			dy = 0;
		}
		else
		{
			return false; // error
		}

		int tx = x + dx;
		int ty = y + dy;

		// check if the first row is blocked (tile stays in ring)
		if (board[x + dx, y + dy] != 0) return false;

		// find the final position
		while (tx + dx >= 0 && tx + dx < size 
			&& ty + dy >= 0 && ty + dy < size
			&& (board[tx + dx, ty + dy] == 0))
		{
			tx += dx;
			ty += dy;
		}

		// remove the tile from the ring and set its goal position
		board[tx, ty] = tile.digit;
		tile.slot = -1;
		tile.next_position = board_positions[ty * size + tx];
		tiles.Remove(tile);
		board_tiles.Add(tile);
		return true;
	}

	// check if the board is completely filled
	public bool IsFull()
	{
		for (int r = 0; r < size - 2; r++)
		{
			for (int c = 0; c < size - 2; c++)
			{
				if (board[r, c] == 0)
					return false;
			}
		}
		return true;
	}

	// check if the outside edge of the board is full
	// (this means the player can't continue, even if the middle is not filled!)
	public bool IsSurrounded()
	{
		for (int i = 0; i < size; i++)
		{
			if ( (board[0, i] == 0) || (board[i, 0] == 0) || (board[size - 1, i] == 0) || (board[i, size - 1] == 0) )
				return false;
		}
		return true;
	}
	
	// check if the board is finished correctly
	public bool IsValid()
	{
		for (int myrow = 0; myrow < size - 2; myrow++)
		{
			for (int mycol = 0; mycol < size - 2; mycol++)
			{
				for (int row = 0; row < size - 2; row++)
				{
					for (int col = 0; col < size - 2; col++)
					{
						if (col != mycol || row != myrow) // dont check self
						{
							if (col != mycol && (board[myrow,col] == board[myrow,mycol])){return false;}
							else if (row != myrow && (board[row,mycol] == board[myrow,mycol])){return false;}
						}
					}
				}

			}
		}
		return true;
	}

}
