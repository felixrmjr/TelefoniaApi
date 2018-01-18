using System.Collections;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TelefoniaApi.Controllers
{
    public class Website
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContatosApiController : ApiController
    {
        Website[] websites = {
            new Website { Id = 1, Name = "BirdMMO.com", Description = "Bird Flapping Game For Everybody"},
            new Website { Id = 2, Name = "SpiderMMO.com", Description = "Spider Versus Spider Death Match"},
            new Website { Id = 3, Name = "LiveAutoWheel.com", Description = "Random Number Generator"},
            new Website { Id = 4, Name = "SeanWasEre.com", Description = "A Blog of Trivial Things"}
        };

        // GET api/Websites
        [Authorize]
        public IEnumerable Get()
        {
            return websites;
        }

        // GET api/Websites/5 
        public Website Get(int id)
        {
            return websites[id];
        }
    }
}