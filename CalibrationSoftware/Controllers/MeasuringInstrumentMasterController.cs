using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using CalibrationSoftware.Models;
namespace CalibrationSoftware.Controllers
{
    [RoutePrefix("Api/MeasuringInstrumentMaster")]
    //[EnableCors(origins: "http://039fbf60.ngrok.io/", headers: "*", methods: "*")]

    public class MeasuringInstrumentMasterController : ApiController
    {
        static string fileName = "";
        static string fileName1 = "";
        static string fileName2 = "";
        static int lastInsertedID = 0;
        CalibrationDBEntities9 objEntity = new CalibrationDBEntities9();




        [HttpGet]
        [Route("GetID")]
        public int getLatestID()
        {
            //var objEntity = new CalibrationDBEntities2();

            var lastid = objEntity.MeasuringInstrumentMasters.OrderByDescending(x => x.ID).FirstOrDefault();
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
        [Route("GetAllMIs")]
        public IQueryable<MeasuringInstrumentMaster> getMIs()
        {
            try
            {
                return objEntity.MeasuringInstrumentMasters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("InsertMIDetails")]
        public IHttpActionResult PostMIDetails(MeasuringInstrumentMaster measuringInstrumentMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if(measuringInstrumentMaster.InspectionCalibrationReport != null)
                {
                    fileName = measuringInstrumentMaster.InspectionCalibrationReport.Replace("C:\\fakepath\\", "");
                    fileName = fileName.Replace(" ", "-");
                    fileName = fileName.Replace(Path.GetExtension(measuringInstrumentMaster.InspectionCalibrationReport), "");
                    fileName = fileName + DateTime.Now + Path.GetExtension(measuringInstrumentMaster.InspectionCalibrationReport);
                    fileName = fileName.Replace(" ", "-");
                    fileName = fileName.Replace(":", "-");
                    measuringInstrumentMaster.InspectionCalibrationReport = fileName;

                }


                if (measuringInstrumentMaster.DrawingOrOtherDocuments != null)
                {
                    fileName1 = measuringInstrumentMaster.DrawingOrOtherDocuments.Replace("C:\\fakepath\\", "");
                    fileName1 = fileName1.Replace(" ", "-");
                    fileName1 = fileName1.Replace(Path.GetExtension(measuringInstrumentMaster.DrawingOrOtherDocuments), "");
                    fileName1 = fileName1 + DateTime.Now + Path.GetExtension(measuringInstrumentMaster.DrawingOrOtherDocuments);
                    fileName1 = fileName1.Replace(" ", "-");
                    fileName1 = fileName1.Replace(":", "-");

                    measuringInstrumentMaster.DrawingOrOtherDocuments = fileName1;

                }


                if (measuringInstrumentMaster.ManualAttachment != null)
                {

                    fileName2 = measuringInstrumentMaster.ManualAttachment.Replace("C:\\fakepath\\", "");
                    fileName2 = fileName2.Replace(" ", "-");
                    fileName2 = fileName2.Replace(Path.GetExtension(measuringInstrumentMaster.ManualAttachment), "");
                    fileName2 = fileName2 + DateTime.Now + Path.GetExtension(measuringInstrumentMaster.ManualAttachment);
                    fileName2 = fileName2.Replace(" ", "-");
                    fileName2 = fileName2.Replace(":", "-");

                    measuringInstrumentMaster.ManualAttachment = fileName2;

                }




                objEntity.MeasuringInstrumentMasters.Add(measuringInstrumentMaster);
                objEntity.SaveChanges();
                lastInsertedID = measuringInstrumentMaster.ID;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return Ok(measuringInstrumentMaster);
        }

        [HttpPut]
        [Route("UpdateMI")]
        public IHttpActionResult UpdateMeasuringInstrumentMaster(MeasuringInstrumentMaster measuringInstrumentMaster, bool isFileChanged)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                MeasuringInstrumentMaster objGM = new MeasuringInstrumentMaster();
                objGM = objEntity.MeasuringInstrumentMasters.Find(measuringInstrumentMaster.ID);

                if (objGM != null)
                {
                    //objGM = measuringInstrumentMaster;
                    objGM.DateOfPurchase = measuringInstrumentMaster.DateOfPurchase;
                    objGM.TypeOfInstrument = measuringInstrumentMaster.TypeOfInstrument;
                    objGM.Specifications = measuringInstrumentMaster.Specifications;
                    objGM.GaugeManufacturerID = measuringInstrumentMaster.GaugeManufacturerID;
                    objGM.GaugeInternalID = measuringInstrumentMaster.GaugeInternalID;
                    objGM.ManufacturerName = measuringInstrumentMaster.ManufacturerName;
                    objGM.SupplierName = measuringInstrumentMaster.SupplierName;
                    objGM.Price = measuringInstrumentMaster.Price;
                    
                    if(measuringInstrumentMaster.InspectionCalibrationReport != null)
                    objGM.InspectionCalibrationReport = measuringInstrumentMaster.InspectionCalibrationReport;
                    
                    objGM.GaugeStatus = measuringInstrumentMaster.GaugeStatus;
                    objGM.CalibrationFrequency = measuringInstrumentMaster.CalibrationFrequency;

                    if(measuringInstrumentMaster.DrawingOrOtherDocuments!= null)
                    objGM.DrawingOrOtherDocuments = measuringInstrumentMaster.DrawingOrOtherDocuments;

                    if(measuringInstrumentMaster.ManualAttachment != null)
                    objGM.ManualAttachment = measuringInstrumentMaster.ManualAttachment;





                    if (isFileChanged && measuringInstrumentMaster.InspectionCalibrationReport != null)
                    {
                        fileName = measuringInstrumentMaster.InspectionCalibrationReport.Replace("C:\\fakepath\\", "");
                        fileName = fileName.Replace(" ", "-");
                        fileName = fileName.Replace(Path.GetExtension(measuringInstrumentMaster.InspectionCalibrationReport), "");

                        fileName = fileName + DateTime.Now + Path.GetExtension(measuringInstrumentMaster.InspectionCalibrationReport);
                        fileName = fileName.Replace(" ", "-");
                        fileName = fileName.Replace(":", "-");

                        objGM.InspectionCalibrationReport = fileName;
                    }



                    if (isFileChanged && measuringInstrumentMaster.DrawingOrOtherDocuments != null)
                    {
                        fileName1 = measuringInstrumentMaster.DrawingOrOtherDocuments.Replace("C:\\fakepath\\", "");
                        fileName1 = fileName1.Replace(" ", "-");
                        fileName1 = fileName1.Replace(Path.GetExtension(measuringInstrumentMaster.DrawingOrOtherDocuments), "");

                        fileName1 = fileName1 + DateTime.Now + Path.GetExtension(measuringInstrumentMaster.DrawingOrOtherDocuments);
                        fileName1 = fileName1.Replace(" ", "-");
                        fileName1 = fileName1.Replace(":", "-");

                        objGM.DrawingOrOtherDocuments = fileName1;
                    }

                   

                    if (isFileChanged && measuringInstrumentMaster.ManualAttachment != null)
                    {
                        fileName2 = measuringInstrumentMaster.ManualAttachment.Replace("C:\\fakepath\\", "");
                        fileName2 = fileName.Replace(" ", "-");
                        fileName2 = fileName.Replace(Path.GetExtension(measuringInstrumentMaster.ManualAttachment), "");

                        fileName2 = fileName + DateTime.Now + Path.GetExtension(measuringInstrumentMaster.ManualAttachment);
                        fileName2 = fileName.Replace(" ", "-");
                        fileName2 = fileName.Replace(":", "-");

                        objGM.ManualAttachment = fileName2;
                    }


                }
                else
                {
                    return NotFound();
                }
                int i = this.objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(measuringInstrumentMaster);

        }


        [HttpPost]
        [Route("PostFile")]
        public Boolean UploadFile()
        {
            //string fileName = null;
            //var httpRequest = HttpContext.Current.Request;
            var httpRequest = HttpContext.Current.Request;
            // var postedFile = httpRequest.Files["fileKey"];
            var i = 0;
            foreach (string file in httpRequest.Files)
            {
               // var postedFile = httpRequest.Files[0];
                var postedFile = httpRequest.Files[file];
                bool isdone1 = false;
                bool isdone2 = false;
                bool isdone3 = false;
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


                        if((fileName != null) && (fileName != "") && !isdone1)
                        {
                            postedFile.SaveAs(Path.Combine(filePath, fileName));
                            fileName = "";
                            i = 1;
                            isdone1 = true;
                            continue;
                        }
                        if ((fileName1 != null) && (fileName1 != "") && !isdone2)
                        {
                            postedFile.SaveAs(Path.Combine(filePath, fileName1));
                            fileName1 = "";
                            isdone2 = true;
                            i = 2;
                            continue;
                        }
                        if ((fileName2 != null) && (fileName2 != "") && !isdone3)
                        {
                            postedFile.SaveAs(Path.Combine(filePath, fileName2));
                            fileName2 = "";
                            isdone3 = true;
                            i = 3;
                            continue;

                        }
                       

                    }
                    catch (Exception ex)
                    {
                        MeasuringInstrumentMaster master = objEntity.MeasuringInstrumentMasters.Find(lastInsertedID);
                        if (master == null)
                        {

                        }
                        else
                        {
                            objEntity.MeasuringInstrumentMasters.Remove(master);
                            objEntity.SaveChanges();
                            lastInsertedID = 0;

                        }

                        throw ex;

                    }

                }
            }
            return true;
            //return false;
        }


        [HttpDelete]
        [Route("DeleteMI")]
        public IHttpActionResult deleteMI(int id)
        {
            MeasuringInstrumentMaster master = objEntity.MeasuringInstrumentMasters.Find(id);
            if (master == null)
                return NotFound();
            objEntity.MeasuringInstrumentMasters.Remove(master);
            objEntity.SaveChanges();

            return Ok(master);
        }

        [HttpGet]
        [Route("GetFile")]
        public HttpResponseMessage getFile(int id,string fieldname)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                MeasuringInstrumentMaster objGM = new MeasuringInstrumentMaster();
                objGM = objEntity.MeasuringInstrumentMasters.Find(id);

                if (objGM == null)
                {
                    responseMessage.StatusCode = HttpStatusCode.NotFound;
                    responseMessage.ReasonPhrase = "Record Not Found";

                    throw new HttpResponseException(responseMessage);
                }

                string filePath = "";
                string filename = "";
                if(fieldname == "DrawingOrOtherDocuments")
                {
                    filePath = HttpContext.Current.Server.MapPath("../../Drawings/" + objGM.DrawingOrOtherDocuments);
                    filename = objGM.DrawingOrOtherDocuments;
                }
                else if (fieldname == "InspectionCalibrationReport")
                {
                 filePath = HttpContext.Current.Server.MapPath("../../Drawings/" + objGM.InspectionCalibrationReport);
                    filename = objGM.InspectionCalibrationReport;
                }
                else if(fieldname == "ManualAttachment")
                {
                    filePath = HttpContext.Current.Server.MapPath("../../Drawings/" + objGM.ManualAttachment);
                    filename = objGM.ManualAttachment;
                }
                //string filePath = HttpContext.Current.Server.MapPath("../../Drawings/" + objGM.MIDrawing);

                if (!File.Exists(filePath))
                {
                    //Throw 404 (Not Found) exception if File not found.
                    responseMessage.StatusCode = HttpStatusCode.NotFound;
                    responseMessage.ReasonPhrase = string.Format("File not found: {0} .", filename);
                    throw new HttpResponseException(responseMessage);
                }

                byte[] bytes = File.ReadAllBytes(filePath);

                responseMessage.Content = new ByteArrayContent(bytes);
                responseMessage.Content.Headers.ContentLength = bytes.LongLength;
                responseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

                responseMessage.Content.Headers.ContentDisposition.FileName = fileName;
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(filename));

                return responseMessage;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
