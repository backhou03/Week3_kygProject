using UnityEngine;

public class BossDie : MonoBehaviour
{
    public GameObject Boss;
    public GameObject BossEffect;
    public GameObject wall;
    public void BossDIe()
    {
        StartCoroutine(BossDieStart());
    }
    System.Collections.IEnumerator BossDieStart()
    {
        BossEffect.SetActive(true);
        wall.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        Boss.SetActive(false);
        //BossEffect.SetActive(false);
    }
}
