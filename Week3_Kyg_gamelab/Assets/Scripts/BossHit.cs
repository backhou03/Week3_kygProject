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
    public void BossHIT()
    {
        try
        {
            crosshairManager.ShowCrosshair();
            Debug.Log(boss.bossHeadHp);
            boss.bossHeadHp -= 1;

            if (boss.bossHeadHp <= 0)
            {
                Debug.Log("보스기절");
                StartCoroutine(KnockDownBoss());
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
