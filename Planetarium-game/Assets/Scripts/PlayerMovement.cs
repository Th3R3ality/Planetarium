using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float m_moveSpeed;
    public float m_maxVelocity;
    public float m_jumpForce;
    public float m_dashLength;

    [SerializeField] Camera m_camera;
    [SerializeField] Rigidbody m_rigidbody;
    [SerializeField] BoxCollider m_boxCollider;

    [SerializeField] Vector3 m_lastWishDir = Vector3.forward;
    [SerializeField] bool m_wantsDash;
    [SerializeField] bool m_wantsJump;

    private void Start()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position+ m_lastWishDir);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_wantsDash = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_wantsJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (m_wantsDash)
        {
            m_wantsDash = false;
            Dash(m_lastWishDir);
        }

        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        var camForward = m_camera.transform.forward * ver;
        var camRight = m_camera.transform.right * hor;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        var wishDir = camForward + camRight;
        if (hor != 0 || ver != 0)
        {
            m_lastWishDir = wishDir;
        }

        var vel = m_rigidbody.velocity;
        var newVel = wishDir * m_moveSpeed * Time.fixedDeltaTime;
        newVel.y = vel.y;
        if (m_wantsJump)
        {
            m_wantsJump = false;
            newVel.y = m_jumpForce;
        }
        m_rigidbody.velocity = newVel;

        
    }

    void Dash(Vector3 Direction)
    {
        RaycastHit hitInfo;
        if (!Physics.BoxCast(m_boxCollider.center, m_boxCollider.size, Direction, out hitInfo))
        {
            transform.position = transform.position + Direction * m_dashLength;
        }
        else
        {
            transform.position = transform.position + Direction * (hitInfo.distance - 0.1f);
        }
    }
}
