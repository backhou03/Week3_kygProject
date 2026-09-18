using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    private MyPlayerInput _input;
    private CharacterController _controller;
    private GameObject _mainCamera;
    public EnemyController enemy_controller;
    private EnemyHit EnemyH;
    [Header("Player")]
    private float _speed;
    private float sprintSpeed = 5.335f;
    public float moveSpeed = 2f;
    public float damage = 10.0f;

    public float SpeedChangeRate = 10f; //가속 감속을 위한 수치
    [Space]
    [Header("Cinemachine")]
    public GameObject CinemachineCameraTarget; //카메라 달곳
    public float TopClamp = 70.0f; // 카메라 위로 각도
    public float BottomClamp = -30.0f; // 카메라 아래 각도
    public float CameraAngleOverride = 0.0f; //카메라 각도 조절용
    public bool LockCameraPosition = false; //필요시 카메라 잠김 활성화를 위한 코드
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
    [SerializeField] private GameObject crosshair;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera"); //카메라가 비어있으면 카메라 할당
        }
        _input = GetComponent<MyPlayerInput>();
        _controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        //시작할때 마우스가 이상한곳으로 튀지 않도록 값을 삽입
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;



    }
    private void OnEnable()
    {
        _input.IsShot += Shot;
        _input.IsZoom += Zoom;
    }
    private void OnDisable()
    {
        _input.IsShot -= Shot;
        _input.IsZoom -= Zoom;

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
        float targetSpeed = _input.sprint ? sprintSpeed : moveSpeed;

        //입력중인지 멈춰 있는지 확인
        if (_input.move == Vector2.zero) targetSpeed = 0;

        //수평의 속도를 가져옴
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        //목표속도에 도달하기 위해 감속,가속을 시키기 위한 오프셋
        float speedOffset = 0.1f;

        float inputMagnitude = 1f; //원래 조이스틱을 누른걸 넣어야하는데 고려안함

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


        //키보드 입력 받아서 정규화
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // 화면 회전을 위한 수학 공식같음
        if (_input.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;

            //봐야할 각도로 부드럽게 움직이게 하는 코드
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // 여기부터 109까지 다시 이해
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    private void CameraRotation()
    {
        //카메라가 잠겨있지 않고 0.1보다 더 움직일 경우 실행
        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //마우스면 1 컨트롤러면 타임델타타임
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            //좌우 x
            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            //상하 y
            _cinemachineTargetPitch -= _input.look.y * deltaTimeMultiplier;
        }

        // ClampAngle로 각도 조절을 해주니까 좌우의 입력을 최대로 받아도 보정해주고
        // 상하각도도 보정해줌
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);

    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    private void Shot()
    {

        if (crosshair.activeSelf)
        {

            RaycastHit hit;
            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 15f, LayerMask.GetMask("Enemy")))
            {
                /*                Debug.Log(hit.transform.position);
                                Debug.Log(hit.transform.name);*/
                CheckHit(hit);
            }

            Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 15, Color.red);
        }

    }
    private void CheckHit(RaycastHit hit)
    {
        try
        {
            EnemyH = hit.collider.GetComponent<EnemyHit>();
            switch (EnemyH.damageType)
            {
                case EnemyHit.EnemyCollisionType.head:
                    EnemyH.HIT(damage * 5);
                    Debug.Log("머리");
                    EnemyH.HitColor();
                    break;
                case EnemyHit.EnemyCollisionType.body:
                    EnemyH.HIT(damage * 2.5f);
                    Debug.Log("몸통");
                    EnemyH.HitColor();
                    break;
                case EnemyHit.EnemyCollisionType.leftArm:
                    EnemyH.HIT(damage * 2);

                    EnemyH.Down();

                    break;
                case EnemyHit.EnemyCollisionType.rightArm:
                    EnemyH.HIT(damage * 2);
                    EnemyH.Down();
                    break;
                case EnemyHit.EnemyCollisionType.leftLeg:
                    EnemyH.HIT(damage * 2);


                    EnemyH.Down();
                    EnemyH.controller.LegCut();
                    break;
                case EnemyHit.EnemyCollisionType.rightLeg:
                    EnemyH.HIT(damage * 2);
                    EnemyH.Down();
                    EnemyH.controller.LegCut();
                    break;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("피격 처리 중 에러 발생: " + e.Message);
        }
    }
    private void Zoom(bool isZooming)
    {

        crosshair.SetActive(isZooming);

    }
}