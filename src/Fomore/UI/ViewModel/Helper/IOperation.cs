namespace Fomore.UI.ViewModel.Helper
{
    public interface IOperation<T>
    {
        void PerformOperation(T entity);
        void Undo(T entity);
    }
}