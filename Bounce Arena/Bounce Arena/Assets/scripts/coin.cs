using UnityEngine;
using System.Collections;

public class coin : MonoBehaviour {

    [SerializeField]
    float radius;
    Vector3 location;
	// Use this for initialization
	void Start () {
        location = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public float Radius
    {
        get
        {
            return radius;
        }
    }
    public Vector3 Location
    {
        get
        {
            return location;
        }
    }

}
