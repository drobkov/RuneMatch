using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
      public GameObject[] ItemPrefabs; // Массив префабов элементов
    public int width = 8;
    public int height = 8;

    private GameObject[,] grid;

    void Start()
    {
        grid = new GameObject[width, height];
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int randomItemIndex = Random.Range(0, ItemPrefabs.Length);
                Vector3 position = new Vector3(x, y, 0);
                GameObject newItem = Instantiate(ItemPrefabs[randomItemIndex], position, Quaternion.identity);
                newItem.transform.parent = this.transform; // Опционально, чтобы упорядочить иерархию
                grid[x, y] = newItem;
            }
        }
    }

}
