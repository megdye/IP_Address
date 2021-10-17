using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IPGeolocation;

namespace IP_Address
{
    public class Program
    {
        public GeolocationParams geoParams = new GeolocationParams();

        static void Main(string[] args)
        {
            var test = new Program();
            test.Get_TwoLetterName_From_IP(test.geoParams.GetIPAddress()); // GB
            test.Get_TwoLetterName_From_IP("23.221.76.66"); // ID
            test.Get_TwoLetterName_From_IP("1.1.1.999999991"); // Throws Exception
            test.Get_TwoLetterName_From_IP("1.1.1.1"); // AU
        }

        public class CountryList
        {
            private CultureTypes _AllCultures;
            public CountryList(bool AllCultures)
            {
                this._AllCultures = (AllCultures) ? CultureTypes.AllCultures : CultureTypes.SpecificCultures;
                this.Countries = GetAllCountries(this._AllCultures);
            }

            public List<CountryInfo> Countries { get; set; }

            public List<CountryInfo> GetCountryInfoByName(string CountryName, bool NativeName)
            {
                return (NativeName) ? this.Countries.Where(info => info.Region.NativeName == CountryName).ToList()
                                    : this.Countries.Where(info => info.Region.EnglishName == CountryName).ToList();
            }

            public string GetTwoLettersName(string CountryName, bool NativeName)
            {
                CountryInfo country = (NativeName) ? this.Countries.Where(info => info.Region.NativeName == CountryName)
                                                                   .FirstOrDefault()
                                                   : this.Countries.Where(info => info.Region.EnglishName == CountryName)
                                                                   .FirstOrDefault();

                return (country != null) ? country.Region.TwoLetterISORegionName : string.Empty;
            }
            private static List<CountryInfo> GetAllCountries(CultureTypes cultureTypes)
            {
                List<CountryInfo> Countries = new List<CountryInfo>();

                foreach (CultureInfo culture in CultureInfo.GetCultures(cultureTypes))
                {
                    if (culture.LCID != 127)
                        Countries.Add(new CountryInfo()
                        {
                            Culture = culture,
                            Region = new RegionInfo(culture.TextInfo.CultureName)
                        });
                }
                return Countries;
            }
        }

        public class CountryInfo
        {
            public CultureInfo Culture { get; set; }
            public RegionInfo Region { get; set; }
        }

        public string Get_TwoLetterName_From_IP(string IP_Add)
        {
            IPGeolocationAPI api = new IPGeolocationAPI("fab4901e71294eb39ec5e3bc24227440");

            geoParams.SetIPAddress(IP_Add);
            geoParams.SetFields("geo,time_zone,currency");
            try
            {
                Geolocation geolocation = api.GetGeolocation(geoParams);
                if (geolocation.GetStatus() == 200)
                {
                    CountryList Countries = new CountryList(false);
                    string TwoLettersName = Countries.GetTwoLettersName(geolocation.GetCountryName(), true);
                    Console.WriteLine(TwoLettersName);
                    return TwoLettersName;
                }
                else
                {
                    Console.WriteLine(geolocation.GetMessage());
                    return geolocation.GetMessage();
                }
            }
            catch (System.Net.WebException e)
            {
                String ex = String.Format("There has been an issue - please read below:\n{0}", e);
                Console.WriteLine(ex);
                return ex;
            }
        }
    }
}