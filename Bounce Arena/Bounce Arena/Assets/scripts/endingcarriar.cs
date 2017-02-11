using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class endingcarriar : MonoBehaviour {


    [SerializeField]
    int amountofwins;
    int shooterwins;
    int runnerwins;
    private string winner;
    Text winnertext;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        shooterwins = 0;
        runnerwins = 0;
	}
	
	// Update is called once per frame
	void Update () {
	if(Application.loadedLevelName == "endingscene")
        {
            if (this.runnerwins < 3 && this.shooterwins < 3)
            {
                GameObject winnertextGO = GameObject.Find("EndingText");
                winnertext = winnertextGO.GetComponent<Text>();
                winnertext.text = winner + " has won the round";
            }
            else if(this.runnerwins == 3 || this.shooterwins == 3)
            {
                GameObject winnertextGO = GameObject.Find("EndingText");
                winnertext = winnertextGO.GetComponent<Text>();
                winnertext.text = winner + " has won the game";
            }
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

    public int Shooterwins
    {
        get
        {
            return shooterwins;
        }
        set
        {
            shooterwins = value;
        }
    }
    public int Runnerwins
    {
        get
        {
            return runnerwins;
        }
        set
        {
            runnerwins = value;
        }
    }

}
