using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CalibrationSoftware.Models;
namespace CalibrationSoftware.Controllers
{
    [RoutePrefix("Api/PartCategoryMaster")]
    public class PartCategoryMasterController : ApiController
    {

        CalibrationDBEntities4 objEntity = new CalibrationDBEntities4();



        [HttpGet]
        [Route("GetID")]
        public int GetLatestID()
        {
            //var objEntity = new CalibrationDBEntities2();

            var lastid = objEntity.PartCategoryMasters.OrderByDescending(x => x.ID).FirstOrDefault();
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
        [Route("GetAllPartCategories")]
        public IQueryable<PartCategoryMaster> GetPartCategories()
        {

            try
            {
                return objEntity.PartCategoryMasters;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [Route("InsertPartCategory")]
        public IHttpActionResult InsertPartCategory(PartCategoryMaster partCategoryMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                objEntity.PartCategoryMasters.Add(partCategoryMaster);
                objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(partCategoryMaster);
        }

        [HttpPut]
        [Route("UpdatePartCategory")]
        public IHttpActionResult UpdatePartCategory(PartCategoryMaster partCategoryMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //  var objGM = new GaugeTypeMaster();
                var objGM = new PartCategoryMaster();
                objGM = objEntity.PartCategoryMasters.Find(partCategoryMaster.ID);

                if (objGM != null)
                {
                    objGM.ID = partCategoryMaster.ID;
                    objGM.PartDetails = partCategoryMaster.PartDetails;

                }
                int i = this.objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(partCategoryMaster);

        }

        [HttpDelete]
        [Route("DeletePartCategory")]
        public IHttpActionResult DeletePartCategory(int id)
        {
            PartCategoryMaster master = objEntity.PartCategoryMasters.Find(id);

            if (master == null)
                return NotFound();
            objEntity.PartCategoryMasters.Remove(master);
            objEntity.SaveChanges();
            return Ok(master);
        }


    }
}
