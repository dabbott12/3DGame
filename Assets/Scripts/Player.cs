using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float defaultMoveSpeed;
    public float runSpeed;
    public float jumpPower;
    public Rigidbody rig;

    public int score;

    private bool isGrounded;
    private bool canDoubleJump;

    public UI ui;

    // Update is called once per frame
    void Update()
    {
        //  gets the horizontal and vertical inputs
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        //  sets our velocity based on our inputs
        rig.velocity = new Vector3(x, rig.velocity.y, z);

        //  creates a copy of our velocity variable and sets Y axis to 0
        Vector3 vel = rig.velocity;
        vel.y = 0;

        //  if we're moving, rotate to face our moving direction
        if (vel.x != 0 || vel.z != 0)
        {
            transform.forward = vel;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = moveSpeed + runSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = defaultMoveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            canDoubleJump = true;
            rig.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        }

        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            if (canDoubleJump)
            {
                canDoubleJump = false;
                rig.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }

        if (transform.position.y < -10)
        {
            GameOver();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up)
        {
            isGrounded = true;
            moveSpeed = defaultMoveSpeed;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int amount)
    {
        score += amount;
        ui.SetScoreText(score);
    }
}
