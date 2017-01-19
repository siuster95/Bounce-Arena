using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ending : MonoBehaviour {

    Button button;
    Text text;
    endingcarriar EC;
    GameManager GM;
    // Use this for initialization
    void Start () {
        button = GameObject.Find("Quitbutton").GetComponent<Button>();
        GameObject ECGO = GameObject.Find("Endingcarrier");
        EC = ECGO.GetComponent<endingcarriar>();
        text = GameObject.Find("Quittext").GetComponent<Text>();
        GameObject GMGO = GameObject.Find("GameManager");
        GM = GMGO.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EC.Runnerwins < 3 && EC.Shooterwins < 3)
        {
            text.text = "Continue";
            button.onClick.AddListener(nextRound);

        }
        else if(EC.Runnerwins == 3 || EC.Shooterwins == 3)
        {
            text.text = "Quit";
            button.onClick.AddListener(close);
        }
    }
    public void close()
    {
        Application.Quit();
    }

    public void nextRound()
    {
        GM.CoinNumber = 0;
        GM.Coinspawnbool = false;
        GM.Obstaclespawn = false;
        Application.LoadLevel("test");
    }
}
