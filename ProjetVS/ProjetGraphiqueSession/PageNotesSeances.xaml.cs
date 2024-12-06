using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetGraphiqueSession
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageNotesSeances : Page
    {
        public PageNotesSeances()
        {
            this.InitializeComponent();
            liste.ItemsSource = Singleton.getInstance().GetSeanceNote();
        }

        // Pour insérer la note dans la BD
        private void RatingControl_ValueChanged(RatingControl sender, object args)
        {
            var ratingControl = sender as RatingControl;

            if (ratingControl != null)
            {
                var dataContext = ratingControl.DataContext as SeanceNote;

                if (dataContext != null)
                {
                    
                    int idSeance = dataContext.IdSeance;
                    double note = ratingControl.Value;

                    Singleton.getInstance().NoteSeance(note, idSeance);
                }
            }            
        }
    }
}
