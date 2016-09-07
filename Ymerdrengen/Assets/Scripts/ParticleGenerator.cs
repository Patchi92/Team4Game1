using UnityEngine;
using System.Collections;
/// <summary
/// Particle generator.
/// 
/// The particle generator simply spawns particles with custom values. 
/// See the Dynamic particle script to know how each particle works..
/// 
/// Visit: www.codeartist.mx for more stuff. Thanks for checking out this example.
/// Credit: Rodrigo Fernandez Diaz
/// Contact: q_layer@hotmail.com
/// </summary>

public class ParticleGenerator : MonoBehaviour
{
    float SPAWN_INTERVAL = 0.005f; // How much time until the next particle spawns
    float lastSpawnTime = float.MinValue; //The last spawn time
    public int PARTICLE_LIFETIME = 3; //How much time will each particle live
    public Vector3 particleForce; //Is there a initial force particles should have?
    public Transform particlesParent; // Where will the spawned particles will be parented (To avoid covering the whole inspector with them)
    [SerializeField]
    private int particleCount = 0;

    public int MaxParticles = 250;

    void Start() { }

    void Update()
    {
        if (particleCount < MaxParticles)
        {
            if (lastSpawnTime + SPAWN_INTERVAL < Time.time)
            {
                // Is it time already for spawning a new particle?
                GameObject newLiquidParticle = (GameObject)Instantiate(Resources.Load("LiquidPhysics/DynamicParticle"));
                Rigidbody rigidBody = newLiquidParticle.GetComponent<Rigidbody>();
                //Spawn a particle
                rigidBody.AddForce(particleForce);
                newLiquidParticle.transform.localScale = newLiquidParticle.transform.localScale * Random.Range(0.5f, 1.3f);
                rigidBody.mass = Random.Range(1f, 50f);
                rigidBody.drag = newLiquidParticle.transform.localScale.sqrMagnitude * 3f;
                newLiquidParticle.transform.position = transform.position;
                newLiquidParticle.transform.parent = particlesParent;			
                lastSpawnTime = Time.time;
                particleCount++;
            }
        }
    }
}
