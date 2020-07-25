using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TwitterManager", menuName = "Gameplay/TwitterManagers")]
public class TwitterManager : SingletonScriptableObject<TwitterManager> {
    public Sprite newsSprite;
    public Sprite johnSprite;
    public Sprite cortneySprite;
    public Sprite tripophobSprite;
    public Sprite unknown1Sprite;
    public Sprite unknown2Sprite;
    public Sprite thoughtsSprite;
    public Sprite doctorSprite;
    public Sprite denisSprite;
    public Sprite jennetSprite;

    public static readonly int maxTweets = 100;

    [NonSerialized]
    public int position = 0;

    private List<Tweet> tweets       = new List<Tweet>();
    private List<Tweet> unsentTweets = new List<Tweet>();

    public int UnreadMessagesCount { get; set; } = 0;

    public List<Tweet> Tweets => tweets.ToList();

    public Tweet NextTweet() {
        if (unsentTweets.Count == 0) {
            return null;
        }

        var tweet = unsentTweets[0];
        unsentTweets.RemoveAt(0);

        tweets.Add(tweet);

        while (tweets.Count > maxTweets) {
            tweets.RemoveAt(0);
        }

        ++UnreadMessagesCount;

        return tweet;
    }

    public void PostTweets(int count) {
        PostTweets(count, false);
    }

    public void PostTweets(int count, bool immediate = false) {
        var portion = TextManager.Instance.PostTweets(position, count);

        if (!immediate)
            portion.ForEach(t => unsentTweets.Add(t));
        else
            portion.ForEach(t => tweets.Add(t));

        position += count;
    }

    public void Save() {
        PlayerPrefs.SetInt("tweetsPosition", position);
        PlayerPrefs.SetString("tweets", JsonConvert.SerializeObject(tweets, Formatting.Indented));
        PlayerPrefs.SetString("unsentTweets", JsonConvert.SerializeObject(unsentTweets, Formatting.Indented));
    }

    public void Load() {
        position = PlayerPrefs.GetInt("tweetsPosition", 0);

        var tweetsData = PlayerPrefs.GetString("tweets", null);
        tweets = string.IsNullOrEmpty(tweetsData) ? new List<Tweet>() : JsonConvert.DeserializeObject<List<Tweet>>(tweetsData);

        var unsentTweetsData = PlayerPrefs.GetString("unsentTweets", null);
        unsentTweets = string.IsNullOrEmpty(unsentTweetsData) ? new List<Tweet>() : JsonConvert.DeserializeObject<List<Tweet>>(unsentTweetsData);
    }

    public void Reset() {
        position = 0;
        tweets = new List<Tweet>();
        unsentTweets = new List<Tweet>();
    }
}
