using UnityEngine;

public class DrawerFixer : MonoBehaviour
{
    [SerializeField] private Vector3 startForce;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(startForce);
    }
}
