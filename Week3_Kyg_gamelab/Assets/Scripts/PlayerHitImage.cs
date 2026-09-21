using UnityEngine;

public class PlayerHitImage : MonoBehaviour
{
    public GameObject playerHitImage;
    public void ShowPlayerHitImage()
    {
        StartCoroutine(CoroutineImage());
    }
    System.Collections.IEnumerator CoroutineImage()
    {
        playerHitImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerHitImage.SetActive(false);
    }
}
