using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject pelotaPrefab;
    public GameObject ladrilloAzul;

    private float[] bloqueAzul = {-5f, -3f, -1f, 1f, 3f, 5f}; 

    void Start()
    {
        SpawnPelota();
        CreaLadrilloAzul();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //crea una pelota al inicio y luego de cada gol
    public void SpawnPelota(){
        Instantiate(pelotaPrefab, new Vector2(0,-3.65f), Quaternion.identity);
    }

    public void CreaLadrilloAzul(){
        for(int i = 0; i < bloqueAzul.Length; i++){
            Instantiate(ladrilloAzul, new Vector2(bloqueAzul[i], 2.14f), Quaternion.identity);
        }
    }
}
