using Cysharp.Threading.Tasks;

public interface IParticleFactory
{
    UniTask<ParticleView> CreateParticle(string particleName);
    UniTask<ParticleViewText> CreateParticleText(string particleName);
}