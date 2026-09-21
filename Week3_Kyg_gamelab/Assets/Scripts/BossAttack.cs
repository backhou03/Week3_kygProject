using UnityEngine;
public class BossAttack : MonoBehaviour
{

    public MyPlayerController cont;
    public PlayerHitImage playerHitImage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHitImage.ShowPlayerHitImage();
            Debug.Log("hhhhhhhhit");
            cont.BossAttack(transform.root.position, 30f);
        }

    }
}
