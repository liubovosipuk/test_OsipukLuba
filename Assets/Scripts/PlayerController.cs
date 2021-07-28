using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float inputDirection;       
    private float verticalVelocity;     
    private float speedMovement = 5.0f;
    private float gravity = 0.2f;
    private float jumpForce = 10.0f;

    private Vector3 moveVector;
    private Vector3 lastMotion; 
    private CharacterController charController;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        moveVector = Vector3.zero; // сбрасываем его в начале каждого кадра
        inputDirection = Input.GetAxis("Horizontal") * speedMovement;

        if (IsControllerGrounded())
        {
            verticalVelocity = 0;
            JumpLogic();
            moveVector.x = inputDirection; // когда перс заземлен мы возволим ему двигаться
        }
        else
        {
            verticalVelocity -= gravity;
            moveVector.x = lastMotion.x;
        }

        moveVector.y = verticalVelocity;
        //moveVector = new Vector3(inputDirection, verticalVelocity, 0);
        charController.Move(moveVector * Time.deltaTime);
        lastMotion = moveVector; // сохраняет его последнее движение и передает в следующий кадр

    } 

    private bool IsControllerGrounded()
    {
        Vector3 leftRayStart;
        Vector3 rigthRayStart;

        leftRayStart = charController.bounds.center;
        rigthRayStart = charController.bounds.center;

        leftRayStart.x -= charController.bounds.extents.x;
        rigthRayStart.x += charController.bounds.extents.x;

        if (Physics.Raycast(leftRayStart, Vector3.down, (charController.height / 2) + 0.2f))
            return true;
        if (Physics.Raycast(rigthRayStart, Vector3.down, (charController.height / 2) + 0.2f))
            return true; 

        //Debug.DrawRay(leftRayStart, Vector3.down, Color.red); 
        //Debug.DrawRay(rigthRayStart, Vector3.down, Color.green); 

        return false; 
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (Input.GetButton("Jump"))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.red, 2.0f);
            moveVector = hit.normal * speedMovement;
            moveVector.y = jumpForce;
        }

        switch (hit.gameObject.tag)
        {
            case "win":
                LevelManager.Instance.Win();
                break;
        }
    }
    private void JumpLogic()
    {
        if (Input.GetButton("Jump"))
        {
            verticalVelocity = jumpForce;
        }
    } 


} // PlayerController
