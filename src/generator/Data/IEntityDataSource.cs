namespace Generator.Data
{
    internal interface IEntityDataSource<out T>
    {
        IReadOnlyCollection<T> Get();
    }
}
