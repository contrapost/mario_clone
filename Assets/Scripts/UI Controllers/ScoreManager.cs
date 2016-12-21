using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int score;

	Text text;

	// Use this for initialization
	void Awake () {
		text = GetComponent<Text> ();
		score = PlayerPrefs.GetInt("CurrentScore", ScoreManager.score);
	}
	
	// Update is called once per frame
	void Update () {
		if (score == 0)
			text.text = "000000";
		if (score < 1000 && score > 0)
		text.text = "000" + score;
		if (score >= 1000 && score < 10000)
			text.text = "00" + score;
		if (score >= 10000 && score < 100000)
			text.text = "0" + score;
		if (score >= 100000)
			text.text = "" + score;
	}
}
