using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using CalibrationSoftware.Models;
namespace CalibrationSoftware.Controllers
{

    [RoutePrefix("Api/ItemMaster")]
    public class ItemMasterController : ApiController
    {
        static string fileName = "";
        static string fileName1 = "";
        static string fileName2 = "";
        static int lastInsertedID = 0;
        CalibrationDBEntities4 objEntity = new CalibrationDBEntities4();




        [HttpGet]
        [Route("GetID")]
        public int getLatestID()
        {
            //var objEntity = new CalibrationDBEntities2();

            var lastid = objEntity.ItemMasters.OrderByDescending(x => x.ID).FirstOrDefault();
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
        [Route("GetAllItems")]
        public IQueryable<ItemMaster> GetItems()
        {
            try
            {
                return objEntity.ItemMasters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllSubItems")]
        public IQueryable<ItemMasterDetail> GetSubItems()
        {
            try
            {
                return objEntity.ItemMasterDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetSubItem")]
        public IQueryable<ItemMasterDetail> GetSubItem(int id)
        {
            try
            {
                return objEntity.ItemMasterDetails.Where((x) => x.ItemMasterID == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("InsertItemMaster")]
        public IHttpActionResult PostItemMaster(ItemMaster itemMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (itemMaster.DrawingUpload != null)
                {
                    fileName = itemMaster.DrawingUpload.Replace("C:\\fakepath\\", "");
                    fileName = fileName.Replace(" ", "-");
                    fileName = fileName.Replace(Path.GetExtension(itemMaster.DrawingUpload), "");
                    fileName = fileName + DateTime.Now + Path.GetExtension(itemMaster.DrawingUpload);
                    fileName = fileName.Replace(" ", "-");
                    fileName = fileName.Replace(":", "-");
                    itemMaster.DrawingUpload = fileName;

                }


                if (itemMaster.ControlPlanUpload != null)
                {
                    fileName1 = itemMaster.ControlPlanUpload.Replace("C:\\fakepath\\", "");
                    fileName1 = fileName1.Replace(" ", "-");
                    fileName1 = fileName1.Replace(Path.GetExtension(itemMaster.ControlPlanUpload), "");
                    fileName1 = fileName1 + DateTime.Now + Path.GetExtension(itemMaster.ControlPlanUpload);
                    fileName1 = fileName1.Replace(" ", "-");
                    fileName1 = fileName1.Replace(":", "-");

                    itemMaster.ControlPlanUpload = fileName1;

                }


                if (itemMaster.PPAPUpload != null)
                {

                    fileName2 = itemMaster.PPAPUpload.Replace("C:\\fakepath\\", "");
                    fileName2 = fileName2.Replace(" ", "-");
                    fileName2 = fileName2.Replace(Path.GetExtension(itemMaster.PPAPUpload), "");
                    fileName2 = fileName2 + DateTime.Now + Path.GetExtension(itemMaster.PPAPUpload);
                    fileName2 = fileName2.Replace(" ", "-");
                    fileName2 = fileName2.Replace(":", "-");

                    itemMaster.PPAPUpload = fileName2;

                }




                objEntity.ItemMasters.Add(itemMaster);
                objEntity.SaveChanges();
                lastInsertedID = itemMaster.ID;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return Ok(itemMaster);
        }

        [HttpPost]
        [Route("InsertItemSubMaster")]
        public IHttpActionResult PostItemMasterDetails(ItemMasterDetail[] details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                foreach (var detail in details)
                {
                    detail.ItemMasterID = lastInsertedID;
                    objEntity.ItemMasterDetails.Add(detail);
                    objEntity.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok("Saved");
        }

        [HttpPut]
        [Route("UpdateItemMaster")]
        public IHttpActionResult UpdateItemMaster(ItemMaster itemMaster, bool isFileChanged)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ItemMaster objGM = new ItemMaster();
                objGM = objEntity.ItemMasters.Find(itemMaster.ID);

                if (objGM != null)
                {
                    //objGM = itemMaster;
                    objGM.DrawingNo = itemMaster.DrawingNo;
                    objGM.Version = itemMaster.Version;
                    objGM.C_Index = itemMaster.C_Index;
                    objGM.PartName = itemMaster.PartName;
                    objGM.PartCode = itemMaster.PartCode;
                    objGM.PartCategory = itemMaster.PartCategory;
                    objGM.CustomerName = itemMaster.CustomerName;
                    objGM.BallooningNumber = itemMaster.BallooningNumber;

                    if (itemMaster.DrawingUpload != null)
                        objGM.DrawingUpload = itemMaster.DrawingUpload;

                    if (itemMaster.ControlPlanUpload != null)
                        objGM.ControlPlanUpload = itemMaster.ControlPlanUpload;

                    if (itemMaster.PPAPUpload != null)
                        objGM.PPAPUpload = itemMaster.PPAPUpload;





                    if (isFileChanged && itemMaster.DrawingUpload != null)
                    {
                        fileName = itemMaster.DrawingUpload.Replace("C:\\fakepath\\", "");
                        fileName = fileName.Replace(" ", "-");
                        fileName = fileName.Replace(Path.GetExtension(itemMaster.DrawingUpload), "");

                        fileName = fileName + DateTime.Now + Path.GetExtension(itemMaster.DrawingUpload);
                        fileName = fileName.Replace(" ", "-");
                        fileName = fileName.Replace(":", "-");

                        objGM.DrawingUpload = fileName;
                    }



                    if (isFileChanged && itemMaster.ControlPlanUpload != null)
                    {
                        fileName1 = itemMaster.ControlPlanUpload.Replace("C:\\fakepath\\", "");
                        fileName1 = fileName1.Replace(" ", "-");
                        fileName1 = fileName1.Replace(Path.GetExtension(itemMaster.ControlPlanUpload), "");

                        fileName1 = fileName1 + DateTime.Now + Path.GetExtension(itemMaster.ControlPlanUpload);
                        fileName1 = fileName1.Replace(" ", "-");
                        fileName1 = fileName1.Replace(":", "-");

                        objGM.ControlPlanUpload = fileName1;
                    }



                    if (isFileChanged && itemMaster.PPAPUpload != null)
                    {
                        fileName2 = itemMaster.PPAPUpload.Replace("C:\\fakepath\\", "");
                        fileName2 = fileName.Replace(" ", "-");
                        fileName2 = fileName.Replace(Path.GetExtension(itemMaster.PPAPUpload), "");

                        fileName2 = fileName + DateTime.Now + Path.GetExtension(itemMaster.PPAPUpload);
                        fileName2 = fileName.Replace(" ", "-");
                        fileName2 = fileName.Replace(":", "-");

                        objGM.PPAPUpload = fileName2;
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

            return Ok(itemMaster);

        }

        [HttpPut]
        [Route("UpdateSubItemMaster")]
        public IHttpActionResult UpdateSubItemMaster(int id, ItemMasterDetail[] details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //ItemMasterDetail itemMasterDetail = new ItemMasterDetail() { ItemMasterID = id };
                //objEntity.ItemMasterDetails.Remove(itemMasterDetail);
                //objEntity.SaveChanges();

                objEntity.ItemMasterDetails.RemoveRange(objEntity.ItemMasterDetails.Where(c => c.ItemMasterID == id));

                objEntity.SaveChanges();

                foreach (var detail in details)
                {
                    detail.ItemMasterID = id;
                    objEntity.ItemMasterDetails.Add(detail);
                    objEntity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok("Updated");
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


                        if ((fileName != null) && (fileName != "") && !isdone1)
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
                        ItemMaster master = objEntity.ItemMasters.Find(lastInsertedID);
                        if (master == null)
                        {

                        }
                        else
                        {
                            objEntity.ItemMasters.Remove(master);
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
        [Route("DeleteItemMaster")]
        public IHttpActionResult deleteMI(int id)
        {
            //delete entry from item master
            ItemMaster master = objEntity.ItemMasters.Find(id);
            if (master == null)
                return NotFound();


            objEntity.ItemMasters.Remove(master);
            objEntity.SaveChanges();

            //delete entry from item master details
            objEntity.ItemMasterDetails.RemoveRange(objEntity.ItemMasterDetails.Where(c => c.ItemMasterID == id));
            objEntity.SaveChanges();

            //delete files
            string rootFolder = HttpContext.Current.Server.MapPath("../../Drawings/");
            if (master.DrawingUpload != null)
            {
                string authorsFile = master.DrawingUpload;

                try
                {
                    // Check if file exists with its full path    
                    if (File.Exists(Path.Combine(rootFolder, authorsFile)))
                    {
                        // If file found, delete it    
                        File.Delete(Path.Combine(rootFolder, authorsFile));
                       // Console.WriteLine("File deleted.");
                    }
                    
                }
                catch (IOException ioExp)
                {
                    throw ioExp;
                   // Console.WriteLine(ioExp.Message);
                }
            }
            if(master.PPAPUpload != null)
            {
                string authorsFile = master.PPAPUpload;

                try
                {
                    // Check if file exists with its full path    
                    if (File.Exists(Path.Combine(rootFolder, authorsFile)))
                    {
                        // If file found, delete it    
                        File.Delete(Path.Combine(rootFolder, authorsFile));
                        // Console.WriteLine("File deleted.");
                    }

                }
                catch (IOException ioExp)
                {
                    throw ioExp;
                    // Console.WriteLine(ioExp.Message);
                }
            }
            if(master.ControlPlanUpload != null)
            {
                string authorsFile = master.ControlPlanUpload;

                try
                {
                    // Check if file exists with its full path    
                    if (File.Exists(Path.Combine(rootFolder, authorsFile)))
                    {
                        // If file found, delete it    
                        File.Delete(Path.Combine(rootFolder, authorsFile));
                        // Console.WriteLine("File deleted.");
                    }

                }
                catch (IOException ioExp)
                {
                    throw ioExp;
                    // Console.WriteLine(ioExp.Message);
                }
            }
            return Ok(master);
        }

        
        [HttpGet]
        [Route("GetFile")]
        public HttpResponseMessage getFile(int id, string fieldname)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                ItemMaster objGM = new ItemMaster();
                objGM = objEntity.ItemMasters.Find(id);

                if (objGM == null)
                {
                    responseMessage.StatusCode = HttpStatusCode.NotFound;
                    responseMessage.ReasonPhrase = "Record Not Found";

                    throw new HttpResponseException(responseMessage);
                }

                string filePath = "";
                string filename = "";
                if (fieldname == "ControlPlanUpload")
                {
                    filePath = HttpContext.Current.Server.MapPath("../../Drawings/" + objGM.ControlPlanUpload);
                    filename = objGM.ControlPlanUpload;
                }
                else if (fieldname == "DrawingUpload")
                {
                    filePath = HttpContext.Current.Server.MapPath("../../Drawings/" + objGM.DrawingUpload);
                    filename = objGM.DrawingUpload;
                }
                else if (fieldname == "PPAPUpload")
                {
                    filePath = HttpContext.Current.Server.MapPath("../../Drawings/" + objGM.PPAPUpload);
                    filename = objGM.PPAPUpload;
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
