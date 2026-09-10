using UnityEngine;
using UnityEngine.Events;

public class HurtBox : MonoBehaviour
{
    public UnityEvent<float> onGetDamage;
    public UnityEvent<DamageInfo> onGetDamageFromHitbox;
    public Transform rootTransform;
    public bool IsDead {get => _isDead; set => SetIsDead(value);}
    [SerializeField] private bool _isDead;

    public void GetDamage(float dmg)
    {
        onGetDamage.Invoke(dmg);
    }

    public void GetDamage(DamageInfo info)
    {
        onGetDamageFromHitbox.Invoke(info);
    }

    public void SetIsDead(bool isDead)
    {
        _isDead = isDead;
    }
}
