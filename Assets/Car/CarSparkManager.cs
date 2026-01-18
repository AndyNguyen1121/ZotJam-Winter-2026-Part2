using UnityEngine;

public class CarSparkManager : MonoBehaviour
{
    public ParticleSystem sparksParticle;
    public Transform followTarget;

    private void Start()
    {
        sparksParticle.Pause();
    }
    private void Update()
    {
        transform.position = followTarget.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            sparksParticle.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            sparksParticle.Stop();
        }
    }
}
