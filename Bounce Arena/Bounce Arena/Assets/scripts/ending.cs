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
        Sroundarray = GameObject.FindGameObjectsWithTag("Sround");
        Croundarray = GameObject.FindGameObjectsWithTag("Cround");
        for(int x =0;x<Sroundarray.Length;x++)
        {
            Sroundarray[x].SetActive(false);
        }
        for(int z = 0;z<EC.Shooterwins;z++)
        {
            Sroundarray[z].SetActive(true);
        }
        for(int y =0;y<Croundarray.Length;y++)
        {
            Croundarray[y].SetActive(false);
        }
        for(int j =0;j<EC.Runnerwins;j++)
        {
            Croundarray[j].SetActive(true);
        }
        
        
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
        Application.LoadLevel("test");
    }
}
