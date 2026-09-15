using UnityEngine;

[System.Serializable]
public class PlayerStat
{
    public float hp;
    public float exp;
    public int level;
}

public class Player : MonoBehaviour
{
    public PlayerStat pcStat;
    private HurtBox _hurtbox;

    void Awake()
    {
        _hurtbox = GetComponent<HurtBox>();
        _hurtbox.onGetDamageFromHitbox.AddListener(GetDamaged);
    }

    void Start()
    {
        pcStat = new (){hp = 100, exp = 0, level = 0};
    }

    public void GainExp(float exp)
    {
        pcStat.exp += exp;

        while(pcStat.exp >= 100)
        {
            pcStat.exp -= 100;
            pcStat.level ++;
        }
    }

    public void GetDamaged(DamageInfo dmgInfo)
    {
        Debug.Log($"player damaged : {dmgInfo._dmg}");
        pcStat.hp -= dmgInfo._dmg;

        if(pcStat.hp <= 0)
        {
            Debug.Log("game defeat");
            gameObject.SetActive(false);
        }
    }
}
