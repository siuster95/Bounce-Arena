using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    Arena arena;
    [SerializeField]
    float radius;
    [SerializeField]
    int hp;
    [SerializeField]
    int speedboostcounter;
    [SerializeField]
    float waitseconds;
    [SerializeField]
    float waitsecondsreload;
    [SerializeField]
    float slowmotimer;
    [SerializeField]
    float slowmoreloadtimer;
    [SerializeField]
    float speedincrease;
    private bool leftbool;
    private bool rightbool;
    private bool upbool;
    private bool downbool;
    private bool slowmo;
    private bool slowmostartbool;
    private bool slowmoreloadbool;
    private bool slowmobuttonbool;
    private Vector3 movementvector;
    private float halfwidth;
    private float halfheight;
    private float halfradius;
    private bool reloadbool;
    private float leftprediction, rightprediction,upprediction,downprediction;
    private Vector3 position;
    private Vector3[] coinposition;
    private GameManager GameManager;
    private Obstacle[] ObstaclesArray;
    GameObject[] speedboostcounters;
    private float speednormal;
    private bool speedboostbool;
    private bool speedupbool;
    private float slowmonormaltime;
    private bool blank;
    private float blankcount;
    // Use this for initialization
    void Start () {
        leftbool = false;
        rightbool=false;
        upbool = false ;
        downbool=false;
        movementvector = Vector3.zero;
        halfwidth = arena.width/2;
        halfheight = arena.height / 2;
        halfradius = this.radius / 2;
        GameObject GameManagerGO = GameObject.Find("GameManager");
        speedboostcounters = GameObject.FindGameObjectsWithTag("speedcounter");
        GameManager = GameManagerGO.GetComponent<GameManager>();
        coinposition = GameManager.CoinLocationsg;
        slowmo = GameManager.Slowmo;
        slowmonormaltime = slowmotimer;
        blank = GameManager.Blanks;
        speedupbool = GameManager.Speedup;
        slowmoreloadbool = false;
        slowmostartbool = false;
        slowmobuttonbool = true;
        speednormal = speed;
        speedboostbool = true;
        reloadbool = false;
        blankcount = 2;
        if (speedupbool == true)
        {
            GameObject GO = GameObject.Find("Speedbar");
            GO.SetActive(true);
            GO = GameObject.Find("PlayertimerBar");
            GO.SetActive(false);
            GO = GameObject.Find("BlankBar");
            GO.SetActive(false);
        }
        if (slowmo == true)
        {
            GameObject GO = GameObject.Find("PlayertimerBar");
            GO.SetActive(true);
            GO = GameObject.Find("Speedbar");
            GO.SetActive(false);
            GO = GameObject.Find("BlankBar");
            GO.SetActive(false);
        }
        if(blank == true)
        {
            GameObject GO = GameObject.Find("BlankBar");
            GO.SetActive(true);
            GO = GameObject.Find("Speedbar");
            GO.SetActive(false);
            GO = GameObject.Find("PlayertimerBar");
            GO.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //starting values
        position = this.transform.position;
        movementvector = Vector3.zero;
        leftbool = Input.GetKey(KeyCode.A);
        upbool = Input.GetKey(KeyCode.W);
        rightbool = Input.GetKey(KeyCode.D);
        downbool = Input.GetKey(KeyCode.S);
        leftprediction = Mathf.Abs((position.x - halfradius - arena.borderwidth) - speed);
        rightprediction = Mathf.Abs((position.x + halfradius + arena.borderwidth) + speed);
        upprediction = Mathf.Abs((position.y + halfradius + arena.borderwidth) + speed);
        downprediction = Mathf.Abs((position.y - halfradius - arena.borderwidth) - speed);
        ObstaclesArray = GameObject.FindObjectsOfType<Obstacle>();
        
        if (GameManager.Gobool == false)
        {
            //check to see if we are getting to left edge, if we are far away enough, add in the left direction
            if (leftbool && leftprediction < halfwidth)
            {
                movementvector.x -= speed;
            }
            //check to see if we are getting to left edge, if we are close enough that adding default speed will go over,calculate the distance needed and add in the left direction
            else if (leftbool && leftprediction > halfwidth)
            {
                movementvector.x -= Mathf.Abs((halfwidth - arena.borderwidth - halfradius) + position.x);
            }
            //check to see if we are getting to right edge, if we are far away enough, add in the right direction
            if (rightbool && rightprediction < halfwidth)
            {
                movementvector.x += speed;
            }
            //check to see if we are getting to right edge, if we are close enough that adding default speed will go over,calculate the distance needed and add in the right direction
            else if (rightbool && rightprediction > halfwidth)
            {
                movementvector.x += Mathf.Abs((halfwidth - arena.borderwidth - halfradius) - position.x);
            }
            //check to see if we are getting to top edge, if we are far away enough, add in the up direction
            if (upbool && upprediction < halfheight)
            {
                movementvector.y += speed;
            }
            //check to see if we are getting to top edge, if we are close enough that adding default speed will go over,calculate the distance needed and add in the up direction
            else if (upbool && upprediction > halfheight)
            {
                movementvector.y += Mathf.Abs((halfheight - arena.borderwidth - halfradius) - position.y);
            }
            //check to see if we are getting to bottom edge, if we are far away enough, add in the down direction
            if (downbool && downprediction < halfheight)
            {
                movementvector.y -= speed;
            }
            //check to see if we are getting to bottom edge, if we are close enough that adding default speed will go over,calculate the distance needed and add in the down direction
            else if (downbool && downprediction > halfheight)
            {
                movementvector.y -= Mathf.Abs((halfheight - arena.borderwidth - halfradius) + position.y);
            }
            movementvector = this.ObstacleCheck(movementvector);
            this.gameObject.transform.position += movementvector;
            //see if we are colliding with a coin 
            this.Coinhit();
            if (speedboostcounter == 0 && reloadbool == false && speedboostbool == true)
            {
                reloadbool = true;
                speedboostbool = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && this.speedupbool == true)
            {
                if (this.speedboostbool == true && speedboostcounter > 0)
                {
                    StartCoroutine(Speedboost());
                }
            }

            if(Input.GetKeyDown(KeyCode.Space) && this.slowmo == true && this.slowmobuttonbool == true)
            {
                this.slowmobuttonbool = false;
                this.slowmostartbool = true;
            }

            if(slowmoreloadbool == true && slowmostartbool == false)
            {
                slowmoreloadbool = false;
                StartCoroutine(slowmoreload());
            }
            if (reloadbool == true && speedboostbool == false)
            {
                reloadbool = false;
                StartCoroutine(speedboostreload());
            }
            if(this.slowmostartbool == true && slowmoreloadbool == false)
            {
                this.slowmoactive();
            }
            if (this.slowmostartbool == false && slowmoreloadbool == true)
            {
                this.bulletspeedup();
            }

            if(Input.GetKeyDown(KeyCode.Space) && blank == true)
            {
                if(blankcount>0)
                {
                    this.Blanksaway();
                }
            }

            //Debug.Log(speedboostcounter);
        }
    }
    public void Coinhit()
    {

            
        coin ccoin = GameObject.FindObjectOfType<coin>();
        if (ccoin != null)
        {
            //find the distance between player and coin
            float distance = Mathf.Abs(Vector3.Distance(this.transform.position, ccoin.Location));
            if (distance < (this.radius + ccoin.Radius))
            {
                Destroy(ccoin.gameObject);
                GameManager.Coinspawnbool = false;
                GameObject Coinicon = GameObject.Find("coin" + GameManager.AmountofCoinsIcon);
                Coinicon.SetActive(false);
                GameManager.AmountofCoinsIcon = GameManager.AmountofCoinsIcon - 1;
                GameManager.CoinNumber++;
            }
        }
    }
    //check to see if the player is crashing into an obstacle
    public Vector3 ObstacleCheck(Vector3 Movement)
    {
        bool closeEnough;
        for(int x =0;x<ObstaclesArray.Length;x++)
        {
            closeEnough = false;
            Obstacle Obstacletest = ObstaclesArray[x];
            //find if the player is close enough to obstacle
            Vector3 Displacement =   this.Position - Obstacletest.Location;
            if(Displacement.x > Obstacletest.Halfwidth)
            {
                Displacement.x = Obstacletest.Halfwidth;
            }
            if(Displacement.y > Obstacletest.Halfwidth)
            {
                Displacement.y = Obstacletest.Halfwidth;
            }
            if(Displacement.x < (-1.0f * Obstacletest.Halfwidth))
            {
                Displacement.x = (-1.0f * Obstacletest.Halfwidth);
            }
            if(Displacement.y < (-1.0f * Obstacletest.Halfwidth))
            {
                Displacement.y = (-1.0f * Obstacletest.Halfwidth);
            }
            Vector3 newloc = Obstacletest.Location + Displacement;
            float distance = Vector3.Distance(newloc, this.Position);
            if(distance<=Obstacletest.Halfwidth)
            {
                closeEnough = true;
            }
            bool ontop = false;
            bool onbottom = false;
            bool onleft = false;
            bool onright = false;
            if((this.position.x+this.radius)>(Obstacletest.Location.x-Obstacletest.Halfwidth+.1f) && (this.position.x-this.radius)<(Obstacletest.Location.x+Obstacletest.Halfwidth-.1f)&&this.position.y>Obstacletest.Location.y)
            {
                ontop = true;
            }
            if ((this.position.x+this.radius) > (Obstacletest.Location.x - Obstacletest.Halfwidth+.1f) && (this.position.x-this.radius) < (Obstacletest.Location.x + Obstacletest.Halfwidth-.1f) && this.position.y < Obstacletest.Location.y)
            {
                onbottom = true;
            }
            if((this.position.y+this.radius)>(Obstacletest.Location.y-Obstacletest.Halfwidth+.1f)&&((this.position.y-this.radius)<(Obstacletest.Location.y+Obstacletest.Halfwidth-.1f))&&this.position.x>Obstacletest.Location.x)
            {
                onright = true;
            }
            if ((this.position.y + this.radius) > (Obstacletest.Location.y - Obstacletest.Halfwidth+.1f) && ((this.position.y - this.radius) < (Obstacletest.Location.y + Obstacletest.Halfwidth-.1f)) && this.position.x < Obstacletest.Location.x)
            {
                onleft = true;
            }
            float ypostestbottom = Mathf.Abs(Obstacletest.Location.y - (this.Position.y + this.Radius));
            float xpostestleft = Mathf.Abs((Obstacletest.Location.x) - (this.Position.x + this.Radius));
            float xpostestright = Mathf.Abs((this.Position.x - this.Radius) - (Obstacletest.Location.x));
            //see if it is crashing into the top 
            if (ontop == true&&downbool==true&&closeEnough==true)
            {
                Movement.y = 0; 
            }
            //see if it is crashing into the bottom
            if (onbottom==true&&upbool == true && closeEnough == true)
            {
               
                
                    Movement.y = 0;
                
            }
            //see if it is crashing into the left 
            
            if (onleft == true && rightbool == true && closeEnough == true)
            {
                
                    Movement.x = 0;
                
            }
            //see if it is crashing into the right 
            
            if (onright == true && leftbool == true && closeEnough == true)
            {
                
                    Movement.x = 0;
                
            }
            
        }
        return Movement;
    }

    IEnumerator Speedboost()
    {
        GameObject speedboostcountericon = GameObject.Find("Speed" + speedboostcounter);
        speedboostcountericon.SetActive(false);
        this.speedboostcounter -= 1;
        this.speed = this.speed + speedincrease;
        yield return new WaitForSeconds(waitseconds);
        this.speed = this.speednormal;
    }
    IEnumerator speedboostreload()
    {
        yield return new WaitForSeconds(waitsecondsreload);
        this.speedboostcounter = 3;
        speedboostbool = true;
        for(int x =0;x<speedboostcounters.Length;x++)
        {
            speedboostcounters[x].SetActive(true);
        }
    }
    IEnumerator slowmoreload()
    {
        yield return new WaitForSeconds(slowmoreloadtimer);
        slowmostartbool = false;
        slowmoreloadbool = false;
        slowmobuttonbool = true;
        slowmotimer = slowmonormaltime;
        GameObject PTGO = GameObject.Find("Playertimer");
        Image timerbar = PTGO.GetComponent<Image>();
        timerbar.fillAmount = 1;
    }
    public void slowmoactive()
    {
        if(slowmotimer >0)
        {
            GameObject[] GObulletlist = GameObject.FindGameObjectsWithTag("bulletactive");
            for(int x=0;x<GObulletlist.Length;x++)
            {
                bullet bchange = GObulletlist[x].GetComponent<bullet>();
                bchange.Speed = .1f;
            }
            slowmotimer -= Time.deltaTime;
            slowmotimerchange();
        }
        else
        {
            slowmoreloadbool = true;
            slowmostartbool = false;
        }
    }
    public void bulletspeedup()
    {
        GameObject[] GObulletlist = GameObject.FindGameObjectsWithTag("bulletactive");
        for (int x = 0; x < GObulletlist.Length; x++)
        {
            bullet bchange = GObulletlist[x].GetComponent<bullet>();
            bchange.Speed = .2f;
        }
    }
    public void slowmotimerchange()
    {
        GameObject PTGO = GameObject.Find("Playertimer");
        Image timerbar = PTGO.GetComponent<Image>();
        float value = slowmotimer / slowmonormaltime;
        timerbar.fillAmount = value;
    }

    public void Blanksaway()
    {
        GameObject BGO = GameObject.Find("Blank" + blankcount);
        BGO.SetActive(false);
        GameObject[] BAGO = GameObject.FindGameObjectsWithTag("bulletactive");
        foreach (GameObject baGO in BAGO)
        {
            Destroy(baGO);
        }
        blankcount -= 1;
    }

    public float Radius
    {
        get
        {
            return radius;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            return position;
        }
    }
}
