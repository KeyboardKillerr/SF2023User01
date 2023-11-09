using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataModel;
using DataModel.Entities;
using DataModel.DataProviders.Ef.Core;
using System.Globalization;

namespace Importer;

internal static class EntitiesImporter
{
    private static readonly DataManager data = DataManager.Get(DataProvidersList.SqlServer);
    private static string GlobalPath = "C:\\Users\\radia\\OneDrive\\Рабочий стол\\09_1.1-2022_6\\09_1.1-2022_6\\Вариант 6\\Сессия 1";
    public static List<List<string>> FancyReader(string path)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var encoding = Encoding.GetEncoding("windows-1251");
        var result = new List<List<string>>();
        using var reader = new StreamReader(encoding: encoding, path: $"{GlobalPath}\\{path}");
        var tmp = new List<string>();
        var args = new List<string>();
        tmp = reader.ReadToEnd().Trim().Split("\n").ToList();
        tmp.RemoveAt(0);
        foreach (var line in tmp)
        {
            args = new();
            foreach (var arg in line.Split(';')) args.Add(arg.Trim());
            result.Add(args);
        }
        return result;
    }

    private static string MatchRole(string role) => role switch
    {
        "Администратор" => "Admin",
        "Менеджер" => "Manager",
        "Клиент" => "Customer",
        _ => throw new ArgumentException()
    };

    private static string MatchStatus(string status) => status switch
    {
        "Завершен" => "Done",
        "Новый" => "New",
        _ => throw new ArgumentException()
    };

    public static void ImportUsers()
    {
        string path = "user_import.csv";
        var parsed = FancyReader(path);
        foreach (var line in parsed)
        {
            var table = new User()
            {
                Id = Convert.ToInt32(line[0]),
                Role = MatchRole(line[1]),
                FirstName = line[2],
                MiddleName = line[3],
                LastName = line[4],
                Login = line[5],
                Password = User.GetHashString(line[6])
            };
            data.Users.Insert(table);
        }
    }

    public static void ImportPickupPoints()
    {
        string path = "Пункты выдачи_import.csv";
        var parsed = FancyReader(path);
        foreach (var line in parsed)
        {
            var table = new PickupPoint()
            {
                Id = Convert.ToInt32(line[0]),
                PostalCode = Convert.ToInt32(line[1]),
                City = line[2],
                Street = line[3],
                Building = Convert.ToInt32(line[4])
            };
            data.PickupPoints.Insert(table);
        }
    }

    public static void ImportProducts()
    {
        string path = "Товар_import\\Товар_import_Товары для животных.csv";
        var parsed = FancyReader(path);
        foreach (var line in parsed)
        {
            var table = new Product()
            {
                Id = line[0],
                Type = line[1],
                MeasurmentUnit = line[2],
                Price = Convert.ToInt32(line[3]),
                MaximumDiscount = Convert.ToInt32(line[4]),
                Manufacturer = line[5],
                Dealer = line[6],
                Category = line[7],
                CurrentDiscount = Convert.ToInt32(line[8]),
                StorageQuantity = Convert.ToInt32(line[9]),
                Description = line[10],
                ImagePath = line[11] == "" ? null : line[11]
            };
            data.Products.Insert(table);
        }
    }

    public static void ImportOrders()
    {
        string path = "Заказ_import.csv";
        var parsed = FancyReader(path);
        foreach (var line in parsed)
        {
            var table = new Order()
            {
                Id = Convert.ToInt32(line[0]),
                OrderDate = DateTime.Parse(line[1]),
                DeliveryDate = DateTime.Parse(line[2]),
                PickupPointKey = Convert.ToInt32(line[3]),
                UserKey = Convert.ToInt32(line[4]),
                ConfirmationCode = Convert.ToInt32(line[5]),
                OrderStatus = MatchStatus(line[6])
            };
            data.Orders.Insert(table);
        }
    }

    public static void ImportOrderProducts()
    {
        string path = "СоставыЗаказов.csv";
        var parsed = FancyReader(path);
        foreach (var line in parsed)
        {
            var table = new OrdersProducts()
            {
                OrderId = Convert.ToInt32(line[0]),
                ProductId = line[1],
                Quantity = Convert.ToInt32(line[2])
            };
            data.OrderProducts.Insert(table);
        }
    }
}