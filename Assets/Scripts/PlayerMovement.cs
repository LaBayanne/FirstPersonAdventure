using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manage the movements of the player, including :
/// - walking
/// - jumping
/// - crouching
/// Moreover, it applies the gravity to the player.
/// <param name="m_speed">the walking speed.</param>
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    protected float m_walkSpeed = 10f;
    [SerializeField]
    protected float m_crouchingSpeed = 5f;
    protected float m_speed;
    [SerializeField]
    protected float m_gravity = -9.81f;
    [SerializeField]
    protected float m_standingJumpHeight = 3f;
    [SerializeField]
    protected float m_crouchingJumpHeight = 2f;
    protected float m_jumpHeight;

    protected CharacterController m_characterController;

    [SerializeField]
    protected float m_crouchingHeight = 1.6f;
    [SerializeField]
    protected float m_speedToCrouch = 6f;
    protected float m_standingHeight;
    protected bool m_isCrouching = false;
    protected float m_currentHeight;
    protected float m_targetHeight;

    protected Vector3 m_velocity;
    [SerializeField]
    protected Transform m_body;
    [SerializeField]
    protected Camera m_playerCamera;
    [SerializeField]
    protected LayerMask m_blockingMask;


    protected float m_time = 20f;

    protected float m_distanceCameraToPlayerHeight;


    protected void Awake()
    {
        m_characterController = GetComponent<CharacterController>();

        
    }

    protected void Start()
    {
        //We try to fix the framerate at 60 FPS
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        m_speed = m_walkSpeed;
        m_jumpHeight = m_standingJumpHeight;
        m_standingHeight = m_characterController.height;
        m_currentHeight = m_standingHeight;
        m_targetHeight = m_standingHeight;
        m_distanceCameraToPlayerHeight = (transform.position.y + m_characterController.height / 2f) - m_playerCamera.transform.position.y;
    }

    protected void Update()
    {
        ManageCrouching();

        Vector3 walkingMovement = ManageWalking();

        ApplyGravity();

        ManageJumping();

        //Apply movement
        m_characterController.Move((walkingMovement + m_velocity) * Time.deltaTime);
    }

    protected void FixedUpdate()
    {
        LerpHeightAccordingToState();
    }

    protected Vector3 ManageWalking()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 movement = x * transform.right + z * transform.forward;
        movement = movement.normalized * m_speed;

        return movement;
    }

    protected void ApplyGravity()
    {
        if (m_characterController.isGrounded && m_velocity.y < 0f)
        {
            //We still apply a low gravity in order to allow a smooth descent of slopes
            m_velocity.y = -4f;
        }

        m_velocity.y += m_gravity * Time.deltaTime;
    }

    protected void ManageJumping()
    {
        if (Input.GetButtonDown("Jump"))// && m_characterController.isGrounded)
        {
            //We use this test instead of characterController.isGrounded cause his distance is low,
            //and it doesn't allow to jump in stairs descent.
            if (Physics.CheckSphere(transform.position - Vector3.up * m_characterController.height / 2f, 
                m_characterController.radius, m_blockingMask))
            {
                m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
            }
        }
    }

    protected void ManageCrouching()
    {
        //Crouching
        if (Input.GetButtonDown("Crouch"))
        {
            if (m_isCrouching)
            {
                //Test if there is enough place to completely stand
                if (!Physics.CheckCapsule(transform.position, transform.position + Vector3.up * (m_standingHeight / 2f + 0.1f),
                    m_characterController.radius, m_blockingMask))
                {
                    m_speed = m_walkSpeed;
                    m_jumpHeight = m_standingJumpHeight;
                    m_currentHeight = m_crouchingHeight;
                    m_targetHeight = m_standingHeight;

                    m_isCrouching = !m_isCrouching;
                    m_time = 0f;
                }
            }
            else
            {
                m_speed = m_crouchingSpeed;
                m_jumpHeight = m_crouchingJumpHeight;
                m_currentHeight = m_standingHeight;
                m_targetHeight = m_crouchingHeight;

                m_isCrouching = !m_isCrouching;
                m_time = 0f;
            }
            
        }
    }

    //This method allows to resize the height of the characterController in a smooth movement
    protected void LerpHeightAccordingToState()
    {
        //when the lerp isn't finish
        if (m_time < 1f)
        {
            m_characterController.height = Mathf.Lerp(m_currentHeight, m_targetHeight, m_time);

            ResizeBodyAccordingToHeight();

            RepositionCameraAccordingToHeight();

            m_time += Time.deltaTime * m_speedToCrouch;
        }
        //when it finished
        else if (m_time < 10f)
        {
            m_characterController.height = m_targetHeight;

            ResizeBodyAccordingToHeight();

            RepositionCameraAccordingToHeight();

            m_time = 20;
        }
    }

    protected void ResizeBodyAccordingToHeight()
    {
        m_body.localScale = new Vector3(m_body.localScale.x, m_characterController.height * 0.5f, m_body.localScale.z);
    }

    protected void RepositionCameraAccordingToHeight()
    {
        m_playerCamera.transform.position = new Vector3(m_playerCamera.transform.position.x,
                transform.position.y + m_characterController.height / 2f - m_distanceCameraToPlayerHeight,
                m_playerCamera.transform.position.z);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.up * m_standingHeight, m_characterController.radius);
        Gizmos.DrawSphere(transform.position - Vector3.up * m_characterController.height / 2f, m_characterController.radius);
    }*/

}
