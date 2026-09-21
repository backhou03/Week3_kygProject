using UnityEngine;

public class HitCrosshairManager : MonoBehaviour
{
    public GameObject hitCrosshair;
    public void ShowCrosshair()
    {
        StartCoroutine(CrosshairActive());
    }
    public System.Collections.IEnumerator CrosshairActive()
    {
        Debug.Log("활성화");
        hitCrosshair.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitCrosshair.SetActive(false);
    }
}
