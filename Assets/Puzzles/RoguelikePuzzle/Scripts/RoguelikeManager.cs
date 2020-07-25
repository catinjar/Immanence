using UnityEngine;

public class RoguelikeManager : MonoBehaviour {
    public RoguelikePuzzle puzzle;

    public GameObject floor;
    public GameObject wall;
    public GameObject player;
    public GameObject enemy;
    public GameObject hp;
    public GameObject door;
    public GameObject key;
    public GameObject boss;
    public GameObject end;
    public GameObject shield;
    public GameObject sword;

    private void Awake() {
        puzzle.LoadMap();

        for (int i = 0; i < RoguelikePuzzle.height; ++i) {
            for (int j = 0; j < RoguelikePuzzle.width; ++j) {
                if (puzzle.tiles[i, j] != RoguelikePuzzle.Tile.Empty) {
                    InstantiateTile(floor, j, -i);
                }

                switch (puzzle.tiles[i, j]) {
                    case RoguelikePuzzle.Tile.Wall:
                        InstantiateTile(wall, j, -i);
                        break;
                    case RoguelikePuzzle.Tile.Player:
                        InstantiateTile(player, j, -i);
                        break;
                    case RoguelikePuzzle.Tile.Enemy:
                        InstantiateTile(enemy, j, -i);
                        break;
                    case RoguelikePuzzle.Tile.HP:
                        InstantiateTile(hp, j, -i);
                        break;
                    case RoguelikePuzzle.Tile.Door:
                        InstantiateTile(door, j, -i);
                        break;
                    case RoguelikePuzzle.Tile.Key:
                        InstantiateTile(key, j, -i);
                        break;
                    case RoguelikePuzzle.Tile.Boss:
                        InstantiateTile(boss, j, -i);
                        break;
                    case RoguelikePuzzle.Tile.End:
                        InstantiateTile(end, j, -i);
                        break;
                    case RoguelikePuzzle.Tile.Shield:
                        InstantiateTile(shield, j, -i);
                        break;
                    case RoguelikePuzzle.Tile.Sword:
                        InstantiateTile(sword, j, -i);
                        break;
                }
            }
        }
    }

    private void InstantiateTile(GameObject tile, int x, int y) {
        var tileInstance = Instantiate(tile);
        tileInstance.transform.position = new Vector3(
            x * tileInstance.transform.localScale.x,
            y * tileInstance.transform.localScale.y,
            tile.transform.position.z
        );
    }
}
