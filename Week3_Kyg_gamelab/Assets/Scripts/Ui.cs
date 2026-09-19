using TMPro;
using UnityEngine;
public class Ui : MonoBehaviour
{
    public TextMeshProUGUI ammoo;
    public TextMeshProUGUI hp;
    public MyPlayerController cont;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void NowAmmo()
    {
        ammoo.text = "Ammo : " + cont.ammo;
    }

    // Update is called once per frame
    public void NowHp()
    {
        hp.text = "Hp : " + cont.hp;
    }
}
