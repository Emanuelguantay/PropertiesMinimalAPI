using PropertiesMinimalAPI.Models;

namespace PropertiesMinimalAPI
{
    public class Data
    {
        public static class DataProperties
        {
            public static List<Properties> Properties = new List<Properties>
            {
                new Properties{Id = 1, Name = "Casa las palmas 1", Description="Descripción test 1", IsActive = true, CreatedAt = DateTime.Now.AddDays(-10), Location = "Test1"},
                new Properties{Id = 2, Name = "Casa las palmas 2", Description="Descripción test 1", IsActive = true, CreatedAt = DateTime.Now.AddDays(-9), Location = "Test2"},
                new Properties{Id = 3, Name = "Casa las palmas 3", Description="Descripción test 1", IsActive = true, CreatedAt = DateTime.Now.AddDays(-6), Location = "Test3"},
                new Properties{Id = 4, Name = "Casa las palmas 4", Description="Descripción test 1", IsActive = true, CreatedAt = DateTime.Now.AddDays(-1), Location = "Test4"},
                new Properties{Id = 5, Name = "Casa las palmas 5", Description="Descripción test 1", IsActive = true, CreatedAt = DateTime.Now, Location = "Test5"},
                new Properties{Id = 6, Name = "Casa las palmas 6", Description="Descripción test 1", IsActive = true, CreatedAt = DateTime.Now.AddDays(-20), Location = "Test6"},
            };
        }
    }
}
