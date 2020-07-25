using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TweetWidget : MonoBehaviour {
    [SerializeField] private Image avatar            = null;
    [SerializeField] private TextMeshProUGUI nick    = null;
    [SerializeField] private TextMeshProUGUI text    = null;
    [SerializeField] private TextMeshProUGUI likes   = null;
    [SerializeField] private TextMeshProUGUI reposts = null;

    private Tweet m_tweet = null;

    private void Update()
    {
        if (m_tweet == null)
            return;

        if (m_tweet.name == "nnnnn")
        {
            int asciiCharacterStart = 65;
            int asciiCharacterEnd = 122;
            int characterCount = 10;

            var sb = new StringBuilder();

            for (int j = 0; j < characterCount; ++j)
                sb.Append((char)(Random.Range(asciiCharacterStart, asciiCharacterEnd)));

            nick.text = sb.ToString();
        }
        else
        {
            nick.text = m_tweet.name;
        }
    }

    public void Setup(Tweet tweet) {
        m_tweet = tweet;

        if (tweet.name == "nnnnn")
        {
            int asciiCharacterStart = 65;
            int asciiCharacterEnd = 122;
            int characterCount = 10;

            var sb = new StringBuilder();

            for (int j = 0; j < characterCount; ++j)
                sb.Append((char)(Random.Range(asciiCharacterStart, asciiCharacterEnd)));

            nick.text = sb.ToString();
        }
        else
        {
            nick.text = tweet.name;
        }

        text.text       = tweet.text;
        likes.text      = tweet.likes.ToString();
        reposts.text    = tweet.reposts.ToString();

        // I don't care
        switch (tweet.sprite) {
            case "news":
                avatar.sprite = TwitterManager.Instance.newsSprite;
                break;
            case "john":
                avatar.sprite = TwitterManager.Instance.johnSprite;
                break;
            case "cortney":
                avatar.sprite = TwitterManager.Instance.cortneySprite;
                break;
            case "tripophob":
                avatar.sprite = TwitterManager.Instance.tripophobSprite;
                break;
            case "unknown1":
                avatar.sprite = TwitterManager.Instance.unknown1Sprite;
                break;
            case "unknown2":
                avatar.sprite = TwitterManager.Instance.unknown2Sprite;
                break;
            case "thoughts":
                avatar.sprite = TwitterManager.Instance.thoughtsSprite;
                break;
            case "doctor":
                avatar.sprite = TwitterManager.Instance.doctorSprite;
                break;
            case "denis":
                avatar.sprite = TwitterManager.Instance.denisSprite;
                break;
            case "jennet":
                avatar.sprite = TwitterManager.Instance.jennetSprite;
                break;
        }
    }

    public void SetVisibility(bool visible) {
        avatar.enabled = visible;
        nick.enabled = visible;
        text.enabled = visible;
        likes.enabled = visible;
        reposts.enabled = visible;
    }
}
