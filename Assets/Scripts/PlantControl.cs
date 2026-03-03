using UnityEngine;

public class PlantControl : MonoBehaviour
{
    public float growTime = 5f;
    public float growTimer = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        growTimer = Mathf.Min(growTimer + Time.deltaTime, growTime);

        transform.localScale = Vector3.Lerp(Vector3.one * 0.1f, Vector3.one, growTimer / growTime);
    }
}
