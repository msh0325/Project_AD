using UnityEngine;

public class PlayerAttack : MonoBehaviour, HitBox.IHitboxResponder
{
    public HitBox hammerHitbox;
    public Animator anim;
    public MoveController moveController;
    void Awake()
    {
        if(hammerHitbox == null)
        {
            enabled = false;
            return;
        }

        hammerHitbox.SetResponder(this);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
    
    public void StartCollision()
    {
        hammerHitbox.StartCheckingCollision();
    }

    public void StopCollision()
    {
        hammerHitbox.StopCheckingCollision();
    }

    public void CollisionedWith(Collider coll)
    {
        HurtBox _hurtBox = coll.GetComponent<HurtBox>();

        if(_hurtBox == null) return;

        DamageInfo info = new (10f, hammerHitbox.transform.position, hammerHitbox);
        _hurtBox.GetDamage(info);
    }
}
