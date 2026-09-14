using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 50;
    public float exp = 100;
    private HurtBox _hurtBox;
    private Player target;
    void Awake()
    {
        _hurtBox = GetComponent<HurtBox>();
        _hurtBox.onGetDamageFromHitbox.AddListener(GetDamaged);
        target = FindAnyObjectByType<Player>();
    }

    private void GetDamaged(DamageInfo dmgInfo)
    {
        Debug.Log(dmgInfo._dmg);
        hp -= dmgInfo._dmg;

        if(hp <= 0) 
        {
            _hurtBox.SetIsDead(true);
            Debug.Log($"exp 지급: {exp}");
            target.GainExp(exp);
            
            gameObject.SetActive(false);
        }
    }
}
