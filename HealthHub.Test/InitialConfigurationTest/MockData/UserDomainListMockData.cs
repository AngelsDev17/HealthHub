using HealthHub.Domain.DomainLists.User;

namespace HealthHub.Test.InitialConfigurationTest.MockData
{
    public static class UserDomainListMockData
    {
        public static List<City> Cities => new()
        {
            new() { Id = "001", Text = "Bogotá D.C." }
        };

        public static List<Gender> Genders => new()
        {
            new() { Id = "Male", Text = "Masculino" },
            new() { Id = "Female", Text = "Femenino" }
        };

        public static List<IdentificationType> IdentificationTypes => new()
        {
            new() { Id = "CC", Text = "Cédula de ciudadanía" },
            new() { Id = "CE", Text = "Cédula de extranjeria" },
            new() { Id = "TI", Text = "Tarjeta de identidad" },
            new() { Id = "PS", Text = "Pasaporte" }
        };

        public static List<JuridicalIdentificationType> JuridicalIdentificationTypes => new()
        {
            new() { Id = "NIT", Text = "Número de identificación tributaria" },
            new() { Id = "RUT", Text = "Registro único tributario" }
        };

        public static List<Locality> Localities => new()
        {
            new() { Id = "001", ParentId = "001", Text = "Usaquén" },
            new() { Id = "002", ParentId = "001", Text = "Chapinero" },
            new() { Id = "003", ParentId = "001", Text = "Santa Fe" },
            new() { Id = "004", ParentId = "001", Text = "San Cristobal" },
            new() { Id = "005", ParentId = "001", Text = "Usme" },
            new() { Id = "006", ParentId = "001", Text = "Tunjuelito" },
            new() { Id = "007", ParentId = "001", Text = "Bosa" },
            new() { Id = "008", ParentId = "001", Text = "Kennedy" },
            new() { Id = "009", ParentId = "001", Text = "Fontibón" },
            new() { Id = "010", ParentId = "001", Text = "Engativá" },
            new() { Id = "011", ParentId = "001", Text = "Suba" },
            new() { Id = "012", ParentId = "001", Text = "Barrios Unidos" },
            new() { Id = "013", ParentId = "001", Text = "Teusaquillo" },
            new() { Id = "014", ParentId = "001", Text = "Los Mártires" },
            new() { Id = "015", ParentId = "001", Text = "Antonio Nariño" },
            new() { Id = "016", ParentId = "001", Text = "Puente Aranda" },
            new() { Id = "017", ParentId = "001", Text = "La candelaria" },
            new() { Id = "018", ParentId = "001", Text = "Rafael Uribe Uribe" },
            new() { Id = "019", ParentId = "001", Text = "Ciudad Bolívar" },
            new() { Id = "020", ParentId = "001", Text = "Sumapaz" },
        };

        public static List<Role> Roles => new()
        {
            new() { Id = "Client", Text = "Cliente" },
            new() { Id = "Professional", Text = "Profesional de salud" },
            new() { Id = "255", Text = "Administrador" }
        };
    }
}
