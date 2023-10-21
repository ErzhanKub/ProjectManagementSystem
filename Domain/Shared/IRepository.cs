namespace Domain.Shared
{
    /// <summary>
    /// Represents a repository for working with entities.
    /// Представляет репозиторий для работы с сущностями.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Returns all entities from the repository.
        /// Возвращает все сущности из репозитория.
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();
        /// <summary>
        /// Creates a new entity in the repository.
        /// Создает новую сущность в репозитории.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task CreateAsync(TEntity entity);
        /// <summary>
        /// Updates an existing entity in the repository.
        /// Обновляет существующую сущность в репозитории.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        /// <summary>
        /// Deletes entities by specified identifiers.
        /// Удаляет сущности по указанным идентификаторам.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Guid[]> DeleteByIdAsync(params Guid[] id);
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
