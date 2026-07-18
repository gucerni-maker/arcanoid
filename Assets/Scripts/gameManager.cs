using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;//requerido para usar SceneManager (reiniciar el juego)

public class gameManager : MonoBehaviour
{

    public AudioClip sonidoLadrillo;//No olvidar agregar un componente Audio Source al prefab del ladrillo (sin ninguna modificacion, es requerido para dar sonido)
    public GameObject pelotaPrefab;
    public GameObject ladrilloAzul;
    public GameObject ladrilloVerde;
    public GameObject ladrilloRojo;
    public GameObject paletaPlayer;
    public paleta posicionPaleta; //debe tener el mismo nombre que el script para poder referenciar
    public UIDocument uiDocument;
    
    private Label scoreText1;
    private Label vidasRestantes;
    private Label finDelJuego;
    private Label continuarJuego;
    private Label puntaje;
    private Label cronometro;
    private Label sinTiempo;
    private Label victoria;
    private Button restartButton;
    private GameObject pelotaActual;
    private AudioSource playerAudio;

    private bool esperandoInput = false;
    private bool timerActivo = true;
    private float tiempoRestante = 120f; //tiempo del cronometro
    private int puntajeTotal = 0;
    private int cantidadVidas = 3;
    private int cuentaLadrillos = 0;

    
    private float[] bloquePosX = {-5f, -3f, -1f, 1f, 3f, 5f}; 

    void Start()
    {

        playerAudio = GetComponent<AudioSource>();

        SpawnPelota();
        CreaLadrillo();

        scoreText1 = uiDocument.rootVisualElement.Q<Label>("puntaje");
        vidasRestantes = uiDocument.rootVisualElement.Q<Label>("vidas");
        finDelJuego = uiDocument.rootVisualElement.Q<Label>("gameOver");
        continuarJuego = uiDocument.rootVisualElement.Q<Label>("continuar");
        cronometro = uiDocument.rootVisualElement.Q<Label>("tiempo");
        sinTiempo = uiDocument.rootVisualElement.Q<Label>("sinTiempo");
        victoria = uiDocument.rootVisualElement.Q<Label>("victoria");
        restartButton = uiDocument.rootVisualElement.Q<Button>("reiniciar");
        restartButton.clicked += ReloadScene;
        
        //Oculta elementos de la interfaz, como pantalla de gameover y boton restart
        finDelJuego.style.display = DisplayStyle.None;
        sinTiempo.style.display = DisplayStyle.None;
        continuarJuego.style.display = DisplayStyle.None;
        victoria.style.display = DisplayStyle.None;
        restartButton.style.display = DisplayStyle.None;
        
    }

    void Update()
    {
         if (esperandoInput && Input.GetKeyDown(KeyCode.Space) && cantidadVidas > 0){
            SpawnPelota();
            esperandoInput = false;
            continuarJuego.style.display = DisplayStyle.None;
        }

        //############## PARA EL CRONOMETRO ################
        if (timerActivo && cantidadVidas > 0)
        {
            tiempoRestante -= Time.deltaTime;
            
            // Actualizar el texto en pantalla
            int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
            cronometro.text = string.Format("{0:00}:{1:00}", minutos, segundos);
            
            // Verificar si el tiempo se acabó
            if (tiempoRestante <= 0f)
            {
                tiempoRestante = 0f;
                timerActivo = false;
                cantidadVidas = 0;
                GameOver();
            }
        }
        //##################################################

    }

    //########  PARA MANEJAR LA CANTIDAD DE VIDAS ##########
    public void PelotaPerdida(){
        esperandoInput = true;
        cantidadVidas--;
        vidasRestantes.text = cantidadVidas.ToString();
        
        if(cantidadVidas < 1){
            GameOver();
        }
        if(cantidadVidas > 0){
            continuarJuego.style.display = DisplayStyle.Flex;
        }
    }
    //######################################################


    //##########  PARA LA PANTALLA GAME OVER #############
    public void GameOver(){
        finDelJuego.style.display = DisplayStyle.Flex;
        restartButton.style.display = DisplayStyle.Flex;

        if(tiempoRestante <= 0){
            sinTiempo.style.display = DisplayStyle.Flex;
        }
        
        timerActivo = false;
        cronometro.text = "00:00";
        vidasRestantes.text = "0";
        
        if (pelotaActual != null)
        {
            Destroy(pelotaActual);
            pelotaActual = null;
        }
    }
    //######################################################


    //###########  PARA LA CREACION DE LA PELOTA ###########
    public void SpawnPelota(){
        //obtenemos la posicion x de la paleta
        float posicionX = posicionPaleta.ObtenerPosicionX(); 

        //crea una pelota y entrega una referencia para destruirla al acabar el tiempo
        pelotaActual = Instantiate(pelotaPrefab, new Vector2(posicionX,-3.65f), Quaternion.identity);
    }
    //######################################################


    //###########  PARA LA CREACION DE LADRILLOS ###########
    public void CreaLadrillo(){
        
        for(int i = 0; i < bloquePosX.Length; i++){
            Instantiate(ladrilloAzul, new Vector2(bloquePosX[i], 2.14f), Quaternion.identity);
            Instantiate(ladrilloVerde, new Vector2(bloquePosX[i], 2.88f), Quaternion.identity);
            Instantiate(ladrilloRojo, new Vector2(bloquePosX[i], 3.62f), Quaternion.identity);
        }
        //multiplicamos el largo del arreglo por la cantidad de instancias
        cuentaLadrillos = bloquePosX.Length * 3;
    }
    //######################################################


    //#### LO QUE SUCEDE LUEGO DE DESTRUIR UN LADRILLO #####

    //controla el puntaje de cada ladrillo
    public void PuntoLadrilloAzul(){
        //Descuenta 1 ladrillo cada vez que se destruye
        cuentaLadrillos--;

        //Reproduce un sonido
        playerAudio.PlayOneShot(sonidoLadrillo, 1.0f);
        
        //Agregar un puntaje a la variable  
        puntajeTotal += 10;

        //Actualiza el puntaje en la UI
        scoreText1.text = puntajeTotal.ToString();
        
        //Si la cantidad de ladrillos es cero, se gana el juego
        if(cuentaLadrillos == 0){
            Victoria();
        }
    }
    public void PuntoLadrilloVerde(){
        cuentaLadrillos--;
        playerAudio.PlayOneShot(sonidoLadrillo, 1.0f);
        puntajeTotal +=  20;
        scoreText1.text = puntajeTotal.ToString();

        if(cuentaLadrillos == 0){
            Victoria();
        }

    }
    public void PuntoLadrilloRojo(){
        cuentaLadrillos--;
        playerAudio.PlayOneShot(sonidoLadrillo, 1.0f);
        puntajeTotal +=  30;
        scoreText1.text = puntajeTotal.ToString();

        if(cuentaLadrillos == 0){
            Victoria();
        }
    }
    //######################################################
    
    //reinicia el juego luego de presionar el boton de reiniciar
    void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //########  CONTROLA LA VENTANA DE VICTORIA ############
    public void Victoria(){
        //Destruimos la pelota y la paleta
        if (pelotaActual != null){
            Destroy(pelotaActual);
            Destroy(paletaPlayer);
            pelotaActual = null;
        }

        //Mostramos el mensaje de victoria
        victoria.style.display = DisplayStyle.Flex;

        //Detenemos el tiempo
        timerActivo = false;
        cronometro.text = "00:00";

        //Mostramos el boton para reiniciar la partida
        restartButton.style.display = DisplayStyle.Flex;
    }
    //######################################################
}
