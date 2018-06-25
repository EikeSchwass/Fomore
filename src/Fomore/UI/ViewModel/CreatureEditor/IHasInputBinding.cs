// Eike Stein: Fomore/UI/IHasInputBinding.cs (2018/06/25)

using System.Windows.Input;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public interface IHasInputBinding
    {
        InputBinding GetInputBinding();
    }
}