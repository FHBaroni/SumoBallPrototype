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
    Esse c�digo em C# � uma fun��o que � executada quando o objeto associado � criado. Ele � usado em jogos para definir o estado inicial do jogo. 
    Nesse caso, a fun��o � respons�vel por criar um power-up aleat�rio e uma onda de inimigos quando o jogo come�a.

    Dentro da fun��o, h� tr�s etapas principais. A primeira etapa usa a fun��o "Random.Range" para gerar um n�mero aleat�rio entre 0 e o comprimento do array 
    "powerupPrefabs" (n�mero de power-ups dispon�veis). O n�mero gerado � armazenado na vari�vel "randomPowerup".

    A segunda etapa usa a fun��o "Instantiate" para criar uma nova inst�ncia do power-up aleat�rio escolhido. A inst�ncia � criada usando a posi��o gerada pela 
    fun��o "GenerateSpawnPosition" e a rota��o do power-up escolhido.

    A terceira etapa usa a fun��o "SpawnEnemyWave" para criar a primeira onda de inimigos. A fun��o "SpawnEnemyWave" cria uma s�rie de inimigos aleat�rios em 
    posi��es aleat�rias usando a fun��o "Instantiate".

    Essa fun��o pode ser usada em jogos de a��o e aventura para criar um ambiente din�mico e desafiador para o jogador. A adi��o de power-ups aleat�rios pode 
    fornecer ao jogador vantagens adicionais para ajud�-lo a derrotar os inimigos. Ao criar a primeira onda de inimigos no in�cio do jogo, o jogador � 
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
    Esse c�digo � uma fun��o em C# que � usada para criar uma onda (wave) de inimigos em um jogo. A fun��o recebe um par�metro "enemiesToSpawn", que � um
    n�mero inteiro que indica quantos inimigos devem ser criados. Dentro da fun��o, h� um loop for que itera at� o valor do par�metro "enemiesToSpawn".
    
    Dentro do loop, a fun��o usa a fun��o "Random.Range" para gerar um n�mero aleat�rio entre 0 e o comprimento do array "enemyPrefabs" (n�mero de inimigos
    dispon�veis). Em seguida, a fun��o usa a fun��o "Instantiate" para criar uma nova inst�ncia de um dos inimigos do array "enemyPrefabs". A inst�ncia � criada
    usando a posi��o gerada pela fun��o "GenerateSpawnPosition" (que provavelmente � uma fun��o que retorna uma posi��o aleat�ria dentro de uma �rea definida)
    e a rota��o do prefab de inimigo escolhido.

    Essa fun��o pode ser usada para criar ondas de inimigos em jogos de a��o e aventura, onde o jogador precisa enfrentar uma s�rie de inimigos para avan�ar para
    o pr�ximo n�vel ou �rea. O n�mero de inimigos gerados pode ser ajustado para tornar o jogo mais ou menos desafiador, e diferentes tipos de inimigos podem ser
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
    Esse c�digo � uma fun��o privada em C# que � usada para gerar uma posi��o aleat�ria de spawn (gera��o de objetos) em um plano 3D. A fun��o retorna um objeto
    do tipo Vector3 que representa a posi��o gerada.

    Dentro da fun��o, h� duas vari�veis float que representam as coordenadas X e Z da posi��o de spawn. Essas vari�veis s�o definidas usando a fun��o
    "Random.Range", que gera um valor aleat�rio dentro do intervalo especificado pela vari�vel "spawnRange". A vari�vel "spawnRange" provavelmente � uma
    vari�vel que � definida em algum lugar do c�digo e representa a dist�ncia m�xima que o objeto gerado pode estar da posi��o atual.

    Em seguida, a fun��o cria um novo objeto do tipo Vector3 usando as coordenadas geradas e define a coordenada Y como zero (0), j� que a posi��o de spawn
    deve estar no plano horizontal. Por fim, a fun��o retorna o objeto Vector3 contendo a posi��o gerada.

    Essa fun��o pode ser usada em conjunto com outras fun��es para gerar objetos em posi��es aleat�rias dentro de uma �rea definida pelo valor de "spawnRange". 
    Isso pode ser �til para criar elementos do jogo, como inimigos que aparecem em locais aleat�rios, objetos que s�o gerados de forma din�mica ou colet�veis 
    que s�o espalhados pelo ambiente do jogo.
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
