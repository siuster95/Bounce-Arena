using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    Arena arena;
    [SerializeField]
    float radius;
    [SerializeField]
    int hp;
    private bool leftbool;
    private bool rightbool;
    private bool upbool;
    private bool downbool;
    private Vector3 movementvector;
    private float halfwidth;
    private float halfheight;
    private float halfradius;
    private float leftprediction, rightprediction,upprediction,downprediction;
    private Vector3 position;
    private Vector3[] coinposition;
    private GameManager GameManager;
    private Obstacle[] ObstaclesArray;

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
        GameManager = GameManagerGO.GetComponent<GameManager>();
        coinposition = GameManager.CoinLocationsg;
        
    }
	
	// Update is called once per frame
	void Update () {

        if(hp<=0)
        {
            Application.Quit();
        }
        //starting values
        position = this.transform.position;
        movementvector = Vector3.zero;
        leftbool = Input.GetKey(KeyCode.A);
        upbool = Input.GetKey(KeyCode.W);
        rightbool = Input.GetKey(KeyCode.D);
        downbool = Input.GetKey(KeyCode.S);
        leftprediction = Mathf.Abs((position.x - halfradius-arena.borderwidth) - speed);
        rightprediction = Mathf.Abs((position.x + halfradius + arena.borderwidth) + speed);
        upprediction = Mathf.Abs((position.y + halfradius + arena.borderwidth) + speed);
        downprediction = Mathf.Abs((position.y - halfradius - arena.borderwidth) - speed);
        ObstaclesArray = GameObject.FindObjectsOfType<Obstacle>();
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
            if((this.position.x+this.radius)>(Obstacletest.Location.x-Obstacletest.Halfwidth+.05) && (this.position.x-this.radius)<(Obstacletest.Location.x+Obstacletest.Halfwidth-.05)&&this.position.y>Obstacletest.Location.y)
            {
                ontop = true;
            }
            if ((this.position.x+this.radius) > (Obstacletest.Location.x - Obstacletest.Halfwidth + .05) && (this.position.x-this.radius) < (Obstacletest.Location.x + Obstacletest.Halfwidth-.05) && this.position.y < Obstacletest.Location.y)
            {
                onbottom = true;
            }
            if((this.position.y+this.radius)>(Obstacletest.Location.y-Obstacletest.Halfwidth+.05)&&((this.position.y-this.radius)<(Obstacletest.Location.y+Obstacletest.Halfwidth-.05))&&this.position.x>Obstacletest.Location.x)
            {
                onright = true;
            }
            if ((this.position.y + this.radius) > (Obstacletest.Location.y - Obstacletest.Halfwidth+.05) && ((this.position.y - this.radius) < (Obstacletest.Location.y + Obstacletest.Halfwidth-.05)) && this.position.x < Obstacletest.Location.x)
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
