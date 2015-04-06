using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameMap: MonoBehaviour {

	public Sprite[] seasons;
	public Sprite[] waters;

	private GameController controller;
	private int currentSeason = 0;
	private float fade = 1f;

	private MeshRenderer renderer1;
	private MeshRenderer renderer2;

	private MeshRenderer waterRrenderer;
	private MeshRenderer iceRrenderer;

	private Transform iceTransform;

	private bool isChange = false;

	// Use this for initialization
	void Start () {
		controller = GameController.GetInstance();
		controller.ChangeSeason += this.OnChangeSeason;

		MeshRenderer[] meshs = GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer mesh in meshs) {
			Debug.Log(mesh.gameObject.name);
			if (mesh.gameObject.name == "ground1_ground_summer_0")
			{
				renderer1 = mesh;
			}
			if (mesh.gameObject.name == "ground2_ground_summer_0")
			{
				renderer2 = mesh;
			}
			if (mesh.gameObject.name == "water1_water_winter_0")
			{
				waterRrenderer = mesh;
			}
			if (mesh.gameObject.name == "ice_water_winter_0")
			{
				iceRrenderer = mesh;
			}
		}

		Transform[] trans = GetComponentsInChildren<Transform> ();
		foreach (Transform tran in trans) {
			if(tran.name == "Tile Collisions ice")
			{
				iceTransform = tran;
			}
		}

		renderer1.material.mainTexture = seasons[controller.Season].texture;
		renderer2.material.mainTexture = seasons[controller.Season].texture;
		waterRrenderer.material.mainTexture = waters[controller.Season].texture;

	}
		

	// Update is called once per frame
	void Update () {
		if (isChange && fade > 0) {
			fade -= GameController.FADE_DURING;
			renderer2.material.color = new Color(1f, 1f, 1f, fade);
		}
	}

	void OnChangeSeason(object sender, EventArgs e)
	{
					
		renderer2.material.mainTexture = renderer1.material.mainTexture;
		renderer1.material.mainTexture = seasons [controller.Season].texture;
		waterRrenderer.material.mainTexture = waters[controller.Season].texture;

		renderer1.material.color = new Color (1f, 1f, 1f, 1f);
		renderer2.material.color = new Color (1f, 1f, 1f, 1f);
			

		fade = 1f;
		isChange = true;

		// when winter
		if (controller.Season == 3) {
			iceTransform.gameObject.SetActive (true);
			iceRrenderer.enabled = true;
		} else {
			iceTransform.gameObject.SetActive (false);
			iceRrenderer.enabled = false;

		}
			
	}
}