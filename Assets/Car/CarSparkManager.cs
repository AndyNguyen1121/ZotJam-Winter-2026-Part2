using UnityEngine;

public class CarSparkManager : MonoBehaviour
{
    public ParticleSystem sparksParticle;
    public Transform followTarget;

    private void Start()
    {
        sparksParticle.Stop();
    }
    private void Update()
    {
        transform.position = followTarget.position;
    }

    private void OnTriggerStay(Collider other)
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
