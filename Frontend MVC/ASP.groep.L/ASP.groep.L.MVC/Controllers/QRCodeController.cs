// using ASP.groep.L.Domain;
// using ASP.groep.L.Infrastructure.Contexts;
// using Microsoft.AspNetCore.Mvc;

// namespace ASP.groep.L.WebAPI.Controllers
// {
//     public class QRCodeController : Controller
//     {
//         private readonly ApplicationDbContext _db;

//         public QRCodeController(ApplicationDbContext db)
//         {
//             _db = db;
//         }

//         public IActionResult Index()
//         {
//             IEnumerable<QRCode> objList = _db.QRCodes;
//             return View(objList);
//         }

//         // GET-Create
//         public IActionResult Create()
//         {
//             return View();
//         }

//         // POST-Create
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Create(QRCode obj)
//         {
//             if (ModelState.IsValid)
//             {
//                 _db.QRCodes.Add(obj);
//                 _db.SaveChanges();
//                 return RedirectToAction("Index");
//             }
//             return View(obj);
//         }

//         // GET-Delete
//         public IActionResult Delete(int? id)
//         {
//             if (id == null || id == 0)
//             {
//                 return NotFound();
//             }
//             var obj = _db.QRCodes.Find(id);
//             if (obj == null)
//             {
//                 return NotFound();
//             }
//             return View(obj);
//         }

//         // POST-Delete
//         public IActionResult DeletePost(int? id)
//         {
//             var obj = _db.QRCodes.Find(id);
//             if (obj == null)
//             {
//                 return NotFound();
//             }
//             _db.QRCodes.Remove(obj);
//             _db.SaveChanges();
//             return RedirectToAction("Index");

//         }

//         // GET-Update
//         public IActionResult Update(int? id)
//         {
//             if (id == null || id == 0)
//             {
//                 return NotFound();
//             }
//             var obj = _db.QRCodes.Find(id);
//             if (obj == null)
//             {
//                 return NotFound();
//             }
//             return View(obj);
//         }

//         // POST-Update
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Update(QRCode obj)
//         {
//             if (ModelState.IsValid)
//             {
//                 _db.QRCodes.Update(obj);
//                 _db.SaveChanges();
//                 return RedirectToAction("Index");
//             }
//             return View(obj);

//         }
//     }
// }
