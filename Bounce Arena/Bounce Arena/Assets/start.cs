using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class start : MonoBehaviour {

    [SerializeField]
    string scenetransitionname;
    Button button;
	// Use this for initialization
	void Start () {
        button = GameObject.Find("StartButton").GetComponent<Button>();
        button.onClick.AddListener(Scenechange);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Scenechange()
    {
        Application.LoadLevel(scenetransitionname);
    }

}
