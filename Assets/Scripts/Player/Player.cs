
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Player : BaseObject
{
    [SerializeField] private TextMeshProUGUI textPoint;
    [SerializeField] private int InitalDamage;
    [SerializeField] private int InitalMaxHealth;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;

    [SerializeField] private float critRate;
    public float CritRate => this.critRate;
    [SerializeField] private float bloodThirstRate;
    public float BloodThirstRate => this.bloodThirstRate;
    [SerializeField] private float rageRate;
    public float RageRate => this.rageRate;
    [SerializeField] private float poisonedRate;
    public float PoisonedRate => this.poisonedRate;
    [SerializeField] private float blazeRate;
    public float BlazeRate => this.blazeRate;
    [SerializeField] private float boltRate;
    public float BoltRate => this.boltRate;
    [SerializeField] private BallCircle ballCircle;
    [SerializeField] private List<AbilityType> abilities;
    public List<AbilityType> Abilities => this.abilities;

    private bool immortal = false;

    public override float HP
    {
        get => base.HP;
        protected set
        {
            base.HP = value;

            // update text point
            textPoint.SetText(((int)HP).ToString());

            // calculate Rage ability again
            if(abilities.Contains(AbilityType.Rage))
                rageRate = Rage.CalculateRageRate(Abilities, 1 - HP / maxhp);
        }

    }

    public override float Damage 
    {
        get => base.Damage * (1 + rageRate);
        protected set => base.Damage = value; 
    }

    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Player>();
            return _instance;
        }
    }

    protected virtual void Start()
    {
        Damage = InitalDamage;
        HP = maxhp = InitalMaxHealth;
        abilities ??= new List<AbilityType>();

        GameManager.Instance.RegisterObserver(this);
        OnInstantiate();
    }

    public override void OnInstantiate()
    {
        HP = maxhp = InitalMaxHealth;
        Damage = InitalDamage;
        abilities ??= new List<AbilityType>();
        playerAttack.AttackSpeed = 1;
        critRate = 0;
        bloodThirstRate = 0;
        rageRate = 0;
        poisonedRate = 0;
        blazeRate = 0;
        boltRate = 0;
        ballCircle.FireCircle = 0;
        ballCircle.PoisonCircle = 0;
        ballCircle.BoltCircle = 0;
    }

    public override void Die(int time = 0)
    {
        GameManager.Instance.OnGameOver();
    }

    public override void OnGamePaused(bool isPaused)
    {
        base.OnGamePaused(isPaused);
        playerAttack.enabled = !isPaused;
        playerMovement.enabled = !isPaused;
        ballCircle.ani.enabled = !isPaused;
    }

    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // prevent enemy push player temporary.
            Physics2D.IgnoreCollision(col, collision.collider, true);
        }

        await Task.Delay(50);
        if (this == null) return;
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(col, collision.collider, false);
        }
    }

    public override void HittedSound(DamageType type)
    {
        AudioManager.instance.PlaySound(Sound.Name.BodyHit4100001);
    }

    public override void TakeDamage(float damage, DamageType type = DamageType.Nomal)
    {
        if (immortal) return;
        base.TakeDamage(damage, type);

        FloatingText.Instantiate(transform.position).SetText("-" + damage.ToString(), type);

        HP -= damage;
        if (HP <= 0) Die();
    }

    public void ReturnToInitialPosition()
    {
        gameObject.transform.position = new Vector2(0, -3);
    }

    public void AddAbility(AbilityType type)
    {
        //playerAttack.bullet.gameObject.AddComponent<>();
        //AbilityFactory.AddAbility(type, playerAttack.bullet.gameObject);
        Abilities.Add(type);

        if(type is AbilityType.CritMaster) 
            critRate = CritMaster.CalculateCritRate(Abilities);

        else if (type is AbilityType.AttackBoost)
            Damage = InitalDamage * AttackBoost.CalculateAttackBoostRate(Abilities);

        else if (type is AbilityType.AttackSpeedBoost)
            playerAttack.AttackSpeed = AttackSpeedBoost.CalculateAttackBoostRate(Abilities);

        else if (type is AbilityType.BloodThirst)
            bloodThirstRate = BloodThirst.CalculateBloodThirstRate(Abilities);

        else if (type is AbilityType.Rage)
            rageRate = Rage.CalculateRageRate(Abilities, 1 - HP/maxhp);

        else if (type is AbilityType.PoisonedTouch)
            poisonedRate = PoisonedTouch.CalculatePoisonedRate(Abilities);

        else if (type is AbilityType.Blaze)
            blazeRate = Blaze.CalculateBlazeRate(Abilities);

        else if (type is AbilityType.Bolt)
            boltRate = Bolt.CalculateBoltRate(Abilities);

        else if (type is AbilityType.FireCircle)
            ballCircle.FireCircle = FireCircle.CalculateFireCircle(Abilities);

        else if (type is AbilityType.BoltCircle)
            ballCircle.BoltCircle = BoltCircle.CalculateBoltCircle(Abilities);

        else if (type is AbilityType.PoisonCircle)
            ballCircle.PoisonCircle = PoisonCircle.CalculatePoisonCircle(Abilities);

        else if (type is AbilityType.HPBoost)
        {
            float rate = HPBoost.CalculateHPBoostRate(Abilities);
            if(InitalMaxHealth * rate > maxhp)
                Healing(InitalMaxHealth * rate - maxhp);
            maxhp = InitalMaxHealth * rate;

        }
    }

    public void ActiveBloodThirst()
    {
        float heal = maxhp * bloodThirstRate;
        if (heal + HP > maxhp) heal = maxhp - HP;

        if (heal > 0) Healing(heal);
    }

    private void Healing(float heal)
    {
        HP+=heal;

        FloatingText.Instantiate(transform.position)
            .SetText(((int)heal).ToString(), DamageType.Healing);
    }

    public async void Revive()
    {
        HP = maxhp * 0.7f;
        immortal = true;
        await Task.Delay(1500);
        if (this == null) return;
        immortal = false;
    }

    //public void OnGamePaused(bool isPaused)
    //{
    //    if (isPaused)
    //    {
    //        var scriptComponents = this.GetComponents<MonoBehaviour>();
    //        foreach (var script in scriptComponents)
    //        {
    //            script.enabled = false;
    //        }
    //    }
    //    else
    //    {
    //        var scriptComponents = this.GetComponents<MonoBehaviour>();
    //        foreach (var script in scriptComponents)
    //        {
    //            script.enabled = true;
    //        }
    //    }
    //}
}
