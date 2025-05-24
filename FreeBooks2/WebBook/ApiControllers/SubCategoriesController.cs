using Bl.IRepository;
using Domains.Entity;
using Microsoft.AspNetCore.Mvc;
using WebBook.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBook.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        public IServicesRepository<SubCategory> _servicesSubCategory { get; }

        public SubCategoriesController(IServicesRepository<SubCategory> servicesSubCategory)
        {
            _servicesSubCategory = servicesSubCategory;
        }
        // GET: api/<SubCategoriesController>
        /// <summary>
        /// get all subcategories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();

            oApiResponse.Data = _servicesSubCategory.GetAll();
            oApiResponse.Errors = null;
            oApiResponse.StatusCode = "200";

            return oApiResponse;
        }

        // GET api/<SubCategoriesController>/5
        /// <summary>
        /// get subcategory by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ApiResponse Get(Guid id)
        {
            ApiResponse oApiResponse=new ApiResponse();
            oApiResponse.Data = _servicesSubCategory.FindById(id);
            oApiResponse.Errors = null;
            oApiResponse.StatusCode = "200";

            return oApiResponse;
        }

        // POST api/<SubCategoriesController>
        /// <summary>
        /// save or update subcategory
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse Post([FromBody] SubCategory subCategory)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                _servicesSubCategory.Save(subCategory);
                oApiResponse.Data ="Done";
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

        // DELETE api/<SubCategoriesController>/5
        /// <summary>
        /// delete subcategory by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ApiResponse Delete(Guid id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                _servicesSubCategory.Delete(id);
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
