namespace CoreHal.PropertyNaming
{
    public interface IPropertyNameConvention
    {
        public string Apply(string propertyName);
    }
}