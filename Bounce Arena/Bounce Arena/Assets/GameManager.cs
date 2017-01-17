using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //1. make obstacles appear

    [SerializeField]
    GameObject ObstacleGO;
    [SerializeField]
    GameObject CoinGO;
    [SerializeField]
    int AmountofObstacle;
    [SerializeField]
    GameObject PlayerGO;
    [SerializeField]
    int amountofCoins;
    private Vector3[] CoinsLocations;
    private Vector3[] ObstacleLocations;
    private float Arenahalfwidth;
    private float Arenahalfheight;
    private float Obstaclehalfwidth;
    private float NegativeObstaclehalfwidth;
    private float Coinradius;
    private float playerradius;
    private Player player;
    private float NegativeArenahalfheight;
    private float NegativeArenahalfwidth;
    private int coinNumber;
    private bool coinspawnbool;
    private int amountofCoinsIcon;
    // Use this for initialization
    void Start () {
        //find how big the arena is 
        GameObject ArenaGO = GameObject.Find("Arena_test");
        Arena Arena = ArenaGO.GetComponent<Arena>();
        //get arena and player data
        player = PlayerGO.GetComponent<Player>();
        playerradius = player.Radius;
        Arenahalfheight = Arena.height / 2;
        Arenahalfwidth = Arena.width / 2;
        NegativeArenahalfheight = Arenahalfheight * -1.0f;
        NegativeArenahalfwidth = Arenahalfwidth * -1.0f;
        //make arrays
        ObstacleLocations = new Vector3[AmountofObstacle];
        CoinsLocations = new Vector3[amountofCoins];
        //get obstacle and coin data
        Obstacle obstacle = ObstacleGO.GetComponent<Obstacle>();
        coin coin = CoinGO.GetComponent<coin>();
        Coinradius = coin.Radius;
        Obstaclehalfwidth = obstacle.Halfwidth;
        NegativeObstaclehalfwidth = -1 * obstacle.Halfwidth;
        //instatiate number
        coinNumber = 0;
        amountofCoinsIcon = amountofCoins;
        coinspawnbool = false;
        this.makeObstacleloc();
        this.Makecoinloc();
        for(int z = 0; z<ObstacleLocations.Length;z++)
        {
            Instantiate(ObstacleGO, ObstacleLocations[z], Quaternion.identity);
        }

        

	}
	
	// Update is called once per frame
	void Update () {
        this.Coinspawn();
        this.winner();
	}

    void makeObstacleloc()
    {
       
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
                if ((xtest - Obstaclehalfwidth - 2.0f) < NegativeArenahalfwidth || (xtest + Obstaclehalfwidth + 2.0f) > Arenahalfwidth)
                {
                    xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                    posTest.x = xtest;
                    continue;
                }
                else if ((ytest - Obstaclehalfwidth - 2.0f) < NegativeArenahalfheight || (ytest + Obstaclehalfwidth + 2.0f) > Arenahalfheight)
                {
                    ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                    posTest.y = ytest;
                    continue;
                }
                //player test
                else if ((playerradius + Obstaclehalfwidth + 2.0f) > Vector3.Distance(posTest, player.gameObject.transform.position))
                {
                    //see which one(x or y) is closer and move it 
                    float xresult = Mathf.Abs(xtest - player.gameObject.transform.position.x);
                    float yresult = Mathf.Abs(ytest - player.gameObject.transform.position.y);
                    if (xresult > yresult)
                    {
                        ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                        posTest.y = ytest;
                        continue;
                    }
                    else if (yresult > xresult)
                    {
                        xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                        posTest.x = xtest;
                        continue;
                    }
                    else if (xresult == yresult)
                    {
                        ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                        posTest.y = ytest;
                        xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                        posTest.x = xtest;
                        continue;
                    }
                }
                //Obstacle check
                int ycounter = 0; ;
                for (int y = 0; y < ObstacleLocations.Length; y++)
                {
                    ycounter = y;
                    Vector3 Vector3test = new Vector3(0.0f, 0.0f, 0.0f);
                    Vector3test = ObstacleLocations[y];
                    if (Vector3test == Vector3.zero)
                    {

                    }
                    else
                    {
                        //check the distance

                        if ((Obstaclehalfwidth * 2 + 2.0f) > Vector3.Distance(posTest, Vector3test))
                        {
                            //see which one(x or y) is closer and move it 
                            float xresult2 = Mathf.Abs(xtest - Vector3test.x);
                            float yresult2 = Mathf.Abs(ytest - Vector3test.y);
                            if (xresult2 > yresult2)
                            {
                                ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                                posTest.y = ytest;
                                break;
                            }
                            else if (yresult2 > xresult2)
                            {
                                xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                                posTest.x = xtest;
                               break;
                            }
                            else if (xresult2 == yresult2)
                            {
                                ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                                posTest.y = ytest;
                                xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                                posTest.x = xtest;
                                break;
                            }
                        }
                    }
                }
                if (ycounter == this.AmountofObstacle - 1)
                {
                    clear = true;
                    ObstacleLocations[x] = posTest;
                }
            }



        }
    }

    public void Makecoinloc()
    {
        //for the amount of coins
        for(int x =0;x<amountofCoins;x++)
        {
            //grab 2 test numbers using the size of arena 
            float xtestc = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
            float ytestc = Random.Range(NegativeArenahalfheight, Arenahalfheight);
            bool clear = false;
            Vector3 testposc = new Vector3(xtestc, ytestc, -2.0f); 
            //need to test arena, obstacles, player and coins
            while(clear == false)
            {
                //arena 

                //test x loc
                if(xtestc-Coinradius - 3f < NegativeArenahalfwidth || xtestc + Coinradius +3f > Arenahalfwidth)
                {
                    xtestc = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                    testposc.x = xtestc;
                    continue;
                }
                //test y loc
                else if (ytestc - Coinradius - 3f < NegativeArenahalfheight || ytestc + Coinradius + 3f > Arenahalfheight)
                {
                    ytestc = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                    testposc.y = ytestc;
                    continue;
                }

                //obstacles
                for(int y =0;y<AmountofObstacle;y++)
                {
                    Vector3 Obstaclevec3test = ObstacleLocations[y];
                    if(Obstaclevec3test == Vector3.zero)
                    {

                    }
                    else
                    {
                        //find closest point on box
                        Vector3 displacement = Obstaclevec3test - testposc;
                        if(displacement.x > Obstaclehalfwidth)
                        {
                            displacement.x = Obstaclehalfwidth;
                        }
                        if(displacement.x<NegativeObstaclehalfwidth)
                        {
                            displacement.x = NegativeObstaclehalfwidth;
                        }
                        if (displacement.y > Obstaclehalfwidth)
                        {
                            displacement.y = Obstaclehalfwidth;
                        }
                        if (displacement.y < NegativeObstaclehalfwidth)
                        {
                            displacement.y = NegativeObstaclehalfwidth;
                        }
                        Vector3 nearpoint = new Vector3(displacement.x + Obstaclevec3test.x, displacement.y + Obstaclevec3test.y, -2.0f);
                        //see if the distance between the closest point and the center of the coin is less then coin radius
                        if (Coinradius +3.0f > Vector3.Distance(testposc, nearpoint))
                        {
                            //see which is closer x or y 
                            float xresult3 = Mathf.Abs(xtestc - Obstaclevec3test.x);
                            float yresult3 = Mathf.Abs(ytestc - Obstaclevec3test.y);
                            if (xresult3 > yresult3)
                            {
                                ytestc = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                                testposc.y = ytestc;
                                break;
                            }
                            else if (yresult3 > xresult3)
                            {
                                xtestc = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                                testposc.x = xtestc;
                                break;
                            }
                            else if (xresult3 == yresult3)
                            {
                                ytestc = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                                testposc.y = ytestc;
                                xtestc = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                                testposc.x = xtestc;
                                break;
                            }
                        }
                    }
                }

                //player
                //check the distance and see if it is bigger then the added radius of the player and coin
                if(Coinradius+playerradius +2.0f>Vector3.Distance(testposc,player.transform.position))
                {
                    //check the x and y again
                    float xresult4 = Mathf.Abs(testposc.x - player.transform.position.x);
                    float yresult4 = Mathf.Abs(testposc.y - player.transform.position.y);

                    if(xresult4>yresult4)
                    {
                        xtestc = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                        testposc.x = xtestc;
                        continue;
                    }
                    if(yresult4>xresult4)
                    {
                        ytestc = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                        testposc.y = ytestc;
                        continue;
                    }
                    if(xresult4==yresult4)
                    {
                        xtestc = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                        testposc.x = xtestc;
                        ytestc = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                        testposc.y = ytestc;
                        continue;
                    }
                }
                int zcounter = 0;
                //coins
                for(int z = 0;z<amountofCoins;z++)
                {
                    zcounter = z;
                    Vector3 testcoinpos = Vector3.zero;
                     testcoinpos = CoinsLocations[z];
                    if(testcoinpos == Vector3.zero)
                    {

                    }
                    else//it has a value, it exist
                    {
                        if(Coinradius * 2 + 2.0f > Vector3.Distance(testposc,testcoinpos))
                        {
                            //check the x and y again
                            float xresult5 = Mathf.Abs(testposc.x - testcoinpos.x);
                            float yresult5 = Mathf.Abs(testposc.y - testcoinpos.y);

                            if (xresult5 > yresult5)
                            {
                                xtestc = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                                testposc.x = xtestc;
                                continue;
                            }
                            if (yresult5 > xresult5)
                            {
                                ytestc = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                                testposc.y = ytestc;
                                continue;
                            }
                            if (xresult5 == yresult5)
                            {
                                xtestc = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                                testposc.x = xtestc;
                                ytestc = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                                testposc.y = ytestc;
                                continue;
                            }
                        }
                    }
                }
                if(zcounter == this.amountofCoinsIcon-1)
                CoinsLocations[x] = testposc;
                clear = true;
                }

            } 
        }

        public void Coinspawn()
        {
            if(coinspawnbool == false && coinNumber<amountofCoins)
            {
            Instantiate(CoinGO, CoinsLocations[coinNumber], Quaternion.identity);
            coinspawnbool = true;
            }
        }
        
        public void winner()
        {
            if(player.Hp==0)
            {
            GameObject ECGO = GameObject.Find("Endingcarrier");
            endingcarriar EC = ECGO.GetComponent<endingcarriar>();
            EC.Winner = "Shooter";
            EC.Runnerwins++;
            Application.LoadLevel("endingscene");
        }
            else if(coinNumber == 6)
            {
            GameObject ECGO = GameObject.Find("Endingcarrier");
            endingcarriar EC = ECGO.GetComponent<endingcarriar>();
            EC.Winner = "Runner";
            EC.Shooterwins++;
            Application.LoadLevel("endingscene");
        }
        
        }
        public bool Coinspawnbool
        {
            get
            {
            return coinspawnbool;
            }
            set
            {
            coinspawnbool = value;
            }
        }

        public int CoinNumber
        {
            get
            {
            return coinNumber;
            }
            set
            {
            coinNumber = value;
            }
        }

        public Vector3[] CoinLocationsg
    {
        get
        {
            return CoinsLocations;
        }
    }

    public int AmountofCoinsIcon
    {
        get
        {
            return amountofCoinsIcon;
        }
        set
        {
            amountofCoinsIcon = value;
        }
    }
}

