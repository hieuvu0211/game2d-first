using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [SerializeField] private GameObject swarmerPrefab; //khai báo đối tượng cần nhân bản trong unity
    [SerializeField] private float swarmerInterval = 0f; //chỉnh thời gian quái xuất hiện
    int enemyCount = 0; // đếm số kẻ địch nhân bản để giới hạn chúng
    int XpositionEnemyPrefab = 25; //tọa độ x của đối tượng nhân bản;
    private void Start()
    {
        StartCoroutine(spawnEnemy(swarmerInterval, swarmerPrefab));
        // StartCoroutine(spawnEnemy(bigSwarmerInterval, bigSwarmerPrefab));
        Invoke("DisableScript", 1f);
    }
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy
            = Instantiate(enemy, new Vector3(XpositionEnemyPrefab, 9, 0), Quaternion.identity);
        GameObject e1 = Instantiate(enemy, new Vector3(50, 12, 0), Quaternion.identity);
        GameObject e2 = Instantiate(enemy, new Vector3(58, 9, 0), Quaternion.identity);
        GameObject e3 = Instantiate(enemy, new Vector3(67, 9, 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy)); // nhân bản
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