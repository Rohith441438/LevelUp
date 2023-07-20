using EmployeeService.Models;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace EmployeeService.Controllers
{
    [RoutePrefix("orders")]
    public class OrdersController : ApiController
    {        
        private IEnumerable<Order> orders = new List<Order>()
        {
            new Order(){Id = 1, Name = "Chicken Biryani"},
            new Order(){Id = 2, Name = "Mutton Biryani"},
            new Order(){Id = 3, Name = "Prawns Biryani"},
            new Order(){Id = 4, Name = "Chicken 65 Biryani"},
            new Order(){Id = 5, Name = "Chicken Tikka Biryani"}
        };


        //Order of Http Action method follows this rules below
        //Compare the Order property of the route attribute.
        //Look at each URI segment in the route template.For each segment, order as follows:Literal segments.
        //Route parameters with constraints.
        //Route parameters without constraints.
        //Wildcard parameter segments with constraints.
        //Wildcard parameter segments without constraints.
        //In the case of a tie, routes are ordered by a case-insensitive ordinal string comparison (OrdinalIgnoreCase) of the route template.

        [Route("allOrders")] //literal
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, orders.ToList());
        }

        [Route("orderById/{ordId:int:range(1,5)}")] //constrained Parameters
        public HttpResponseMessage GetOrderById(int ordId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, orders.FirstOrDefault(x => x.Id == ordId));
        }

        [Route("orderByName/{ordName}")] //unconstrained Parameters
        public HttpResponseMessage GetOrderByName(string ordName)
        {
            return Request.CreateResponse(HttpStatusCode.OK, orders.FirstOrDefault(x => x.Name.ToLower() == ordName.ToLower()));
        }

        [Route("orderByIdNew", Order = 1)]
        public HttpResponseMessage GetOrder(int ordId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, orders.FirstOrDefault());
        }

        [Route("{*date:datetime}")] //wildCard
        public HttpResponseMessage GetOrderByWild(DateTime date)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Just to checking purpose");
        }
    }
}
