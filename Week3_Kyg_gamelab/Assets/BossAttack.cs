using System.Collections;
using UnityEngine;
public class BossAttack : MonoBehaviour
{
    public Animator ani;
    public MyPlayerController cont;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {

            ani.SetTrigger("Hit");
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        cont.IsHit = true;
        yield return new WaitForSeconds(2);
        cont.IsHit = false;
    }
}
