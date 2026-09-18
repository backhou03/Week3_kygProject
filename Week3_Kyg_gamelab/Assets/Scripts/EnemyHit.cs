using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public enum EnemyCollisionType
    {
        head,
        body,
        leftArm,
        rightArm,
        leftLeg,
        rightLeg
    }
    public EnemyCollisionType damageType;
    public EnemyController controller;


    public void HIT(float value)
    {
        try
        {
            controller.health -= value;
            if (controller.health <= 0)
                controller.die();
        }
        catch
        {
            Debug.Log("controller is not connected");
        }
    }
    public void Down()
    {
        transform.SetParent(null);
        gameObject.AddComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("Debris");

    }
}
