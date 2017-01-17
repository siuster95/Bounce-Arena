using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ending : MonoBehaviour {

    Button button;
    Text text;
    endingcarriar EC;
    // Use this for initialization
    void Start () {
        button = GameObject.Find("Quitbutton").GetComponent<Button>();
        GameObject ECGO = GameObject.Find("Endingcarrier");
        EC = ECGO.GetComponent<endingcarriar>();
        text = GameObject.Find("Quittext").GetComponent<Text>();
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
        Application.LoadLevel("test");
    }
}
