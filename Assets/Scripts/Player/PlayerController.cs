using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LFS.FirstPerson.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController instance;

        public static PlayerController Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<PlayerController>();

                return instance;
            }
        }

        Rigidbody rb;
        Collider coll;

        [Header("Player state")]
        /*[HideInInspector]*/ public bool canMove = true;
        /*[HideInInspector]*/ public bool isMoving = false;

        [Header("Inputs")]
        [SerializeField] InputActionReference moveInput;

        [Header("Movement Settings")]
        [SerializeField, Range(0f, 10f)] float maxSpeed = 3; //Player max move speed
        [SerializeField, Range(0f, 50f)] float accelerationRate = 20; //Rate at which the player accelerates
        [SerializeField, Range(0f, 50f)] float deccelerationRate = 20; //Rate at which the player deccelerates
        float currentSpeed; //Current speed of the player;
        Vector3 moveDir;

        [Space, Header("Floor detection")]
        [SerializeField] float bottomOffset = 0f; //Offset from player center
        [SerializeField] float floorCheckRadius = 0.2f; //How large the detection for the floor is
        [SerializeField] LayerMask floorLayers; //What layers we can stand on

        [Space, Header("PhysicsMaterial")]
        [SerializeField] PhysicMaterial slideMat;
        [SerializeField] PhysicMaterial stickMat;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();

            Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Confined;
        }

        void FixedUpdate()
        {
            if (canMove)
                Move();
            else
            {
                UpdateMoveSpeed(0, 0);
                rb.velocity = moveDir.normalized * currentSpeed;
            }
        }

        void Move()
        {
            bool isGrounded = CheckFloor(-Vector3.up);
            
            Vector2 dir = moveInput.action.ReadValue<Vector2>();

            isMoving = (dir.x != 0 || dir.y !=0);

            if (isMoving)
                moveDir = (transform.forward * dir.y) + (transform.right * dir.x);

            UpdateMoveSpeed(dir.x, dir.y);
            moveDir = moveDir.normalized * currentSpeed;
            moveDir = AdjustVelocityToSlope(moveDir);

            rb.velocity = moveDir;


            //PhysicsMaterial so we don't slide off of slopes or stick to walls
            /*if (isMoving)
                coll.material = slideMat;
            else
                coll.material = stickMat;*/
        }

        void UpdateMoveSpeed(float x, float z)
        {
            if (isMoving)
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, accelerationRate * Time.deltaTime);
            else
                currentSpeed = Mathf.Lerp(currentSpeed, 0, deccelerationRate * Time.deltaTime);
        }

        Vector3 AdjustVelocityToSlope(Vector3 velocity)
        {
            Ray ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 1.2f))
            {
                Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                Vector3 adjustedVelocity = slopeRotation * velocity;

                if (adjustedVelocity.y < 0)
                {
                    return adjustedVelocity;
                }
            }
            
            if (rb.velocity.y < 0)
                velocity.y += rb.velocity.y;

            return velocity;
        }

        public void DisablePlayer()
        {
            currentSpeed = 0;
            rb.velocity = Vector3.zero;

            canMove = false;

            coll.enabled = false;
            rb.useGravity = false;
        }

        public void EnablePlayer()
        {
            canMove = true;

            coll.enabled = true;
            rb.useGravity = true;
        }

        public bool CheckFloor(Vector3 Direction)
        {
            Vector3 Pos = transform.position + (Direction * bottomOffset);
            Collider[] hitColliders = Physics.OverlapSphere(Pos, floorCheckRadius, floorLayers);
            if (hitColliders.Length > 0)
            {
                return true;
            }

            return false;
        }

        void OnDrawGizmosSelected()
        {
            //floor check
            Gizmos.color = Color.yellow;
            Vector3 Pos = transform.position + (-transform.up * bottomOffset);
            Gizmos.DrawSphere(Pos, floorCheckRadius);
        }
    }
}