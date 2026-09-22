using UnityEngine;
public class Boss : MonoBehaviour
{
    public float health = 10000;
    public GameObject left_leg;
    public GameObject right_leg;

    public Transform target;
    public Rigidbody rb;
    public MyPlayerController playerCont;
    public bool follow;
    public Ui ui;
    public Collider hand;
    public GameObject boss;
    public Animator ani;
    public float bossHeadHp = 3;
    public float bossDistance;
    public bool bossKnockDown = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        /*        if (left_leg.activeInHierarchy == false && right_leg.activeInHierarchy == false)
                {


                }*/
        float distance = (target.position - transform.position).magnitude;
        Vector3 lookDir = target.position - transform.position;
        if (follow && (distance < 100) && (bossKnockDown == false))
        {
            /*            Debug.Log(distance);
                        Debug.Log(lookDir.x);*/
            lookDir.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.fixedDeltaTime * 5f);
            rb.AddForce(lookDir * 0.1f, ForceMode.VelocityChange);
            rb.linearVelocity = Vector3.zero;
        }
        Vector3 ddistance = transform.position - target.transform.position;
        bossDistance = ddistance.sqrMagnitude;
        if (bossDistance < 21 && (bossKnockDown == false))
        {
            ani.SetTrigger("Hit");
        }
        if (bossKnockDown)
            rb.linearVelocity = Vector3.zero;


    }

    public void die()
    {
        Debug.Log("사망");
        Destroy(gameObject);
    }
    public void EnableHit()
    {
        if (hand != null)
        {
            hand.enabled = true;
        }



        //offset = -0.4f
        //height = 0.6
    }
    public void disableHit()
    {
        if (hand != null)
        {
            hand.enabled = false;
        }
    }
}

