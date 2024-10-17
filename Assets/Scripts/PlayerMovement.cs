using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public static bool TriggerTick = false;
    public static bool FreezeControl = false;
    public static int MoveSlots = 6;

    public GameObject CubeVisual;
    public float stepDistance = 1f;
    public float timeBetweenSteps = 0.5f;
    public Vector3 destinationPosition;
    public UnityEvent onDead;

    public LayerMask SlimeLayer;

    float timer;
    Controls m_controls;
    Vector3 destinationRotation;
    Vector2 movementInput;

    public void Awake()
    {
        m_controls = new Controls();
        m_controls.Enable();

        destinationPosition = new Vector3();
        destinationRotation = new Vector3();

        GetComponent<PlayerSlimeSpawner>().MakePath(destinationPosition);
        MoveSlots = 6;
    }

    void Update()
    {
        if (MoveSlots < 0) onDead.Invoke();

        movementInput = m_controls.Player.Movement.ReadValue<Vector2>();
        if(movementInput.x != 0f && movementInput.y != 0f) { movementInput.x = 1f; movementInput.y = 0f; } // <- Hard code only being able to move in one dir at a time, default to x-axis

        timer -= Time.deltaTime;
        if (timer < 0f) timer = 0f;

        if (FreezeControl && timer == 0f)
        {
            destinationPosition = transform.position;
            return;
        }

        TriggerTick = false;
        if (movementInput != Vector2.zero && timer <= 0f)
        {
            if (!VerifyMove(movementInput)) return;

            destinationRotation = Vector3.zero;
            CubeVisual.transform.rotation = Quaternion.identity;

            SnapPlayerPosToGrid();

            Move();

            //On Path Check
            Ray OnSlime = new Ray(destinationPosition, Vector3.down);

            if (Physics.Raycast(OnSlime, out RaycastHit hit, 1f, SlimeLayer))
            {
                hit.collider.gameObject.GetComponent<SlimePathData>().Kill();
                GetComponent<PlayerSlimeSpawner>().MakePath(destinationPosition);
            }
            else
            {
                GetComponent<PlayerSlimeSpawner>().MakePath(destinationPosition);
                MoveSlots--;
            }

            timer = timeBetweenSteps;
        }

        transform.position = Vector3.Lerp(transform.position, destinationPosition, 1f - (timer / timeBetweenSteps));
        CubeVisual.transform.rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, destinationRotation, (1f - (timer/timeBetweenSteps))* 2.5f));
    }

    public void LateUpdate()
    {
        FreezeControl = false;
    }

    void SnapPlayerPosToGrid()
    {
        float snappedX = Mathf.Round(transform.position.x / 2f) * 2f;
        float snappedZ = Mathf.Round(transform.position.z / 2f) * 2f;

        transform.position = new Vector3(snappedX, transform.position.y, snappedZ);
        destinationPosition = transform.position;
    }

    void Move()
    {
        TriggerTick = true;

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        destinationPosition += movement* stepDistance;

        destinationRotation = new Vector3(movement.z * 90f, 0f, -movement.x * 90f);

        return;
    }

    bool VerifyMove(Vector2 attemptedMove)
    {
        Vector3 stepVector = new Vector3(attemptedMove.x, 0f, attemptedMove.y);

        //Chasm Check
        Ray WillBeOnGroundRay = new Ray(transform.position + (2f * stepVector), Vector3.down);

        //Wall Check
        Ray WillHitWall = new Ray(transform.position, stepVector);

        return Physics.Raycast(WillBeOnGroundRay, 1f) && !Physics.Raycast(WillHitWall, 1f);
    }
}
