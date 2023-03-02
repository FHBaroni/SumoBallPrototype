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
    Esse código é uma função em C# que é chamada a cada quadro (frame) e é usada para atualizar o comportamento de um objeto que está seguindo um alvo
    definido pela variável "target". Dentro da função, há uma condição que verifica se a variável "homing" é verdadeira e se o valor de "target" não é nulo. 
    Se ambas as condições forem verdadeiras, a função executa as ações necessárias para fazer com que o objeto siga o alvo.

    A primeira ação que a função executa é calcular a direção em que o objeto deve se mover para chegar ao alvo. Isso é feito subtraindo a posição do alvo 
    da posição atual do objeto e normalizando o resultado. Em seguida, a função atualiza a posição do objeto adicionando a direção calculada multiplicada 
    pelo valor da variável "speed" e pelo tempo que passou desde o último quadro, que é representado pela variável "Time.deltaTime".

    Por fim, a função utiliza a função "LookAt" para fazer com que o objeto fique sempre virado para o alvo. Isso garante que o objeto sempre siga em direção
    ao alvo, mesmo que o alvo se mova. É importante notar que o objeto que possui esse script precisa ter um Collider para que possa detectar a colisão com o alvo.
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
     Esse código é uma função pública em C# que se chama "Fire" e recebe como parâmetro um Transform chamado "newTarget". 
    Dentro da função, o valor de "target" é definido como o valor de "homingTarget". A variável "homing" também é definida como verdadeira. 
    Por fim, a função utiliza a função "Destroy" para destruir o objeto que possui esse script após um determinado período de tempo,
    que é definido pela variável "aliveTimer".

    Essa função é provavelmente usada para criar um efeito de tiro em algum objeto no jogo, em que o objeto é direcionado a um alvo
    definido pela variável "newTarget" e começa a seguir esse alvo com a ajuda das variáveis "target" e "homing". 

    A destruição do objeto após um tempo definido pela variável "aliveTimer" pode ser usada para limitar a quantidade de objetos no jogo
    e evitar que muitos objetos se acumulem na cena, o que pode impactar negativamente o desempenho do jogo.
     */
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    /*
     Esse código é uma função em C# que é chamada quando um objeto colide com outro objeto, e recebe como parâmetro um objeto do tipo "Collision" chamado
    "collision". Dentro da função, há uma condição que verifica se o valor de "target" não é nulo. Se não for, a função verifica se o objeto que colidiu
    possui uma tag igual à tag do objeto "target".

    Caso a tag seja igual, a função acessa o Rigidbody do objeto que colidiu e utiliza a normal da colisão para calcular a direção oposta ao ponto de contato.
    Em seguida, a função adiciona uma força ao objeto "target" na direção oposta, multiplicada pelo valor da variável "rocketStrength" 
    e com o modo de força definido como "Impulse". Por fim, a função utiliza a função "Destroy" para destruir o objeto que possui esse script.

    Essa função pode ser usada para criar uma mecânica em que um objeto é lançado em direção a um alvo específico quando colide com outro objeto
    que possui uma tag correspondente. O valor da variável "rocketStrength" pode ser usado para ajustar a intensidade do lançamento. 
    É importante notar que o objeto que possui esse script precisa ter um Collider para que a colisão possa ser detectada.
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
