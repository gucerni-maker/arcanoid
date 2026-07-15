using UnityEngine;

public class movimientoPelota : MonoBehaviour
{
    public float velocidad = 10f;
    private Rigidbody2D rb;
    private gameManager gm;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Nos comunicamos con el gameManager
        gm = FindFirstObjectByType<gameManager>();

        LanzarPelota();
    }

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

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("bordeInferior")){
            if (gm != null){
                gm.PelotaPerdida(); 
            }

           Destroy(gameObject);
        }
    } 

    void OnCollisionEnter2D(Collision2D collision){
        CorregirTrayectoriaHorizontal();
    }    

    void CorregirTrayectoriaHorizontal()
    {
        // 1. Definimos la velocidad vertical MÍNIMA que permitimos
        float minVelocidadY = 3.5f; 

        // 2. Obtenemos la velocidad actual de la pelota
        Vector2 velocidadActual = rb.linearVelocity;

        // 3. Verificamos si el movimiento vertical es demasiado pequeño (casi horizontal)
        if (Mathf.Abs(velocidadActual.y) < minVelocidadY)
        {
            // Determinamos si la pelota iba hacia arriba (1) o hacia abajo (-1)
            int direccionY = velocidadActual.y >= 0 ? 1 : -1;
            
            // Determinamos si la pelota iba hacia la derecha (1) o izquierda (-1)
            int direccionX = velocidadActual.x >= 0 ? 1 : -1;

            // 4. Forzamos la velocidad Y al mínimo permitido
            float nuevaVelocidadY = minVelocidadY * direccionY;

            // 5. Recalculamos la velocidad X para que la velocidad TOTAL de la pelota no cambie.
            // Usamos Pitágoras: (VelocidadTotal)^2 = (VelX)^2 + (VelY)^2
            // Despejando VelX: VelX = RaizCuadrada( (VelTotal)^2 - (VelY)^2 )
            float nuevaVelocidadX = direccionX * Mathf.Sqrt(Mathf.Pow(velocidad, 2) - Mathf.Pow(minVelocidadY, 2));

            // 6. Aplicamos la nueva velocidad corregida
            rb.linearVelocity = new Vector2(nuevaVelocidadX, nuevaVelocidadY);
            
            // Opcional: Un log para que veas cuándo ocurre la corrección
            Debug.Log("Trayectoria horizontal corregida");
        }
    }   
}
 