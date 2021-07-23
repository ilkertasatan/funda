using System.Collections.Generic;

namespace Funda.Assignment.Infrastructure.PropertyServices.FundaPartnerApi
{
    public class AanbodServiceResponse
    {
        public IList<Object> Objects { get; set; }
        
        public class Object
        {
            public int GlobalId { get; set; }

            public bool IsVerhuurd { get; set; }

            public bool IsVerkocht { get; set; }

            public bool IsVerkochtOfVerhuurd { get; set; }

            public int KoopprijsTot { get; set; }

            public string Adres { get; set; }
        
            public string Postcode { get; set; }
        
            public int MakelaarId { get; set; }

            public string MakelaarNaam { get; set; }
        }
    }
}