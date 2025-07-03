public interface IDamageble : IHealthNotifier
{
	void TakeDamage(int amount);
	void Heal(int amount);
	void Die();
	void SetMaxHealth();
	void SetCanTakeDamage(bool canTakeDamage);
}
