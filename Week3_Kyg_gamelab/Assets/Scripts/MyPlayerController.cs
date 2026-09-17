using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    private MyPlayerInput _input;
    private CharacterController _controller;
    private GameObject _mainCamera;
    [Header("Player")]
    private float _speed;
    public float moveSpeed = 2f;

    public float SpeedChangeRate = 10f; //가속 감속을 위한 수치
    [Space]
    [Header("Cinemachine")]
    public GameObject CinemachineCameraTarget; //카메라 달곳
    public float TopClamp = 70.0f; // 카메라 위로 각도
    public float BottomClamp = -30.0f; // 카메라 아래 각도
    public float CameraAngleOverride = 0.0f; //카메라 고정인거 같은데 아직 이해 못함
    public bool LockCameraPosition = false; //카메라 각도 고정 관련인데 아직 이해 못함
    public float RotationSmoothTime = 0.12f;
    private float _cinemachineTargetYaw; //좌우 각도 조절
    private float _cinemachineTargetPitch; //상하 각도 조절
    private float _targetRotation = 0.0f;
    private float rotationVelocity;
    private float _verticalVelocity; //수직이동 고정을 위한수치
    private float _terminalVelocity = 53.0f;
    private bool IsCurrentDeviceMouse = true;
    private float _threshold = 0.1f;
    private float _rotationVelocity;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera"); //카메라가 비어있으면 카메라 할당
        }
    }
    void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y; //좌우를 조정하는 Yaw에 y값 넣기
        _input = GetComponent<MyPlayerInput>();
        _controller = GetComponent<CharacterController>();
        if (_cinemachineTargetYaw > 0)
        {
            Debug.Log(_cinemachineTargetYaw);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void LateUpdate()
    {
        CameraRotation();
    }
    void Move()
    {
        //원래는 달리기 중인지 확인해야하는데 일단 움직임 먼저 구현하려고 move만 채용
        float targetSpeed = moveSpeed;

        //입력중인지 멈춰 있는지 확인
        if (_input.move == Vector2.zero) targetSpeed = 0;

        //수평의 속도를 가져옴
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        //목표속도에 도달하기 위해 감속,가속을 시키기 위한 오프셋
        float speedOffset = 0.1f;

        float inputMagnitude = 1f; //모름

        //목표속도 감속 가속을 위한 코드
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

            //3자리 올림
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }


        //아래 모름 일단 복붙
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_input.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    private void CameraRotation()
    {

        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {

            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch -= _input.look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
        Debug.Log(CinemachineCameraTarget.transform.rotation.eulerAngles);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

}
