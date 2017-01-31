namespace FCT.Client.Dto.Interfaces
{
    /// <summary>
    /// Принимает Visitor обработчик и возвращает результат типа T
    /// </summary>
    public interface IDtoProcessorAccepter
    {
        T Visit<T>(IDtoProcessVisitor<T> visitor);
    }
}