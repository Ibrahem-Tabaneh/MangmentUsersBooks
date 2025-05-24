using Bl.IRepository;
using Domains.Entity;
using Microsoft.AspNetCore.Mvc;
using WebBook.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBook.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public IServicesRepository<Book> _servicesBook { get; }

        public BooksController(IServicesRepository<Book> servicesBook)
        {
            _servicesBook = servicesBook;
        }
        // GET: api/<BooksController>
        /// <summary>
        /// get all books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse= new ApiResponse();

            oApiResponse.Data = _servicesBook.GetAll();
            oApiResponse.Errors = null;
            oApiResponse.StatusCode = "200";
            
            return oApiResponse;
        }

        // GET api/<BooksController>/5
        /// <summary>
        /// get book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ApiResponse Get(Guid id)
        {
            ApiResponse oApiResponse = new ApiResponse();

            oApiResponse.Data=_servicesBook.FindById(id);
            oApiResponse.Errors = null;
            oApiResponse.StatusCode = "200";
            
            return oApiResponse ;
        }

        // POST api/<BooksController>
        /// <summary>
        /// save or update book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse Post([FromBody] Book book)
        {
            ApiResponse oApiResponse = new ApiResponse();

            try
            {
                _servicesBook.Save(book);
                oApiResponse.Data = "Done";
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";

            }
            catch (Exception ex)
            {
                oApiResponse.Data = "Done";
                oApiResponse.Errors =ex.Message;
                oApiResponse.StatusCode = "502";
            }
           
          return oApiResponse ; 
        }

       
        // DELETE api/<BooksController>/5
        /// <summary>
        /// delete book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ApiResponse Delete(Guid id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                _servicesBook.Delete(id);
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
