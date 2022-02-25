using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventRegistration.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string StaffsEndpoint = $"{Prefix}/staffs";
        public static readonly string RolesEndpoint = $"{Prefix}/roles"; 
        public static readonly string VenuesEndpoint = $"{Prefix}/venues";
    }
}
