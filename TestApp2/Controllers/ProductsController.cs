using System.Linq;
using System.Web.Mvc;
using TestApp2.Models;
using System.Data.Entity;
using TestApp2.ViewModels;
using System;

namespace TestApp2.Controllers
{
	public class ProductsController : Controller
	{

		private ApplicationDbContext _context;

		public ProductsController()
		{
			_context = new ApplicationDbContext();
		}

		// GET: Products
		[HttpGet]
		public ActionResult Index(string searchString)
		{
			var products = _context.Products
			.Include(p => p.Category);

			if (!String.IsNullOrEmpty(searchString))
			{
				products = products.Where(
					s => s.Name.Contains(searchString) ||
					s.Category.Name.Contains(searchString));
			}

			return View(products.ToList());
		}

		[HttpGet]
		public ActionResult Create()
		{
			var viewModel = new ProductCategoryViewModel
			{
				Categories = _context.Categories.ToList()
			};
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(Product product)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (_context.Products.Any(p => p.Name.Contains(product.Name)))
			{
				ModelState.AddModelError("Name", "Product Name Already Exists.");
				return View();
			}

			var newProduct = new Product
			{
				Name = product.Name,
				Price = product.Price,
				CategoryId = product.CategoryId
			};

			_context.Products.Add(newProduct);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var productInDb = _context.Products.SingleOrDefault(p => p.Id == id);

			if (productInDb == null)
			{
				return HttpNotFound();
			}

			_context.Products.Remove(productInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var productInDb = _context.Products.SingleOrDefault(p => p.Id == id);

			if (productInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new ProductCategoryViewModel
			{
				Product = productInDb,
				Categories = _context.Categories.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(Product product)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var productInDb = _context.Products.SingleOrDefault(p => p.Id == product.Id);

			if (productInDb == null)
			{
				return HttpNotFound();
			}

			productInDb.Name = product.Name;
			productInDb.Price = product.Price;
			productInDb.CategoryId = product.CategoryId;
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}