using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

	public static float FADE_DURING = .1f;

	/// <summary>
	/// The current season.
	/// </summary>
	private int season;
	public int Season
	{
		get{ return season; }
		set{ season = value; }
	}

	/// <summary>
	/// The current year.
	/// </summary>
	private int year;
	public int Year
	{
		get{ return year; }
		set{ year = value; }
	}

//	public class SeansonEventArgs : EventArgs {
//		public readonly int season;
//		public readonly int year;
//		public SeansonEventArgs(int season, int year) {
//			this.season = season;
//			this.year = year;
//		}
//	}

	public event EventHandler ChangeSeason;


	private static GameController instance;

	public static GameController GetInstance()
	{
		return instance;
	}

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		ChangeSeason (this, EventArgs.Empty);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space)) {

			Debug.Log(this);
			season ++;
			if (season > 3) {
				season = 0;
				year++;
			}
			ChangeSeason (this, EventArgs.Empty);
		}

	
	}

//	void OnChangeSeason(object sender, SeansonEventArgs e)
//	{
//		Debug.Log (e.season + " " + e.year);
//	}
}
