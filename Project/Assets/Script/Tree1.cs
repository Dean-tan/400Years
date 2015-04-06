using UnityEngine;
using System.Collections;
using System;

public class Tree1 : MonoBehaviour {

	private int ARRAY_LENS = 29;

	private GameController controller;

	public Sprite[] spring_sprites;
	public Sprite[] summer_sprites;
	public Sprite[] autumn_sprites;
	public Sprite[] winter_sprites;

	public int age = 5;
	private int lastYear = 0;


	private SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		controller = GameController.GetInstance();
		controller.ChangeSeason += this.OnChangeSeason;

		sprite = GetComponent<SpriteRenderer> ();

		UpdateSprite (age);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnChangeSeason(object sender, EventArgs e)
	{
		int addAge = controller.Year - lastYear;
		lastYear = controller.Year;

		age += addAge;

		UpdateSprite (age);

	}

	void UpdateSprite(int age)
	{
//		Debug.Log ("UpdateSprite age=" + age);
		if (age > ARRAY_LENS) age = 0;
		
		if (controller.Season == 0){
			sprite.sprite = spring_sprites[age];
		}
		
		if (controller.Season == 1){
			sprite.sprite = summer_sprites[age];
		}
		
		if (controller.Season == 2){
			sprite.sprite = autumn_sprites[age];
		}
		
		if (controller.Season == 3){
			sprite.sprite = winter_sprites[age];
		}
	}

	void SetAge(String value)
	{
		age = Convert.ToInt32 (value);
		UpdateSprite (age);
	}
}
