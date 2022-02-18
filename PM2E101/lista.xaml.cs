using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E101
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class lista : ContentPage
    {
        public lista()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ListaEmpleados.ItemsSource = await App.BaseDatos.listaempleados();
        }



        private async void ListaEmpleados_ItemTapped(object sender, ItemTappedEventArgs e)
        {


            String sexResult = await DisplayActionSheet("Seleccione una opción ", "Cancelar", null, "Vista", "Mapa");


            if (sexResult == "Vista")
            {
                Models.constructor item = (Models.constructor)e.Item;
                var newpage = new vista();
                newpage.BindingContext = item;
                await Navigation.PushAsync(newpage);
            }

            if (sexResult == "Mapa")
            {

                   Models.constructor item = (Models.constructor)e.Item;
                   var newpage = new mapa();
                   newpage.BindingContext = item;
                   await Navigation.PushAsync(newpage);
            }

           


        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

      
    }
}