using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataModel.Entities;
using System.Threading.Tasks;
using ViewModelBase.Commands.QuickCommands;
using DataModel;
using ViewModel.Other;
using System.ComponentModel;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace ViewModel.ViewModels;

public class MainViewModel : ViewModelBase.ViewModelBase
{
    private readonly DataManager data = Helper.DataModel;
    private readonly int Step = 5;
    private Presentation OuterContext { get; init; }

    public Command OrderByPrice { get; private init; }
    public Command OrderByPriceDescend { get; private init; }
    public Command ResetPriceOrder { get; private init; }
    public Command ResetTypeFilter { get; private init; }
    public Command ResetManufacturerFilter { get; private init; }
    public Command ResetDescriptionFilter { get; private init; }
    public Command ResetPriceFilter { get; private init; }
    public Command LoadFirstPage { get; private init; }
    public Command LoadLastPage { get; private init; }
    public Command LoadNextPage { get; private init; }
    public Command LoadPreviousPage { get; private init; }
    public Command UnlogUser { get; private init; }
    public Command<Product> EditProduct { get; private init; }
    public Command<Product> RemoveProduct { get; private init; }

    public ObservableCollection<Product> Products { get; private init; }
    public ObservableCollection<string> Manufacturers { get; private set; }
    private Counter Skiper { get; set; }

    public MainViewModel(Presentation outerContext)
    {
        OuterContext = outerContext;

        OrderByPrice = new(SelectAscendingPriceOrder, OrderByPriceCanExecute);
        OrderByPriceDescend = new(SelectDecendingPriceOrder, OrderByPriceDescendCanExecute);
        ResetPriceOrder = new(ResetOrder, CanExecute);
        ResetTypeFilter = new(ResetType, CanExecute);
        ResetManufacturerFilter = new(ResetManufacturer, CanExecute);
        ResetDescriptionFilter = new(ResetDescription, CanExecute);
        ResetPriceFilter = new(ResetPrice, CanExecute);
        LoadFirstPage = new(SetFirstPage, FirstPageCanExecute);
        LoadLastPage = new(SetLastPage, LastPageCanExecute);
        LoadNextPage = new(SetNextPage, NextPageCanExecute);
        LoadPreviousPage = new(SetPreviousPage, PreviousPageCanExecute);
        UnlogUser = new(Unlog, CanExecute);
        EditProduct = new(EditProductFunc, EditProductCanExecute);
        RemoveProduct = new(RemoveProductFunc, RemoveProductCanExecute);

        Products = new();
        Manufacturers = new();

        CurrentUser = outerContext.LoginVM.CurrentUser;

        Refresh();
    }

    internal void Refresh()
    {
        ItemsQuantity = data.Products.Items.Count();
        UpdateManufacturersObservableCollection(ProductsToManufacturers());

        var products = data.Products.Items
            .Where(FilterByManufacturer(_selectedManufacturer))
            .Where(x => x.Type.StartsWith(_typeFilter))
            .Where(x => x.Manufacturer.StartsWith(_manufacturerFilter))
            .Where(x => x.Description.StartsWith(_descriptionFilter))
            .Where(x => x.Price.ToString().StartsWith(_priceFilter));

        ItemsFound = products.Count();

        switch (OrderMode)
        {
            case Orders.Ascend:
                products = OrderByPriceFunc(products);
                break;
            case Orders.Descend:
                products = OrderByPriceDescendFunc(products);
                break;
            case Orders.Default:
                break;
        }

        UpdateProductsObservableCollection(TakePage(products));
    }

    private void AdjustSkiper()
    {
        var maximumSkip = CalculateMaximumSkipValue(_itemsFound);
        if (Skiper is null) Skiper = new(maximumSkip, Step);
        else
        {
            if (maximumSkip == Skiper.MaximumValue) return;
            Skiper = new(maximumSkip, Step);
        }
    }

    private void UpdateProductsObservableCollection(IEnumerable<Product> table)
    {
        var tmpProducts = Products.ToList();
        foreach (var product in tmpProducts) Products.Remove(product);
        foreach (var product in table) Products.Add(product);
    }

    private void UpdateManufacturersObservableCollection(IEnumerable<string> manufacturers)
    {
        foreach (var manufacturer in manufacturers)
            if (!Manufacturers.Contains(manufacturer)) Manufacturers.Add(manufacturer);
    }

    private IQueryable<Product> TakePage(IQueryable<Product> table)
    {
        AdjustSkiper();
        CurrentPage = Skiper.CurrentValue / Step;
        MaximumPage = _itemsFound / Step + (_itemsFound % Step == 0 ? 0 : 1);
        LoadFirstPage.RaiseCanExecuteChanged();
        LoadLastPage.RaiseCanExecuteChanged();
        LoadNextPage.RaiseCanExecuteChanged();
        LoadPreviousPage.RaiseCanExecuteChanged();
        return table.Skip(Skiper.CurrentValue).Take(Step);
    }

    private static IQueryable<Product> OrderByPriceFunc(IQueryable<Product> table)
    { return table.OrderBy(x => x.Price); }

    private static IQueryable<Product> OrderByPriceDescendFunc(IQueryable<Product> table)
    { return table.OrderByDescending(x => x.Price); }

    private static Expression<Func<Product, bool>> FilterByManufacturer(string filter)
    {
        if (filter is null) return p => true;
        if (filter == "Все производители") return p => true;
        else return p => p.Manufacturer == filter;
    }

    public void UpdateCurrentUser() => CurrentUser = OuterContext.LoginVM.CurrentUser;
    private void SelectAscendingPriceOrder() => OrderMode = Orders.Ascend;
    private void SelectDecendingPriceOrder() => OrderMode = Orders.Descend;
    private void ResetOrder() => OrderMode = Orders.Default;
    private void ResetType() => TypeFilter = "";
    private void ResetManufacturer() => ManufacturerFilter = "";
    private void ResetDescription() => DescriptionFilter = "";
    private void ResetPrice() => PriceFilter = "";
    private void SetFirstPage()
    {
        Skiper.Reset();
        Refresh();
    }
    private void SetLastPage()
    {
        Skiper.SetCurrentValue(Skiper.MaximumValue);
        Refresh();
    }
    private void SetNextPage()
    {
        Skiper.Increase();
        Refresh();
    }
    private void SetPreviousPage()
    {
        Skiper.Decrease();
        Refresh();
    }
    private void Unlog()
    {
        Helper.MainToLogin?.Invoke(null);
        ResetAll();
    }

    private void EditProductFunc(Product? product)
    {
        Helper.MainToEdit?.Invoke(null);
    }

    private async void RemoveProductFunc(Product? product)
    {
        if (product is null) return;
        Products.Remove(Products.First(x => x.Id == product.Id));
        await data.Products.DeleteAsync(product.Id);
        Refresh();
    }

    private bool CanExecute()
    { return true; }
    private bool FirstPageCanExecute()
    { return CurrentPage > 1; }
    private bool LastPageCanExecute()
    { return CurrentPage < MaximumPage; }
    private bool NextPageCanExecute()
    { return CurrentPage < MaximumPage; }
    private bool PreviousPageCanExecute()
    { return CurrentPage > 1; }
    private bool OrderByPriceCanExecute()
    { return OrderMode != Orders.Ascend; }
    private bool OrderByPriceDescendCanExecute()
    { return OrderMode != Orders.Descend; }
    private bool EditProductCanExecute(Product? product)
    {
        if (CurrentUser is null) return false;
        return CurrentUser.Role == "Admin";
    }
    private bool RemoveProductCanExecute(Product? product)
    {
        if (product is null) return false;
        var order = data.OrderProducts.Items.FirstOrDefault(x => x.ProductId == product.Id);
        if (order is null) return true;
        return false;
    }

    private IEnumerable<string> ProductsToManufacturers()
    {
        HashSet<string> manufacturers = new() { "Все производители" };
        foreach (var product in data.Products.Items.ToList().DistinctBy(x => x.Manufacturer)) manufacturers.Add(product.Manufacturer);
        
        return manufacturers;
    }

    private int CalculateMaximumSkipValue(int n)
    {
        n = n % Step == 0 ? n - Step : (n / Step) * Step;
        if (n < 0) return 0;
        return n;
    }

    private void ResetAll()
    {
        CurrentUser = null;
        SelectedManufacturer = "Все производители";
        TypeFilter = "";
        ManufacturerFilter = "";
        DescriptionFilter = "";
        PriceFilter = "";
        OrderMode = Orders.Default;
    }

    private string _selectedManufacturer = "Все производители";
    public string SelectedManufacturer
    {
        get { return _selectedManufacturer; }
        set { if (Set(ref _selectedManufacturer, value)) Refresh(); }
    }

    private string _typeFilter = "";
    public string TypeFilter
    {
        get { return _typeFilter; }
        set { if (Set(ref _typeFilter, value)) Refresh(); }
    }

    private string _manufacturerFilter = "";
    public string ManufacturerFilter
    {
        get { return _manufacturerFilter; }
        set { if (Set(ref _manufacturerFilter, value)) Refresh(); }
    }

    private string _descriptionFilter = "";
    public string DescriptionFilter
    {
        get { return _descriptionFilter; }
        set { if (Set(ref _descriptionFilter, value)) Refresh(); }
    }

    private string _priceFilter = "";
    public string PriceFilter
    {
        get { return _priceFilter; }
        set { if (Set(ref _priceFilter, value)) Refresh(); }
    }

    private int _itemsQuantity = 0;
    public int ItemsQuantity
    {
        get { return _itemsQuantity; }
        private set { Set(ref _itemsQuantity, value); }
    }

    private int _itemsFound = 0;
    public int ItemsFound
    {
        get { return _itemsFound; }
        set { Set(ref _itemsFound, value); }
    }

    private int _currentPage = 0;
    public int CurrentPage
    {
        get { return _currentPage + 1; }
        private set { Set(ref _currentPage, value); }
    }

    private int _maximumPage = 0;
    public int MaximumPage
    {
        get { return _maximumPage; }
        private set { Set(ref _maximumPage, value); }
    }

    private enum Orders
    {
        Ascend,
        Descend,
        Default
    }

    private Orders _orderMode = Orders.Default;
    private Orders OrderMode
    {
        get { return _orderMode; }
        set
        {
            if (Set(ref _orderMode, value))
            {
                OrderByPrice.RaiseCanExecuteChanged();
                OrderByPriceDescend.RaiseCanExecuteChanged();
                Refresh();
            }
        }
    }

    private User? _currentUser = null;
    public User? CurrentUser
    {
        get { return _currentUser; }
        private set
        {
            if (Set(ref _currentUser, value))
            {
                EditProduct.RaiseCanExecuteChanged();
            }
        }
    }
}
