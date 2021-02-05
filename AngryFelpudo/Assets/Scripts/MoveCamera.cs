using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform projetil; //segue

    public Transform limiteEsquerdo;
    public Transform limiteDireito;

    Vector3 novaPosicao;

	void Update ()
    {
        novaPosicao = transform.position; // posiçao atual
        novaPosicao.x = projetil.position.x; // segue o projetil apenas no eixo X
        novaPosicao.x = Mathf.Clamp(novaPosicao.x, limiteEsquerdo.position.x, limiteDireito.position.x);//limita a posiçao para nao sair
        //funcao de classe matematica = (posiçao,min,max) 
        transform.position = novaPosicao;// atribui a nova posiçao a camera
	}
}
