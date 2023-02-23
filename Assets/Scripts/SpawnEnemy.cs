using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    public int bossRound;
    public GameObject[] powerupPrefabs;
    public GameObject[] enemyPrefabs;
    public GameObject[] miniEnemyPrefabs;
    public GameObject bossPrefab;


    /*
    Esse código em C# é uma função que é executada quando o objeto associado é criado. Ele é usado em jogos para definir o estado inicial do jogo. 
    Nesse caso, a função é responsável por criar um power-up aleatório e uma onda de inimigos quando o jogo começa.

    Dentro da função, há três etapas principais. A primeira etapa usa a função "Random.Range" para gerar um número aleatório entre 0 e o comprimento do array 
    "powerupPrefabs" (número de power-ups disponíveis). O número gerado é armazenado na variável "randomPowerup".

    A segunda etapa usa a função "Instantiate" para criar uma nova instância do power-up aleatório escolhido. A instância é criada usando a posição gerada pela 
    função "GenerateSpawnPosition" e a rotação do power-up escolhido.

    A terceira etapa usa a função "SpawnEnemyWave" para criar a primeira onda de inimigos. A função "SpawnEnemyWave" cria uma série de inimigos aleatórios em 
    posições aleatórias usando a função "Instantiate".

    Essa função pode ser usada em jogos de ação e aventura para criar um ambiente dinâmico e desafiador para o jogador. A adição de power-ups aleatórios pode 
    fornecer ao jogador vantagens adicionais para ajudá-lo a derrotar os inimigos. Ao criar a primeira onda de inimigos no início do jogo, o jogador é 
    imediatamente desafiado e incentivado a jogar mais.
    */
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            if(waveNumber % bossRound == 0)
            {
                SpawnBosssWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        }
    }

    /*
    Esse código é uma função em C# que é usada para criar uma onda (wave) de inimigos em um jogo. A função recebe um parâmetro "enemiesToSpawn", que é um
    número inteiro que indica quantos inimigos devem ser criados. Dentro da função, há um loop for que itera até o valor do parâmetro "enemiesToSpawn".
    
    Dentro do loop, a função usa a função "Random.Range" para gerar um número aleatório entre 0 e o comprimento do array "enemyPrefabs" (número de inimigos
    disponíveis). Em seguida, a função usa a função "Instantiate" para criar uma nova instância de um dos inimigos do array "enemyPrefabs". A instância é criada
    usando a posição gerada pela função "GenerateSpawnPosition" (que provavelmente é uma função que retorna uma posição aleatória dentro de uma área definida)
    e a rotação do prefab de inimigo escolhido.

    Essa função pode ser usada para criar ondas de inimigos em jogos de ação e aventura, onde o jogador precisa enfrentar uma série de inimigos para avançar para
    o próximo nível ou área. O número de inimigos gerados pode ser ajustado para tornar o jogo mais ou menos desafiador, e diferentes tipos de inimigos podem ser
    adicionados ao array "enemyPrefabs" para aumentar a variedade de inimigos que aparecem.
    */
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
        }
    }

    /*
    Esse código é uma função privada em C# que é usada para gerar uma posição aleatória de spawn (geração de objetos) em um plano 3D. A função retorna um objeto
    do tipo Vector3 que representa a posição gerada.

    Dentro da função, há duas variáveis float que representam as coordenadas X e Z da posição de spawn. Essas variáveis são definidas usando a função
    "Random.Range", que gera um valor aleatório dentro do intervalo especificado pela variável "spawnRange". A variável "spawnRange" provavelmente é uma
    variável que é definida em algum lugar do código e representa a distância máxima que o objeto gerado pode estar da posição atual.

    Em seguida, a função cria um novo objeto do tipo Vector3 usando as coordenadas geradas e define a coordenada Y como zero (0), já que a posição de spawn
    deve estar no plano horizontal. Por fim, a função retorna o objeto Vector3 contendo a posição gerada.

    Essa função pode ser usada em conjunto com outras funções para gerar objetos em posições aleatórias dentro de uma área definida pelo valor de "spawnRange". 
    Isso pode ser útil para criar elementos do jogo, como inimigos que aparecem em locais aleatórios, objetos que são gerados de forma dinâmica ou coletáveis 
    que são espalhados pelo ambiente do jogo.
    */
    private Vector3 GenerateSpawnPosition()
    {
        float spawnXPosition = Random.Range(-spawnRange, spawnRange);
        float spawnZPosition = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnXPosition, 0, spawnZPosition);
        return randomPos;
    }
    void SpawnBosssWave(int currentRound)
    {
        int miniEnemyToSpawn;
        if (bossRound != 0)
        {
            miniEnemyToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemyToSpawn = 1;
        }

        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemyToSpawn;
    }
    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
}
