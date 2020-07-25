using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwitterApp : App {
    [SerializeField]
    private GameObject feed = null;
    
    [SerializeField]
    private TweetWidget tweetPrefab = null;
    
    [SerializeField]
    private RectTransform activeArea = null;

    [SerializeField]
    private ScrollRect scroll;

    public List<TweetWidget> tweets = new List<TweetWidget>(TwitterManager.maxTweets);

    private const float minSendTime = 3.0f;
    private const float maxSendTime = 7.0f;

    private float time = 0.0f;
    private float sendTime;

    private void OnEnable()
    {
        if (scroll != null)
            scroll.verticalNormalizedPosition = 0.0f;

        TwitterManager.Instance.UnreadMessagesCount = 0;
    }

    private void Awake() {
        sendTime = Random.Range(minSendTime, maxSendTime);
    }

    private void Start() {
        foreach (var tweet in TwitterManager.Instance.Tweets) {
            var tweetWidget = Instantiate(tweetPrefab, feed.transform);
            tweetWidget.Setup(tweet);
            tweets.Add(tweetWidget);
        }
    }

    public override void UpdateApp() {
        time += Time.deltaTime;

        if (time > sendTime) {
            var tweet = TwitterManager.Instance.NextTweet();
            
            if (tweet != null) {
                var tweetWidget = Instantiate(tweetPrefab, feed.transform);
                tweetWidget.Setup(tweet);
                tweets.Add(tweetWidget);
            }

            time = 0.0f;
            sendTime = Random.Range(minSendTime, maxSendTime);
        }

        for (int i = 0, tweetsCount = tweets.Count; i < tweetsCount; ++i) {
            tweets[i].SetVisibility(activeArea.rect.Contains(activeArea.InverseTransformPoint(tweets[i].transform.position)));
        }
    }
}
