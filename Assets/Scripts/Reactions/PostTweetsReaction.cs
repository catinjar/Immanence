public class PostTweetsReaction : Reaction {
    public int count;
    public bool immediate;

    public override void React()
        => TwitterManager.Instance.PostTweets(count, immediate);
}
