using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    private MyPlayerInput _input;
    private CharacterController _controller;
    private float _speed;
    public float moveSpeed = 2f;
    public float _verticalVelocity;
    public float SpeedChangeRate = 10f; //가속 감속을 위한 수치

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input = GetComponent<MyPlayerInput>();
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

        //목표속도 감속 가속을 위한 코드
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * 1, Time.deltaTime * SpeedChangeRate);

            //3자리 올림
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
        Vector3 d = new Vector3(0, 0, 1);
        //가능 방향에 정규화
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
        _controller.Move(inputDirection * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
}
