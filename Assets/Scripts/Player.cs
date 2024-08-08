using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    public Animator animator;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += gravity * Time.deltaTime * Vector3.down;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump") ) {
                StartCoroutine(jump());
                //Jump();
            }
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {

                    StartCoroutine(jump());

                    //Jump();
                }
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        }
    }

    private void Jump()
    {
        direction = Vector3.up * jumpForce;

        animator.SetInteger("jump", 1);
    }
    
    IEnumerator jump()
    {
        direction = Vector3.up * jumpForce;

        animator.SetInteger("jump", 1);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetInteger("jump", 0);

    }
}
