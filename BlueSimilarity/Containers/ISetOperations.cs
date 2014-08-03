namespace BlueSimilarity.Containers
{
	public interface ISetOperations<T> where T: class
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="set"></param>
		/// <returns></returns>
		T Union(T set);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="set"></param>
		/// <returns></returns>
		T Intersect(T set);
	}
}
