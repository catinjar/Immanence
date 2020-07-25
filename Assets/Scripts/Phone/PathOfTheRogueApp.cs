using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class RectTransformExtensions {
    public static bool Overlaps(this RectTransform a, RectTransform b) {
        return a.WorldRect().Overlaps(b.WorldRect());
    }
    public static bool Overlaps(this RectTransform a, RectTransform b, bool allowInverse) {
        return a.WorldRect().Overlaps(b.WorldRect(), allowInverse);
    }

    public static Rect WorldRect(this RectTransform rectTransform) {
        var sizeDelta = rectTransform.sizeDelta;
        float rectTransformWidth = sizeDelta.x * rectTransform.lossyScale.x;
        float rectTransformHeight = sizeDelta.y * rectTransform.lossyScale.y;

        var position = rectTransform.position;
        return new Rect(position.x - rectTransformWidth / 2f, position.y - rectTransformHeight / 2f, rectTransformWidth, rectTransformHeight);
    }
}

public class PathOfTheRogueApp : App {
    [Serializable]
    public class Wall {
        public RectTransform Transform;
        public RectTransform TopWall;
        public RectTransform BottomWall;
        public bool Collide = true;
    }

    [SerializeField]
    private GameObject m_gameScreen;
    [SerializeField]
    private GameObject m_menuScreen;

    [SerializeField]
    private RectTransform m_world;
    [SerializeField]
    private RectTransform m_bird;
    [SerializeField]
    private List<Wall> m_walls;
    [SerializeField]
    private RectTransform m_spawn;

    [SerializeField]
    private TextMeshProUGUI m_scoreText;
    [SerializeField]
    private TextMeshProUGUI m_highscore;

    [SerializeField]
    private float m_accelerationY;
    [SerializeField]
    private float m_jumpImpulse;
    [SerializeField]
    private float m_wallsVelocityX;
    [SerializeField]
    private float m_wallsOffset;

    [SerializeField]
    private AudioSource m_music;
    [SerializeField]
    private AudioSource m_scoreSound;
    [SerializeField]
    private AudioSource m_deathSound;

    private Vector3 m_velocity;
    private Vector3 m_startPosition;

    private int m_score = 0;

    private void Awake() {
        m_startPosition = m_bird.localPosition;
    }

    private void OnEnable() {
        ShowMenu();
        GameOver();

        m_music.Play();
    }

    private void OnDisable() {
        m_music.Stop();
    }

    public override void UpdateApp() {
        if (!m_gameScreen.gameObject.activeInHierarchy)
            return;

        m_velocity.y += m_accelerationY * Time.deltaTime;
        m_bird.localPosition += m_velocity * Time.deltaTime;

        foreach (var wall in m_walls) {
            wall.Transform.localPosition += new Vector3(m_wallsVelocityX, 0.0f, 0.0f);

            if (m_bird.Overlaps(wall.TopWall) || m_bird.Overlaps(wall.BottomWall)) {
                GameOver();
                ShowMenu();

                m_deathSound.Play();
            }

            if (m_bird.Overlaps(wall.Transform) && wall.Collide) {
                ++m_score;
                m_scoreText.text = m_score.ToString();
                wall.Collide = false;

                m_scoreSound.Play();
            }

            if (!wall.Transform.Overlaps(m_world)) {
                wall.Transform.localPosition = new Vector3(m_spawn.localPosition.x, UnityEngine.Random.Range(-m_wallsOffset, m_wallsOffset), 0.0f);
                wall.Collide = true;
            }
        }

        if (!m_bird.Overlaps(m_world)) {
            GameOver();
            ShowMenu();

            m_deathSound.Play();
        }
    }

    private void GameOver() {
        m_velocity = Vector3.zero;
        m_bird.localPosition = m_startPosition;

        foreach (var wall in m_walls) {
            wall.Transform.localPosition = new Vector3(m_spawn.localPosition.x, UnityEngine.Random.Range(-m_wallsOffset, m_wallsOffset), 0.0f);
            wall.Collide = true;
        }

        if (m_score > PlayerProgress.Instance.flappyHighscore) {
            PlayerProgress.Instance.flappyHighscore = m_score;
        }

        m_score = 0;
        m_scoreText.text = m_score.ToString();
    }

    public void StartGame() {
        m_gameScreen.SetActive(true);
        m_menuScreen.SetActive(false);
        GameOver();
    }

    public void ShowMenu() {
        m_gameScreen.SetActive(false);
        m_menuScreen.SetActive(true);
        m_highscore.text = PlayerProgress.Instance.flappyHighscore.ToString();
    }

    public void Jump() {
        m_velocity.y = m_jumpImpulse;
    }
}
