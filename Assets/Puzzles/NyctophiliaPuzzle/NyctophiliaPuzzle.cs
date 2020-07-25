using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NyctophiliaPuzzle", menuName = "Puzzles/NyctophiliaPuzzle", order = 4)]
public class NyctophiliaPuzzle : ScriptableObject {
    [System.Serializable]
    public class Enemy {
        public SceneField scene;
        public float position;
        public List<int> path = new List<int>();
        public float timeToMove = 0.0f;
        public bool haunting = false;
        public bool alive = true;
    }

    [System.Serializable]
    public class Node {
        [System.Serializable]
        public class Edge {
            public int node;
            public SceneField scene;
        }

        public SceneField scene;
        public Edge[] edges;
    }

    public NyctophiliaPuzzle storedState;

    public Portal currentPortal = null;
    public bool hasKey = false;

    public GameObject enemyPrefab;

    public Enemy[] enemies;
    public Node[] nodes;

    public void Update() {
        foreach (var enemy in enemies) {
            if (!enemy.alive) {
                continue;
            }

            if (!enemy.haunting || enemy.scene == SceneManager.GetActiveScene().name) {
                continue;
            }

            enemy.timeToMove -= Time.deltaTime;

            if (enemy.path.Count == 0) {
                continue;
            }

            if (nodes[enemy.path[0]].scene == SceneManager.GetActiveScene().name) {
                enemy.path.RemoveAt(0);
            }

            if (enemy.timeToMove < 0.0f) {
                enemy.timeToMove = Random.Range(4.0f, 6.0f);
                enemy.path.RemoveAt(0);
                
                SpawnEnemy(enemy);

                if (enemy.path.Count > 0) {
                    enemy.scene = nodes[enemy.path[0]].scene;
                }
            }
        }
    }

    public void SceneStart() {
        foreach (var enemy in enemies) {
            if (!enemy.alive) {
                continue;
            }

            if (enemy.scene == SceneManager.GetActiveScene().name) {
                SpawnEnemy(enemy);
            }
            else {
                FindPaths();
            }
        }
    }

    public void FindPaths() {
        foreach (var enemy in enemies) {
            enemy.path.Clear();

            int nodeIndex = 0;
            for (int i = 0; i < nodes.Length; ++i) {
                if (enemy.scene.SceneName == nodes[i].scene.SceneName) {
                    nodeIndex = i;
                }
            }

            FindPath(enemy, nodeIndex, -1);

            enemy.timeToMove = Random.Range(4.0f, 6.0f);
        }
    }

    private bool FindPath(Enemy enemy, int nodeIndex, int previousIndex) {
        var node = nodes[nodeIndex];

        if (node.scene.SceneName == SceneManager.GetActiveScene().name) {
            enemy.path.Insert(0, nodeIndex);
            return true;
        }

        foreach (var edge in node.edges) {
            if (edge.node != previousIndex) {
                bool foundPath = FindPath(enemy, edge.node, nodeIndex);

                if (foundPath) {
                    enemy.path.Insert(0, nodeIndex);
                    return true;
                }
            }
        }

        return false;
    }

    public void RestoreState() {
        currentPortal = null;
        hasKey = false;

        enemies = new Enemy[storedState.enemies.Length]; // Needs deep clone
        for (int i = 0; i < enemies.Length; ++i) {
            enemies[i] = new Enemy();
            enemies[i].scene = storedState.enemies[i].scene;
            enemies[i].position = storedState.enemies[i].position;
        }

        nodes = (Node[])storedState.nodes.Clone();
    }

    private void SpawnEnemy(Enemy enemy) {
        var enemyInstance = Instantiate(enemyPrefab);

        var spawnPoints = FindObjectsOfType<SpawnPoint>();
        foreach (var spawnPoint in spawnPoints) {
            if (spawnPoint.Contains(enemy.scene)) {
                enemy.position = spawnPoint.transform.position.x;
            }
        }

        var position = enemyInstance.transform.position;
        position.x = enemy.position;
        enemyInstance.transform.position = position;

        enemy.haunting = true;
        enemyInstance.GetComponent<global::Enemy>().parent = enemy;
    }

    public void GoToPortal() {
        if (currentPortal == null) {
            return;
        }

        Initiate.Fade(currentPortal.scene, Color.black, 0.75f);
        currentPortal = null;

        PlayerState.Instance.Free();
        PhoneState.Instance.opened = false;

        foreach(var enemy in enemies) {
            enemy.haunting = false;
        }
    }

    public void GetKey() {
        hasKey = true;
    }
}