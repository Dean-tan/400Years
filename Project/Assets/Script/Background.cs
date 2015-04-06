using UnityEngine;
using System.Collections;
using System;


public class Background : MonoBehaviour {

	public Sprite[] backgrounds;
	private SpriteRenderer renderer1;
	private SpriteRenderer renderer2;

	private float fade1 = 1f;
	private float fade2 = 0f;
	private bool isChange = false;

	// Use this for initialization
	void Start () {
		GameController.GetInstance ().ChangeSeason += this.OnChangeSeason;

		foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>()) {
			Debug.Log( spriteRenderer.name);
			if (spriteRenderer.name == "background1")
			{
				renderer1 = spriteRenderer;
			}

			if (spriteRenderer.name == "background2")
			{
				renderer2 = spriteRenderer;
			}
		}

		renderer1.color = new Color (1, 1, 1, fade1);
		renderer2.color = new Color (1, 1, 1, fade2);
	}
	
	// Update is called once per frame
	void Update () {
		if (isChange) {
			fade1 -= GameController.FADE_DURING;;
			fade2 += GameController.FADE_DURING;;
			renderer1.color = new Color (1, 1, 1, fade1);
			renderer2.color = new Color (1, 1, 1, fade2);

			if (fade2 >= 1f) isChange = false;
		}
	}

	void OnChangeSeason(object sender, EventArgs e)
	{
		GameController controller = (GameController)sender;
		Debug.Log (controller.Season + " " + controller.Year);

		renderer1.sprite = renderer2.sprite;
		fade1 = 1f;
		fade2 = 0f;
		renderer1.color = new Color (1, 1, 1, fade1);
		renderer2.color = new Color (1, 1, 1, fade2);

		renderer2.sprite = backgrounds [controller.Season];

		isChange = true;
	}
}
