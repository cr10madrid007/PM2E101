using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E101.Models;

namespace PM2E101
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vista : ContentPage
    {
        public vista()
        {
            InitializeComponent();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var emple = new constructor
            {
                codigo = Convert.ToInt32(lblCodigo.Text)
            };
            var resultado = await App.BaseDatos.EmpleadoBorrar(emple);
            if (resultado != 0)
            {
                await DisplayAlert("Aviso", "Empleado Borrado con exito!!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Aviso", "Error!!", "OK");
            }
            

        }
    }
}