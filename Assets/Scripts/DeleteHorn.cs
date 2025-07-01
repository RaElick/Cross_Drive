using UnityEngine;

public class DeleteHorn : MonoBehaviour
{
    public float timeToDelete = 2f;

    void Start()
    {
        Destroy(gameObject, timeToDelete);
    }


}
