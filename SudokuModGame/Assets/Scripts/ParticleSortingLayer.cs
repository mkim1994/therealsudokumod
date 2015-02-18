using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour {
	
	void Start ()
	{
		// Set the sorting layer of the particle system.
		//particleSystem.emissionRate = 0.1f;
		particleSystem.renderer.sortingLayerName = "Background";
		particleSystem.renderer.sortingOrder = 2;

		/*Color c = particleSystem.startColor;
		c.a = 0.0f;
	particleSystem.startColor = c;*/
		ParticleAnimator pA = GetComponent<ParticleAnimator>();
		Color[] colors = pA.colorAnimation;

		colors[0].a = 0.0f;
		colors[1].a = 0.2f;
		colors[2].a = 0.4f;
		colors[3].a = 0.6f;
		colors[4].a = 1.0f;





		//particleSystem.renderer.material.color = Color.Lerp ();
	}
}