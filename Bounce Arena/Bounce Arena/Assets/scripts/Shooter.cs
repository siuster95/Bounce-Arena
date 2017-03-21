using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shooter : MonoBehaviour {

    [SerializeField]
    GameObject place1;
    [SerializeField]
    GameObject place2;
    [SerializeField]
    GameObject place3;
    [SerializeField]
    GameObject place4;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject Clone;
    [SerializeField]
    float initabilitytimer;
    [SerializeField]
    float initshadowclonetimer;
    [SerializeField]
    float initshadowclonecooldown;
    [SerializeField]
    GameObject timer;
    [SerializeField]
    float cooldowntimer;
    Vector3 loc1, loc2, loc3, loc4;
    protected Vector3 mousepos, offset;

    int bulletcount;
    [SerializeField]
    int reloadtime;
    int position;
    GameObject[] bullets;
    private Image timerimage;
    protected bool bool1, bool2, bool3, bool4, shootbool, reloadbool, infiniteshooterbool, infinteactive, infinitecooldownbool, shadowclonebool, shadowclonepressed, shadowcloneactive, shadowclonecooldown;
    protected float abilitytime,shadowclonetime,shadowclonecooldowntime;
    GameManager GM;
    // Use this for initialization
    protected virtual void Start ()
    {
        loc1 = place1.transform.position;
        loc2 = place2.transform.position;
        loc3 = place3.transform.position;
        loc4 = place4.transform.position;
        bool1 = false;
        bool2 = false;
        bool3 = false;
        bool4 = false;
        shootbool = true;
        abilitytime = initabilitytimer;
        reloadbool = false;
        bulletcount = 6;
        //start off at loc1
        this.transform.position = loc1;
        position = 1;
        offset = new Vector3(0, 0, -2.0f);
        bullets = GameObject.FindGameObjectsWithTag("bullet");
        timerimage = timer.GetComponent<Image>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        shadowclonebool = GM.Shadowclones;
        shadowclonepressed = false;
        shadowcloneactive = false;
        shadowclonecooldown = false;
        shadowclonetime = initshadowclonetimer;
        shadowclonecooldowntime = initshadowclonecooldown;
        infiniteshooterbool = GM.InfiniteShooter;
        infinteactive = false;
        infinitecooldownbool = false;

    }

    // Update is called once per frame
   protected virtual void Update()
    {
        if (GM.Gobool == false)
        {
            //move the character around with keyboard input
            //see if any of the buttons are being held on too
            if (bool1 == false && bool2 == false && bool3 == false && bool4 == false && shadowcloneactive == false)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    bool1 = true;
                    this.transform.position = loc1;
                    position = 1;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    bool2 = true;
                    this.transform.position = loc2;
                    position = 2;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    bool3 = true;
                    this.transform.position = loc3;
                    position = 3;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    bool4 = true;
                    this.transform.position = loc4;
                    position = 4;
                }
            }
            //when they are released, turn them back to false
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                bool1 = false;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                bool2 = false;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                bool3 = false;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                bool4 = false;
            }

            //rotation
            mousepos = Input.mousePosition;
            mousepos = Camera.main.ScreenToWorldPoint(mousepos);
            mousepos.z = -2.0f;
            offset.x = mousepos.x - this.transform.position.x;
            offset.y = mousepos.y - this.transform.position.y;
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (bulletcount <= 0 && shootbool == true && infinteactive == false)
            {
                shootbool = false;
                reloadbool = true;
            }

            //Shooting
            if (Input.GetMouseButtonDown(0) && shootbool == true && infinteactive == false)
            {
                //Debug.Log(bulletcount);
                Instantiate(bullet, this.transform.position, Quaternion.identity);
                GameObject bulletIcon = GameObject.Find("bullet" + this.bulletcount);
                bulletIcon.SetActive(false);
                this.bulletcount -= 1;

            }
            //infinite bullet shooting
            else if (Input.GetMouseButtonDown(0) && infinteactive == true)
            {
                Instantiate(bullet, this.transform.position, Quaternion.identity);
            }
            //reload
            if (reloadbool == true)
            {
                StartCoroutine(Reload());
            }
            //ability timer
            if (infinteactive == true && infinitecooldownbool == false && infiniteshooterbool == true)
            {
                abilitytime = abilitytime - Time.deltaTime;
                if (abilitytime > 0.0f)
                {
                    timerimage.fillAmount = abilitytime / initabilitytimer;
                }
                else
                {
                    infinitecooldownbool = true;
                    abilitytime = initabilitytimer;
                }

            }

            //activate ability
            if (infinteactive == false && Input.GetMouseButtonDown(1) && infinitecooldownbool == false && infiniteshooterbool == true)
            {
                infinteactive = true;
            }
            if (infinitecooldownbool == true && infinteactive == true)
            {
                StartCoroutine(Cooldown());
            }

            //shadowclone 

            //see if it was pressed
            if(shadowclonebool == true && shadowclonepressed == false && shadowclonecooldown == false && Input.GetMouseButtonDown(1))
            {
                shadowclonepressed = true;
                shadowcloneactive = true;
            }
            
            //when pressed, spawn clones
            if(shadowclonepressed == true && shadowcloneactive == true)
            {
                //look at position and spawn others at other points
                if(position == 1)
                {
                    Instantiate(Clone, loc2, Quaternion.identity);
                    Instantiate(Clone, loc3, Quaternion.identity);
                    Instantiate(Clone, loc4, Quaternion.identity);
                }
                else if(position == 2)
                {
                    Instantiate(Clone, loc1, Quaternion.identity);
                    Instantiate(Clone, loc3, Quaternion.identity);
                    Instantiate(Clone, loc4, Quaternion.identity);
                }
                else if(position == 3)
                {
                    Instantiate(Clone, loc1, Quaternion.identity);
                    Instantiate(Clone, loc2, Quaternion.identity);
                    Instantiate(Clone, loc4, Quaternion.identity);
                }
                else if(position == 4)
                {
                    Instantiate(Clone, loc1, Quaternion.identity);
                    Instantiate(Clone, loc2, Quaternion.identity);
                    Instantiate(Clone, loc3, Quaternion.identity);
                }
                shadowclonepressed = false;
            }

            //if it was pressed, make the ability active and timer count down
            if(shadowcloneactive == true  && shadowclonecooldown == false)
            {
                shadowclonetime = shadowclonetime - Time.deltaTime;
                if(shadowclonetime > 0.0f)
                {
                    timerimage.fillAmount = shadowclonetime / initshadowclonetimer;
                }
                else
                {
                    shadowclonecooldown = true;
                    shadowcloneactive = false;
                    shadowclonetime = initshadowclonetimer;

                    //destroy clones
                    GameObject[] Clonedestory = GameObject.FindGameObjectsWithTag("Clone");
                    for(int x =0;x<Clonedestory.Length;x++)
                    {
                        Destroy(Clonedestory[x]);
                    }
                }
            }
            //cooldown of shadowclone
            if(shadowclonecooldown == true)
            {
                //make a countdown appear
                shadowclonecooldowntime = shadowclonecooldowntime - Time.deltaTime;
                //if it reaches zero,reset the timer and bools and make timer bar go back up 
                if (shadowclonecooldowntime<=0.0f)
                {
                    //bools
                    shadowclonecooldown = false;
                    shadowclonepressed = false;
                    //timer
                    shadowclonecooldowntime = initshadowclonecooldown;
                    //timer bar 
                    timerimage.fillAmount = 1;
                }
            }
        }
    }

    IEnumerator Reload()
    {
        reloadbool = false;
        //Debug.Log("reloading");
        yield return new WaitForSeconds(reloadtime);
        //Debug.Log("finished reloading");
        shootbool = true;
        bulletcount = 6;
       for(int x =0;x<bullets.Length;x++)
        {
            bullets[x].SetActive(true);
        }
    }

     IEnumerator Cooldown()
    {
        infinteactive = false;
        yield return new WaitForSeconds(cooldowntimer);
        timerimage.fillAmount = 1;
        infinitecooldownbool = false;
    }

    //getters

    public bool Shootbool
    {
        get
        {
            return shootbool;
        }
    }

    public float Abilitytime
    {
        get
        {
            return abilitytime;
        }
    }

    public bool Reloadbool
    {
        get
        {
            return reloadbool;
        }
    }

    public bool Infiniteactive
    {
        get
        {
            return infinteactive;
        }
    }
}
