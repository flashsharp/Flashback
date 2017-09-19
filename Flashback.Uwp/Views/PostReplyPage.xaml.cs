using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlashbackUwp.Views
{   
    public sealed partial class PostReplyPage : Page
    {
        public PostReplyPage() => InitializeComponent();

        bool _confirmLeaving;

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            // Det h�r �r ett j�vla hack...
            // 1) Det funkar inte korrekt med att s�tta e.cancel = true / false
            // 2) template10 buggar om man g�r detta i vymodellen
            // 3) om navingeringen bort sker till ett menyalternativ i hamburgarmenyn s� s�tts den default till selected �ven om man avbryter
            // Vi s�tter om menyns selected till null d� sidan �nd� inte h�nger p� n�gon meny och vi anv�nder oss av manuell-backnavigering om man v�ljer ja            
            // vi m�ste s�tta den sist annars triggar menyn sig sj�lv ytterligare en navigering. Jag orkar inte mer �n s� h�r just nu.
            if (!string.IsNullOrWhiteSpace(MessageField.Text) && !_confirmLeaving)
            {
                e.Cancel = true;
                
                var dialog = new Windows.UI.Popups.MessageDialog("�r du s�ker p� att du vill l�mna sidan?", "Bekr�fta");

                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ja") {Id = 0});
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Avbryt") {Id = 1});

                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;

                if ((await dialog.ShowAsync()).Label == "Ja")
                {
                    _confirmLeaving = true;
                    ViewModel.NavigationService.GoBack();
                }
                else
                {
                    Shell.HamburgerMenu.Selected = null;
                }
            }
        }
    }
}
