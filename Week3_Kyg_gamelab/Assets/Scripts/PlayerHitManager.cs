using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHitManager : MonoBehaviour
{
    public MyPlayerController playerCont;
    public PlayerHitImage img;
    public bool IsInvincible = false;
    public void PlayerHitManage()
    {
        if (IsInvincible) return;
        playerCont.hp -= 1;
        img.ShowPlayerHitImage();
        if (playerCont.hp > 0)
        {

            Debug.Log(playerCont.hp);

            StartCoroutine(PlayerHit());
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }

    }
    System.Collections.IEnumerator PlayerHit()
    {
        IsInvincible = true;
        yield return new WaitForSeconds(1f);
        IsInvincible = false;
    }

}
