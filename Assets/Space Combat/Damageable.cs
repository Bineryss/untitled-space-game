public interface IDamageable
{
    public Target TargetType { get; set; }
    public void TakeDamage(int ammount);

}
