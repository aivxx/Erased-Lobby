using UnityEngine;

public class Consumer : MonoBehaviour
{

    Collider _collider;


    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        Consumable consumable = other.GetComponent<Consumable>();
        if (consumable != null && !consumable.IsFinished)
        {
            consumable.Consume();
        }
    }

   
}
