using UnityEngine;

public class AttackAnimations : MonoBehaviour
{
    public Animator anim;
    public ThirdPersonMovement movement;
    public PlayerHitbox hitbox;

    private void Start()
    {
        movement = GetComponentInParent<ThirdPersonMovement>();
        anim = movement.anim;
    }

    private void Update()
    {
        Punch();
        Kick();
    }
    void Punch()
    {
        if (Input.GetMouseButtonDown(0) && !movement.isMoving &&
            anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetBool("Punch", true);
            movement.canMove = false;
        }
    }

    void Kick()
    {
        if (Input.GetMouseButtonDown(1) && !movement.isMoving &&
            anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetBool("Kick", true);
            movement.canMove = false;
        }
    }

    public void ShowHitbox(string attack)
    {
        if (attack.Equals("Punch"))
        {
            hitbox.ChangeSize(1, 1.5f);
        }
        else if (attack.Equals("Kick"))
        {
            hitbox.ChangeSize(2, 2);
        }
        hitbox.gameObject.SetActive(true);
    }

    public void HideHitbox()
    {
        hitbox.gameObject.SetActive(false);
    }


    public void ReturnFromAttack(string name)
    {
        anim.SetBool(name, false);
        movement.canMove = true;
    }
}
