using Cysharp.Threading.Tasks;

public class ParticleFactory : IParticleFactory
{
    private readonly IFactory _factory;

    public ParticleFactory(IFactory factory)
    {
        _factory = factory;
    }

    public async UniTask<ParticleView> CreateParticle(string particleName)
    {
        var prefab = await _factory.CreateAsync(particleName);
        ParticleView view = prefab.GetComponent<ParticleView>();
        return view;
    }

    public async UniTask<ParticleViewText> CreateParticleText(string particleName)
    {
        var prefab = await _factory.CreateAsync(particleName);
        ParticleViewText view = prefab.GetComponent<ParticleViewText>();
        return view;
    }
}