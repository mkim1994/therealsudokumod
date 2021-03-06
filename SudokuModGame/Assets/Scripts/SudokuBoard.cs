﻿using UnityEngine;
using System.Collections;

public class SudokuBoard : MonoBehaviour {
	
	private int[,] board;
	private int[,] ring;
	private int size = 5;
	private int fullmask;
	
	public float step_interval = 1.0f; // seconds between moves
	
	void Start () {
		board = new int[size, size];
		for (int r = 0; r < size; r++)
		{
			for (int c = 0; c < size; c++)
			{
				board[r,c] = 0;
			}
		}

		ring = new int[4, size - 1];
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < size - 1; j++)
			{
				ring[i, j] = 0;
			}
		}

		fullmask = 0;
		for (int i = 1; i <= size; i++)
		{
			fullmask |= (1 << i);
		}

		// start the regular board updates
		InvokeRepeating("StepAll", 0, step_interval);
	}

	// put things to happen at a steady rate in here
	// invoked from Start above
	void StepAll()
	{
		// example code for basic game
		StepRingClockwise(); // rotate one slot
		ring[0, 0] = 0; // keep the top-left corner empty (destroy tiles that go all the way around)
		ring[0, 1] = ((int) (Random.value * (size - 1))) + 1; // spawn a random tile out of the top-left corner
		// DERP TEST - note there seems to be a bug with the firing. prototype things and help me find it :)
		FireRightEdge();
	}
	
	void FixedUpdate () {
	}
	
	// Update is called once per frame
	void Update () {
		// don't put any code here, unless you are adding graphical effects to the board maybe
	}
	
	// check if the board is completely filled
	public bool IsFull()
	{
		for (int r = 0; r < size; r++)
		{
			for (int c = 0; c < size; c++)
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
		for (int i = 0; i < size; i++)
		{
			int rowmask = 0;
			int colmask = 0;

			for (int j = 0; j < size; j++)
			{
				rowmask ^= (1 << board[i,j]);
				colmask ^= (1 << board[j,i]);
			}

			if ( (rowmask != fullmask) || (colmask != fullmask) )
				return false;
		}
		return true;
	}

	// move all tiles in the ring - does not handle spawning / despawning
	public void StepRingClockwise()
	{
		int tmp = ring[3, 3];
		for (int i = 3; i >= 0; i--)
		{
			for (int j = 3; j > 0; j--)
			{
				ring[i, j] = ring[i, j - 1];
			}
			ring[i, 0] = (i > 0) ? ring[i - 1, 3] : tmp;
		}
	}

	public void StepRingCounterClockwise()
	{	
		int tmp = ring[0, 0];
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				ring[i, j] = ring[i, j + 1];
			}
			ring[i, 3] = (i < 3) ? ring[i + 1, 0] : tmp;
		}
	}

	public int GetBoardDigit(int row, int col)
	{
		return board[row, col];
	}

	public int GetRingDigit(int side, int idx)
	{
		return ring[side, idx];
	}
	
	// reads the value from a ring tile, and puts it as far into the board as it can go
	// returns true if the piece was moved; if the piece was blocked in the outer row,
	// returns false and does not modify the ring
	//
	// you can trigger animations based on the return value!
	public bool FireRingTile(int side, int idx)
	{
		if (ring[side, idx] == 0) return false; // that ring tile is empty!

		if (idx == 0) return false; // I can't move corner pieces!

		int row, col, dx, dy;

		if (side == 0)
		{
			row = 0;
			col = idx - 1;
			dy = 1;
			dx = 0;
		}
		else if (side == 2) 
		{
			row = size - 1;
			col = size - 1 - idx;
			dy = -1;
			dx = 0;
		}
		else if (side == 1)
		{
			row = idx - 1;
			col = size - 1;
			dx = -1;
			dy = 0;
		}
		else if (side == 3)
		{
			row = size - 1 - idx;
			col = 0;
			dx = 1;
			dy = 0;
		}
		else return false; // invalid side or idx!

		if (board[row, col] != 0) return false; // the first slot is blocked!

		// find the farthest empty slot in that direction
		for (int i = 0; i < size - 1; i++)
		{
			if (board[row + dy, col + dx] == 0)
			{
				row += dy;
				col += dx;
			}
		}

		board[row, col] = ring[side, idx];
		ring[side, idx] = 0;
		return true;
	}


	// utility functions for prototyping whole-side swipes
	public void FireTopEdge()
	{
		for (int i = 1; i < 4; i++)
			FireRingTile(0, i);
	}
	public void FireRightEdge()
	{
		for (int i = 1; i < 4; i++)
			FireRingTile(1, i);
	}
	public void FireBottomEdge()
	{
		for (int i = 1; i < 4; i++)
			FireRingTile(2, i);
	}
	public void FireLeftEdge()
	{
		for (int i = 1; i < 4; i++)
			FireRingTile(3, i);
	}
}
