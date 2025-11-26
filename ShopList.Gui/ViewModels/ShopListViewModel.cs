using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Gui.Model;
using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Windows.Input;

namespace ShopList.Gui.ViewModels
{
    public partial class ShopListViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _nombreDelArticulo = string.Empty;
        [ObservableProperty]
        private int _cantidadAComprar = 1;
        [ObservableProperty]
        private Item? _itemSeleccionado;

        //public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Item> Items { get; }

        //public string NombreDelArticulo
        //{
        //    get => _nombreDelArticulo;
        //    set
        //    {
        //        if(value != _nombreDelArticulo)
        //        {
        //            _nombreDelArticulo = value;
        //            OnPropertyChanged(nameof(NombreDelArticulo));
        //        }
        //    }
        //}
        //public int CantidadAComprar
        //{
        //    get => _cantidadAComprar;
        //    set
        //    {
        //        if (value != _cantidadAComprar)
        //        {
        //            _cantidadAComprar = value;
        //            OnPropertyChanged(nameof(CantidadAComprar));
        //        }
        //    }
        //}

        //public ICommand AgregarShopListItemCommand 
        //{ 
        //    get; 
        //    private set; 
        //}

        public ShopListViewModel()
        {
            Items = new ObservableCollection<Item>();
            CargarDatos();
            //AgregarShopListItemCommand = new Command(AgregarShopListItem);
        }

        [RelayCommand]
        public void AgregarShopListItem()
        {
            if(string.IsNullOrEmpty(NombreDelArticulo) || CantidadAComprar <= 0)
            {
                return;
            }
            Random generador = new Random();
            var item = new Item
            {
                Id = generador.Next(),
                Nombre = NombreDelArticulo,
                Cantidad = CantidadAComprar,
                Comprado = false
            };
            Items.Add(item);
            NombreDelArticulo = string.Empty;
            CantidadAComprar = 1;
        }
        [RelayCommand]
        public void EliminarShopListItem()
        {
            if (ItemSeleccionado != null)
            {
                int indice = Items.IndexOf(ItemSeleccionado);
                Item? nuevoSeleccionado;
                if (Items.Count > 1)
                {
                    if (indice < Items.Count - 1)
                    {
                        nuevoSeleccionado = Items[indice + 1];
                    }
                    else
                    {
                        nuevoSeleccionado = Items[indice - 1];
                    }
                }
                else
                {
                    nuevoSeleccionado = null;
                }
                Items.Remove(ItemSeleccionado);
                ItemSeleccionado = nuevoSeleccionado;
            }
        }
        private void CargarDatos()
        {
            Items.Add(new Item()
            {
                Id = 1,
                Nombre = "Leche",
                Cantidad = 2,
                Comprado = false,

            });
            Items.Add(new Item()
            {
                Id = 2,
                Nombre = "Papas",
                Cantidad = 1,
                Comprado = true,

            });
            Items.Add(new Item()
            {
                Id = 3,
                Nombre = "Jamones",
                Cantidad = 30,
                Comprado = false,

            });
        }
        //private void OnPropertyChanged(string propertyName) 
        //{
        //    PropertyChanged?.Invoke (this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}