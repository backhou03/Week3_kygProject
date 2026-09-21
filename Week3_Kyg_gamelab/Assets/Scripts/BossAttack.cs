using UnityEngine;
public class BossAttack : MonoBehaviour
{

    public MyPlayerController cont;
    public PlayerHitImage playerHitImage;
    public PlayerHitManager PlayerHitManagee;
    public Ui ui;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHitManagee.PlayerHitManage();
            ui.NowHp();
            cont.BossAttack(transform.root.position, 30f);
        }

    }
}
