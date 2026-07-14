using UnityEngine;

public class ladrilloRojo : MonoBehaviour
{
    private int golpe = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        golpe++;
        if (collision.gameObject.CompareTag("pelota") && golpe == 3){//Se destruye a los 3 golpes
            Destroy(gameObject);
            golpe = 0;
        } 
    }
}
