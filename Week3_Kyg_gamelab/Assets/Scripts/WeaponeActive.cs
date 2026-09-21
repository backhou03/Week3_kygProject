using UnityEngine;

public class WeaponeActive : MonoBehaviour
{
    public GameObject Secret;
    public GameObject WeaponObject;
    public bool weapon = false;
    public MyPlayerController PlayerCont;


    public void WeaponActive()
    {
        WeaponObject.SetActive(false);
        Secret.SetActive(true);
        PlayerCont.damage *= 10000;
    }

}
