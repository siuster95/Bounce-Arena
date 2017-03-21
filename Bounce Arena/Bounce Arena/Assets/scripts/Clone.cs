using UnityEngine;
using System.Collections;

public class Clone : Shooter {

   //I need to grab info from Shooter to copy it over here


    //fields
    [SerializeField]
    GameObject BulletClone;
    private GameObject SGO;

    // Use this for initialization
    protected override void Start()
    {
        SGO = GameObject.Find("Shooter_test");
        Shooter SO = SGO.GetComponent<Shooter>();
        this.shootbool = SO.Shootbool;
    }

    // Update is called once per frame
   protected override void Update ()
    {
        //rotation
        this.mousepos = Input.mousePosition;
        this. mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        this.mousepos.z = -2.0f;
        this.offset.x = this.mousepos.x - this.transform.position.x;
        this.offset.y = this.mousepos.y - this.transform.position.y;
        var angle = Mathf.Atan2(this.offset.y, this.offset.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);

        //get updated on shooting bools from original
        SGO = GameObject.Find("Shooter_test");
        Shooter SO = SGO.GetComponent<Shooter>();
        this.shootbool = SO.Shootbool;

        //Shooting
        if (Input.GetMouseButtonDown(0) && shootbool == true && infinteactive == false)
        {
            //Debug.Log(bulletcount);
            Instantiate(BulletClone, this.transform.position, Quaternion.identity);
        }
    }
}
