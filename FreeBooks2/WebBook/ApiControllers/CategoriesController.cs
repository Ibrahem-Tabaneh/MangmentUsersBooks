using Bl.IRepository;
using Domains.Entity;
using Microsoft.AspNetCore.Mvc;
using WebBook.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBook.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public IServicesRepository<Category> _servicesCategory { get; }

        public CategoriesController(IServicesRepository<Category> servicesCategory)
        {
            _servicesCategory = servicesCategory;
        }
        // GET: api/<CategoriesController>
        /// <summary>
        /// get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiRespnse=new ApiResponse();

            oApiRespnse.Data = _servicesCategory.GetAll();
            oApiRespnse.Errors = null;
            oApiRespnse.StatusCode = "200";

            return oApiRespnse;
        }

        // GET api/<CategoriesController>/5
        /// <summary>
        /// get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ApiResponse Get(Guid id)
        {
            ApiResponse oApiRespnse = new ApiResponse();

            oApiRespnse.Data = _servicesCategory.FindById(id);
            oApiRespnse.Errors = null;
            oApiRespnse.StatusCode = "200";

            return oApiRespnse;
        }

        // POST api/<CategoriesController>
        /// <summary>
        /// save or update category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse Post([FromBody] Category category)
        {
            ApiResponse oApiRespnse=new ApiResponse();

            try
            {
                _servicesCategory.Save(category);
                oApiRespnse.Data = "Done";
                oApiRespnse.Errors = null;
                oApiRespnse.StatusCode = "200";

            }catch (Exception ex)
            {
                oApiRespnse.Data = "Done";
                oApiRespnse.Errors = ex.Message;
                oApiRespnse.StatusCode = "502";
            }
          
            return oApiRespnse; 
        }     

        // DELETE api/<CategoriesController>/5
        /// <summary>
        /// delete category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ApiResponse Delete( Guid id)
        {
            ApiResponse oApiResponse = new ApiResponse();

            try
            {
                _servicesCategory.Delete(id);
                oApiResponse.Data = "Done";
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";

            }
            catch (Exception ex)
            {
                oApiResponse.Data = "Done";
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "502";
            }
            return oApiResponse;
        }
    }
}
