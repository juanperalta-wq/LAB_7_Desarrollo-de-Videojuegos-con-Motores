using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
public class MuzzleController : MonoBehaviour
{
    public Transform Enemy;
    public GameObject BulletPrefab;
    public GameObject headTurret;
    public Transform FirePointRight;
    public Transform FirePointLeft;

    public float rotationSpeed;
    public float BulletSpeed = 20f;

    private float fireRate = 1f;
    private float nextFireTime = 0f;
    private bool useRightFirePoint = true;

    public ParticleSystem ShootParticlesLeft;
    public ParticleSystem ShootParticlesRight;
    void Start()
    {

    }

    void Update()
    {
        Detection();
        Shoot();
    }

    public void Detection()
    {
        if (Enemy == null) return;

        Vector3 HeadDir = (Enemy.transform.position - transform.position);

        Quaternion targetQuaternion = Quaternion.LookRotation(HeadDir);
        //transform.rotation = targetQuaternion;
        headTurret.transform.rotation = Quaternion.Slerp(headTurret.transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime);

        float distanceToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);
        if (distanceToEnemy > 15f) return;
    }
    public void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            Transform currentFirePoint = useRightFirePoint ? FirePointRight : FirePointLeft;
            ParticleSystem currentParticles = useRightFirePoint ? ShootParticlesRight : ShootParticlesLeft;
            useRightFirePoint = !useRightFirePoint;

            Debug.Log("Torreta Dispara");
            currentParticles.Play();

            GameObject bullet = Instantiate(BulletPrefab, currentFirePoint.position, currentFirePoint.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = currentFirePoint.forward * BulletSpeed;
            }
            Destroy(bullet, 3f);
        }
    }
}
