using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    float radius;
    [SerializeField]
    float width;
    [SerializeField]
    float height;
    Vector3 direction;
    bool infield;
    float left, right, top, bottom;
    int bounce;


	// Use this for initialization
	void Start ()
    {
        GameObject ShooterObject = GameObject.Find("Shooter_test");
        Shooter shooter = ShooterObject.GetComponent<Shooter>();
        this.direction = shooter.transform.right;
        infield = false;
        left =  width / 2 * -1;
        right =  width / 2;
        top = height / 2;
        bottom =  height / 2 * -1;
        bounce = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position += direction * speed;
        //turn true when it is in the field
        if (infield == false)
        {
            if (this.transform.position.x - radius > left && this.transform.position.x + radius < right && this.transform.position.y - radius > bottom && this.transform.position.y+radius < top)
            {
                infield = true;
            }
        }
        //bounce the ball
        else if (infield == true)
        {
            if (bounce < 4)
            {
                if (this.transform.position.x - radius < left  )
                {
                    this.direction.x = this.direction.x * -1.0f;
                    bounce++;
                }
                 if(this.transform.position.x + radius > right)
                {
                    this.direction.x = this.direction.x * -1.0f;
                    bounce++;
                }
                 if (this.transform.position.y + radius > top)
                {
                    this.direction.y = this.direction.y * -1.0f;
                    bounce++;
                }
                 if (this.transform.position.y - radius < bottom)
                {
                    this.direction.y = this.direction.y * -1.0f;
                    bounce++;
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
	}
}
