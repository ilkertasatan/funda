namespace Funda.Assignment.Domain
{
    public interface ITranslateProperty<in T> where T : class
    {
        Property Translate(T response);
    }
}