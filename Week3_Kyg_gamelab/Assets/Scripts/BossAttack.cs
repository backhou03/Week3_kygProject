using UnityEngine;
public class BossAttack : MonoBehaviour
{

    public MyPlayerController cont;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Debug.Log("hhhhhhhhit");
            cont.BossAttack(transform.root.position, 30f);
        }

    }
}
