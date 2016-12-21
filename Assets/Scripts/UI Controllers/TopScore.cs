using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopScore : MonoBehaviour {

    public static int topScore;

    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        topScore = PlayerPrefs.GetInt("HighScore", topScore);
    }

    // Update is called once per frame
    void Update()
    {
		if (topScore < 100) {
			text.text = "TOP = 000000" + topScore;
		} else if (topScore >= 100 && topScore < 1000) {
			text.text = "TOP = 0000" + topScore;
		} else if (topScore >= 1000 && topScore < 10000) {
			text.text = "TOP = 000" + topScore;
		} else if (topScore >= 10000 && topScore < 100000) {
			text.text = "TOP = 00" + topScore;
		} else if (topScore >= 100000 && topScore < 1000000) {
			text.text = "TOP = 0" + topScore;
		} else if (topScore >= 1000000) {
			text.text = "TOP = " + topScore;
		}
    }
}
