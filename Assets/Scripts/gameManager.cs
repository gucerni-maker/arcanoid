using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject pelotaPrefab;
    public GameObject ladrilloAzul;
    public GameObject ladrilloVerde;
    public GameObject ladrilloRojo;

    private float[] bloquePosX = {-5f, -3f, -1f, 1f, 3f, 5f}; 

    void Start()
    {
        SpawnPelota();
        CreaLadrillo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //crea una pelota al inicio y luego de cada gol
    public void SpawnPelota(){
        Instantiate(pelotaPrefab, new Vector2(0,-3.65f), Quaternion.identity);
    }

    public void CreaLadrillo(){
        for(int i = 0; i < bloquePosX.Length; i++){
            Instantiate(ladrilloAzul, new Vector2(bloquePosX[i], 2.14f), Quaternion.identity);
            Instantiate(ladrilloVerde, new Vector2(bloquePosX[i], 2.88f), Quaternion.identity);
            Instantiate(ladrilloRojo, new Vector2(bloquePosX[i], 3.62f), Quaternion.identity);
        }
    }
}
