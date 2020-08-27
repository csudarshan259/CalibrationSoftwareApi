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
    [RoutePrefix("Api/DepartmentMaster")]
    //[EnableCors(origins: "http://039fbf60.ngrok.io/", headers: "*", methods: "*")]

    public class DepartmentController : ApiController
    {
        

            CalibrationDBEntities5 objEntity = new CalibrationDBEntities5();



            [HttpGet]
            [Route("GetID")]
            public int getLatestID()
            {
                //var objEntity = new CalibrationDBEntities2();

                var lastid = objEntity.DepartmentMasters.OrderByDescending(x => x.ID).FirstOrDefault();
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
            [Route("GetAllDepartments")]
            public IQueryable<DepartmentMaster> GetDepartments()
            {

                try
                {
                    return objEntity.DepartmentMasters;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            [HttpPost]
            [Route("InsertDepartment")]
            public IHttpActionResult insertDepartment(DepartmentMaster department)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {


                    objEntity.DepartmentMasters.Add(department);
                    objEntity.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return Ok(department);
            }

            [HttpPut]
            [Route("UpdateDepartment")]
            public IHttpActionResult updateDepartment(DepartmentMaster department)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                  //  var objGM = new GaugeTypeMaster();
                var objGM = new DepartmentMaster();
                    objGM = objEntity.DepartmentMasters.Find(department.ID);

                    if (objGM != null)
                    {
                        objGM.ID = department.ID;
                        objGM.DepartmentName = department.DepartmentName;

                    }
                    int i = this.objEntity.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return Ok(department);

            }

            [HttpDelete]
            [Route("DeleteDepartment")]
            public IHttpActionResult deleteDepartment(int id)
            {
                DepartmentMaster master = objEntity.DepartmentMasters.Find(id);

                if (master == null)
                    return NotFound();
                objEntity.DepartmentMasters.Remove(master);
                objEntity.SaveChanges();
                return Ok(master);
            }

        
    }
}
