using UnityEngine;
using System.Collections;


public class NumberGeneration : MonoBehaviour {
	
	public Sprite tile1;
	public Sprite tile2;
	public Sprite tile3; 
	
	//public GameGod god;
	
	SpriteRenderer sp;
	//public CountCarry god;
	
	// Use this for initialization
	void Start () {
		sp = GetComponent<SpriteRenderer>();
		
		GameObject godd = GameObject.Find("GameGod");
		CountCarry god = godd.GetComponent<CountCarry>();
		
		int r = Random.Range (0, 3);
		
		print(r);
		
		if(r == 0 && god.count1 == 4){
			r = Random.Range (1,3);
		}
		else if(r == 1 && god.count2 == 4){
			int ran = Random.Range (0, 2);
			if(ran == 0) r = 0;
			else if(ran == 1) r = 2;
		}
		else if(r == 2 && god.count3 == 4){
			r = Random.Range (0, 2);
		}
		
		if(r == 0){
			sp.sprite = tile1;
			god.count1++;
		}
		else if(r == 1){
			sp.sprite = tile2;
			god.count2++;
		}
		else if(r == 2){
			sp.sprite = tile3;
			god.count3++;
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
