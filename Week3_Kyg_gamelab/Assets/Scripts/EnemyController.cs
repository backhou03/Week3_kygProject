using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;
    public GameObject left_leg;
    public GameObject right_leg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void die()
    {
        Debug.Log("사망");
        Destroy(gameObject);
    }
    public void LegCut()
    {
        Debug.Log("다리절단");
        if (left_leg.activeInHierarchy == false && right_leg.activeInHierarchy == false)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        else
        {
            return;
        }
    }
}
