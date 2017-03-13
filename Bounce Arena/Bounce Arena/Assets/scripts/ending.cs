using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ending : MonoBehaviour {

    Button button;
    Text text;
    endingcarriar EC;
    GameManager GM;
    GameObject[] Sroundarray;
    GameObject[] Croundarray;
    // Use this for initialization
    void Start () {
        button = GameObject.Find("Quitbutton").GetComponent<Button>();
        GameObject ECGO = GameObject.Find("Endingcarrier");
        EC = ECGO.GetComponent<endingcarriar>();
        text = GameObject.Find("Quittext").GetComponent<Text>();
        GameObject GMGO = GameObject.Find("GameManager");
        GM = GMGO.GetComponent<GameManager>();
        Sroundarray = new GameObject[3];
        Croundarray = new GameObject[3];
        //find them all and turn them false
        this.setup();
        //set the correct true
        this.score();
        //set the button to the condition of games won by each player
        if (EC.Runnerwins < 3 && EC.Shooterwins < 3)
        {
            text.text = "Continue";
            button.onClick.AddListener(nextRound);

        }
        else if (EC.Runnerwins == 3 || EC.Shooterwins == 3)
        {
            text.text = "Quit";
            button.onClick.AddListener(close);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
       
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
        GM.Threebool = true;
        GM.Twobool = true;
        GM.Onebool = true;
        GM.Gobool = true;
        Application.LoadLevel("test");
    }

    public void setup()
    {
        for (int z = 0; z < 3; z++)
        {

            GameObject sround = GameObject.Find("Sround" +(z+1).ToString());
            Sroundarray[z] = sround;
            sround.SetActive(false);
        }
        for (int j = 0; j < 3; j++)
        {
            GameObject cround = GameObject.Find("Cround" + (j+1).ToString());
            Croundarray[j] = cround;
            cround.SetActive(false);
        }
    }

    public void score()
    {
        for (int z = 0; z < EC.Shooterwins; z++)
        {
            Sroundarray[z].SetActive(true);
        }
        for (int j = 0; j < EC.Runnerwins; j++)
        {
            Croundarray[j].SetActive(true);
        }
    }
}
