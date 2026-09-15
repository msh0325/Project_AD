using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum EnemyState {Chase, Aim, Dash}
    private EnemyState state = EnemyState.Chase;
    public float hp = 50;
    public float exp = 100;
    public float speed = 1f;

    [SerializeField] private float aimDuration = 0.5f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.5f;
    private float stateTimer;  
    private Vector3 dashDirection;

    [SerializeField] private float detectRange = 5f;
    [SerializeField] private EnemyAttack enemyAttack;
    private float lastAttackTime = -999f;
    private bool isMove = false;
    private HurtBox _hurtBox;
    private Player target;

    public void SetMoving(bool value) => isMove = value;
    [SerializeField] private float attackCooltime = 5f;
    void Awake()
    {
        _hurtBox = GetComponent<HurtBox>();
        _hurtBox.onGetDamageFromHitbox.AddListener(GetDamaged);
        target = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if(target == null) return;

        switch(state)
        {            
            case EnemyState.Chase:
                if(CanDetectPlayer() && Time.time - lastAttackTime >= attackCooltime)
                {
                    lastAttackTime = Time.time;
                    state = EnemyState.Aim;
                    stateTimer = aimDuration;
                    enemyAttack.Attack();                    
                }
                break;

            case EnemyState.Aim:
                FacePlayer();
                stateTimer -= Time.deltaTime;
                if(stateTimer <= 0f)
                {
                    dashDirection = transform.forward;
                    state = EnemyState.Dash;
                    stateTimer = dashDuration;
                    enemyAttack.StartCollision();
                }
                break;
            
            case EnemyState.Dash:
                transform.position += dashDirection * dashSpeed * Time.deltaTime;
                stateTimer -= Time.deltaTime;
                if(stateTimer <= 0f)
                {
                    enemyAttack.StopCollision();
                    state = EnemyState.Chase;
                    isMove = true;
                }
                break;
                
        }
    }

    void FixedUpdate()
    {
        if(target == null || !isMove) return;

        MoveToTarget();
    }

    private bool CanDetectPlayer()
    {
        Vector3 toPlayer = target.transform.position - transform.position;
        float dist = toPlayer.magnitude;

        if(dist > detectRange) return false;

        return true;
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void FacePlayer()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.y = 0;

        if(dir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
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
