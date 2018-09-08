namespace BitLottery.Database.Interfaces
{
    /// <summary>
    /// Generic Repository for storing, retrieving, updating and deleting entities
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <typeparam name="K">The key type</typeparam>
    public interface IRepository<T, K>
    {
        /// <summary>
        /// Insert the entity in the repository
        /// </summary>
        /// <returns>The id of the entity</returns>
        int Insert(T entity);

        /// <summary>
        /// Get the entity from the repository
        /// </summary>
        T Get(K key);

        /// <summary>
        /// Update the entity from the repository
        /// </summary>
        /// <returns>Has the update been successfull</returns>
        bool Update(T entity, K key);

        /// <summary>
        /// Delete the entity from the repository
        /// </summary>
        /// <returns>Has the delete been successfull</returns>
        bool Delete(K key);
    }
}
