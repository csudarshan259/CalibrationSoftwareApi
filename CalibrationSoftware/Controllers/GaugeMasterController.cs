using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CalibrationSoftware.Models;
using System.Web;
using System.IO;
using System.Net.Http.Headers;
using System.Web.Http.Cors;

namespace CalibrationSoftware.Controllers
{
    [RoutePrefix("Api/GaugeMaster")]
    //[EnableCors(origins: "http://039fbf60.ngrok.io/", headers: "*", methods: "*")]

    public class GaugeMasterController : ApiController
    {
        static string fileName = "";
        static int lastInsertedID = 0;
        CalibrationDBEntities1 objEntity = new CalibrationDBEntities1();
      
       
        [HttpGet]
        [Route("AllGauges")]
        public IQueryable<GaugeMaster> getGauges()
        {
            try
            {
                return objEntity.GaugeMasters;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("InsertGaugeDetails")]
        public IHttpActionResult PostGaugeDetails(GaugeMaster gaugeMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                fileName = gaugeMaster.GaugeDrawing.Replace("C:\\fakepath\\", "");
                fileName = fileName.Replace(" ", "-");
                fileName = fileName.Replace(Path.GetExtension(gaugeMaster.GaugeDrawing), "");
               
                fileName = fileName + DateTime.Now + Path.GetExtension(gaugeMaster.GaugeDrawing);
                fileName = fileName.Replace(" ", "-");
                fileName = fileName.Replace(":", "-");

                gaugeMaster.GaugeDrawing = fileName;
                objEntity.GaugeMasters.Add(gaugeMaster);
                objEntity.SaveChanges();
                lastInsertedID = gaugeMaster.ID;
            }catch(Exception ex)
            {
                throw ex;

            }

            return Ok(gaugeMaster);
        }

        [HttpPut]
        [Route("UpdateGauge")]
        public IHttpActionResult UpdateGaugeMaster(GaugeMaster gaugeMaster,bool isFileChanged)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                GaugeMaster objGM = new GaugeMaster();
                objGM = objEntity.GaugeMasters.Find(gaugeMaster.ID);

                if(objGM != null)
                {
                    //objGM = gaugeMaster;
                    objGM.GaugeName = gaugeMaster.GaugeName;
                    objGM.GaugeType = gaugeMaster.GaugeType;
                    objGM.GaugeItemCode = gaugeMaster.GaugeItemCode;
                    objGM.GaugeDimAndTolerance = gaugeMaster.GaugeDimAndTolerance;
                    objGM.GaugeDrawing = gaugeMaster.GaugeDrawing;
                    objGM.GaugeMaterial = gaugeMaster.GaugeMaterial;
                    objGM.ComponentDetails = gaugeMaster.ComponentDetails;
                    objGM.CalibrationScheduling = gaugeMaster.CalibrationScheduling;

                    if (isFileChanged)
                    {
                        fileName = gaugeMaster.GaugeDrawing.Replace("C:\\fakepath\\", "");
                        fileName = fileName.Replace(" ", "-");
                        fileName = fileName.Replace(Path.GetExtension(gaugeMaster.GaugeDrawing), "");

                        fileName = fileName + DateTime.Now + Path.GetExtension(gaugeMaster.GaugeDrawing);
                        fileName = fileName.Replace(" ", "-");
                        fileName = fileName.Replace(":", "-");

                        objGM.GaugeDrawing = fileName;
                    }
               

                }
                int i = this.objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(gaugeMaster);

        }


        [HttpPost]
        [Route("PostFile")]
        public Boolean UploadFile()
        {
            //string fileName = null;
            //var httpRequest = HttpContext.Current.Request;
            var httpRequest = HttpContext.Current.Request;
            // var postedFile = httpRequest.Files["fileKey"];
            var postedFile = httpRequest.Files[0];
            
            if (postedFile != null)
            {
                //fileName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).ToArray()).Replace(" ", "-");
                //fileName = fileName + DateTime.Now + Path.GetExtension(postedFile.FileName);
                try
                {
                   // var filePath = HttpContext.Current.Server.MapPath("D:/Web/Backend/CalibrationSoftware/Drawings/" + fileName);
                    //var filePath = HttpContext.Current.Server.MapPath("D:/Web/Backend/CalibrationSoftware/Drawings/" + fileName);
                   
                    //var filePath = HttpContext.Current.Server.MapPath("../Drawings/" + fileName);
                    
                    var filePath = HttpContext.Current.Server.MapPath("../../Drawings/");


                
                    postedFile.SaveAs(Path.Combine(filePath,fileName));
                    fileName = "";
                    return true;

                }
                catch(Exception ex)
                {
                    GaugeMaster master = objEntity.GaugeMasters.Find(lastInsertedID);
                    if (master == null)
                    {

                    }
                    else
                    {
                        objEntity.GaugeMasters.Remove(master);
                        objEntity.SaveChanges();
                        lastInsertedID = 0;
                    }
                  
                    throw ex;
                    
                }
                
            }
            return false;
        }


        [HttpDelete]
        [Route("DeleteGauge")]
        public IHttpActionResult deleteGauge(int id)
        {
            GaugeMaster master = objEntity.GaugeMasters.Find(id);
            if (master == null)
                return NotFound();
            objEntity.GaugeMasters.Remove(master);
            objEntity.SaveChanges();

            return Ok(master);
        }

        [HttpGet]
        [Route("GetFile")]
        public HttpResponseMessage getFile(int id)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                GaugeMaster objGM = new GaugeMaster();
                objGM = objEntity.GaugeMasters.Find(id);

                if(objGM == null)
                {
                    responseMessage.StatusCode = HttpStatusCode.NotFound;
                    responseMessage.ReasonPhrase = "Record Not Found";

                    throw new HttpResponseException(responseMessage);
                }

                string filePath =  HttpContext.Current.Server.MapPath("../../Drawings/"+objGM.GaugeDrawing);

                if (!File.Exists(filePath))
                {
                    //Throw 404 (Not Found) exception if File not found.
                    responseMessage.StatusCode = HttpStatusCode.NotFound;
                    responseMessage.ReasonPhrase = string.Format("File not found: {0} .", objGM.GaugeDrawing);
                    throw new HttpResponseException(responseMessage);
                }

                byte[] bytes = File.ReadAllBytes(filePath);

                responseMessage.Content = new ByteArrayContent(bytes);
                responseMessage.Content.Headers.ContentLength = bytes.LongLength;
                responseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                
                responseMessage.Content.Headers.ContentDisposition.FileName = objGM.GaugeDrawing;
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(objGM.GaugeDrawing));

                return responseMessage;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
