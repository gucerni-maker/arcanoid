using UnityEngine;
using UnityEngine.UIElements;

public class gameManager : MonoBehaviour
{
    public GameObject pelotaPrefab;
    public GameObject ladrilloAzul;
    public GameObject ladrilloVerde;
    public GameObject ladrilloRojo;
    public paleta posicionPaleta; //debe tener el mismo nombre que el script para poder referenciar
    public UIDocument uiDocument;

    private Label scoreText1;
    private Label vidasRestantes;
    private Label finDelJuego;
    private Label continuarJuego;
    private bool esperandoInput = false;
    private int puntajeTotal = 0;
    private int cantidadVidas = 3;
    private Label puntaje;
    private float[] bloquePosX = {-5f, -3f, -1f, 1f, 3f, 5f}; 

    void Start()
    {
        SpawnPelota();
        CreaLadrillo();
        scoreText1 = uiDocument.rootVisualElement.Q<Label>("puntaje");
        vidasRestantes = uiDocument.rootVisualElement.Q<Label>("vidas");
        finDelJuego = uiDocument.rootVisualElement.Q<Label>("gameOver");
        continuarJuego = uiDocument.rootVisualElement.Q<Label>("continuar");
        finDelJuego.style.display = DisplayStyle.None;
        continuarJuego.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {
         if (esperandoInput && Input.GetKeyDown(KeyCode.Space) && cantidadVidas > 0){
            SpawnPelota();
            esperandoInput = false;
            continuarJuego.style.display = DisplayStyle.None;
        }
    }

    // Este método es llamado por la pelota cuando cae al vacío
    public void PelotaPerdida()
    {
        esperandoInput = true;
        cantidadVidas--;
        vidasRestantes.text = cantidadVidas.ToString();
        
        if(cantidadVidas < 1){
            finDelJuego.style.display = DisplayStyle.Flex;
        }
        if(cantidadVidas > 0){
            continuarJuego.style.display = DisplayStyle.Flex;
        }
    }

    //crea una pelota al inicio y luego de cada gol
    public void SpawnPelota(){
        float posicionX = posicionPaleta.ObtenerPosicionX(); //obtenemos la posicion x de la paleta
        Instantiate(pelotaPrefab, new Vector2(posicionX,-3.65f), Quaternion.identity);
    }

    public void CreaLadrillo(){
        for(int i = 0; i < bloquePosX.Length; i++){
            Instantiate(ladrilloAzul, new Vector2(bloquePosX[i], 2.14f), Quaternion.identity);
            Instantiate(ladrilloVerde, new Vector2(bloquePosX[i], 2.88f), Quaternion.identity);
            Instantiate(ladrilloRojo, new Vector2(bloquePosX[i], 3.62f), Quaternion.identity);
        }
    }

    //controla el puntaje de cada ladrillo
    public void PuntoLadrilloAzul(){
        puntajeTotal += 10;
        scoreText1.text = puntajeTotal.ToString();
    }
    public void PuntoLadrilloVerde(){
        puntajeTotal +=  20;
        scoreText1.text = puntajeTotal.ToString();
    }
    public void PuntoLadrilloRojo(){
        puntajeTotal +=  30;
        scoreText1.text = puntajeTotal.ToString();
    }
}
