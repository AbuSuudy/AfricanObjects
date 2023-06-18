using AfricanObjects.Interface;
using AfricanObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AfricanObjects.Service
{
    public class MuseumCollection : IMuseumCollection
    {
        private readonly IEnumerable<IMuseumService> museumServices;
        private readonly IMetMuseumService metMuseumServivce;


        public MuseumCollection(IEnumerable<IMuseumService> museumServices, IMetMuseumService metMuseumServivce)
        {
            this.museumServices = museumServices;
            this.metMuseumServivce = metMuseumServivce;

        }
        public async Task<MuseumObject> GetMuseumObjectFromCollection()
        {
            MuseumObject museumObject = null;
            try
            {
                int rnd = RandomNumberGenerator.GetInt32(0,3);
                bool harvardSkip ;

                switch (rnd)
                {
                    case 0:

                        do
                        {
                            //Large volume of Sherd and Folio in Harvard Archieve so will skip them intermittently for more diversity of posts 
                            harvardSkip = Convert.ToBoolean(RandomNumberGenerator.GetInt32(0, 2));

                            //Harvard
                            museumObject = await museumServices.ElementAt(0).GetMuseumObject();


                        } while (museumObject == null || harvardSkip && (museumObject?.Title == "Sherd" || museumObject.Title.Contains("Folio")));

                        break;

                    case 1:

                        do
                        {
                            //Smithsonian
                            museumObject = await museumServices.ElementAt(1).GetMuseumObject();

                        } while (museumObject == null );

                        break;

                    default:

                        do
                        {
                            //The Met
                            museumObject = await metMuseumServivce.GetMuseumObject();

                        } while (museumObject == null);

                        break;

                }

            }
            catch (Exception ex)
            {
                return null;

            }

            return museumObject; 

        }

    }
}
