namespace CommentTrackerTest.Subscription.MiddleWare
{
    public static class DatabaseSubscriptionMiddleWare
    {
        public static void UseDatabaseSubscription<T>(this IApplicationBuilder builder, string tableName) where T : class ,IDatabaseSubscription
        {
           var subscription = (T)builder.ApplicationServices.GetService(typeof(T));
            subscription.Subscribe(tableName);
        }
    }
}
