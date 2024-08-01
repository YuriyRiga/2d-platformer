public class SpawnerCoin : Spawner<Coin>
{
    protected override void Subscribe(Coin coin)
    {
        coin.CoinDisable += Release;
    }

    protected override void Unsubscribe(Coin coin)
    {
        coin.CoinDisable -= Release;
        Destroy(coin.gameObject);
    }
}

