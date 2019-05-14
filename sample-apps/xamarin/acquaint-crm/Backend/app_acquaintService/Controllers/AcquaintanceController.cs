using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Acquaint.Service.DataObjects;
using Acquaint.Service.Models;

namespace Acquaint.Service.Controllers
{
    public class AcquaintanceController : TableController<Acquaintance>
    {
        private AcquaintDbContext _DataContext;
        
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _DataContext = new AcquaintDbContext();
            DomainManager = new EntityDomainManager<Acquaintance>(_DataContext, Request, enableSoftDelete: true);
        }

        // GET tables/Acquaintance/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public IQueryable<Acquaintance> GetAllAcquaintances()
        {
            return Query();
        }

        // GET tables/Acquaintance/3BF73C70-1DEE-47DC-8150-3A9BF368F01E
        public SingleResult<Acquaintance> GetAcquaintance(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Acquaintance/3BF73C70-1DEE-47DC-8150-3A9BF368F01E
        public async Task<Acquaintance> PatchAcquaintance(string id, Delta<Acquaintance> patch)
        {
            return await UpdateAsync(id, patch);
        }

        // POST tables/Acquaintance
        public async Task<IHttpActionResult> PostAcquaintance(Acquaintance item)
        {
            Acquaintance current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Acquaintance/3BF73C70-1DEE-47DC-8150-3A9BF368F01E
        public async Task DeleteAcquaintance(string id)
        {
            await DeleteAsync(id);
        }
    }
}