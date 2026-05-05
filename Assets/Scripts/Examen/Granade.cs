using UnityEngine;
using UnityEngine.Events;

public class Granade : MonoBehaviour
{
    public float timer;
    public float radius;
    public LayerMask mask;
    public UnityEvent OnExplotion;

    void Start()
    {
        Invoke(nameof(OnExplode), timer);
    }

    public void OnExplode()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, radius, mask);

        foreach (var coll in colls)
        {
            Destroy(coll.gameObject);
        }

        // CAMBIO: evento para partículas/sonido
        OnExplotion?.Invoke();

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
