using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


using Plugin.Media;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Geolocator;
using PM2E101.Models;

namespace PM2E101
{
    public partial class MainPage : ContentPage
    {

        string ruta="";
        public MainPage()
        {
            InitializeComponent();
            InizializatePlugins();
        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            decision();

        }
        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            guardar();
        }


        private async void InizializatePlugins()
        {

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    // Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    txtLatitud.Text = location.Latitude.ToString();
                    txtLongitud.Text = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }


        private async void tomar()
        {
            var takepic = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "PhotoApp",
                Name = "TEST.jpg"
            });


            ruta = takepic.Path;


            if (takepic != null)
            {
                foto.Source = ImageSource.FromStream(() => { return takepic.GetStream(); });

            }



            var Sharephoto = takepic.Path;
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Foto",
                File = new ShareFile(Sharephoto)
            });
        }
        private async void galeria()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No soportado", "Error de permisos", "OK");
                return;

            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });


            ruta = file.Path;




            if (file != null)
            {
                foto.Source = ImageSource.FromStream(() => { return file.GetStream(); });
                return;
            }


        }



        private async void decision()
        {
            String sexResult = await DisplayActionSheet("Seleccione una opción ", "Cancelar", null, "Tomar", "Galeria");


            if (sexResult == "Tomar")
            {
                tomar();
            }

            if (sexResult == "Galeria")
            {
                galeria();
            }

        }



        private async void guardar (){
           
            if ( ruta==""  || String.IsNullOrWhiteSpace(txtDescripcion.Text) || String.IsNullOrWhiteSpace(txtLatitud.Text) || String.IsNullOrWhiteSpace(txtLongitud.Text )) { 
                await DisplayAlert("Error", "No completó todos los campos", "OK");



            }
            else
            {

                var emple = new constructor
                {
                    latitud = txtLatitud.Text,
                    longitud = txtLongitud.Text,
                    descripcion= txtDescripcion.Text ,
                    imgRuta = ruta
                };
                var resultado = await App.BaseDatos.EmpleadoGuardar(emple);
                if (resultado != 0)
                {
                    await DisplayAlert("Aviso", "Empleado Ingresado con exito!!", "OK");
                    foto.Source = ("camara2.png");
                    ruta = "";
                    txtDescripcion.Text = "";
                    InizializatePlugins();



                }
                else
                {
                    await DisplayAlert("Aviso", "Error!!", "OK");
                }




            }
        }

        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            var newpage = new lista();
            await Navigation.PushAsync(newpage);
        }
    }
}
