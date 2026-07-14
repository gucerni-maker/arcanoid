using UnityEngine;

public class ladrilloVerde : MonoBehaviour
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
        if (collision.gameObject.CompareTag("pelota") && golpe == 2){//Se destruye a los 2 golpes
            Destroy(gameObject);
            golpe = 0;
        } 
    }
}
