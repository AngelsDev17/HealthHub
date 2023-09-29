using HealthHub.Test.InitialConfigurationTest.MockData;

namespace HealthHub.Test.InitialConfigurationTest;

[Collection("DomainListsConfiguration")]
public class DomainListsConfiguration
{
    private readonly DatabaseFixture _databaseFixture;

    public DomainListsConfiguration(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
    }


    [Fact]
    public async Task UpdateCityDomainList()
    {
        await _databaseFixture._cityRepository.DeleteManyAsync();
        await _databaseFixture._cityRepository.InsertManyAsync(entities: UserDomainListMockData.Cities);
    }

    [Fact]
    public async Task UpdateGenderDomainList()
    {
        await _databaseFixture._genderRepository.DeleteManyAsync();
        await _databaseFixture._genderRepository.InsertManyAsync(entities: UserDomainListMockData.Genders);
    }

    [Fact]
    public async Task UpdateIdentificationTypeDomainList()
    {
        await _databaseFixture._identificationTypeRepository.DeleteManyAsync();
        await _databaseFixture._identificationTypeRepository.InsertManyAsync(entities: UserDomainListMockData.IdentificationTypes);
    }

    [Fact]
    public async Task UpdateLocalityDomainList()
    {
        await _databaseFixture._localityRepository.DeleteManyAsync();
        await _databaseFixture._localityRepository.InsertManyAsync(entities: UserDomainListMockData.Localities);
    }

    [Fact]
    public async Task UpdateRoleDomainList()
    {
        await _databaseFixture._roleRepository.DeleteManyAsync();
        await _databaseFixture._roleRepository.InsertManyAsync(entities: UserDomainListMockData.Roles);
    }
}
