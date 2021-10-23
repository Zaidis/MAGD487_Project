using UnityEngine;

public class DetachFromParent : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(null);
    }
}
