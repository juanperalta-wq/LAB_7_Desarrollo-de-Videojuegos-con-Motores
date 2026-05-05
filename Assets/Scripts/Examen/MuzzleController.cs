using UnityEngine;
using System.Collections.Generic;

public class MuzzleController : MonoBehaviour
{
    public List<int> ListaEnemigos;

    public Transform Enemy;
    public GameObject BulletPrefab;
    public GameObject headTurret;
    public Transform FirePointRight;
    public Transform FirePointLeft;

    public float rotationSpeed;
    public float BulletSpeed = 20f;

    public float RatioDetection = 10;
    public LayerMask enemyLayer;

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
        if (Enemy != null)
        {
            Shoot();
        }
    }

    public void Detection()
    {
        //detección automática con OverlapSphere
        Collider[] enemies = Physics.OverlapSphere(transform.position, RatioDetection, enemyLayer);

        if (enemies.Length == 0)
        {
            Enemy = null;
            return;
        }

        Enemy = enemies[0].transform;

        Vector3 dir = Enemy.position - transform.position;
        Quaternion targetQuaternion = Quaternion.LookRotation(dir);

        headTurret.transform.rotation = Quaternion.Slerp(headTurret.transform.rotation,targetQuaternion,rotationSpeed * Time.deltaTime);
    }

    public void Shoot()
    {
        if (Enemy == null) return;

        //control de distancia real
        float distance = Vector3.Distance(transform.position, Enemy.position);
        if (distance > RatioDetection) return;

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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RatioDetection);
    }
}