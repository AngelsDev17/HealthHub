using Angels.Packages.Cache.Services;
using Angels.Packages.MongoDb.ApplicationContext;
using Angels.Packages.MongoDb.Models;
using HealthHub.Domain.Models.DomainLists.User;
using HealthHub.Persistence.Repositories.DomainLists.User;

namespace HealthHub.Test.InitialConfigurationTest;

public class DatabaseFixture : IAsyncLifetime
{
    public readonly ApplicationDbContext _applicationDbContext;

    public readonly IMongoCollection<City> _collectionCity;
    public readonly IMongoCollection<Gender> _collectionGender;
    public readonly IMongoCollection<IdentificationType> _collectionIdentificationType;
    public readonly IMongoCollection<Locality> _collectionLocality;
    public readonly IMongoCollection<Role> _collectionRole;

    public readonly CityRepository _cityRepository;
    public readonly GenderRepository _genderRepository;
    public readonly IdentificationTypeRepository _identificationTypeRepository;
    public readonly LocalityRepository _localityRepository;
    public readonly RoleRepository _roleRepository;

    public DatabaseFixture()
    {
        var cacheService = new CacheService(logger: Mock.Of<ILogger<CacheService>>());

        var dbSettings = new DbSettings()
        {
            DatabaseName = "HealthHubDomainListsDb",
            ConnectionString = "mongodb://localhost:27017"
        };

        _applicationDbContext = new ApplicationDbContext(dbSettings);

        _collectionCity = _applicationDbContext.SetMongoCollection<City>();
        _collectionGender = _applicationDbContext.SetMongoCollection<Gender>();
        _collectionIdentificationType = _applicationDbContext.SetMongoCollection<IdentificationType>();
        _collectionLocality = _applicationDbContext.SetMongoCollection<Locality>();
        _collectionRole = _applicationDbContext.SetMongoCollection<Role>();

        _cityRepository = new CityRepository(
            applicationDbContext: _applicationDbContext,
            logger: Mock.Of<ILogger<CityRepository>>(),
            cacheService: cacheService);

        _genderRepository = new GenderRepository(
            applicationDbContext: _applicationDbContext,
            logger: Mock.Of<ILogger<GenderRepository>>(),
            cacheService: cacheService);

        _identificationTypeRepository = new IdentificationTypeRepository(
            applicationDbContext: _applicationDbContext,
            logger: Mock.Of<ILogger<IdentificationTypeRepository>>(),
            cacheService: cacheService);

        _localityRepository = new LocalityRepository(
            applicationDbContext: _applicationDbContext,
            logger: Mock.Of<ILogger<LocalityRepository>>(),
            cacheService: cacheService);

        _roleRepository = new RoleRepository(
            applicationDbContext: _applicationDbContext,
            logger: Mock.Of<ILogger<RoleRepository>>(),
            cacheService: cacheService);
    }


    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync() => Task.CompletedTask;
}

[CollectionDefinition("DomainListsConfiguration")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture> { }
