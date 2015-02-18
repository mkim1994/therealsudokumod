using System;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
	public float timeLeft = 50.0f;
	
	public void Update()
	{
		timeLeft -= Time.deltaTime;
		
		if (timeLeft <= 0.0f)
		{
			// End the level here.
			guiText.text = "You ran out of time";
		}
		else
		{
			guiText.text = "Time left = " + (int)timeLeft + " seconds";
		}
		
	}
	
}
