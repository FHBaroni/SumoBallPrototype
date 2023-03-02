using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private bool homing;
    private float speed = 30.0f;
    private float rocketStrength = 30.0f;
    private float aliveTimer = 2.0f;
    private Transform target;

    /*
    Esse c�digo � uma fun��o em C# que � chamada a cada quadro (frame) e � usada para atualizar o comportamento de um objeto que est� seguindo um alvo
    definido pela vari�vel "target". Dentro da fun��o, h� uma condi��o que verifica se a vari�vel "homing" � verdadeira e se o valor de "target" n�o � nulo. 
    Se ambas as condi��es forem verdadeiras, a fun��o executa as a��es necess�rias para fazer com que o objeto siga o alvo.

    A primeira a��o que a fun��o executa � calcular a dire��o em que o objeto deve se mover para chegar ao alvo. Isso � feito subtraindo a posi��o do alvo 
    da posi��o atual do objeto e normalizando o resultado. Em seguida, a fun��o atualiza a posi��o do objeto adicionando a dire��o calculada multiplicada 
    pelo valor da vari�vel "speed" e pelo tempo que passou desde o �ltimo quadro, que � representado pela vari�vel "Time.deltaTime".

    Por fim, a fun��o utiliza a fun��o "LookAt" para fazer com que o objeto fique sempre virado para o alvo. Isso garante que o objeto sempre siga em dire��o
    ao alvo, mesmo que o alvo se mova. � importante notar que o objeto que possui esse script precisa ter um Collider para que possa detectar a colis�o com o alvo.
    */
    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        if (homing && target != null)
        {
            {
                Vector3 moveDirection = (target.transform.position - transform.position).normalized;
                transform.position += moveDirection * speed * Time.deltaTime;
                var rot = transform.rotation;
                rot.x += Time.deltaTime * 10;
               
                transform.LookAt(target);
            }
        }
    }

    /*
     Esse c�digo � uma fun��o p�blica em C# que se chama "Fire" e recebe como par�metro um Transform chamado "newTarget". 
    Dentro da fun��o, o valor de "target" � definido como o valor de "homingTarget". A vari�vel "homing" tamb�m � definida como verdadeira. 
    Por fim, a fun��o utiliza a fun��o "Destroy" para destruir o objeto que possui esse script ap�s um determinado per�odo de tempo,
    que � definido pela vari�vel "aliveTimer".

    Essa fun��o � provavelmente usada para criar um efeito de tiro em algum objeto no jogo, em que o objeto � direcionado a um alvo
    definido pela vari�vel "newTarget" e come�a a seguir esse alvo com a ajuda das vari�veis "target" e "homing". 

    A destrui��o do objeto ap�s um tempo definido pela vari�vel "aliveTimer" pode ser usada para limitar a quantidade de objetos no jogo
    e evitar que muitos objetos se acumulem na cena, o que pode impactar negativamente o desempenho do jogo.
     */
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    /*
     Esse c�digo � uma fun��o em C# que � chamada quando um objeto colide com outro objeto, e recebe como par�metro um objeto do tipo "Collision" chamado
    "collision". Dentro da fun��o, h� uma condi��o que verifica se o valor de "target" n�o � nulo. Se n�o for, a fun��o verifica se o objeto que colidiu
    possui uma tag igual � tag do objeto "target".

    Caso a tag seja igual, a fun��o acessa o Rigidbody do objeto que colidiu e utiliza a normal da colis�o para calcular a dire��o oposta ao ponto de contato.
    Em seguida, a fun��o adiciona uma for�a ao objeto "target" na dire��o oposta, multiplicada pelo valor da vari�vel "rocketStrength" 
    e com o modo de for�a definido como "Impulse". Por fim, a fun��o utiliza a fun��o "Destroy" para destruir o objeto que possui esse script.

    Essa fun��o pode ser usada para criar uma mec�nica em que um objeto � lan�ado em dire��o a um alvo espec�fico quando colide com outro objeto
    que possui uma tag correspondente. O valor da vari�vel "rocketStrength" pode ser usado para ajustar a intensidade do lan�amento. 
    � importante notar que o objeto que possui esse script precisa ter um Collider para que a colis�o possa ser detectada.
    */
    void OnCollisionEnter(Collision col)
    {
        if (target != null)
        {
            if (col.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -col.contacts[0].normal;
                targetRigidbody.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
