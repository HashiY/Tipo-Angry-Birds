using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrastaProjetil : MonoBehaviour {

    bool clicou;

    public float esticadaMaxima = 3.0f; // do estilingue
    float esticadaMaximaQuadrada; //  quadrada

    SpringJoint2D mola; // pega
    Rigidbody2D meuRigidbody;

    Transform estilingue;
    Ray raioParaMouse; // raio do mauso
    Ray raioEstilingueFrente;

    CircleCollider2D colisor;
    float medidaCirculo; // pegar o raio do CircleCollider2D

    Vector2 velocidadeAnterior;

    public LineRenderer linhaFrente;
    public LineRenderer linhaTras;

    private void Awake() // antes do primeiro flaime
    {
        mola = GetComponent<SpringJoint2D>(); // pega 
        meuRigidbody = GetComponent<Rigidbody2D>(); // pega inicializa
        colisor = GetComponent<CircleCollider2D>(); // pega
    }

    void Start () {
        estilingue = mola.connectedBody.transform; // acessa a mola e procura o transforme
        esticadaMaximaQuadrada = esticadaMaxima * esticadaMaxima; // valor ao quadrado 
        raioParaMouse = new Ray(estilingue.position, Vector3.zero); // ponto de partida, zero
        raioEstilingueFrente = new Ray(linhaFrente.transform.position, Vector3.zero);//

        medidaCirculo = colisor.radius; // pega o raio do c

        ConfiguraLinha();
	}
	
    void ConfiguraLinha()
    {
        //pontu inicial, indrx 0 na sua propria posiçao + abreviaçao de um valor no eixo z = v(0,0,1) * ..
        linhaFrente.SetPosition(0, linhaFrente.transform.position + Vector3.forward * -0.03f);
        linhaTras.SetPosition(0, linhaTras.transform.position + Vector3.forward * -0.01f);

        linhaFrente.sortingLayerName = "Frente;"; // pega esse Layers e deixa com esses nomes nomes
        linhaTras.sortingLayerName = "Frente;";

        linhaFrente.sortingOrder = 3; // e essas ordens
        linhaTras.sortingOrder = 1;
    }

    void AtualizaLinha()
    {
        Vector2 estilingueParaProgetil = transform.position - linhaFrente.transform.position;//direçao do ponto
        raioEstilingueFrente.direction = estilingueParaProgetil; // direcao
        Vector3 pontoDeAmarra = raioEstilingueFrente.GetPoint(estilingueParaProgetil.magnitude + medidaCirculo);//onde e o ponto q vai ser desenhada a ultima parte da linha

        pontoDeAmarra.z = -0.03f;// usa isso pois os Layer do LineRenderer nao esta funcionando 
        linhaFrente.SetPosition(1, pontoDeAmarra); // esta setando as posiçoes no ponto final com os pontoDeAmarra
        pontoDeAmarra.z = -0.01f; // para a linha aparecer atras do personagem
        linhaTras.SetPosition(1, pontoDeAmarra);
    }

	void Update ()
    {
        if (clicou == true) // se 
            Arrastar();    // arrasta

        if(mola != null) // se nao for nula
        {        // se o rigibody nao for kine e a velociade > que o rigibody da velociadae atual
            if (!meuRigidbody.isKinematic && velocidadeAnterior.sqrMagnitude > meuRigidbody.velocity.sqrMagnitude)
            {
                Destroy(mola); // destria a mola
                meuRigidbody.velocity = velocidadeAnterior; //qunado chegar no meio do estilingue onde a velo cai por bater numa parede
            }                                              //entao para isso nao ocorrer pega a velo de um freime anterior

            if (!clicou) // se nao
                velocidadeAnterior = meuRigidbody.velocity; // quanda a velocidade anterior no Rigidbody para ganhar velocidade

            AtualizaLinha();
        }
        else
        {

        }
	}

    private void OnMouseDown() // detecta o mause
    {
        clicou = true;
        mola.enabled = false; // desliga o componenete para nao lutar contara a força da mola
    }

    private void OnMouseUp() // detecta o mause
    {
        clicou = false;
        mola.enabled = true; // reabilita a firça da mola
        meuRigidbody.bodyType = RigidbodyType2D.Dynamic; // a funçao fisica e ativada
    }

    void Arrastar() 
    {   //cordenada de tela canto da tela, cordenada do mundo = (0,0,0)   , na posicao do mause
        Vector3 posicaoMouseMundo = Camera.main.ScreenToWorldPoint(Input.mousePosition); // funçao .ScreenToWorldPoint da camera main ,cordenada de tela para o mundo
        Vector2 estilingueParaMouse = posicaoMouseMundo - estilingue.position; // -a posiçao da mola
        

        if(estilingueParaMouse.sqrMagnitude > esticadaMaximaQuadrada) //magnetude ao quadrado for maior q ao quadrado
        {
            raioParaMouse.direction = estilingueParaMouse; // a direçao desse raio e o estilingueParaMouse, usa para mesmo que o mause va alem da distancia maxima o passaro continue puxado nesse lado
            posicaoMouseMundo = raioParaMouse.GetPoint(esticadaMaxima);//posiciona o projetil dentro desse raio sem sair da distancia maxima
        }

        posicaoMouseMundo.z = -0.02f; // para nao pegar o numero q esta na camera
        transform.position = posicaoMouseMundo; // de onde o script esta adicionado
    }
}
