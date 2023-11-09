using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DataModel.DataProviders.Ef.Core.Repositories;
using System.IO;
using DataModel.Entities;

namespace DataModel
{
    public class DataManager
    {
        public ICityRep City { get; }
        public ICountryRep Country { get; }
        public IDirectionRep Direction { get; }
        public IEventInfoRep EventInfo { get; }
        public IEventRep Event { get; }
        public IEventsDirectionRep EventsDirection { get; }
        public IEventsJudgeRep EventsJudge { get; }
        public IJudgeRep Judge { get; }
        public IModeratorRep Moderator { get; }
        public IUsersARep UsersA { get; }

        private DataManager(
            ICityRep city,
            ICountryRep country,
            IDirectionRep direction,
            IEventRep event_,
            IEventInfoRep eventInfo,
            IEventsDirectionRep eventsDirection,
            IEventsJudgeRep eventsJudge,
            IJudgeRep judge,
            IModeratorRep moderator,
            IUsersARep usersA)
        {
            City = city;
            Country = country;
            Direction = direction;
            Event = event_;
            EventInfo = eventInfo;
            EventsDirection = eventsDirection;
            EventsJudge = eventsJudge;
            Judge = judge;
            Moderator = moderator;
            UsersA = usersA;
        }

        public static DataManager Get(DataProvidersList dataProviders)
        {
            switch (dataProviders)
            {
                case DataProvidersList.Json:
                case DataProvidersList.Txt:
                case DataProvidersList.Oracle:
                case DataProvidersList.SqLite:
                    throw new NotSupportedException("Поставщики данных находятся в стадии разработки");
                case DataProvidersList.MySql:
                    throw new NotSupportedException("Поставщики данных находятся в стадии разработки");
                case DataProvidersList.SqlServer:
                    var context = new DataProviders.Ef.Core.DataContext();
                    context.Database.EnsureCreated();
                    return new DataManager
                    (
                        new EfCity(context),
                        new EfCountry(context),
                        new EfDirection(context),
                        new EfEvent(context),
                        new EfEventInfo(context),
                        new EfEventsDirection(context),
                        new EfEventsJudge(context),
                        new EfJudge(context),
                        new EfModerator(context),
                        new EfUsersA(context)
                    );
                default:
                    throw new NotSupportedException("Поставщики данных неизвестен");
            }
        }
    }
}
