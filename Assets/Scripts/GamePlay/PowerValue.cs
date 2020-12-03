using UnityEngine;
public class PowerValue:MonoBehaviour
{
    // Start is called before the first frame update
    public float startHP;
    public float HP;
    public float MaxShield;
    public float Shield;
    public float basicDamage;
    public float damage;
    public int Score;

    private Animator anim;
    private GameObject[] Coin;
    private bool isDead;
    [HideInInspector]
    public float lastTimeGetDame;
    private float lastTimeShieldUp;
    private void Start()
    {
        if (startHP == 0) HP = 1;
        else HP = startHP;
        if (basicDamage == 0)
        {
            damage = 1;
            basicDamage = 1;
        }
        else damage = basicDamage;
        Shield = MaxShield;
        gameObject.TryGetComponent<Animator>(out anim);
        Coin = new GameObject[3];
        Coin[0] = Resources.Load<GameObject>("GameObject/Item/Coins/Coin_Bronze");
        Coin[1] = Resources.Load<GameObject>("GameObject/Item/Coins/Coin_Silver");
        Coin[2] = Resources.Load<GameObject>("GameObject/Item/Coins/Coin_Gold");
    }
    private void Update()
    {
        if (HP <= 0 && !isDead)
        {
            isDead = true;
            gameObject.layer = LayerMask.NameToLayer("Die");
            SetLayerRecursively(gameObject,LayerMask.NameToLayer("Die"));
            if (gameObject.tag != "Player")
            {
                Object.Destroy(this.gameObject, 2f);
                Invoke("DropCoin", 1f);
            }
            anim.SetBool("Die", true);
        }
        if(HP>0) ShieldUp();
    }
    private void SetLayerRecursively(GameObject gameObject, int layerNumber)
    {
        if (gameObject == null) return;
        foreach (Transform trans in gameObject.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
    public void Health(float HealthHP)
    {
        HP += HealthHP;
        if (HP > startHP)
        {
            HP = startHP;
        }
    }
    public void GetDamage(float reasonDamage)
    {
        float dame = reasonDamage;
        if (Shield > 0)
        {
            if(Shield >= dame)
            {
                Shield -= dame;
                dame = 0;
            }
            else
            {
                Shield = 0;
                dame -= Shield;
            }
            
        }
        if (dame < HP) this.HP -= dame;
        else HP = 0;
        lastTimeGetDame = Time.time;
    }
    void ShieldUp()
    {
        if(Time.time - lastTimeGetDame >= 7f)
        {
            if (Time.time - lastTimeShieldUp >= 2f)
            {
                lastTimeShieldUp = Time.time;
                if (Shield < MaxShield) Shield++;
            }
        }
    }

    private void DropCoin()
    {
        while (Score > 0)
        {
            if (Score >= 12) 
            {
                Coin[2].GetComponent<Coin>().isBounce = true;
                Instantiate(Coin[2], transform.position, new Quaternion());
                Coin[2].GetComponent<Coin>().isBounce = false;
                Score -= 3;
            }
            else if (Score >= 5)
            {
                Coin[1].GetComponent<Coin>().isBounce = true;
                Instantiate(Coin[1], transform.position, new Quaternion());
                Coin[1].GetComponent<Coin>().isBounce = false;
                Score -= 2;
            }
            else
            {
                Coin[0].GetComponent<Coin>().isBounce = true;
                Instantiate(Coin[0], transform.position, new Quaternion());
                Coin[0].GetComponent<Coin>().isBounce = false;
                Score -= 1;
            }
        }
    }
}
