using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyMap2 : MonoBehaviour
{

    [SerializeField] private GameObject[] swarmerPrefab; //khai báo đối tượng cần nhân bản trong unity
    [SerializeField] private float swarmerInterval = 0f; //chỉnh thời gian quái xuất hiện
    int enemyCount = 0; // đếm số kẻ địch nhân bản để giới hạn chúng
    int XpositionEnemyPrefab = 25; //tọa độ x của đối tượng nhân bản;
    private void Start()
    {
        StartCoroutine(spawnEnemy(swarmerInterval, swarmerPrefab));
        // StartCoroutine(spawnEnemy(bigSwarmerInterval, bigSwarmerPrefab));
        Invoke("DisableScript", 1f);
    }
    private IEnumerator spawnEnemy(float interval, GameObject[] enemy)
    {
        yield return new WaitForSeconds(interval);

        foreach (GameObject item in enemy)
        {
            if (item.name == "knight")
            {
                float y1 = -2.17f;
                GameObject e1 = Instantiate(item, new Vector3(56, y1, 0), Quaternion.identity);
                // GameObject e2 = Instantiate(item, new Vector3(36, y1, 0), Quaternion.identity);
                StartCoroutine(spawnEnemy(interval, enemy));
            }
            if (item.name == "Skeleton")
            {
                GameObject e2 = Instantiate(item, new Vector3(45, -5, 0), Quaternion.identity);
                // GameObject e3 = Instantiate(item, new Vector3(67, 9, 0), Quaternion.identity);
                StartCoroutine(spawnEnemy(interval, enemy));
            }
            if (item.name == "Goblin")
            {
                GameObject e3 = Instantiate(item, new Vector3(84, -1, 0), Quaternion.identity);
                // GameObject e3 = Instantiate(item, new Vector3(67, 9, 0), Quaternion.identity);
                StartCoroutine(spawnEnemy(interval, enemy));
            }
            if (item.name == "Mushroom")
            {
                float y2 = -11.29f;
                float y3 = -16.25f;
                GameObject e4 = Instantiate(item, new Vector3(25, y2, 0), Quaternion.identity);
                GameObject e5 = Instantiate(item, new Vector3(36, y3, 0), Quaternion.identity);
                StartCoroutine(spawnEnemy(interval, enemy));
            }
            if (item.name == "candy")
            {
                GameObject e6 = Instantiate(item, new Vector3(52, -10, 0), Quaternion.identity);
                GameObject e7 = Instantiate(item, new Vector3(60, 0, 0), Quaternion.identity);
                GameObject e8 = Instantiate(item, new Vector3(81, 4, 0), Quaternion.identity);
                StartCoroutine(spawnEnemy(interval, enemy));
            }
        }




        // nhân bản
        // newEnemy.transform.position = new Vector3(XpositionEnemyPrefab, 9, 0); //vị trí nhân bản xuất hiện
        XpositionEnemyPrefab += 2;
        enemyCount += 1;
    }
    private void FixedUpdate()
    {
        DisableScript(); // set up giới hạn nhân bản kẻ địch
    }
    void DisableScript()
    {
        if (enemyCount >= 1) // chỉ nhân bản 2 kẻ địch
            gameObject.SetActive(false);
    }
}