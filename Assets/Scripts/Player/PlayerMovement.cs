using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private PlayerMain _playerMain;
    internal CharacterController _controller;
    private Transform _cameraTransform;
    [Header("Movement Variables")]
    public bool CanMove;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float _turnSpeed = 10f;
    private float rotationSmoothTime = 0.12f;

    [Header("Jump Variables")]
    [SerializeField] private float _maxJump;
    [SerializeField] private float _jumpCooldown;
    private Vector3 moveDirection;
    private float yVelocity = 0.0f;
    private bool _isJumping;
    private float _jumpTime;
    private float _jumpCooldownTime;
    internal bool _isFalling;

    [Header("Duck Variables")]
    [SerializeField] private float _duckTime;
    [SerializeField] private Vector3 _characterDuckCenter;

    private PlayerAnimations _playerAnimations;

    void Start()
    {
        _playerMain = GetComponent<PlayerMain>();
        _controller = GetComponent<CharacterController>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _cameraTransform = Camera.main.transform;
       
        CanMove = true;
    }
  
    void Update()
    {
        
        PlayerMove();
        Duck();
        Jump();
    }

    private void PlayerMove()
    {
        if (CanMove)
        {
            Vector2 moveInput = _playerMain._playerControls._move.ReadValue<Vector2>();                                                                             //Get Character Input
            moveDirection = new Vector3(moveInput.x, 0, moveInput.y);                                                                   //Set the direction in the variable
            moveDirection *= moveSpeed;
            Vector3 inputDirection = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;                                            //Set the variable for the player input
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;    //Calculate the direction depending on the camera angle
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _turnSpeed, rotationSmoothTime);        //Set the turn speed for the player to turn smoothly
            transform.rotation = Quaternion.Euler(0f, rotation, 0f);                                                                    //Rotate the player according the camera angle
        }
        else moveDirection = Vector3.zero;                                                                                              //If the character is inmobilized, set the movedirection to zero so he stand still
        moveDirection.y = yVelocity;                                                                                                    //This is the gravity force applied on the Jump() function
        _controller.Move(moveDirection * Time.deltaTime);                                                                               // Move the character
    }
    private void Jump()
    {

        if (_controller.isGrounded)                                                                            //Check if player is grounded
        {
            _jumpTime = 0;
            if (_jumpCooldownTime > 0) _jumpCooldownTime -= Time.deltaTime;
        }
        if (_playerMain._playerControls._jump.IsPressed() && _jumpTime < _maxJump && _jumpCooldownTime <= 0 && CanMove)                            //If the player is ready to jump again, jups and start the animation
        {
            _playerAnimations.JumpAnimation();
            _jumpTime += Time.deltaTime;
            _isJumping = true;
        }
        else _isJumping = false;
       


        float jumpVelocity = Mathf.Sqrt(jumpHeight * -2.0f * gravity);                                      //Calculating the jump force

        if (_isJumping)                                                                                     //Set the jump cooldown so when the player gets to the ground, will have some time to be able to jump again
        {
            yVelocity = jumpVelocity;
            _jumpCooldownTime = _jumpCooldown;
        }
        else yVelocity += gravity * Time.deltaTime;
    }

    private void Duck()
    {
        if (!_controller.isGrounded) return;
        if (_playerMain._playerControls._duck.WasPressedThisFrame() || Mouse.current.rightButton.wasPressedThisFrame)
        {
            StartCoroutine(SetCharacterHeight());
        }
       
    }

    IEnumerator SetCharacterHeight()                                //Reduce the size of the collider to prevent collission with the cylinder and ajust the height of the collider, later on set it back to normal
    {
        float originalHeight = _controller.height;                  //Store the original heigh of the character controller
        Vector3 originalCenter = _controller.center;                //Also store the center of the collider because when you reduce the height, you have to adjust the collider position because t goes down a bit
        _controller.height = originalHeight / 2;                    //Duck the player and enable the animation layer for the duck
        _controller.center = _characterDuckCenter;
        _playerAnimations.DuckAnimation();
        yield return new WaitForSeconds(_duckTime);
        _playerAnimations.StandUpAnimation();                       //Restore everything to the previous values
        _controller.height = originalHeight;
        _controller.center = originalCenter;
    }

    public void Fall()                                 //Set the trigger to fall to trigger the animation and inmovilize the character
    {
        _isFalling = true;
        CanMove = false;
    }


    public void GetUp()                                 //This is trigger by the getting up animation to allow the player to move again
    {
        if (_isFalling && _controller.isGrounded)
        {
            _isFalling = false;
            CanMove = true;
        }
    }
   
       
}

