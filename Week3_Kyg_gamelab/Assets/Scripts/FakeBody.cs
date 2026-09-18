using UnityEngine;

public class FakeBody : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
