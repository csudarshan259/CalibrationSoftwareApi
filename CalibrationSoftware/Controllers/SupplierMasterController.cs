using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using CalibrationSoftware.Models;

namespace CalibrationSoftware.Controllers
{
    [RoutePrefix("Api/SupplierMaster")]
    //[EnableCors(origins: "http://039fbf60.ngrok.io/", headers: "*", methods: "*")]

    public class SupplierMasterController : ApiController
    {

        CalibrationDBEntities7 objEntity = new CalibrationDBEntities7();



        [HttpGet]
        [Route("GetID")]
        public int GetLatestID()
        {
            //var objEntity = new CalibrationDBEntities2();

            var lastid = objEntity.SupplierMasters.OrderByDescending(x => x.ID).FirstOrDefault();
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
        [Route("GetAllSuppliers")]
        public IQueryable<SupplierMaster> GetSuppliers()
        {

            try
            {
                return objEntity.SupplierMasters;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [Route("InsertSupplier")]
        public IHttpActionResult InsertSupplier(SupplierMaster supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                objEntity.SupplierMasters.Add(supplier);
                objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(supplier);
        }

        [HttpPut]
        [Route("UpdateSupplier")]
        public IHttpActionResult UpdateSupplier(SupplierMaster supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //  var objGM = new GaugeTypeMaster();
                var objGM = new SupplierMaster();
                objGM = objEntity.SupplierMasters.Find(supplier.ID);

                if (objGM != null)
                {
                    objGM.ID = supplier.ID;
                    objGM.SupplierName = supplier.SupplierName;
                    objGM.Address = supplier.Address;
                    objGM.EmailId = supplier.EmailId;
                    objGM.ContactPersonName  = supplier.ContactPersonName;
                    objGM.ContactNumber = supplier.ContactNumber;

                }
                int i = this.objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(supplier);

        }

        [HttpDelete]
        [Route("DeleteSupplier")]
        public IHttpActionResult DeleteSupplier(int id)
        {
            SupplierMaster master = objEntity.SupplierMasters.Find(id);

            if (master == null)
                return NotFound();
            objEntity.SupplierMasters.Remove(master);
            objEntity.SaveChanges();
            return Ok(master);
        }


    }
}
