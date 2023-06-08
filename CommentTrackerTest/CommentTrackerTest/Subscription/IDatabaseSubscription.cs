namespace CommentTrackerTest.Subscription
{
    public interface IDatabaseSubscription
    {
        void Subscribe(string tableName);
    }
}
