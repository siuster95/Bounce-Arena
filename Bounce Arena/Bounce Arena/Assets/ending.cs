using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ending : MonoBehaviour {

    Button button;

    // Use this for initialization
    void Start () {
        button = GameObject.Find("Quitbutton").GetComponent<Button>();
        button.onClick.AddListener(close);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void close()
    {
        Application.Quit();
    }
}
