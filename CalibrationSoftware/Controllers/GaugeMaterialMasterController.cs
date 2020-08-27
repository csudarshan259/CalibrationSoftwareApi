using CalibrationSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CalibrationSoftware.Controllers
{

    [RoutePrefix("Api/GaugeMaterialMaster")]
    //[EnableCors(origins: "http://039fbf60.ngrok.io/", headers: "*", methods: "*")]

    public class GaugeMaterialMasterController : ApiController
    {
       

            GaugeMaterialMasterEntities objEntity = new GaugeMaterialMasterEntities();

            [HttpGet]
            [Route("GetID")]
            public int getLatestID()
            {
                //var objEntity = new CalibrationDBEntities2();

                var lastid = objEntity.GaugeMaterialMasters.OrderByDescending(x => x.ID).FirstOrDefault();
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
            [Route("GetAllGaugeMaterial")]
            public IQueryable<GaugeMaterialMaster> getAllGaugeMaterials()
            {

                try
                {
                    return objEntity.GaugeMaterialMasters;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            [HttpPost]
            [Route("InsertGaugeMaterialDetails")]
            public IHttpActionResult insertGaugeMaterialMaster(GaugeMaterialMaster gaugeMaterialMaster)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {


                    objEntity.GaugeMaterialMasters.Add(gaugeMaterialMaster);
                    objEntity.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return Ok(gaugeMaterialMaster);
            }


            [HttpPut]
            [Route("UpdateGaugeMaterial")]
            public IHttpActionResult UpdateGaugeMaterialMaster(GaugeMaterialMaster gaugeMaterialMaster)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    var objGM = new GaugeMaterialMaster();
                    objGM = objEntity.GaugeMaterialMasters.Find(gaugeMaterialMaster.ID);

                    if (objGM != null)
                    {
                        //objGM = gaugeMaterialMaster;
                    objGM.ID = gaugeMaterialMaster.ID;
                    objGM.MaterialName = gaugeMaterialMaster.MaterialName;

                    }
                    int i = this.objEntity.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return Ok(gaugeMaterialMaster);

            }

            [HttpDelete]
            [Route("DeleteGaugeMaterial")]
            public IHttpActionResult DeleteGaugeMaterial(int id)
        {
            GaugeMaterialMaster master = objEntity.GaugeMaterialMasters.Find(id);

            if (master == null)
                return NotFound();
            objEntity.GaugeMaterialMasters.Remove(master);
            objEntity.SaveChanges();
            return Ok(master);
        }

        }

    
}
