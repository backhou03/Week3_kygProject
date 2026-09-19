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

        if (follow)
        {
            Vector3 lookDir = target.position - transform.position;
            lookDir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.fixedDeltaTime * 5f);
            rb.AddForce(lookDir * 0.2f, ForceMode.VelocityChange);
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCont.hp -= 1;
            Debug.Log(playerCont.hp);
            Debug.Log("hit");
        }

    }
    //offset = -0.4f
    //height = 0.6
}

