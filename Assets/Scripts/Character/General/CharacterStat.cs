using UnityEngine;

namespace Character.General
{
    public class CharacterStat : MonoBehaviour
    {
        public static CharacterStat Instance;

        [SerializeField] private HealthBar healthBar;

        private int finalMaxHealth = 1;
        private int finalDamage;
        private int finalArmor;
        private float finalSpeed;

        [SerializeField] private int baseMaxHealth;
        [SerializeField] private int baseDamage;
        [SerializeField] private int baseArmor;
        [SerializeField] private float baseSpeed = 4.0f;

        private int offsetMaxHealth = 0;
        private int offsetDamage = 0;
        private int offsetArmor = 0;
        private float offsetSpeed = 0f;

        [SerializeField] private float jumpForce = 7.5f;
        [SerializeField] private float rollForce = 6.0f;
        private int currentHealth = 1;

        private void Awake()
        {
            Instance = this;
            currentHealth = finalMaxHealth = baseMaxHealth;
            finalDamage = baseDamage;
            finalArmor = baseArmor;
            finalSpeed = baseSpeed;
        }

        public void SetStatOffset(int offsetArmor, int offsetHealth, int offsetDamage, int offsetSpeed)
        {
            MaxHealth = baseMaxHealth + (offsetMaxHealth = offsetHealth);
            Armor = baseArmor + (this.offsetArmor = offsetArmor);
            Damage = baseDamage + (this.offsetDamage = offsetDamage);
            Speed = baseSpeed + (this.offsetSpeed = offsetSpeed);
            currentHealth = finalMaxHealth;
        }

        public void ResetStat()
        {
            MaxHealth = baseMaxHealth - offsetMaxHealth;
            Armor = baseArmor - offsetArmor;
            Damage = baseDamage - offsetDamage;
            Speed = baseSpeed - offsetSpeed;
            currentHealth = finalMaxHealth;
        }


        #region Getter Setter

        public int MaxHealth
        {
            get => finalMaxHealth;
            set
            {
                finalMaxHealth = value;
            }
        }


        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = Mathf.Max(0, value);
                healthBar.UpdateHealthValue((float)currentHealth / finalMaxHealth);
                Debug.Log("Remote current health: " + (float)currentHealth / finalMaxHealth);
            }
        }

        public int Damage
        {
            get { return finalDamage; }
            set { finalDamage = value; }
        }

        public int Armor
        {
            get { return finalArmor; }
            set { finalArmor = value; }
        }

        public float Speed
        {
            get { return finalSpeed; }
            set { finalSpeed = value; }
        }

        public float JumpForce => jumpForce;

        public float RollForce => rollForce;

        #endregion
    }
}
