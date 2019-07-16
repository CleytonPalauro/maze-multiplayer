using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* "using System"
 * O namespace System contém as classes fundamentais e
 * as classes base que definem tipos de dados de referência e valor,
 * eventos e manipuladores de eventos, interfaces,
 * atributos e exceções de processamento comumente usados.
 * https://docs.microsoft.com/pt-br/dotnet/api/system
 */

public class MapGenerator : MonoBehaviour
{
    [Header("Configurations")]
    public string mapHolderName = "Generated Map";

    [TextArea(4, 20)]
    public string stringMap = "";

    public Vector3 mapSize;

    public Transform[] tilePrefabs;

    [Header("Modifyers")]
    public bool centralized = true;

    [Range(-90, 90)]
    public int rotateTo = 0;

    [Range(1, 10)]
    public int scaleTo = 1;

    private int[] arrayList;

    private int[,] matrizMap;

	void Start ()
    {
        FillMap();

        GenerateMap();
    }
	
	void Update () { }

    /// <summary>
    /// Método para preencher a matriz virtual do mapa.
    /// </summary>

    public void FillMap()
    {
        /* "Array.ConvertAll"
         * Converte uma matriz de um tipo para uma matriz de outro tipo.
         * https://docs.microsoft.com/pt-br/dotnet/api/system.array.convertall
         */

        /* "int.Parse"
         * Converte a representação de cadeia de caracteres de um número em
         * um estilo e formato específicos da cultura especificados em
         * seu equivalente de inteiro.
         * https://docs.unity3d.com/ScriptReference/String.html
         */

        arrayList = Array.ConvertAll(stringMap.Split(','), int.Parse);

        int width = (int)mapSize.x;

        int height = (int)mapSize.z;

        matrizMap = new int[width, height];

        int count = 0;

        for(int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if(count >= arrayList.Length)
                {
                    matrizMap[x, z] = 0;
                }
                else
                {
                    matrizMap[x, z] = arrayList[count];
                }

                count++;
            }
        }
    }

    /// <summary>
    /// Método para construir o mapa na cena do jogo.
    /// </summary>
     
    public void GenerateMap()
    {
        mapSize = new Vector3(mapSize.x, 0f, mapSize.z);

        /* "transform.Find(name)"
         * Encontra um filho pelo nome e retorna o "transform" do
         * filho encontrado ou "null" se nenhum filho for encontrado.
         * https://docs.unity3d.com/ScriptReference/Transform.Find.html
         */

        if (transform.Find(mapHolderName))
        {
            Destroy(transform.Find(mapHolderName).gameObject);
            Debug.LogWarning("GameObject foi encontrado e destruido.");
        }

        /* "new GameObject(name)"
         * Cria um novo GameObject com o nome com o qual o GameObject é criado.
         * https://docs.unity3d.com/ScriptReference/GameObject-ctor.html
         */

        Transform mapHolder = new GameObject(mapHolderName).transform;

        mapHolder.parent = transform;

        // NORMALIZERS!
        int myCenter = 0;
        float myOffset = 0;

        if (centralized)
        {
            myCenter = 2;
            myOffset = 0.5f;
        }
        else
        {
            myCenter = 1;
            myOffset = 1f;
        }

        for(int x = 0; x < mapSize.x; x++)
        {
            for(int z = 0; z < mapSize.z; z++)
            {
                Vector3 tilePosition =
                    new Vector3(
                        -mapSize.x / myCenter + myOffset + x,
                        0f,
                        -mapSize.z / myCenter + myOffset + z);

                int localIndex = 0;

                if (matrizMap[x,z] < tilePrefabs.Length)
                {
                    localIndex = matrizMap[x, z];
                }
                else
                {
                    localIndex = 0;
                }

                Transform newTile =
                    Instantiate(
                        tilePrefabs[localIndex],
                        tilePosition,
                        Quaternion.Euler(Vector3.up * rotateTo)
                    );

                newTile.localScale = Vector3.one;

                newTile.parent = mapHolder;
            }
        }

        mapHolder.localScale = Vector3.one * scaleTo;
    }
}
