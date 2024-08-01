public class SpawnerHeal : Spawner<Heal>
{
    protected override void Subscribe(Heal heal)
    {
        heal.HealDisable += Release;
    }

    protected override void Unsubscribe(Heal heal)
    {
        heal.HealDisable -= Release;
        Destroy(heal.gameObject);
    }
}