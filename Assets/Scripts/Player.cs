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

    void Start()
    {
        pcStat = new (){hp = 100, exp = 0, level = 0};
    }

    void Update()
    {
        
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
}
