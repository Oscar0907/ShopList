using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Gui.Model;
using ShopList.Gui.Persistence;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        private Item? _itemSeleccionado = null;

        [ObservableProperty]
        private ObservableCollection<Item>? _items = null;
        private ShopListDataBase? _database = null;

        public ShopListViewModel()
        {
            _database = new ShopListDataBase();
            Items = new ObservableCollection<Item>();
            GetItems();
            if (Items.Count > 0)
            {
                ItemSeleccionado = Items.First();
            }
            else
            {
                ItemSeleccionado = null;
            }

        }

        [RelayCommand]
        public async Task AgregarShopListItem()
        {
            if(string.IsNullOrEmpty(NombreDelArticulo) || CantidadAComprar <= 0)
            {
                return;
            }
            var item = new Item
            {
                Nombre = NombreDelArticulo,
                Cantidad = CantidadAComprar,
                Comprado = false
            };
            await _database.SaveItemAsync(item);
            GetItems();
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
        private async void GetItems()
        {

            IEnumerable<Item> itemsFromDb = await _database.GetAllItemsAsync();
            Items = new ObservableCollection<Item>(itemsFromDb);

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
    }
}