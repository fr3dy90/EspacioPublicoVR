using UnityEngine;

//TODO: Esto es increiblemente cochino, debe poder arreglarse mejor INVESTIGAR MEJOR
public class ParticlesController : MonoBehaviour
{
    private ParticleSystem[] particleSystems;
    [SerializeField] private bool isOneShot = false;
    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void ShowParticles()
    {
        if(particleSystems != null && particleSystems.Length > 0)
        {
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Play();
            }
        }
    }

    public void HideParticles()
    {
        if (particleSystems != null && particleSystems.Length > 0)
        {
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Stop();
            }
        }
        gameObject.SetActive(false);
    }

    public void DestroyParticles()
    {
        Destroy(gameObject);
    }
    private void LateUpdate()
    {
        if (isOneShot)
        {
            bool allParticlesStopped = true;
            if (particleSystems != null && particleSystems.Length > 0)
            {
                foreach (ParticleSystem ps in particleSystems)
                {
                    if (ps.IsAlive(true))
                    {
                        allParticlesStopped = false;
                        break;
                    }
                }
            }

            if (allParticlesStopped)
            {
                DestroyParticles();
            }
        }
    }
}
