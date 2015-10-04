using UnityEngine;
using System.Collections;

public class PlayerVisuals : PlayerBase {
    public GameObject deathExplosion;
    public Animator m_Animator;

    float lastRecievedTime;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGui()
	{


	}

    public void serializeState(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) {
            float run = m_Animator.GetFloat("Forward");
            stream.SendNext(run);
        }
        else {
            float run = (float)stream.ReceiveNext();
            updateAnimatorRun(run);
        }
        
    }
    public void deathVisuals()
    {
        Instantiate(deathExplosion, this.transform.position, transform.rotation);
    }
    public void updateAnimator(float m_ForwardAmount, float m_turnAmount, bool m_Crouching, bool m_IsGrounded)
    {
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        //m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Crouch", m_Crouching);
        m_Animator.SetBool("OnGround", m_IsGrounded);
    }

    public void updateAnimatorRun(float m_ForwardAmount)
    {
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
    }

    void updateAnimatorRun(float m_ForwardAmount, float currentTime)
    {

    }

    public void updateAnimatorJump(bool m_IsGrounded)
    {
       // m_Animator.SetBool("OnGround", m_IsGrounded);
    }
}
