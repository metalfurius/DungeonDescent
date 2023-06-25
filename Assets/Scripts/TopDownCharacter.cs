using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacter : MonoBehaviour
{
    private PlayerInput input;
    [SerializeField]
    private bool rotateTowardMouse;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Camera cam;
    Rigidbody rb;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpDelay = 0.1f;
    private float timeStamp;
    Vector3 movementVector;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        var targetVector = new Vector3(input.InputVector.x, 0, input.InputVector.y);
        movementVector = MoveTowardTarget(targetVector);
        CheckForJump();

        if (!rotateTowardMouse)
        {
            RotateTowardMovementVector(movementVector);
        }
        if (rotateTowardMouse)
        {
            RotateFromMouseVector();
        }
    }

    private void CheckForJump()
    {
        if (input.grounded && input.Jump && timeStamp<=Time.time)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            timeStamp = Time.time + jumpDelay;
        }
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
    }
    private void RotateFromMouseVector()
    {
        Ray ray = cam.ScreenPointToRay(input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            Vector3 targetVector = new(hitInfo.point.x, transform.position.y, hitInfo.point.z);
            Quaternion targetRotation = Quaternion.LookRotation(targetVector - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = movementSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, cam.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        targetVector = Vector3.Normalize(targetVector);
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;

    }
}
