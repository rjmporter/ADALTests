using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens;
using adalAuthContext = Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext;
namespace ConsoleApplication1
{
   class Program
   {
      public const string discoveryApi = "https://api.office.com/discovery/v1.0/me/services";
      static void Main(string[] args)
      {
         ClientCredential clientCreds = new ClientCredential("73b9497a-9afb-4ad8-b0d8-fe7786c620d8", "BxAnEHi7r1yeUYaSxkm4YqJq+ZSyxmWhaFxeYJKPEfQ=");
         UserAssertion userAssertion = new UserAssertion("eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSIsImtpZCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLndpbmRvd3MubmV0IiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvNzdkMzA5NjMtNzg3Mi00ZDExLTkzNzQtMmMyYjJhMTJhZDY1LyIsImlhdCI6MTQ1MjczODgzMSwibmJmIjoxNDUyNzM4ODMxLCJleHAiOjE0NTI3NDI3MzEsImFjciI6IjEiLCJhbXIiOlsicHdkIl0sImFwcGlkIjoiNzNiOTQ5N2EtOWFmYi00YWQ4LWIwZDgtZmU3Nzg2YzYyMGQ4IiwiYXBwaWRhY3IiOiIxIiwiZmFtaWx5X25hbWUiOiJQb3J0ZXIiLCJnaXZlbl9uYW1lIjoiUm9zcyIsImlwYWRkciI6Ijc1LjEyOS4xNzkuMTEiLCJuYW1lIjoiUG9ydGVyLCBSb3NzIiwib2lkIjoiY2UzMDIzNmUtZWRhNC00N2FmLTk1NjgtMzFhOWRhZmZkNGZlIiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTY5NzM3OTE5LTE2ODk1OTIzMjMtMTQxNTcxMzcyMi02MzIyIiwicHVpZCI6IjEwMDMzRkZGODVDREYxQTMiLCJzY3AiOiJGaWxlcy5SZWFkV3JpdGUgRmlsZXMuUmVhZFdyaXRlLkFwcEZvbGRlciBGaWxlcy5SZWFkV3JpdGUuU2VsZWN0ZWQgUGVvcGxlLlJlYWQgVXNlci5SZWFkIiwic3ViIjoiWE5RcFRPdE1TN0J6a05lMHhVSXhJd1VNcDhPR2FJVUdnRW1QUTV6MzZzNCIsInRpZCI6Ijc3ZDMwOTYzLTc4NzItNGQxMS05Mzc0LTJjMmIyYTEyYWQ2NSIsInVuaXF1ZV9uYW1lIjoici5wb3J0ZXJAdGVjaHNtaXRoLmNvbSIsInVwbiI6InIucG9ydGVyQHRlY2hzbWl0aC5jb20iLCJ2ZXIiOiIxLjAifQ.TwYHsRCsk1BMszGQ4tKNFXQGviEJ9o0070aKlXaQEYnp56coZcQp-hF6kJrzw_pSOBC4G6umzflTrH8NwkcEKdlvkGsHDc2LXu4pzU_4ndoUaPrn4LiNFf5taH-zWlPfGbgc64ok5-GB_XWWfk2EYnLBcI9-tu4qjM3MR-z5zI6aK3hWYSgqtFt7PeOYwrPfmwTFI5jdm9wTRYCnoBHSw3nFpWz_aBtlXlKTHR0EAF4-osqEu2rCk2G3siwdhZVBCIGZEVlsoQg1tmybU14f5EVf8ljVtCkGTC7okstyIkTNrp7TL6H7SlbtKzEcpvas2b310Z5uqazkYLNd-A-87g");
         adalAuthContext context = new adalAuthContext("https://login.windows.net/common/");
         Uri redirectUri = new Uri("http://client-connector/terminate");
         try
         {
            var authResults = context.AcquireToken("https://api.office.com/discovery/", clientCreds, userAssertion);
         using (var webClient = new WebClient())
         {

               webClient.Headers[HttpRequestHeader.Authorization] = authResults.CreateAuthorizationHeader();
            try
            {
               var jsonData = webClient.DownloadString(discoveryApi);
               System.IO.File.WriteAllText("../../output.json", jsonData);
               var results = JObject.Parse(jsonData);
               
                  
            }
            catch (WebException wex)
            {
               var response = wex.Response;
                  Console.WriteLine(wex.ToString());

            }
         }
         }
         catch (Microsoft.IdentityModel.Clients.ActiveDirectory.AdalServiceException adalErr) {
            var error = adalErr.ErrorCode;
            Console.WriteLine(adalErr.ToString());
         }
         catch(Exception ex)
         {
            Console.WriteLine(ex.ToString());
         }
         Console.ReadLine();
      }
   }
}
