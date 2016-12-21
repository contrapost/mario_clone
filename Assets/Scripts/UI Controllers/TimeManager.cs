using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class TimeManager : MonoBehaviour {

	
	public static int time;
	public GameObject usualMusic;
	public GameObject speedUpMusic;
	public static int restTime;
	
	Text text;
	
	// Use this for initialization
	void Awake () {
		text = GetComponent<Text> ();
		text.text = "";
		time = 380 + (int)(Time.time);
		usualMusic.GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayButtonScript.gameLoaded) {
			restTime = time - (int)(Time.time);
			text.text = "" + restTime;
			if (restTime == 60) {
				usualMusic.GetComponent<AudioSource> ().Stop ();
				speedUpMusic.GetComponent<AudioSource> ().Play ();
			}
			if (restTime == 58) {
				usualMusic.GetComponent<AudioSource> ().pitch = 1.2f;
				usualMusic.GetComponent<AudioSource> ().Play ();
			}

			if (restTime == 0)
				Application.LoadLevel ("gameOver");
			if (MarioController.win) {
				usualMusic.GetComponent<AudioSource> ().Stop ();
			}
		} 
	}
}
