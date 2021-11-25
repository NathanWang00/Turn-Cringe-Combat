using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Names: Jackson Brazeal
public class Character : Damageable
{
    [SerializeField] public string[] weapons;
    public float moveSpeed = 3f;
    float velX;
    float velY;
    bool facingRight = true;
    Rigidbody2D rigbody;

    // Inherits the damageable code and adds knockback, movement, and turnsystem
    // Seperating into Damageable -> Character -> PlayerCharacter / Enemy because of turn and possible AI reasons

    // Someone tell me (Nathan) if they know a better way to do this while including inheritance. Also I know it's a little overkill but I already had the code.
    public enum States
    {
        Still,
        Running,
        Midair,
        //Hitstun, only needed if they take damage on their turn which shouldn't really happen
        Dead,

        // Player specific stuff
        Menu,
    }

    [SerializeField] // Serialied for debugging, changing it won't do anything
    public States state;

    protected bool dead = false, turnActive = false;

    protected virtual void Start()
    {
        state = States.Still;
        SwitchWeapons(weapons[0]);
        rigbody = GetComponent<Rigidbody2D> ();
    }

    protected override void FixedUpdate()
    {
        // For handling things that occur to a character over a period of time like friction or leftover momentumn
        // One time transitions happen in the fuctions like setting a bool or jumping

        Vector3 localScale = transform.localScale;
        if (velX > 0) {
            facingRight = true;
        }
        else if (velX < 0) {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0))){
            localScale.x *= -1;
        }

        transform.localScale = localScale;
        switch (state) 
        {
            case States.Still:
                break;

            case States.Running:
                break;

            case States.Midair:
                break;

            case States.Dead:
                break;
        }
        base.FixedUpdate();
    }

    private void Update(){
        if(Input.GetKeyDown("1")){
            SwitchWeapons(weapons[0]);
        }
        else if(Input.GetKeyDown("2")){
            SwitchWeapons(weapons[1]);
        }
        //probably want to run check loops for if turn is active so the whole squad doesn't move.
        velX = Input.GetAxisRaw ("Horizontal");
        velY = rigbody.velocity.y;
        rigbody.velocity = new Vector2 (velX * moveSpeed, velY);
        //make them flip you idget.
    }

    public override void Hurt(float damage, Vector2 hitForce)
    {
        rb2D.AddForce(hitForce, ForceMode2D.Impulse);
        base.Hurt(damage, hitForce);
    }

    protected override void Die()
    {
        dead = true;
        base.Die();
    }

    private void SwitchWeapons(string weapon){
        switch(weapon)
        {
            case "revolver":
                DisableAllWeapons();
                this.transform.Find("Revolver").gameObject.SetActive(true);
                break;
            case "sniper":
                DisableAllWeapons();
                this.transform.Find("Sniper").gameObject.SetActive(true);
                break;
            case "dynamite":
                DisableAllWeapons();
                this.transform.Find("Dynamite").gameObject.SetActive(true);
                break;
            case "beer":
                DisableAllWeapons();
                this.transform.Find("Beer").gameObject.SetActive(true);
                break;
            case "horse":
                DisableAllWeapons();
                this.transform.Find("Horse").gameObject.SetActive(true);
                break;
            case "frying pan":
                DisableAllWeapons();
                this.transform.Find("Frying Pan").gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void DisableAllWeapons(){
        foreach(Transform child in this.transform){
            //disable weapons by name
            if(child.tag == "Weapon"){
                    child.gameObject.SetActive(false);
                }
        }
    }
}
