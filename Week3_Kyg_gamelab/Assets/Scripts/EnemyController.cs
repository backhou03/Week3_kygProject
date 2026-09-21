using System.Collections;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public float health = 100f;
    public GameObject left_leg;
    public GameObject right_leg;
    public Transform target;
    public Rigidbody rb;
    public MyPlayerController playerCont;
    public bool follow;
    public Collider left_Arm;
    public Collider right_Arm;
    public Ui ui;
    public PlayerHitImage playerHitImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = (target.position - transform.position).magnitude;
        Vector3 lookDir = target.position - transform.position;
        if (follow && (distance < 6))
        {
            /*            Debug.Log(distance);
                        Debug.Log(lookDir.x);*/
            lookDir.y = 0;
            Vector3 moveDir = lookDir.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.fixedDeltaTime * 5f);
            rb.AddForce(moveDir * 2.5f, ForceMode.VelocityChange);
            rb.linearVelocity = Vector3.zero;
        }



    }
    public void die()
    {
        Debug.Log("사망");
        Destroy(gameObject);
    }
    public void LegCut()
    {
        Debug.Log("다리절단");
        if (left_leg.activeInHierarchy == false && right_leg.activeInHierarchy == false)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            follow = false;
            StartCoroutine(Dd());
            rb.useGravity = true;

        }
        else
        {
            return;
        }
    }

    IEnumerator Dd()
    {
        yield return new WaitForSeconds(2f);
        follow = true;



    }
    public void AttackEnable()
    {
        left_Arm.enabled = true;
        right_Arm.enabled = true;
    }
    public void AttackDisable()
    {
        left_Arm.enabled = false;
        right_Arm.enabled = false;

    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            playerHitImage.ShowPlayerHitImage();
            playerCont.hp -= 1;
            ui.NowHp();
            Debug.Log("현재 체력 : " + playerCont.hp);
            Debug.Log("hit");
        }

    }
    //offset = -0.4f
    //height = 0.6
}

