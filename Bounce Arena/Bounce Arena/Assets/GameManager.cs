using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //1. make obstacles appear

    [SerializeField]
    GameObject ObstacleGO;
    [SerializeField]
    int AmountofObstacle;
    [SerializeField]
    GameObject PlayerGO;
    private Vector3[] ObstacleLocations;
    private float Arenahalfwidth;
    private float Arenahalfheight;
    private float Obstaclehalfwidth;
    private float playerradius;
    private Player player;
	// Use this for initialization
	void Start () {
        //find how big the arena is 
        GameObject ArenaGO = GameObject.Find("Arena_test");
        Arena Arena = ArenaGO.GetComponent<Arena>();
        player = PlayerGO.GetComponent<Player>();
        playerradius = player.Radius;
        Arenahalfheight = Arena.height / 2;
        Arenahalfwidth = Arena.width / 2;
        float NegativeArenahalfheight = Arenahalfheight * -1.0f;
        float NegativeArenahalfwidth = Arenahalfwidth * -1.0f;
        ObstacleLocations = new Vector3[AmountofObstacle];
        Obstacle obstacle = ObstacleGO.GetComponent<Obstacle>();
        Obstaclehalfwidth = obstacle.Halfwidth;
        //for the amount of obstacles, put that many on the field
        for (int x = 0; x < AmountofObstacle; x++)
        {
            bool clear = false;
            //find a random x and y 
            float xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
            float ytest = Random.Range(NegativeArenahalfheight, Arenahalfwidth);
            Vector3 posTest = new Vector3(xtest, ytest, -2.0f);
            //see if the x is at a wrong place
            while (clear == false)
            {
                //the fence, Obstacles and the player
                //fence x test
                if (xtest - Obstaclehalfwidth < NegativeArenahalfwidth || xtest + Obstaclehalfwidth > Arenahalfwidth)
                {
                    xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                    posTest.x = xtest;
                }
                else if (ytest - Obstaclehalfwidth < NegativeArenahalfheight || ytest + Obstaclehalfwidth > Arenahalfheight)
                {
                    ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                    posTest.y = ytest;
                }
                //player test
                else if((playerradius+Obstaclehalfwidth)>Vector3.Distance(posTest,player.gameObject.transform.position))
                {
                    //see which one(x or y) is closer and move it 
                    float xresult = Mathf.Abs(xtest - player.gameObject.transform.position.x);
                    float yresult = Mathf.Abs(ytest - player.gameObject.transform.position.y);
                    if(xresult>yresult)
                    {
                        ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                        posTest.y = ytest;
                    }
                    else if (yresult > xresult)
                    {
                        xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                        posTest.x = xtest;
                    }
                    else if(xresult == yresult)
                    {
                        ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                        posTest.y = ytest;
                        xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                        posTest.x = xtest;
                    }
                }
                //Obstacle check
                for(int y =0;y<ObstacleLocations.Length;y++)
                {
                    Vector3 Vector3test = ObstacleLocations[x];
                    if(Vector3test == Vector3.zero)
                    {

                    }
                    else
                    {
                        //check the distance
                        if(Obstaclehalfwidth*2>Vector3.Distance(posTest,Vector3test))
                        {
                            //see which one(x or y) is closer and move it 
                            float xresult2 = Mathf.Abs(xtest - Vector3test.x);
                            float yresult2 = Mathf.Abs(ytest - Vector3test.y);
                            if (xresult2 > yresult2)
                            {
                                ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                                posTest.y = ytest;
                            }
                            else if (yresult2 > xresult2)
                            {
                                xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                                posTest.x = xtest;
                            }
                            else if (xresult2 == yresult2)
                            {
                                ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                                posTest.y = ytest;
                                xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                                posTest.x = xtest;
                            }
                        }
                    }
                }
                clear = true;
                ObstacleLocations[x] = posTest;
            }


            
        }
        for(int z = 0; z<ObstacleLocations.Length;z++)
        {
            Instantiate(ObstacleGO, ObstacleLocations[z], Quaternion.identity);
        }


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
