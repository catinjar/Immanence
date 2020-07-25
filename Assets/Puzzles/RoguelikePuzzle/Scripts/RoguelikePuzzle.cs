using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "RoguelikePuzzle", menuName = "Puzzles/RoguelikePuzzle", order = 3)]
public class RoguelikePuzzle : ScriptableObject {
    public enum Tile {
        Empty,
        Floor,
        Wall,
        Player,
        Enemy,
        HP,
        Door,
        Key,
        Boss,
        End,
        Sword,
        Shield
    }

    public Tile[,] tiles;

    public const int width = 107;
    public const int height = 98;

    public Vector2Int position;
    public Vector2Int previousPosition;

    public Vector2Int Move(int x, int y) {
        var newPosition = position + new Vector2Int(x, y);

        previousPosition = position;
        position = newPosition;

        return position;
    }

    public void LoadMap() {
        var text = Resources.Load<TextAsset>($"Map").text;

        tiles = new Tile[height, width];

        int i = 0, j = 0, k = 0;
        while (i < height && j < width) {
            char symbol = text[k];

            ++k;

            if (symbol == '\n' || symbol == '\r') {
                continue;
            }

            switch (symbol) {
                case ',':
                    tiles[i, j] = Tile.Empty;
                    break;
                case '.':
                    tiles[i, j] = Tile.Floor;
                    break;
                case '#':
                    tiles[i, j] = Tile.Wall;
                    break;
                case 'p':
                    tiles[i, j] = Tile.Player;
                    position = new Vector2Int(j, i);
                    break;
                case 'e':
                    tiles[i, j] = Tile.Enemy;
                    break;
                case 'h':
                    tiles[i, j] = Tile.HP;
                    break;
                case 'd':
                    tiles[i, j] = Tile.Door;
                    break;
                case 'k':
                    tiles[i, j] = Tile.Key;
                    break;
                case 'b':
                    tiles[i, j] = Tile.Boss;
                    break;
                case '0':
                    tiles[i, j] = Tile.End;
                    break;
                case 's':
                    tiles[i, j] = Tile.Shield;
                    break;
                case 'w':
                    tiles[i, j] = Tile.Sword;
                    break;
            }

            ++j;
            if (j == width) {
                j = 0;
                ++i;
            }
        }
    }
}
