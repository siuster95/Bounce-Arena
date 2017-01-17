using UnityEngine;
using System.Collections;

public class OAmanager : MonoBehaviour {

    [SerializeField]
    int Amounttowin;
    int runnerwins;
    int shooterwins;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        runnerwins = 0;
        shooterwins = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
