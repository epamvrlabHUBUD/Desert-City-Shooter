using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public static int scoreValue = 0;
    Text score;
    
	void Start () {
        score = GetComponent<Text> ();
	}
	
	void Update () {
        score.text = "" + scoreValue;
	}
    
    public void ResetScore() /* NOT WORKING */
    {
        scoreValue = 0;
        score.text = "0";
    }
}