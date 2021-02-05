using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetador : MonoBehaviour {

    public Rigidbody2D alvo;
    SpringJoint2D mola;

    public float velocidadeParada = 0.025f; // parar qunado estiver rolando no chao
    float velocidadeParadaQuadrada; // para calcular na magnetude

    private void Awake()
    {
        mola = alvo.GetComponent<SpringJoint2D>(); // para nao ocorrer ate ser lançado
    }

    // Use this for initialization
    void Start ()
    {
        velocidadeParadaQuadrada = velocidadeParada * velocidadeParada;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R)) // se aperta R
            Resetar(); // receta
        //se a velociade do alvo < velocidade ao quadrado e se a mola = nula
        if (alvo.velocity.sqrMagnitude < velocidadeParadaQuadrada && mola == null)
            Resetar();

	}

    void Resetar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // cena que esta agora
    }

    private void OnTriggerExit2D(Collider2D colisao)//se sai
    {
        if (colisao.GetComponent<Rigidbody2D>() == alvo) // se colidir 
            Resetar();
    }
}
