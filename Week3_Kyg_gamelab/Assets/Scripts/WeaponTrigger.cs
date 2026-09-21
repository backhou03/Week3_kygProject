using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    public GameObject Active;
    public WeaponeActive WeaponCode;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Active.SetActive(true);
            WeaponCode.WeaponActive();
        }
    }
}
