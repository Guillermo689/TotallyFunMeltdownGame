using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private PlayerMain _playerMain;
    private Animator _animator;

    private int _velocityID;
    private int _isGroundedID;
    private int _isFallingID;
    private int _jumpID;
    // Start is called before the first frame update
    void Start()
    {
        _playerMain = GetComponent<PlayerMain>();
        _animator = GetComponentInChildren<Animator>();
        _velocityID = Animator.StringToHash("Velocity");
        _isGroundedID = Animator.StringToHash("IsGrounded");
        _isFallingID = Animator.StringToHash("IsFalling");
        _jumpID = Animator.StringToHash("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        MovementAnimation();
        GroundedAnimation();
        Fall();
    }

    internal void MovementAnimation()
    {
        _animator.SetFloat(_velocityID, _playerMain._playerMovement._controller.velocity.magnitude);
    }
    internal void GroundedAnimation()
    {
        _animator.SetBool(_isGroundedID, _playerMain._playerMovement._controller.isGrounded);
    }
    internal void DuckAnimation()                              //Activate the duck layer
    {
        _animator.SetLayerWeight(1, 1);
    }
    internal void StandUpAnimation()                            //Deactivate the duck layer
    {
        _animator.SetLayerWeight(1, 0);
    }
    public void Fall()
    {
        _animator.SetBool(_isFallingID, _playerMain._playerMovement._isFalling);
    }
    public void JumpAnimation()
    {
        _animator.SetTrigger(_jumpID);
        StandUpAnimation();
    }
   
}
