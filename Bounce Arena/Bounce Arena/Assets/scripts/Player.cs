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
    coin[] coinarray;

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
        
    }
	
	// Update is called once per frame
	void Update () {

        if(hp<=0)
        {
            Application.Quit();
        }
        //starting values
        coinarray = GameObject.FindObjectsOfType<coin>();
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
        //check to see if we are getting to left edge, if we are far away enough, add in the left direction
        if(leftbool && leftprediction < halfwidth)
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
        this.gameObject.transform.position += movementvector;
        //see if we are colliding with a coin 
        this.Coinhit();
    }

    public void Coinhit()
    {
        for(int x =0;x<coinarray.Length;x++)
        {
            coin ccoin = coinarray[x];
            //find the distance between player and coin
            float distance = Mathf.Abs(Vector3.Distance(this.transform.position, ccoin.Location));
            if(distance < (this.radius + ccoin.Radius))
            {
                Destroy(ccoin.gameObject);
            }
        }
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
}
