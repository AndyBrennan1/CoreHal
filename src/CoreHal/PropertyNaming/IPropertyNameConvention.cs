namespace CoreHal.PropertyNaming
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPropertyNameConvention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string Apply(string propertyName);
    }
}