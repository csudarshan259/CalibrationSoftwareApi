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
    [RoutePrefix("Api/LocationMaster")]
    //[EnableCors(origins: "http://039fbf60.ngrok.io/", headers: "*", methods: "*")]

    public class LocationMasterController : ApiController
    {
        CalibrationDBEntities6 objEntity = new CalibrationDBEntities6();



        [HttpGet]
        [Route("GetID")]
        public int getLatestID()
        {
            //var objEntity = new CalibrationDBEntities2();

            var lastid = objEntity.LocationMasters.OrderByDescending(x => x.ID).FirstOrDefault();
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
        [Route("GetAllLocations")]
        public IQueryable<LocationMaster> GetLocations()
        {

            try
            {
                return objEntity.LocationMasters;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [Route("InsertLocation")]
        public IHttpActionResult AddLocation(LocationMaster location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                objEntity.LocationMasters.Add(location);
                objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(location);
        }

        [HttpPut]
        [Route("UpdateLocation")]
        public IHttpActionResult UpdateLocation(LocationMaster location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //  var objGM = new GaugeTypeMaster();
                var objGM = new LocationMaster();
                objGM = objEntity.LocationMasters.Find(location.ID);

                if (objGM != null)
                {
                    objGM.ID = location.ID;
                    objGM.LocationName = location.LocationName;

                }
                int i = this.objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(location);

        }

        [HttpDelete]
        [Route("DeleteLocation")]
        public IHttpActionResult DeleteLocation(int id)
        {
            LocationMaster master = objEntity.LocationMasters.Find(id);

            if (master == null)
                return NotFound();
            objEntity.LocationMasters.Remove(master);
            objEntity.SaveChanges();
            return Ok(master);
        }


    }
}
