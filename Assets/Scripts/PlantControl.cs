using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class PlantControl : MonoBehaviour
{
    public float growTime = 5f;
    public float growTimer = 0f;

    private Inventory inventory;
    private ItemPickup pickup;
    private PlantControl plantScript;

    void Start()
    {
        pickup = GetComponent<ItemPickup>();
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        plantScript = GetComponent<PlantControl>();
    }

    void Update()
    {
        growTimer = Mathf.Min(growTimer + Time.deltaTime, growTime);

        transform.localScale = Vector3.Lerp(Vector3.one * 0.1f, Vector3.one, growTimer / growTime);
    }

    public void PickupPlant()
    {
        if (plantScript.growTimer == plantScript.growTime)
        {
            pickup.PickUp(inventory);

            Debug.Log("Plant harvested!");
        }
    }
}
