using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using CalibrationSoftware.Models;
using Newtonsoft.Json;

namespace CalibrationSoftware.Controllers
{
    //[EnableCors(origins: "http://039fbf60.ngrok.io/", headers: "*", methods: "*")]
    [RoutePrefix("Api/User")]
    public class UserMasterController : ApiController
    {

        CalibrationDBEntities3 objEntity = new CalibrationDBEntities3();


        [HttpPost]
        [Route("InsertUser")]
        public HttpResponseMessage insertUser(UserMaster user)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.OK,user);

            if (!ModelState.IsValid)
            {
                responseMessage.StatusCode = HttpStatusCode.BadRequest;
                responseMessage.ReasonPhrase = "Bad Request";
                //return BadRequest(ModelState);
            }
            try
            {
                var lastid = objEntity.UserMasters.SingleOrDefault(user1 =>user1.EmailId == user.EmailId);
                var lastid1 = objEntity.UserMasters.SingleOrDefault(user1 => user1.UserName == user.UserName);
                if (lastid != null )
                {
                    responseMessage.StatusCode = HttpStatusCode.Found;
                    responseMessage.ReasonPhrase = "Email Already Exists";

                    return responseMessage;
                }
                else if(lastid1 != null)
                {

                    responseMessage.StatusCode = HttpStatusCode.Found;
                    responseMessage.ReasonPhrase = "UserName Already Exists";

                    return responseMessage;
                }
               


                objEntity.UserMasters.Add(user);
                objEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonString = JsonConvert.SerializeObject(user);
            //byte[] data = Encoding.UTF8.GetBytes(jsonString);
            UserToken token = new UserToken();
            token = token.generateUSertoken(user.UserName);

            responseMessage = Request.CreateResponse(HttpStatusCode.OK, token);
            return responseMessage;
        }

        [HttpPost]
        [Route("GetUser")]
        public IHttpActionResult GetUser(UserMaster user)
        {
            try
            {
               // var user1 = objEntity.UserMasters.FirstOrDefault(u => (u.EmailId == user.EmailId) && (u.Password == user.Password));
                var user1 = objEntity.UserMasters.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == user.Password));
                
                if(user1 == null)
                {
                    return NotFound();
                }

                UserToken userToken = new UserToken();

                userToken.UserName = user1.UserName;

                DateTime dt = DateTime.Now;
                long nowAsLong = dt.ToBinary();
                byte[] nowByttes = BitConverter.GetBytes(nowAsLong);

                byte[] message = Encoding.ASCII.GetBytes(user1.UserName);
                HMACSHA256 hash = new HMACSHA256(nowByttes);

               byte[] token = hash.ComputeHash(message);

                userToken.Token = string.Concat(token.Select(x => x.ToString()));

                return Ok(userToken);

            }
            catch(Exception ex)
            {
                throw ex;
            }


        }
        

    }
}
