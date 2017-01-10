using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class endingcarriar : MonoBehaviour {

    private string winner;
    Text winnertext;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	if(Application.loadedLevelName == "endingscene")
        {
            GameObject winnertextGO = GameObject.Find("EndingText");
            winnertext = winnertextGO.GetComponent<Text>();
            winnertext.text = winner + " has won";
        }
	}

    public string Winner
    {
        get
        {
            return winner;
        }
        set
        {
            winner = value;
        }
    }

}
