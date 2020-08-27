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
    [RoutePrefix("Api/GaugeTypeMaster")]
    //[EnableCors(origins: "http://039fbf60.ngrok.io/", headers: "*", methods: "*")]

    public class GaugeTypeMasterController : ApiController
    {

        CalibrationDBEntities2 objEntity = new CalibrationDBEntities2();
       


        [HttpGet]
        [Route("GetID")]
        public int getLatestID()
        {
            //var objEntity = new CalibrationDBEntities2();
            
                  var lastid = objEntity.GaugeTypeMasters.OrderByDescending(x => x.ID).FirstOrDefault();
            if (lastid != null)
            {
                return lastid.ID+1;
            }else
            {
                return 1;
            }

        }
        [HttpGet]
        [Route("GetAllGaugeTypes")]
        public IQueryable<GaugeTypeMaster> getAllGaugeTypes()
        {

            try
            {
                return objEntity.GaugeTypeMasters;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        [HttpPost]
        [Route("InsertGaugeTypeDetails")]
        public IHttpActionResult insertGaugeTypeMaster(GaugeTypeMaster gaugeTypeMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                objEntity.GaugeTypeMasters.Add(gaugeTypeMaster);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(gaugeTypeMaster);
        }

        [HttpPut]
        [Route("UpdateGaugeType")]
        public IHttpActionResult UpdateGaugeTypeMaster(GaugeTypeMaster gaugeTypeMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var objGM = new GaugeTypeMaster();
                objGM = objEntity.GaugeTypeMasters.Find(gaugeTypeMaster.ID);

                if (objGM != null)
                {
                    objGM.ID = gaugeTypeMaster.ID;
                    objGM.GaugeType = gaugeTypeMaster.GaugeType;

                }
                int i = this.objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(gaugeTypeMaster);

        }

        [HttpDelete]
        [Route("DeleteGaugeType")]
        public IHttpActionResult deleteGaugeType(int id)
        {
            GaugeTypeMaster master = objEntity.GaugeTypeMasters.Find(id);

            if (master == null)
                return NotFound();
            objEntity.GaugeTypeMasters.Remove(master);
            objEntity.SaveChanges();
            return Ok(master);
        }

    }
}
