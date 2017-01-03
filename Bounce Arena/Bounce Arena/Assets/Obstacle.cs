using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    float halfwidth;
    [SerializeField]
    float border;
    Vector3 location;

    void Start () {
        location = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 Location
    {
        get
        {
            return location;
        }
    }

    public float Halfwidth
    {
        get
        {
            return halfwidth;
        }
    }
    public float Border
    {
        get
        {
            return border;
        }
    }
}
