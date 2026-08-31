using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestoApp.Presentation.Models;

namespace RestoApp.Presentation.ViewModels;

public partial class EventsViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<EventModel> _events = new();

    [ObservableProperty]
    private EventModel? _selectedEvent;

    // Form properties
    [ObservableProperty]
    private string _newName = string.Empty;

    [ObservableProperty]
    private DateTimeOffset _newFecha = DateTimeOffset.Now.AddDays(7);

    [ObservableProperty]
    private string _newDescripcion = string.Empty;

    [ObservableProperty]
    private string _feedbackMessage = string.Empty;

    [ObservableProperty]
    private bool _hasFeedback = false;

    public EventsViewModel()
    {
        LoadMockEvents();
    }

    private void LoadMockEvents()
    {
        Events = new ObservableCollection<EventModel>
        {
            new EventModel { Id = 1, Nombre = "Día del Amigo", Fecha = DateTime.Today.AddDays(15), Descripcion = "Menú festivo con copa de bienvenida para grupos de amigos.", ReservasVinculadasCount = 12, Activo = true },
            new EventModel { Id = 2, Nombre = "Cena Show Jazz", Fecha = DateTime.Today.AddDays(3), Descripcion = "Presentación en vivo del quinteto de Jazz 'Blue Note'.", ReservasVinculadasCount = 8, Activo = true },
            new EventModel { Id = 3, Nombre = "Año Nuevo RestoApp", Fecha = new DateTime(DateTime.Today.Year, 12, 31), Descripcion = "Gran cena de fin de año con brindis y DJ.", ReservasVinculadasCount = 25, Activo = true },
            new EventModel { Id = 4, Nombre = "San Valentín", Fecha = new DateTime(DateTime.Today.Year, 2, 14), Descripcion = "Cena romántica de 3 pasos con maridaje de vinos.", ReservasVinculadasCount = 0, Activo = false }
        };
        SelectedEvent = Events.FirstOrDefault();
    }

    [RelayCommand]
    private void CreateEvent()
    {
        if (string.IsNullOrWhiteSpace(NewName))
        {
            FeedbackMessage = "❌ Error: El nombre del evento es obligatorio.";
            HasFeedback = true;
            return;
        }

        var newEvt = new EventModel
        {
            Id = Events.Count + 1,
            Nombre = NewName,
            Fecha = NewFecha.Date,
            Descripcion = NewDescripcion,
            ReservasVinculadasCount = 0,
            Activo = true
        };

        Events.Insert(0, newEvt);
        SelectedEvent = newEvt;

        FeedbackMessage = $"🎉 Evento '{newEvt.Nombre}' registrado exitosamente en el catálogo.";
        HasFeedback = true;

        NewName = string.Empty;
        NewDescripcion = string.Empty;
    }

    [RelayCommand]
    private void ToggleEventActive(EventModel evt)
    {
        evt.Activo = !evt.Activo;
        FeedbackMessage = $"Evento '{evt.Nombre}' ahora está {(evt.Activo ? "Activo" : "Inactivo")}.";
        HasFeedback = true;
    }
}
