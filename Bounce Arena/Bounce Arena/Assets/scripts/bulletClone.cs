using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class bulletClone :  bullet {

    
	// Use this for initialization
	protected override void Start ()
    {
        //find the shooter that is most closest to us so that I can grab that direction
        GameObject[] CloneObjects = GameObject.FindGameObjectsWithTag("Clone");
        float distance = -1.0f;
        Clone shooter = null;
        for(int x =0;x<CloneObjects.Length;x++)
        {
            float tempdist = Vector3.Distance(this.transform.position, CloneObjects[x].transform.position);
            // if this is the first....
            if(distance == -1.0f)
            {
                distance = tempdist;
                shooter = CloneObjects[x].GetComponent<Clone>();
            }
            else
            {
                //change if the distance is smaller
                if(tempdist<distance)
                {
                    distance = tempdist;
                    shooter = CloneObjects[x].GetComponent<Clone>();
                }
            }
        }
        this.direction = shooter.transform.right;
        this.ObstacleList = GameObject.FindObjectsOfType<Obstacle>();
        GameObject ArenaGO = GameObject.Find("Arena_test");
        this.arena = ArenaGO.GetComponent<Arena>();
        this.infield = false;
        this.left =  arena.width / 2 * -1;
        this.right =  arena.width / 2;
        this.top = arena.height / 2;
        this.bottom =  arena.height / 2 * -1;
        this.bounce = 0;
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        this.transform.position += this.direction * this.speed;
        //turn true when it is in the field
        if (this.infield == false)
        {
            if (this.transform.position.x - radius > left && this.transform.position.x + radius < right && this.transform.position.y - radius > bottom && this.transform.position.y+radius < top)
            {
                infield = true;
            }
        }
        //bounce the ball
        else if (this.infield == true)
        {
            if (this.bounce < 4)
            {
                //bounce off edges of arena
                if (this.transform.position.x - radius < left  )
                {
                    this.direction.x = this.direction.x * -1.0f;
                    this.bounce++;
                }
                 if(this.transform.position.x + radius > right)
                {
                    this.direction.x = this.direction.x * -1.0f;
                    this.bounce++;
                }
                 if (this.transform.position.y + radius > top)
                {
                    this.direction.y = this.direction.y * -1.0f;
                    this.bounce++;
                }
                 if (this.transform.position.y - radius < bottom)
                {
                    this.direction.y = this.direction.y * -1.0f;
                    this.bounce++;
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

   


}


