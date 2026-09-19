using Unity.Cinemachine;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public CinemachineThirdPersonFollow thirdPersonFollow;
    public float targetDistance = 2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void ZoomAim()
    {
        thirdPersonFollow.CameraDistance = targetDistance;
    }
}
