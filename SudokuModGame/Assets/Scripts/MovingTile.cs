using UnityEngine;
using UnityEngine.UI;
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

	public Image sprite;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		// todo: make this an increasing ramp, not smooth slowdown
		float delta = (Time.realtimeSinceStartup - tickTime) / step_interval;
		transform.position = Vector3.Lerp(old_position, next_position, delta * delta);

		// fast fade in for new pieces
		if (slot == 0)
		{
			transform.SetAsLastSibling(); // make this the last thing drawn so it goes on the bottom
			sprite.color = new Color(1, 1, 1, Mathf.Min(2 * delta, 1));
		}

		// fade out at the end of the cycle
		else if (slot == (board.size + 1) * 4 - 1)
		{
			transform.SetAsFirstSibling(); // make this the first drawn, goes on bottom
			sprite.color = new Color(1, 1, 1, (1 - delta));

			if (delta > 0.95)
				sprite.enabled = false;
		}
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
