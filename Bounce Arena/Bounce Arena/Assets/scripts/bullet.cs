using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    Obstacle[] ObstacleList;
    Arena arena;
	// Use this for initialization
	void Start ()
    {
        GameObject ShooterObject = GameObject.Find("Shooter_test");
        Shooter shooter = ShooterObject.GetComponent<Shooter>();
        this.direction = shooter.transform.right;
        ObstacleList = GameObject.FindObjectsOfType<Obstacle>();
        GameObject ArenaGO = GameObject.Find("Arena_test");
        arena = ArenaGO.GetComponent<Arena>();
        infield = false;
        left =  arena.width / 2 * -1;
        right =  arena.width / 2;
        top = arena.height / 2;
        bottom =  arena.height / 2 * -1;
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
                //bounce off edges of arena
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
                //bounce off obstacles
                this.bounceOffobstacle();
                this.hitplayer();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
	}

    public void bounceOffobstacle()
    {
        for(int x =0;x<ObstacleList.Length;x++)
        {
            Obstacle Obstacle = ObstacleList[x];
            //find the Vector between the bullets and obstacles
            Vector3 Displacement = this.transform.position - Obstacle.Location;
            //find the closestpoint
            Vector3 Closestpoint = Displacement;
            float negativeHalfwidth = -1 * Obstacle.Halfwidth;
            if (Closestpoint.x > Obstacle.Halfwidth)
            {
                Closestpoint.x = Obstacle.Halfwidth;
            } 
            else if(Closestpoint.x< negativeHalfwidth)
            {
                Closestpoint.x = negativeHalfwidth;
            }
            if (Closestpoint.y > Obstacle.Halfwidth)
            {
                Closestpoint.y = Obstacle.Halfwidth;
            }
            else if (Closestpoint.y < negativeHalfwidth)
            {
                Closestpoint.y = negativeHalfwidth;
            }
            //find the location for the closest point on box
            Vector3 Closestpointlocation = Obstacle.Location + Closestpoint;
            //find the distance between the bullet center and closestpoint
            float distancebetweenBandO = Vector3.Distance(Closestpointlocation, this.transform.position);
            distancebetweenBandO = distancebetweenBandO + Obstacle.Border;
            if (this.radius > distancebetweenBandO)
            {
                bounce++;
                float xtest = Mathf.Abs(this.transform.position.x - Obstacle.Location.x);
                float ytest = Mathf.Abs(this.transform.position.y - Obstacle.Location.y);
                if(xtest>ytest)
                {
                    this.direction.x = this.direction.x * -1;
                }
                else if(ytest>xtest)
                {
                    this.direction.y = this.direction.y * -1;
                }
            }
        }
    }

    public void hitplayer()
    {
        GameObject playerGO = GameObject.Find("Player_test");
        Player player = playerGO.GetComponent<Player>();
        float distance = Vector3.Distance(this.transform.position, playerGO.transform.position);
        if(distance<(this.radius+player.Radius))
        {
            GameObject HPicon = GameObject.Find("HP" + player.Hp);
            HPicon.SetActive(false);
            player.Hp = player.Hp - 1;
            Destroy(this.gameObject);
        }
    }


}


