using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Grader.DAL.Db;
using King.FlightSearch.DAL.Repository;
using King.FlightSearch.DAL.Repository.Dto;
using King.FlightSearch.Services;
using King.FlightSearch.Services.External.Airports;

namespace King.FlightSearch.Web.Infrastructure
{
    // http://docs.autofac.org/en/latest/integration/webforms.html
    public class DI
    {
        internal static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<FlightDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<AirportMapper>().AsSelf();
            builder.RegisterType<SearchEntryMapper>().AsSelf();
            builder.RegisterType<ItineraryEntryMapper>().AsSelf();
            builder.RegisterType<FlightEntryMapper>().AsSelf();

            builder.RegisterType<AirportRepository>().AsSelf();
            builder.RegisterType<SearchEntryRepository>().AsSelf();
            builder.RegisterType<ItineraryEntryRepository>().AsSelf();
            builder.RegisterType<FlightEntryRepository>().AsSelf();

            builder.RegisterType<FlightApiService>().AsSelf();
            builder.RegisterType<AirportApiService>().AsSelf();

            builder.RegisterType<FlightService>()
                .AsSelf();
        }
    }
}