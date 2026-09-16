using UnityEngine;

public class DamageInfo
{
    public float _dmg;
    public Vector3 _dmgPos;
    public HitBox _attackerHitbox;
    public float _knockbackForce;

    public DamageInfo(float dmg, Vector3 dmgPos, HitBox attackerHitbox, float knockbackForce=0f)
    {
        _dmg = dmg;
        _dmgPos = dmgPos;
        _attackerHitbox = attackerHitbox;
        _knockbackForce = knockbackForce;
    }
}
