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
    public GameObject fakeBody;
    public Material _red;
    public Renderer renderer;
    public HitCrosshairManager crosshairManager;


    public void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }
    public void HIT(float value)
    {
        try
        {
            crosshairManager.ShowCrosshair();
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
        //gameObject.layer = LayerMask.NameToLayer("Debris"); 원래는 데미지 적용 안시킬려고 만든건데 이제 필요없음

        gameObject.SetActive(false);
        fakeBody.SetActive(true);

    }
    public void HitColor()
    {
        renderer.material = _red;
    }

}
