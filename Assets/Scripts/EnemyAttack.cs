using UnityEngine;

public class EnemyAttack : MonoBehaviour, HitBox.IHitboxResponder
{
    public HitBox hitBox;
    public Animator anim;
    public Enemy enemy;    
    void Awake()
    {
        if(hitBox == null)
        {
            enabled = false;
            return;
        }

        hitBox.SetResponder(this);
    }

    public void Attack()
    {
        enemy.SetMoving(false);
        anim.SetTrigger("Aim");
    }

    public void StartCollision()
    {
        anim.SetTrigger("Dash");
        hitBox.StartCheckingCollision();
    }

    public void StopCollision()
    {
        anim.SetTrigger("End");
        hitBox.StopCheckingCollision();
    }

    public void CollisionedWith(Collider coll)
    {
        HurtBox _hurtBox = coll.GetComponent<HurtBox>();

        if(_hurtBox == null) return;

        DamageInfo info = new (10f, hitBox.transform.position, hitBox);
        _hurtBox.GetDamage(info);
    }
}
