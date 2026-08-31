using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestoApp.Presentation.Models;

namespace RestoApp.Presentation.ViewModels;

public partial class TablesViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<TableModel> _allTables = new();

    [ObservableProperty]
    private ObservableCollection<TableModel> _filteredTables = new();

    [ObservableProperty]
    private string _selectedSector = "Todos";

    [ObservableProperty]
    private TableModel? _selectedTable;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public string[] Sectors => new[] { "Todos", "Salón Principal", "Terraza", "Barra", "VIP" };

    public int TotalLibres => AllTables.Count(t => t.Estado == "Libre");
    public int TotalReservadas => AllTables.Count(t => t.Estado == "Reservada");
    public int TotalOcupadas => AllTables.Count(t => t.Estado == "Ocupada");
    public int TotalEnLimpieza => AllTables.Count(t => t.Estado == "En Limpieza");

    public TablesViewModel()
    {
        LoadMockTables();
        FilterTables();
    }

    private void LoadMockTables()
    {
        AllTables = new ObservableCollection<TableModel>
        {
            new TableModel { Id = 1, NroMesa = 1, Capacidad = 2, Ubicacion = "Salón Principal", Estado = "Libre" },
            new TableModel { Id = 2, NroMesa = 2, Capacidad = 4, Ubicacion = "Salón Principal", Estado = "Ocupada", ClienteAsignado = "Juan Pérez", MontoConsumo = 24500.00m, MozoAsignado = "Marcos" },
            new TableModel { Id = 3, NroMesa = 3, Capacidad = 4, Ubicacion = "Salón Principal", Estado = "Reservada", ClienteAsignado = "Ana Rossi", MozoAsignado = "Lucía" },
            new TableModel { Id = 4, NroMesa = 4, Capacidad = 6, Ubicacion = "Salón Principal", Estado = "En Limpieza" },
            new TableModel { Id = 5, NroMesa = 5, Capacidad = 2, Ubicacion = "Terraza", Estado = "Libre" },
            new TableModel { Id = 6, NroMesa = 6, Capacidad = 4, Ubicacion = "Terraza", Estado = "Ocupada", ClienteAsignado = "Carlos V.", MontoConsumo = 38900.00m, MozoAsignado = "Pedro" },
            new TableModel { Id = 7, NroMesa = 7, Capacidad = 4, Ubicacion = "Terraza", Estado = "Reservada", ClienteAsignado = "Roberto M." },
            new TableModel { Id = 8, NroMesa = 8, Capacidad = 2, Ubicacion = "Barra", Estado = "Libre" },
            new TableModel { Id = 9, NroMesa = 9, Capacidad = 2, Ubicacion = "Barra", Estado = "Ocupada", ClienteAsignado = "Gonzalo T.", MontoConsumo = 12000.00m, MozoAsignado = "Sofia" },
            new TableModel { Id = 10, NroMesa = 10, Capacidad = 8, Ubicacion = "VIP", Estado = "Reservada", ClienteAsignado = "Empresa ACME", MozoAsignado = "Mariano" },
            new TableModel { Id = 11, NroMesa = 11, Capacidad = 6, Ubicacion = "VIP", Estado = "Libre" },
            new TableModel { Id = 12, NroMesa = 12, Capacidad = 4, Ubicacion = "Salón Principal", Estado = "Libre" }
        };
        SelectedTable = AllTables.FirstOrDefault();
    }

    partial void OnSelectedSectorChanged(string value)
    {
        FilterTables();
    }

    private void FilterTables()
    {
        if (SelectedSector == "Todos")
        {
            FilteredTables = new ObservableCollection<TableModel>(AllTables);
        }
        else
        {
            FilteredTables = new ObservableCollection<TableModel>(AllTables.Where(t => t.Ubicacion == SelectedSector));
        }
    }

    [RelayCommand]
    private void SelectTable(TableModel table)
    {
        SelectedTable = table;
    }

    [RelayCommand]
    private void ChangeState(string newState)
    {
        if (SelectedTable == null) return;

        SelectedTable.Estado = newState;
        if (newState == "Libre" || newState == "En Limpieza")
        {
            SelectedTable.ClienteAsignado = null;
            SelectedTable.MontoConsumo = 0;
        }

        StatusMessage = $"Mesa #{SelectedTable.NroMesa} actualizada a estado '{newState}'.";
        
        OnPropertyChanged(nameof(TotalLibres));
        OnPropertyChanged(nameof(TotalReservadas));
        OnPropertyChanged(nameof(TotalOcupadas));
        OnPropertyChanged(nameof(TotalEnLimpieza));
    }
}
