using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecebeDano : MonoBehaviour {

    public float pontosDeVida = 2;
    public Sprite imagemMachucado;
    public float velocidadeParaDano = 5;

    private float pontosDeVidaAtuais;
    private float velocidadeParaDanoQuadrado;
    private SpriteRenderer sRenderer; // trocar o sprite

	void Start () {

        pontosDeVidaAtuais = pontosDeVida; // 
        velocidadeParaDanoQuadrado = velocidadeParaDano * velocidadeParaDano;
        sRenderer = GetComponent<SpriteRenderer>();//pega
		
	}

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (!colisao.gameObject.CompareTag("Arma")) // nao tiver a tag arma
            return;
        if (colisao.relativeVelocity.sqrMagnitude < velocidadeParaDanoQuadrado) //inserra essa execuçao
            return;

        sRenderer.sprite = imagemMachucado; // usa o sprite
        pontosDeVidaAtuais--;

        if(pontosDeVidaAtuais <= 0) // se zerar
        {
            Matar();
        }
    }

    void Matar()
    {
        sRenderer.enabled = false; // desliga 
        GetComponent<Collider2D>().enabled = false; // desliga
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; // fica kine para nao interagir
        GetComponent<ParticleSystem>().Play(); // usa o 
    }
}
