
using BookLibrary.Domain.Interfaces;

namespace BookLibrary.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook _bookService;

        public BookController(IBook bookService)
        {
            _bookService = bookService;
        }



        [HttpGet]
        public IActionResult Dashboard()
        { 
            var result = _bookService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBookDetails(AddBookRequest request)
        {  
            var book = request.Adapt<Book>();

            var result = _bookService.Add(book);
            if (result == Domain.Enums.OpStatus.Success)
            {
                return Redirect("Dashboard");
            }
            return View("Details");
        }


        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
             
            var result = _bookService.GetById(id);

            return View(result);
        }

        [HttpPost]
        public IActionResult EditBookDetails(EditBookRequest request)
        { 
            var book = request.Adapt<Book>();

            var result = _bookService.Update(book);
            if (result == Domain.Enums.OpStatus.Success)
            {
                return Redirect($"Edit/{request.Id}");
            }
            return View("Dashboard");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        { 
            var response = _bookService.Delete(id);
            return RedirectToAction("Dashboard");
        }
    }
}
