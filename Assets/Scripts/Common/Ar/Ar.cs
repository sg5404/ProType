using UnityEngine;
using UnityEngine.Events;

public class Ar : MonoBehaviour
{
    public float MaxHP { get; set; }
    public float HP { get; set; }
    public float ATK { get; set; }

    public Rigidbody2D rigid { get; protected set; }
    public Vector2 lastVelocity { get; protected set; }

    public bool isDoubleAttack { get; protected set; }

    protected float pushPower;

    public UnityEvent BeforeCrash;
    public UnityEvent AfterCrash;
    public UnityEvent BeforeBattle;
    public UnityEvent AfterBattle;
    public UnityEvent BeforeAttack;
    public UnityEvent AfterAttack;
    public UnityEvent BeforeDefence;
    public UnityEvent AfterDefence;
    public UnityEvent OnOutDie;
    public UnityEvent OnBattleDie;

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected void StatReset() // ��ġ �ʱ�ȭ
    {
        HP = MaxHP;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Out"))
        {
            OnOutDie.Invoke();
        }
        else if (collision.transform.CompareTag("Object"))
        {
            rigid.velocity = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal) * pushPower / 2;
            AfterCrash?.Invoke();
        }
    }

    public virtual void AttackFinish()
    {
        if (!DeadCheck())
            AfterCrash?.Invoke(); //�浹 ���� �ߵ��ϴ� Ʈ����
    }

    protected bool DeadCheck()
    {
        if (HP <= 0)
        {
            OnBattleDie.Invoke();
            if (HP <= 0)
            {
                return true;
            }
        }
        return false;
    }

    public void SetRigidPower(Vector2 power)
    {
        rigid = GetComponent<Rigidbody2D>();

        rigid.velocity = power;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(CompareTag("Out"))
        {
            Debug.Log("����");
            //���߿� ���� ������ ã�Ƽ� ���ƿ��°ͱ��� ������ٿ���
        }
    }
}
