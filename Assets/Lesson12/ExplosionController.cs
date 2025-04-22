using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _sphere;
    [SerializeField] private ParticleSystem _sparks;
    [SerializeField] private ParticleSystem _wave;
    [SerializeField] private ParticleSystem _smoke;



    private ParticleSystem.MainModule _sphereMain;
    private ParticleSystem.ShapeModule _sparksShape;
    private ParticleSystem.ShapeModule _waveShape;
    private ParticleSystem.ShapeModule _smokeShape;

    public void ApplyRadius(float radius)
    {

        if (_sphere != null)
        {
            _sphereMain = _sphere.main;
            _sphereMain.startSize = radius * 2f;
        }

        if (_sparks != null)
        {
            _sparksShape = _sparks.shape;
            _sparksShape.radius = radius * 4f / 3f;
        }

        if (_wave != null)
        {
            _waveShape = _wave.shape;
            _waveShape.radius = radius * 4f / 3f;
        }

        if (_smoke != null)
        {
            _smokeShape = _smoke.shape;
            _smokeShape.radius = radius * 4f / 3f;
        }

    }

    public void Play()
    {
        _sphere.Play(true);
        _sparks.Play(true);
        _wave.Play(true);
        _smoke.Play(true);
    }
}
