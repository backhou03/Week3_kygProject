using UnityEngine;

public class BossHit : MonoBehaviour
{
    public enum EnemyCollisionType
    {
        head,
        body,
        leftArm,
        rightArm,
        leftLeg,
        rightLeg
    }
    public EnemyCollisionType bossDamageType;
    public GameObject bossSad;
    public Boss boss;
    public HitCrosshairManager crosshairManager;
    public MyPlayerController Playerconf;
    public GameObject Boss;
    public BossDie bossdie;
    public void BossHIT()
    {
        try
        {
            crosshairManager.ShowCrosshair();


            boss.health -= Playerconf.damage;
            Debug.Log(boss.bossHeadHp);
            if (boss.bossHeadHp <= 0)
            {
                Debug.Log("보스기절");
                StartCoroutine(KnockDownBoss());
            }
            if (boss.health <= 0)
            {

                bossdie.BossDIe();
            }
        }
        catch
        {
            Debug.Log("controller is not connected");
        }
    }
    public System.Collections.IEnumerator KnockDownBoss()
    {
        boss.bossKnockDown = true;
        bossSad.SetActive(true);
        yield return new WaitForSeconds(5);
        boss.bossKnockDown = false;
        bossSad.SetActive(false);
        boss.bossHeadHp = 3;
    }

}
