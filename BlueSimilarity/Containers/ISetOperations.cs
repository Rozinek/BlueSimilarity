namespace BlueSimilarity.Containers
{
	public interface ISetOperations<T> where T : class
	{
		#region Methods (public)

		/// <summary>
		/// </summary>
		/// <param name="set"></param>
		/// <returns></returns>
		T Intersect(T set);

		/// <summary>
		/// </summary>
		/// <param name="set"></param>
		/// <returns></returns>
		T Union(T set);

		#endregion
	}
}