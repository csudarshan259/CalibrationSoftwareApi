using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CalibrationSoftware.Models;
namespace CalibrationSoftware.Controllers
{
    [RoutePrefix("Api/CustomerMaster")]

    public class CustomerMasterController : ApiController
    {

        CalibrationDBEntities4 objEntity = new CalibrationDBEntities4();



        [HttpGet]
        [Route("GetID")]
        public int GetLatestID()
        {
            //var objEntity = new CalibrationDBEntities2();

            var lastid = objEntity.CustomerMasters.OrderByDescending(x => x.ID).FirstOrDefault();
            if (lastid != null)
            {
                return lastid.ID + 1;
            }
            else
            {
                return 1;
            }

        }
        [HttpGet]
        [Route("GetAllCustomers")]
        public IQueryable<CustomerMaster> GetCustomers()
        {

            try
            {
                return objEntity.CustomerMasters;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [Route("InsertCustomer")]
        public IHttpActionResult InsertCustomer(CustomerMaster customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                return Ok(customer);

                objEntity.CustomerMasters.Add(customer);
                objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(customer);
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public IHttpActionResult UpdateCustomer(CustomerMaster customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //  var objGM = new GaugeTypeMaster();
                var objGM = new CustomerMaster();
                objGM = objEntity.CustomerMasters.Find(customer.ID);

                if (objGM != null)
                {
                    objGM.ID = customer.ID;
                    objGM.CustomerName = customer.CustomerName;
                    objGM.CustomerAddress = customer.CustomerAddress;
                    objGM.CustomerEmail = customer.CustomerEmail;
                    objGM.CustomerContactPersonName = customer.CustomerContactPersonName;
                    objGM.CustomerContactNumber = customer.CustomerContactNumber;

                }
                int i = this.objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(customer);

        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            CustomerMaster master = objEntity.CustomerMasters.Find(id);

            if (master == null)
                return NotFound();
            objEntity.CustomerMasters.Remove(master);
            objEntity.SaveChanges();
            return Ok(master);
        }
    }
}
