using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject pelotaPrefab;
    void Start()
    {
        SpawnPelota();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //crea una pelota al inicio y luego de cada gol
    public void SpawnPelota(){
        Instantiate(pelotaPrefab, new Vector2(0,-3.65f), Quaternion.identity);
    }
}
