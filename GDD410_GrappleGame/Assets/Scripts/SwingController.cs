using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwingController : MonoBehaviour
{
    //Walking State ONLY
    public float speed = 6.0F;
    public float jumpSpeed = 20.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    
    public CharacterController controller;
    public Camera cam;
    enum State { Swinging, Falling, Walking };
    State state;
    public Pendulum pendulum;
    Vector3 previousPosition;
    float distToGround;
    Vector3 hitPos;
    public Transform[] closest_tether;
    public LineRenderer lr;

     void Start()
    {
        controller = GetComponent<CharacterController>();
        state = State.Walking;
        pendulum.bob_tr.transform.parent = pendulum.tether.tether_tr;
        previousPosition = transform.localPosition;

        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }
    void Update()
    { 
        DetermineState();
        Debug.Log(state);
        switch (state)
        {
            case State.Swinging:
                DoSwingAction();
                break;
            case State.Falling:
                DoFallingAction();
                break;
            case State.Walking:
                DoWalkingAction();
                break;
        }
        previousPosition = transform.localPosition;
    }
    bool IsGrounded()
    {
       
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    void DetermineState()
    {
        // Determine State
        if (IsGrounded())
        {
            state = State.Walking;
          
        }
        else if (Input.GetButtonDown("Fire1"))
        {
             if (state == State.Walking)
             {
                pendulum.bob.velocity = moveDirection;
             }

            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (Transform potentialTarget in closest_tether)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            Debug.Log(bestTarget);
            pendulum.SwitchTether(bestTarget.position);
            state = State.Swinging;
           
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if (state == State.Swinging)
            {
                state = State.Falling;
              
            }
        }
    }

    void DoSwingAction()
    {
       
        if (Input.GetAxis("Vertical") == 1)
        {
            pendulum.bob.velocity += pendulum.bob.velocity.normalized * .02f;
        }
        if (Input.GetAxis("Horizontal")== -1)
        {
            pendulum.bob.velocity += -cam.transform.right * .01f;
        }
        if (Input.GetAxis("Horizontal")==1)
        {
            pendulum.bob.velocity += cam.transform.right * .01f;
        }
        transform.localPosition = pendulum.MoveBob(transform.localPosition, previousPosition, Time.deltaTime);
        previousPosition = transform.localPosition;
    }

    void DoFallingAction()
    {
        
        pendulum.arm.length = Mathf.Infinity;
        transform.localPosition = pendulum.Fall(transform.localPosition, Time.deltaTime);
        previousPosition = transform.localPosition;

    }

    void DoWalkingAction()
    {
       
        pendulum.bob.velocity = Vector3.zero;
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0.0f;
            moveDirection *= speed;

            if(moveDirection != Vector3.zero)
            {
                transform.forward = moveDirection;
            }

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
               
            }

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }


        void OnControllerColliderHit(ControllerColliderHit hit)
        {
        if (hit.gameObject.name == "Respawn")
        {
            //if too far from arena, reset level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        }

    void OnCollisionEnter(Collision collision)
    {

        Vector3 undesiredMotion = collision.contacts[0].normal * Vector3.Dot(pendulum.bob.velocity, collision.contacts[0].normal);
        pendulum.bob.velocity = pendulum.bob.velocity - (undesiredMotion * 1.2f);
        hitPos = transform.position;

        if (collision.gameObject.name == "Respawn")
        {
            //if too far from arena, reset level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
}
