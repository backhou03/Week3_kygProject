using UnityEngine;

public class Tr : MonoBehaviour
{
    public GameObject kim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            kim.SetActive(true);
        }
    }
}
