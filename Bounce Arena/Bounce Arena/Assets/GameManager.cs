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
    float playerradius;
    [SerializeField]
    int amountofCoins;
    [SerializeField]
    float ArenaWidth;
    [SerializeField]
    float ArenaHeight;
    private Vector3[] CoinsLocations;
    private Vector3[] CoinsLocations2;
    private Vector3[] CoinsLocations3;
    private Vector3[] CoinsLocations4;
    private Vector3[] CoinsLocations5;
    private Vector3[] ObstacleLocations;
    private Vector3[] ObstacleLocations2;
    private Vector3[] ObstacleLocations3;
    private Vector3[] ObstacleLocations4;
    private Vector3[] ObstacleLocations5;
    private float Arenahalfwidth;
    private float Arenahalfheight;
    private float Obstaclehalfwidth;
    private float NegativeObstaclehalfwidth;
    private float Coinradius;
   
    private float NegativeArenahalfheight;
    private float NegativeArenahalfwidth;
    private int coinNumber;
    private bool coinspawnbool;
    private int amountofCoinsIcon;
    private int roundnumber;
    private bool obstaclespawn;
    // Use this for initialization
    void Start () {
        //find how big the arena is 
        //get arena and player data
        Arenahalfheight = ArenaHeight / 2;
        Arenahalfwidth = ArenaWidth / 2;
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
        //fill up 5 arrays of both obstacles and coins
        ObstacleLocations = new Vector3[AmountofObstacle];
        ObstacleLocations2 = new Vector3[AmountofObstacle];
        ObstacleLocations3 = new Vector3[AmountofObstacle];
        ObstacleLocations4 = new Vector3[AmountofObstacle];
        ObstacleLocations5 = new Vector3[AmountofObstacle];
        CoinsLocations = new Vector3[amountofCoins];
        CoinsLocations2 = new Vector3[amountofCoins];
        CoinsLocations3 = new Vector3[amountofCoins];
        CoinsLocations4 = new Vector3[amountofCoins];
        CoinsLocations5 = new Vector3[amountofCoins];
        this.makeObstacleloc(ObstacleLocations);
        this.makeObstacleloc(ObstacleLocations2);
        this.makeObstacleloc(ObstacleLocations3);
        this.makeObstacleloc(ObstacleLocations4);
        this.makeObstacleloc(ObstacleLocations5);
        this.Makecoinloc(CoinsLocations,ObstacleLocations);
        this.Makecoinloc(CoinsLocations2, ObstacleLocations2);
        this.Makecoinloc(CoinsLocations3, ObstacleLocations3);
        this.Makecoinloc(CoinsLocations4, ObstacleLocations4);
        this.Makecoinloc(CoinsLocations5, ObstacleLocations5);
        //initilize the round
        roundnumber = 1;
        obstaclespawn = false;
        DontDestroyOnLoad(this.gameObject);

        

	}

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "test" && obstaclespawn == false)
        {
            if (roundnumber == 1)
            {
                for (int z = 0; z < ObstacleLocations.Length; z++)
                {
                    Instantiate(ObstacleGO, ObstacleLocations[z], Quaternion.identity);
                    obstaclespawn = true;
                }
            }
            else if (roundnumber == 2)
            {
                for (int z = 0; z < ObstacleLocations.Length; z++)
                {
                    Instantiate(ObstacleGO, ObstacleLocations2[z], Quaternion.identity);
                    obstaclespawn = true;
                }
            }
            else if (roundnumber == 3)
            {
                for (int z = 0; z < ObstacleLocations.Length; z++)
                {
                    Instantiate(ObstacleGO, ObstacleLocations3[z], Quaternion.identity);
                    obstaclespawn = true;
                }
            }
            else if (roundnumber == 4)
            {
                for (int z = 0; z < ObstacleLocations.Length; z++)
                {
                    Instantiate(ObstacleGO, ObstacleLocations4[z], Quaternion.identity);
                    obstaclespawn = true;
                }
            }
            else if (roundnumber == 5)
            {
                for (int z = 0; z < ObstacleLocations.Length; z++)
                {
                    Instantiate(ObstacleGO, ObstacleLocations5[z], Quaternion.identity);
                    obstaclespawn = true;
                }
            }
        }

        if (Application.loadedLevelName == "test")
        {
            if (roundnumber == 1)
            {
                this.Coinspawn(CoinsLocations);
            }
            else if (roundnumber == 2)
            {
                this.Coinspawn(CoinsLocations2);
            }
            else if (roundnumber == 3)
            {
                this.Coinspawn(CoinsLocations3);
            }
            else if (roundnumber == 4)
            {
                this.Coinspawn(CoinsLocations4);
            }
            else if (roundnumber == 5)
            {
                this.Coinspawn(CoinsLocations5);
            }
            this.winner();
        }
    }

    void makeObstacleloc(Vector3[] obstaclelocations)
    {
       
        //for the amount of obstacles, put that many on the field
        for (int x = 0; x < AmountofObstacle; x++)
        {
            bool clear = false;
            //find a random x and y 
            float xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
            float ytest = Random.Range(NegativeArenahalfheight, Arenahalfwidth);
            Vector3 posTest = new Vector3(xtest, ytest, -2.0f);
            Vector3 playerlocation = new Vector3(0.0f, 0.0f, -2.0f);
            //see if the x is at a wrong place
            while (clear == false)
            {
                //the fence, Obstacles and the player
                //fence x test
                if ((xtest - Obstaclehalfwidth - 3.0f) < NegativeArenahalfwidth || (xtest + Obstaclehalfwidth + 3.0f) > Arenahalfwidth)
                {
                    xtest = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                    posTest.x = xtest;
                    continue;
                }
                else if ((ytest - Obstaclehalfwidth - 3.0f) < NegativeArenahalfheight || (ytest + Obstaclehalfwidth + 3.0f) > Arenahalfheight)
                {
                    ytest = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                    posTest.y = ytest;
                    continue;
                }
                //player test
                
                else if ((playerradius + Obstaclehalfwidth + 2.0f) > Vector3.Distance(posTest,playerlocation ))
                {
                    //see which one(x or y) is closer and move it 
                    float xresult = Mathf.Abs(xtest - playerlocation.x);
                    float yresult = Mathf.Abs(ytest - playerlocation.y);
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
                for (int y = 0; y < obstaclelocations.Length; y++)
                {
                    ycounter = y;
                    Vector3 Vector3test = new Vector3(0.0f, 0.0f, 0.0f);
                    Vector3test = obstaclelocations[y];
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
                    obstaclelocations[x] = posTest;
                }
            }



        }
    }

    public void Makecoinloc(Vector3[] coinlocation,Vector3[] obstaclelocation)
    {
        //for the amount of coins
        for(int x =0;x<amountofCoins;x++)
        {
            //grab 2 test numbers using the size of arena 
            float xtestc = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
            float ytestc = Random.Range(NegativeArenahalfheight, Arenahalfheight);
            bool clear = false;
            Vector3 playerlocation = new Vector3(0.0f, 0.0f, -2.0f);
            Vector3 testposc = new Vector3(xtestc, ytestc, -2.0f); 
            //need to test arena, obstacles, player and coins
            while(clear == false)
            {
                //arena 

                //test x loc
                if(xtestc-Coinradius - 7f < NegativeArenahalfwidth || xtestc + Coinradius +7f > Arenahalfwidth)
                {
                    xtestc = Random.Range(NegativeArenahalfwidth, Arenahalfwidth);
                    testposc.x = xtestc;
                    continue;
                }
                //test y loc
                else if (ytestc - Coinradius - 7f < NegativeArenahalfheight || ytestc + Coinradius + 7f > Arenahalfheight)
                {
                    ytestc = Random.Range(NegativeArenahalfheight, Arenahalfheight);
                    testposc.y = ytestc;
                    continue;
                }
                int ycounter = 0;
                //obstacles
                for (int y =0;y<AmountofObstacle;y++)
                {
                    ycounter = y;
                    Vector3 Obstaclevec3test = Vector3.zero;
                    Obstaclevec3test = obstaclelocation[y];
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
                //if it had to change an x or y, have the loop repeat itself
                if(ycounter!=AmountofObstacle-1)
                {
                    continue;
                }
                //player
                //check the distance and see if it is bigger then the added radius of the player and coin
                if(Coinradius+playerradius +2.0f>Vector3.Distance(testposc,playerlocation))
                {
                    //check the x and y again
                    float xresult4 = Mathf.Abs(testposc.x - playerlocation.x);
                    float yresult4 = Mathf.Abs(testposc.y - playerlocation.y);

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
                    testcoinpos = coinlocation[z];
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
                if(zcounter == this.amountofCoins-1)
                coinlocation[x] = testposc;
                clear = true;
                }

            } 
        }

        public void Coinspawn(Vector3[] coinlocations)
        {
            if(coinspawnbool == false && coinNumber<amountofCoins)
            {
            Instantiate(CoinGO, coinlocations[coinNumber], Quaternion.identity);
            coinspawnbool = true;
            }
        }
        
        public void winner()
        {
        GameObject PGO = GameObject.Find("Player_test");
        Player player = PGO.GetComponent<Player>();
            if(player.Hp==0)
            {
            GameObject ECGO = GameObject.Find("Endingcarrier");
            endingcarriar EC = ECGO.GetComponent<endingcarriar>();
            EC.Winner = "Shooter";
            EC.Runnerwins++;
            player.Hp = 3;
            AmountofCoinsIcon = amountofCoins;
            this.roundnumber++;
            
            Application.LoadLevel("endingscene");
        }
            else if(coinNumber == 6)
            {
            GameObject ECGO = GameObject.Find("Endingcarrier");
            endingcarriar EC = ECGO.GetComponent<endingcarriar>();
            EC.Winner = "Runner";
            EC.Shooterwins++;
            player.Hp = 3;
            AmountofCoinsIcon = amountofCoins;
            this.roundnumber++;

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
    public int RoundNumber
    {
        get
        {
            return roundnumber;
        }
        set
        {
            roundnumber = value;
        }
    }
    public bool Obstaclespawn
    {
        get
        {
            return obstaclespawn;
        }
        set
        {
            obstaclespawn = value;
        }
    }
}

