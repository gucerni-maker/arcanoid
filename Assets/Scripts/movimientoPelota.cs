using UnityEngine;

public class movimientoPelota : MonoBehaviour
{
    
    public float velocidad = 10f;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LanzarPelota();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LanzarPelota(){
        float[] angulos ={30f,45f,60f,120f,135f,150f};
        float angulo = angulos[Random.Range(0, angulos.Length)];
        float rad = angulo * Mathf.Deg2Rad;
        Vector2 direccion = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        rb.linearVelocity = direccion * velocidad;
    }





}
